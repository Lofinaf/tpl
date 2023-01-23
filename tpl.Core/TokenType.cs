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
        AND, OR,
        TRUE, FALSE,

        // Single
        MINUS, PLUS, DIV, MUL, DOT,
        SLASH, LPAR, RPAR,
        SCOPE_OPEN, SCOPE_CLOSE, SIGN,

        // Single + Single = Mix
        PLUS_SIGN, MINUS_SIGN,

        // Literals
        STRING, NUMBER, IDN,
	}
}
