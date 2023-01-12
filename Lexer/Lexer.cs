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
        };

        public enum ThrowErrors : int
        {
            UNKNOWNSYBMOL = 0,
            LSQNOTFOUND = 5,
            INTYPEISSTRING = 10,
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
                switch (item)
                {
                    default:
                        if (char.IsWhiteSpace(item) || IsBracket(item.ToString()) || IsSemicolon(item.ToString()) || IsDot(item.ToString()))
                        {
                            word += $" {item}";
                            ret += $" {word}";
                            word = "";
                            break;
                        }
                        word += $"{item}";
                        break;
                }
            }
            return ret;
        }

        public enum Param
        {
            safed,
            unsafed,
        }

        public enum TokenTypes
        {
            // k - keyword, s - string && integer, UPPER - Func Branch
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
            k_const, // const
            k_func, // function
            k_stop, // stop program with exception
            s_space, // Space symbol define
        }

        public static void RunTokens(List<string> Tokens, ref Stack<string> Stack, ref Dictionary<string, string> Variables, Param param)
        {
            for (int pos = 0; pos < Tokens.Count; pos++)
            {
                var token = Tokens[pos];
                switch (token)
                {

                    case "PRINT":
                        if (Tokens[pos+1] == "lsq")
                        {
                            string ValueToWrite = "";
                            bool isFirstWord = true;
                            bool WordIsText = false;
                            for (int word = pos + 2; word < _maxWords; word++)
                            {
                                if (Tokens[word] == "s_symbol")
                                {
                                    WordIsText = true;
                                    for (int wix = word+1; wix < _maxWords; wix++)
                                    {
                                        if (Tokens[wix] == "s_symbol")
                                        {
                                            WordIsText = false;
                                            Console.WriteLine(ValueToWrite);
                                            break;
                                        }
                                        if (isFirstWord)
                                        {
                                            ValueToWrite += $"{Tokens[wix]}";
                                            isFirstWord = false;
                                            continue;
                                        }
                                        ValueToWrite += $" {Tokens[wix]}";
                                    }
                                    continue;
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

                                if (Tokens[word] == "rsq")
                                {
                                    Console.WriteLine(ValueToWrite);
                                    break;
                                }
                            }
                            break;
                        }
                        ThrowError(ThrowErrors.LSQNOTFOUND, 2);
                        return;

                    default:
                        break;
                }
            }
        }

        public static List<string> RunSource(string Source)
        {
            List<string> tokens = new List<string>();

            var codetocompare = Lex(Source).Split(new string[]{" "}, StringSplitOptions.RemoveEmptyEntries);
            for (int pos = 0; pos < codetocompare.Length; pos++)
            {
                switch (codetocompare[pos])
                {
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
                        tokens.Add(TokenTypes.s_symbol2.ToString());
                        break;

                    case "exit":
                        return tokens;

                    default:
                        tokens.Add(codetocompare[pos]);
                        break;
                }
            }
            return tokens;
        }
    }
}
