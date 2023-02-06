#include <stdio.h>
#include <assert.h>
#include <setjmp.h>
#include <string.h>
#include <ctype.h>
#include <stdbool.h>

#define __DLL               /* Use header inside a dll file */
#include "packet_parser.h"

static jmp_buf env;

static void skip_space(char **source)
{
    while (isspace(**source))
        ++*source;
}

static Key_Value *next_field(char **rest)
{
    Key_Value kv = malloc(sizeof(struct Key_Value));
    assert( kv != NULL );
    memset(kv, 0, sizeof(struct Key_Value));

    skip_space(rest);

    char *tok_rest = *rest, *key = strtok_r(tok_rest, "\"", &tok_rest);

    if (key == NULL)
        THROW_ERROR(BAD_INPUT);

    if (strcmp(key, "TYPE") && strcmp(key, "VALUE") && strcmp(key, "LIT") && strcmp(key, "LINE"))
        THROW_ERROR(INVALID_FIELD);
    
    strcpy(KEY(kv), key);
    if (tok_rest[0] == '\"'){
        VALUE(kv)[0] = '\0';
        (* rest) += ( tok_rest - key ) + 1;
        return kv;
    }

    char *value = strtok_r(tok_rest, "\"", &tok_rest);

    if (value == NULL)
        THROW_ERROR(BAD_INPUT);

    strcpy(VALUE(kv), value);

    (* rest) += ( tok_rest - key );
    return kv;
}

static Token next_token(char **rest, bool *last_token)
{
    assert( rest );

    Token token = malloc(RECVESTED_FIELDS * sizeof(Key_Value));
    assert( token );

    skip_space( rest );

    if (**rest != '{')
        THROW_ERROR(BAD_INPUT);

    (* rest) += 1; /* { */

    for (int i = 0; i < RECVESTED_FIELDS; i++){
        token[i] = next_field(rest);
        last_token && (!strcmp(KEY(token[i]), "END") || !strcmp(VALUE(token[i]), "END")) && (*last_token |= 01);
    }
    
    if (**rest != '}')
        THROW_ERROR(BAD_INPUT);

    (* rest) += 1; /* } */

    skip_space(rest);

    return token;
}

static void table_append(struct Token_Table * const table, const Token tok)
{
    table->tokens = realloc(table->tokens, sizeof(Token) * ++table->ntokens);
    table->tokens[table->ntokens - 1] = tok;
}

static void table_destroy(struct Token_Table *table)
{
    assert( table );
    for(size_t token = 0; token < table->ntokens; token++){
        for (int field = 0; field < RECVESTED_FIELDS; field++)
            free(table->tokens[token][field]);
        free(table->tokens[token]);
    }
    free(table);
}

static struct Token_Table *parse_tokens(FILE * const stream)
{
    assert( stream != NULL );

    struct Token_Table *table = malloc(sizeof(*table));
    assert( table != NULL );
    memset(table, 0, sizeof(*table));

    fseek(stream, 0, SEEK_END);
    long file_raw_size = ftell(stream);
    if (!file_raw_size || file_raw_size < 0)
        THROW_ERROR(EMPTY_FILE);

    rewind(stream);
    char *file_raw = malloc(file_raw_size + 1);
    if (!file_raw)
        THROW_ERROR(LARGE_FILE);

    fread(file_raw, 1, file_raw_size, stream);
    rewind(stream);
    file_raw[file_raw_size] = '\0';

    Token tok;
    char *rest = file_raw;

    bool last_token = false;
    do {
        tok = next_token(&rest, &last_token);
        table_append(table, tok);
    } while (!last_token);

    free(file_raw);

    return table;
}

static void generate_json_object(const Token token, FILE * const stream)
{
    fprintf(stream, "\t\t{"); /* JSON object begin */

    const char *prefix = "\n";
    for (int i = 0; i < RECVESTED_FIELDS; i ++){
        fprintf(stream, prefix);
        fprintf(stream, "\t\t\t\"%.*s\" : \"%.*s\"", MAX_KEY, KEY(token[i]), MAX_VALUE, VALUE(token[i]));
        prefix = ",\n";
    }

    fprintf(stream, "\n\t\t}"); /* JSON object end */
}

static void generate_json_from_table(const struct Token_Table *table, FILE * const stream)
{
    assert( stream != NULL );
    
    fprintf(stream, "{\n\t\"Tokens\": ["); /* JSON global object & array begin */
    const char *prefix = "\n";

    for (size_t tok = 0; tok < table->ntokens; tok++){
        fprintf(stream, prefix);
        generate_json_object(table->tokens[tok], stream);
        prefix = ",\n";
    }

    fprintf(stream, "\n\t]\n}"); /* JSON global object & array end */
}

Parse_Result parse_packet(const char *packet_path, const char *output_path)
{
    Parse_Result error_handle;

    /* Not common exiting */
    if (error_handle = setjmp(env))
        return error_handle;

    assert(packet_path != NULL);
    assert(output_path != NULL);

    FILE *source = fopen(packet_path, "rb+");

    if (!source){
        perror(packet_path);
        THROW_ERROR(NOT_SUCH_FILE);
    }
    
    struct Token_Table *table = parse_tokens(source);
    fclose(source);

    FILE *out = fopen(output_path, "wb+");

    if (!out){
        perror(output_path);
        THROW_ERROR(ACCESS_DENIED);
    }

    generate_json_from_table(table, out);
    table_destroy(table);

    fclose(out);
    return VALID_CODE(SUCCESS);
}

const char *parse_result_to_cstr(Parse_Result const val)
{
    #define ENTRY(a, b)\
    case PARSE_RESULT_##a:\
        result = b;\
        break;

    const char *result;
    switch (val){
        #include "error_table.def"
        #undef ENTRY
    default:
        result = "Unknown error.";
        break;
    }

    return result;
}

BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD fdwReason, LPVOID lpvReserved ){ return TRUE; }