using System;
using System.Collections.Generic;
using tpl.Runtime.Interpreter.Loader;
using tpl.Core;

namespace tpl.Runtime.Interpreter.Analysis
{
    public class Lexer
    {
        public string Source { get; set; }

        public List<Token> ReturnTokens = new List<Token>(); // not property!

        public LoaderErrors loaderErrors;

        private bool _haveError = false;

        private int _startof = 0;
        private int _position = 0;
        private int _line = 1;

        public Lexer(string source, LoaderErrors loader)
        {
            Source = source;
            loaderErrors = loader;
        }

        public void ToToken()
        {
            var t = _skip();

            switch (t)
            {
                case '(': _tokenCreater(TokenType.LPAR); break;
                case ')': _tokenCreater(TokenType.RPAR); break;

                case '{': _tokenCreater(TokenType.SCOPE_OPEN); break;
                case '}': _tokenCreater(TokenType.SCOPE_CLOSE); break;

                case ',': _tokenCreater(TokenType.COMMA); break;
                case '.': _tokenCreater(TokenType.DOT); break;

                case '-': _tokenCreater(TokenType.MINUS); break;
                case '+': _tokenCreater(TokenType.PLUS); break;
                case '*': _tokenCreater(TokenType.MUL); break;

                case '!': _tokenCreater(_find('=') ? TokenType.BANG_EQUAL : TokenType.BANG); break;
                case '=': _tokenCreater(_find('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL); break;
                case '<': _tokenCreater(_find('=') ? TokenType.LESS_EQUAL : TokenType.LESS); break;
                case '>': _tokenCreater(_find('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER); break;

                default:
                    loaderErrors.Throw($"Unknown char in line {_line}, position {_position}", ConsoleColor.Red);
                    break;
            }
        }

        public List<Token> ScanSource()
        {
            while (!_isEnd())
            {
                _startof = _position;
                ToToken();
            }
            return ReturnTokens;
        }

        #region Tools

        private bool _isEnd() => (_position >= Source.Length);
        private char _skip() => (Source[_position++]);

        private void _tokenCreater(TokenType token) => _create(token, null);
        private void _create(TokenType createTokenType, object lit)
        {
            var text = Source.Substring(_startof, _position);
            ReturnTokens.Add(new Token(createTokenType, text, _line));
        }

        private bool _find(char character)
        {
            if (!_isEnd()) return false;
            if (Source[_position] != character) return false;

            _position++;
            return true;
        }

        #endregion

    }
}
