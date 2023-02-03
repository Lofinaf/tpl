#ifndef _PACKET_PARSER_H
#define _PACKET_PARSER_H

#include <windows.h>
#include <stdio.h>

#define MAX_KEY     32L
#define MAX_VALUE   MAX_KEY

#ifdef __DLL
    #define RECVESTED_FIELDS    4

    #define THROW_ERROR(code)   longjmp(env, code)

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

typedef enum {
    PARSE_RESULT_SUCCESS = 0,
    PARSE_RESULT_BAD_INPUT,
    PARSE_RESULT_INVALID_FIELD,
    PARSE_RESULT_NOT_SUCH_FILE,
    PARSE_RESULT_EMPTY_FILE,
    PARSE_RESULT_LARGE_FILE,
    PARSE_RESULT_ACCESS_DENIED,
} Parse_Result;

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

SPEC Parse_Result parse_packet(const char *packet_path, const char *output_path);
SPEC const char *parse_result_to_cstr(Parse_Result const val);

#ifdef __cplusplus
    }
#endif

#endif // _PACKET_PARSER_H