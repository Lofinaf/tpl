using System;
using System.Collections.Generic;
using tpl.Engine.Core.Loader;
using tpl.Runtime.Results;
using tpl.Core;

namespace tpl.Engine.Core.Analysis
{
    public class Lexer
    {
        public string Source { get; set; }

        public List<Token> ReturnTokens = new List<Token>(); // not property!

        public LoaderErrors loaderErrors = new LoaderErrors(new InterpreterResult());

        private static readonly Dictionary<string, TokenType> _keywords = new Dictionary<string, TokenType>
        {
            {"print", TokenType.PRINT},
            {"var", TokenType.VAR},
            {"true", TokenType.TRUE},
            {"false", TokenType.FALSE},
        };

        private int _startof = 0;
        private int _position = 0;
        private int _line = 1;

        public Lexer(string source)
        {
            Source = source;
        }

        public void ToToken()
        {
            var t = _skip();

            switch (t)
            {
                case '/':
                    if (_find('/'))
                    {
                        while (_currentWord() != '\n' && !_isEnd())
                        {
                            _skip();
                        }
                        break;
                    }
                    _tokenCreater(TokenType.SLASH);
                    break;

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

                case '\n':
                    _line++;
                    break;

                case '\r':
                case '\t':
                case ' ':
                    break;

                // Literals
                case '"':
                    _parseString();
                    break;

                default:
                    if (_isInteger(t))
                    {
                        _parseNumber();
                        break;
                    }
                    if (_isText(t))
                    {
                        _indParse();
                        break;
                    }
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
            ReturnTokens.Add(new Token(TokenType.END, "nil", _line, null));
            return ReturnTokens;
        }

        #region Tools

        private void _indParse()
        {
            while (_isNum(_currentWord()))
            {
                _skip();
            }
            var Value = Source.Substring(_startof, _position - _startof);

            if (!_keywords.TryGetValue(Value, out TokenType token))
            {
                token = TokenType.IDN;
            }
            _tokenCreater(token);
        }

        private bool _isNum(char c) => (_isText(c) || _isInteger(c));
        private bool _isText(char c)
        {
            return (c >= 'a' && c <= 'z') ||
                   (c >= 'A' && c <= 'Z') ||
                   c == '_';
        }

        private bool _isEnd() => (_position >= Source.Length);
        private char _skip() => (Source[_position++]);

        private char _currentWord() => (_isEnd() ? '\0' : Source[_position]);
        private char _nextWord()
        {
            if (_position+1 >= Source.Length)
            {
                return '\0';
            }
            return Source[_position + 1];
        }

        private void _tokenCreater(TokenType token) => _create(token, null);
        private void _create(TokenType createTokenType, object lit)
        {
            var text = Source.Substring(_startof, _position - _startof);
            ReturnTokens.Add(new Token(createTokenType, text, _line, lit));
        }

        private void _parseNumber()
        {
            while (_isInteger(_currentWord()))
            {
                _skip();
            }

            if (_currentWord() != '.' && _isInteger(_nextWord()))
            {
                _skip();

                while (_isInteger(_currentWord()))
                {
                    _skip();
                }
            }

            _create(TokenType.NUMBER, double.Parse(Source.Substring(_startof, _position - _startof)));
        }

        private void _parseString()
        {            
            while (_currentWord() != '"' && !_isEnd())
            {
                if (_currentWord() == '\n')
                {
                    _line++;
                }
                _skip();
            }

            if (_isEnd())
            {
                loaderErrors.Throw($"String was not be end, line {_line}, position {_position}", ConsoleColor.Red); return;
            }
            _skip();

            var Value = Source.Substring(_startof+1, (_position - _startof) - 2);
            _create(TokenType.STRING, Value);
        }

        private bool _isInteger(char character) => (character >= '0' && character <= '9');

        private bool _find(char character)
        {
            if (_isEnd()) return false;
            if (Source[_position] != character) return false;

            _position++;
            return true;
        }

        #endregion

    }
}
