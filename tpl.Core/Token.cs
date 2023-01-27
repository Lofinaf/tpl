using System.Collections.Generic;

namespace tpl.Core
{
    public class Token
    {
        public TokenType Type { get; private set; }
        public string Value { get; private set; }

        public int Line { get; private set; }
        public object Lit { get; private set; }

        public Token(TokenType type, string value, int position, object lit)
        {
            Type = type;
            Value = value;
            Line = position;
            Lit = lit;
        }
    }
}
