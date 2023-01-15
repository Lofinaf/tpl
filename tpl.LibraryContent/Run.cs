using System;
using System.Collections.Generic;

using static tpl.LibraryContent.Lexer;

namespace tpl.LibraryContent
{
    public class Run
    {
        private const int _maxWords = 10000;

        public enum Param
        {
            def,
            debug,
        }

        public enum TokenTypes
        {
            // k - keyword, s - string && integer param, UPPER - Func Branch, o - operator, f - field
            PRINT, // print()
            lsq, // (
            rsq, // )
            s_symbol, // "
            s_symbol2, // '
            k_import, // import
            s_symbolunk, // Unknown Symbol
            EXIT, // exit()
            function, // defined func
            variable, // defined var
            constant, // define constant
            k_var, // var
            o_sign, // =
            o_plus, // +
            k_const, // const
            k_func, // function
            k_stop, // stop program with exception
            s_space, // Space symbol define
            f_int, // integer
        }

        public static void ReadTokens(List<string> Tokens, ref Stack<string> Stack, ref Dictionary<string, string> Variables, Param param)
        {
            int line = 1;
            for (int pos = 0; pos < Tokens.Count; pos++)
            {
                var token = Tokens[pos];
                if (param == Param.debug)
                {
                    Console.WriteLine($"Token: {token}, Position: {pos}; Tokens count: {Tokens.Count}");
                }
                switch (token)
                {

                    case "s_space":
                        line++;
                        break;

                    case "o_sign":
                        if (Variables.TryGetValue(Tokens[pos-1], out string leftSign) && Variables.TryGetValue(Tokens[pos+1], out string rightSign))
                        {
                            Variables[Tokens[pos - 1]] = Variables[Tokens[pos + 1]];
                            break;
                        }
                        if (Variables.TryGetValue(Tokens[pos - 1], out string leftVarSign))
                        {
                            Variables[Tokens[pos - 1]] = Tokens[pos + 1];
                            break;
                        }
                        ThrowError(ThrowErrors.OPERATORERR, line, Tokens[pos], 5);
                        break;

                    case "o_plus":
                        if (Variables.TryGetValue(Tokens[pos - 1], out string leftPlus) && Variables.TryGetValue(Tokens[pos + 1], out string rightPlus))
                        {
                            Variables[Tokens[pos - 1]] += Variables[Tokens[pos + 1]];
                            break;
                        }
                        if (Variables.TryGetValue(Tokens[pos - 1], out string leftVarPlus))
                        {
                            if (Tokens[pos + 1].StartsWith("N"))
                            {
                                if (int.TryParse(Tokens[pos+1], out int rightNumPlus) && (int.TryParse(Variables[Tokens[pos - 1]], out int leftNumPlus)))
                                {
                                    int value = rightNumPlus + leftNumPlus;
                                    Variables[Tokens[pos - 1]] = value.ToString();
                                }
                                break;
                            }
                            Variables[Tokens[pos - 1]] += Tokens[pos + 1];
                            break;
                        }
                        ThrowError(ThrowErrors.OPERATORERR, line, Tokens[pos], 4);
                        break;

                    case "k_var":
                        try
                        {
                            Variables.Add(Tokens[pos + 1], "null");
                        }
                        catch (Exception)
                        {
                            ThrowError(ThrowErrors.VARCANBEDECLARED, line, Tokens[pos], 5);
                        }
                        break;

                    case "PRINT":
                        if (Tokens[pos+1] == "lsq")
                        {
                            string ValueToWrite = "";
                            bool isFirstWord = true;
                            bool isText = false;
                            for (int word = pos + 1; word <= _maxWords; word++)
                            {
                                if (Tokens.Count > word)
                                {
                                    var Token = Tokens[word];
                                    if (Token == "rsq")
                                    {
                                        break;
                                    }
                                    if (Token == "s_symbol")
                                    {
                                        if (isText)
                                        {
                                            isText = false;
                                            continue;
                                        }
                                        isText = true;
                                        continue;
                                    }
                                    if (!IsTokenKey(Token) && isFirstWord)
                                    {
                                        if (isText)
                                        {
                                            isFirstWord = false;
                                            ValueToWrite += $"{Token}";
                                            continue;
                                        }
                                        try
                                        {
                                            ValueToWrite += $"{Variables[Token]}";
                                        }
                                        catch (Exception)
                                        {
                                            ThrowError(ThrowErrors.INTYPEISSTRING, line, Token, 3);
                                        }
                                        continue;
                                    }
                                    if (!IsTokenKey(Token))
                                    {
                                        if (isText)
                                        {
                                            isFirstWord = false;
                                            ValueToWrite += $" {Token}";
                                            continue;
                                        }
                                        try
                                        {
                                            ValueToWrite += $" {Variables[Token]}";
                                        }
                                        catch (Exception)
                                        {
                                            ThrowError(ThrowErrors.INTYPEISSTRING, line, Token, 10);
                                        }
                                        continue;
                                    }
                                    continue;
                                }
                                break;
                            }
                            Console.WriteLine(ValueToWrite);
                            break;
                        }
                        ThrowError(ThrowErrors.LSQNOTFOUND, 2);
                        return;

                    default:
                        break;
                }
            }
        }

        public static List<string> ToTokens(string Source)
        {
            List<string> tokens = new List<string>();

            var codetocompare = Lex(Source).Split(new string[]{" "}, StringSplitOptions.RemoveEmptyEntries);
            for (int pos = 0; pos < codetocompare.Length; pos++)
            {
                // Console.WriteLine(codetocompare[pos]);
                switch (codetocompare[pos])
                {
                    case "\n":
                        tokens.Add(TokenTypes.s_space.ToString());
                        break;

                    case "var":
                        tokens.Add(TokenTypes.k_var.ToString());
                        break;

                    case "=":
                        tokens.Add(TokenTypes.o_sign.ToString());
                        break;
                    case "+":
                        tokens.Add(TokenTypes.o_plus.ToString());
                        break;

                    case "print":
                        tokens.Add(TokenTypes.PRINT.ToString());
                        break;

                    case "(":
                        tokens.Add(TokenTypes.lsq.ToString());
                        break;

                    case ")":
                        tokens.Add(TokenTypes.rsq.ToString());
                        break;

                    case "\"":
                        tokens.Add(TokenTypes.s_symbol.ToString());
                        break;

                    case "\'":
                        tokens.Add(TokenTypes.s_symbol.ToString());
                        break;

                    case "exit":
                        break;

                    case "int":
                        break;

                    default:
                        if (codetocompare[pos-1] != "int")
                        {
                            tokens.Add(codetocompare[pos]);
                            break;
                        }
                        tokens.Add($"N{codetocompare[pos]}");
                        break;
                }
            }
            return tokens;
        }
    }
}
