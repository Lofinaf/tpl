using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpl.Core;

namespace tpl.Engine.Core.Parser
{
    public class Parser
    {
        public List<Token> Tokens { get; set; }

        private int _startof = 0;
        private int _position = 0;

        public Parser(List<Token> tokens)
        {
            Tokens = tokens;
        }

        private void _toNode()
        {
            var Current = _skip();

            switch (Current.Type)
            {
                case TokenType.PRINT:

                    break;

                case TokenType.END:
                    return;

                default:
                    break;
            }
        }

        public void ScanTokens()
        {
            while (!_isEnd())
            {
                _startof = _position;
                _toNode();
            }
        }

        #region Tools
        private bool _isEnd() => (_position >= Tokens.Count);
        private Token _skip() => (Tokens[_position++]);

        #endregion
    }
}
