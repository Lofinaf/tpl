using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tpl.Core
{
    public class TokenType
    {
        public string TokenName { get; private set; }
        public Regex HowToFound { get; private set; }

        public TokenType(string tokenName, Regex howToFound)
        {
            TokenName = tokenName ?? throw new ArgumentNullException(nameof(tokenName));
            HowToFound = howToFound ?? throw new ArgumentNullException(nameof(howToFound));
        }
    }
}
