using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tpl.Core
{
    public class Token
    {
        public string Value { get; private set; }
        public int Position { get; private set; }

        public TokenType Type { get; private set; }

        public Token(string value, int position, TokenType type)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Position = position;
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}
