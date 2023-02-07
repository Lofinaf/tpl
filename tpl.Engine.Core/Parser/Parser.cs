using System;
using System.IO;
using System.Collections.Generic;
using tpl.Core;
using tpl.Engine.Core.Parser.Statements;
using tpl.Runtime.Results;
using tpl.Engine.Core.Parser.Expressions;

namespace tpl.Engine.Core.Parser
{
    public class Parser : IEngine
    {
        public List<Token> Tokens { get; set; }
        public AST AST { get; private set; } = new AST();

        public Loader.LoaderErrors LoaderErrors { get; set; } = new Loader.LoaderErrors(new InterpreterResult());

        private int _position = 0;

        public Parser(List<Token> tokens)
        {
            Tokens = tokens;
        }

        public void ReadTokenByPos()
        {
            var token = Tokens[_position];

            switch (token.Type)
            {
                case TokenType.PRINT:
                    _parsePrintStatement();
                    break;

                case TokenType.PACKAGE:
                    _parsePackage();
                    break;

                default:
                    _parseExpression();
                    break;
            }
        }

        public AST ScanTokens()
        {
            while (!_isEnd())
            {
                ReadTokenByPos();
                _position += 1;
            }
            return AST;
        }

        #region ParsingOtherThings

        private void _parseExpression()
        {
            if (_peekToken().Type == TokenType.PLUS)
            {
                _parseOperator();
            }
            _jumpToNext();
        }

        private void _parseOperator()
        {
            var expr = new BinaryOperatorExpression
            {
                Operator = _peekToken(),
                Left = _previous(),
                Right = _advance()
            };
            AST.Add(expr);
        }

        private void _parsePackage()
        {
            _jumpToNext();
            if (Tokens[_position].Type is TokenType.STRING)
            {
                File.WriteAllText(Tokens[_position].Lit.ToString(), TokenToPackage(Tokens));
            }
        }

        private void _parsePrintStatement()
        {
            var statement = new PrintStatement()
            {
                NodeName = "PrintStatement"
            };
            _jumpToNext();
            switch (_peekToken().Type)
            {
                case TokenType.STRING:
                    statement.LiteralToPrint = _peekToken().Lit;
                    goto GetOut;

                case TokenType.IDN:
                    statement.LiteralToPrint = $"__{_peekToken().Value}";
                    goto GetOut;

                default:
                    break;
            }

        GetOut:
            AST.Add(statement);
            return;
        }
        #endregion

        #region Tools
        private Token _peekToken() => (Tokens[_position]);
        private bool _isEnd() => (_position >= Tokens.Count);
        private Token _advance() => (Tokens[_position++]);
        private Token _previous() => (Tokens[_position--]);
        private void _jumpToNext()
        {
            if (_isEnd()) return;
            _position += 1;
        }

        public string TokenToPackage(List<Token> list)
        {
            string content = "";
            foreach (var item in list)
            {
                content += $"\n{{TYPE\"{item.Type}\" VALUE\"{item.Type}\" LIT\"{item.Lit}\" LINE\"{item.Line}\"}}";
            }
            return content;
        }
        #endregion
    }
}
