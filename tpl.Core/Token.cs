using System.Collections.Generic;

namespace tpl.Core
{
    public class Token
    {
        public TokenType Type { get; private set; }
        public string Value { get; private set; }

        public int Position { get; private set; }

        public Token(TokenType type, int position)
        {
            Type = type;
            Position = position;
        }

        public Token(TokenType type, string value, int position)
        {
            Type = type;
            Value = value;
            Position = position;
        }
    }
}
