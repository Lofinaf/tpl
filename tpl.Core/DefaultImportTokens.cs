using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tpl.Core
{
    public class DefaultImportTokens
    {
        public TokenType LSQ; // (
        public TokenType RSQ; // )
        public TokenType PRINT; // print


        public void Init()
        {
            LSQ = new TokenType("LSQ", new Regex(@".("));
            RSQ = new TokenType("RSQ", new Regex(@".)"));
        }
    }
}
