namespace tpl.Core
{
    public enum TokenType
	{
        // Keywords
        TRUE, FALSE,
        PRINT, VAR,
        FUNC, RET,
        SCOPE, IMPORT,
        CONST, CVAR,
        IF, ELSE,
		PACKAGE,

        // Single
        MINUS, PLUS, DIV, MUL, DOT,
        SLASH, LPAR, RPAR, BANG, LESS, GREATER,
        SCOPE_OPEN, SCOPE_CLOSE, EQUAL, COMMA, DT, DT_DT, 

        // Token 1 + Token 2 = Token 1 + 2
        BANG_EQUAL, EQUAL_EQUAL,
        LESS_EQUAL, GREATER_EQUAL,

        // Literals
        STRING, NUMBER, IDN,

        END
	}
}
