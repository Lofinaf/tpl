using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            foreach (var item in Enum.GetValues(typeof(Run.TokenTypes)))
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
    }
}
