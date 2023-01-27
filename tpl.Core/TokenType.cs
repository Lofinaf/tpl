using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tpl.Core
{
    public enum TokenType
	{
        // Keywords
        TRUE, FALSE,
        PRINT, VAR,

        // Single
        MINUS, PLUS, DIV, MUL, DOT,
        SLASH, LPAR, RPAR, BANG, LESS, GREATER,
        SCOPE_OPEN, SCOPE_CLOSE, EQUAL, COMMA,

        // Token 1 + Token 2 = Token 1 + 2
        BANG_EQUAL, EQUAL_EQUAL,
        LESS_EQUAL, GREATER_EQUAL,

        // Literals
        STRING, NUMBER, IDN,

        END
	}
}
