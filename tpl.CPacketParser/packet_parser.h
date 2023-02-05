#ifndef _PACKET_PARSER_H
#define _PACKET_PARSER_H

#include <windows.h>
#include <stdio.h>

#define MAX_KEY     32L
#define MAX_VALUE   MAX_KEY

#ifdef __DLL
    #define RECVESTED_FIELDS    4

    #define VALID_CODE(code)    PARSE_RESULT_##code
    #define THROW_ERROR(code)   longjmp(env, VALID_CODE(code))

    #define KEY(p)   ((struct Key_Value *)(p))->key
    #define VALUE(p) ((struct Key_Value *)(p))->value

    #define EXPORT_FUNCTION     __declspec(dllexport)
    #define SPEC EXPORT_FUNCTION
#else
    #define SPEC extern
#endif

#ifdef __cplusplus
    extern "C" {
#endif

#define ENTRY(a, b) PARSE_RESULT_##a,
typedef enum {
    #include "error_table.def"
} Parse_Result;
#undef ENTRY

__attribute__((packed)) struct Key_Value {
    char key  [MAX_KEY];
    char value[MAX_VALUE];
};
typedef void *Key_Value;
typedef Key_Value *Token;

struct Token_Table {
    Token *tokens;
    size_t ntokens;
};

#ifdef __DLL
    /* Tokens processing */
    static Key_Value *next_field(char **);
    static Token next_token(char **, bool *);
    /* Table */
    static void table_append(struct Token_Table * const, const Token);
    static void table_destroy(struct Token_Table *);
    static struct Token_Table *parse_tokens(FILE * const);
    /* Json generation */
    static void generate_json_object(const Token, FILE * const);
    static void generate_json_from_table(const struct Token_Table *, FILE * const);
#endif

SPEC Parse_Result parse_packet(const char *packet_path, const char *output_path);
SPEC const char *parse_result_to_cstr(Parse_Result const val);

#ifdef __cplusplus
    }
#endif

#endif // _PACKET_PARSER_H