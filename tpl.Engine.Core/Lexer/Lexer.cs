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
            {"const", TokenType.CONST},
            {"var", TokenType.VAR},
            {"true", TokenType.TRUE},
            {"false", TokenType.FALSE},
            {"func", TokenType.FUNC},
            {"return", TokenType.RET},
            {"import", TokenType.IMPORT},
            {"package", TokenType.PACKAGE},
            {"if", TokenType.IF},
            {"else", TokenType.ELSE},
            {"scope", TokenType.SCOPE},
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
                    if (_charExist('/'))
                    {
                        while (_currentChar() != '\n' && !_isEnd())
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

                case '!': _tokenCreater(_charExist('=') ? TokenType.BANG_EQUAL : TokenType.BANG); break;
                case '=': _tokenCreater(_charExist('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL); break;
                case '<': _tokenCreater(_charExist('=') ? TokenType.LESS_EQUAL : TokenType.LESS); break;
                case '>': _tokenCreater(_charExist('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER); break;
                case ':': _tokenCreater(_charExist(':') ? TokenType.DT_DT : TokenType.DT); break;

                case '\n':
                    _line++;
                    break;

                case ';':
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
                    loaderErrors.Throw($"Unknown char {_currentChar()}; in line {_line}, char position {_position} --> \"word\" ", ConsoleColor.Red);
                    break;
            }
        }

        public void RunPreCommand()
        {
            var t = _currentChar();

            switch (t)
            {
                case '_':
                    if (!_charExist('_')) break;
                    _parseCommand();
                    break;

                default:
                    break;
            }
        }

        public List<Token> ScanSource()
        {
            ReturnTokens.Clear();
            _startof = 0;
            _position = 0;
            _line = 0;
            while (!_isEnd())
            {
                _startof = _position;
                ToToken();
            }
            ReturnTokens.Add(new Token(TokenType.END, "null", _line, null));
            return ReturnTokens;
        }

        #region Tools

        private void _parseCommand()
        {
            Console.WriteLine(_skip());
        }

        private void _indParse()
        {
            while (_isNum(_currentChar()))
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

        private char _currentChar() => (_isEnd() ? '\0' : Source[_position]);
        private char _nextChar()
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
            while (_isInteger(_currentChar()))
            {
                _skip();
            }

            if (_currentChar() != '.' && _isInteger(_nextChar()))
            {
                _skip();

                while (_isInteger(_currentChar()))
                {
                    _skip();
                }
            }

            _create(TokenType.NUMBER, double.Parse(Source.Substring(_startof, _position - _startof)));
        }

        private void _parseString()
        {            
            while (_currentChar() != '"' && !_isEnd())
            {
                if (_currentChar() == '\n')
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

        private bool _charExist(char character)
        {
            if (_isEnd()) return false;
            if (Source[_position] != character) return false;

            _position++;
            return true;
        }

        #endregion

    }
}
