using System;
using System.Collections.Generic;

namespace tpl.LibraryContent
{
    public class Lexer
    {
        private const int _maxWords = 10000; 

        #region ErrorThrowingParam
        private protected static string[] _aboutErrorsEN =
        {
            "Unknown Symbol",
            "Uncorrect file!",
            "\"(\" not found",
            "You try to print integer, but you value this is string",
            "String is specific",
            "Error operator using",
        };

        public enum ThrowErrors : int
        {
            UNKNOWNSYBMOL = 0,
            LSQNOTFOUND = 5,
            INTYPEISSTRING = 10,
            SPECIFICSTR = 2,
            OPERATORERR = 2,
        }

        public static void ThrowError(ThrowErrors Error, int Line, string Word, int Id)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Unhandled Exception, id: {Error}, line: {Line}, char: {Word}; {_aboutErrorsEN[Id]}");
            Console.ResetColor();
        }
        public static void ThrowError(ThrowErrors Error, int Line, int Id)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Unhandled Exception, id: {Error}, line: {Line}; {_aboutErrorsEN[Id]}");
            Console.ResetColor();
        }
        public static void ThrowError(ThrowErrors Error, int Id)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Unhandled Exception, id: {Error}; {_aboutErrorsEN[Id]}");
            Console.ResetColor();
        }
        #endregion

        public static bool IsTokenKey(string text)
        {
            foreach (var item in Enum.GetValues(typeof(TokenTypes)))
            {
                if (text == item.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsNumber(string text)
        {
            return int.TryParse(text, out int i);
        }
        public static bool IsSign(string text)
        {
            return (text == "=");
        }
        public static bool IsDot(string text)
        {
            return (text == ".");
        }
        public static bool IsSemicolon(string text)
        {
            return (text == ";");
        }
        public static bool IsBracket(string text)
        {
            return text == "(" || text == ")" || text == "\"" || text == "{" || text == "}";
        }

        public static string Lex(string text)
        {
            string ret = "";
            string word = "";
            foreach (var item in text)
            {
                if (char.IsWhiteSpace(item) || IsBracket(item.ToString()) || IsSemicolon(item.ToString()) || IsDot(item.ToString()))
                {
                    word += $" {item}";
                    ret += $" {word}";
                    word = "";
                    continue;
                }
                word += $"{item}";
                continue;
            }
            return ret;
        }

        public enum Param
        {
            def,
            debug,
        }

        public enum TokenTypes
        {
            // k - keyword, s - string && integer param, UPPER - Func Branch, o - operator
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
            k_const, // const
            k_func, // function
            k_stop, // stop program with exception
            s_space, // Space symbol define
        }

        public static void ReadTokens(List<string> Tokens, ref Stack<string> Stack, ref Dictionary<string, string> Variables, Param param)
        {
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
                        break;

                    case "o_sign":
                        if (Variables.TryGetValue(Tokens[pos-1], out string left) && Variables.TryGetValue(Tokens[pos+1], out string right))
                        {

                        }
                        ThrowError(ThrowErrors.OPERATORERR, 5);
                        break;

                    case "k_var":
                        Console.WriteLine(Tokens[pos+1]);
                        Variables.Add(Tokens[pos+1], "null");
                        break;

                    case "PRINT":
                        if (Tokens[pos+1] == "lsq")
                        {
                            string ValueToWrite = "";
                            bool isFirstWord = true;
                            bool WordIsText = false;
                            for (int word = pos + 2; word <= _maxWords; word++)
                            {
                                    if (Tokens[word] == "s_symbol")
                                    {
                                        WordIsText = true;
                                        for (int wix = word + 1; wix <= _maxWords; wix++)
                                        {
                                            if (Tokens.Count > wix && WordIsText)
                                            {
                                                if (Tokens[wix] == "s_symbol")
                                                {
                                                    WordIsText = false;
                                                    continue;
                                                }
                                                if (!IsTokenKey(Tokens[wix]))
                                                {
                                                    if (isFirstWord)
                                                    {
                                                        ValueToWrite += $"{Tokens[wix]}";
                                                        isFirstWord = false;
                                                        continue;
                                                    }
                                                    ValueToWrite += $" {Tokens[wix]}";
                                                    continue;
                                                }
                                                // Need fix
                                                //if (Tokens[wix] == "rsq" && WordIsText)
                                                //{
                                                //    ThrowError(ThrowErrors.SPECIFICSTR, 0, Tokens[wix], 4);
                                                //    break;
                                                //}
                                            }
                                        }
                                    }

                                    if (int.TryParse(Tokens[word], out int i) && !WordIsText)
                                    {
                                        if (isFirstWord)
                                        {
                                            ValueToWrite += $"{Tokens[word]}";
                                            isFirstWord = false;
                                            continue;
                                        }
                                        ValueToWrite += $" {Tokens[word]}";
                                        continue;
                                    }
                                    // Need to fix
                                    //else if (!WordIsText)
                                    //{
                                    //    ThrowError(ThrowErrors.SPECIFICSTR, 3);
                                    //}

                                    if (Tokens[word] == "rsq")
                                    {
                                        Console.WriteLine(ValueToWrite);
                                        break;
                                    }
                                break;
                            }
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

                    default:
                        tokens.Add(codetocompare[pos]);
                        break;
                }
            }
            return tokens;
        }
    }
}
