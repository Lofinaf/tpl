using System.Collections.Generic;
using tpl.Core;

namespace tpl.Runtime.Interpreter.Lexer
{
    public class Lexer
    {
        public string Source { get; set; }

        private List<Token> _tokens;

        private int _startof = 0;
        private int _position = 0;
        private int _line = 1;

        public Lexer(string source) => source = Source;

        public void ToToken()
        {
            
        }

        public List<Token> ScanSource()
        {
            while (!_isEnd())
            {
                _startof = _position;

            }

            return _tokens;
        }

        #region Tools

        private bool _isEnd() => (_position >= Source.Length);

        private char _skip() => (Source.);

        #endregion

    }
}
