using System;

namespace tpl.LibraryContent
{
    public class Exception
    {
        #region ErrorThrowingParam
        private protected static string[] _aboutErrorsEN =
        {
            "Unknown Symbol",
            "Uncorrect file!",
            "\"(\" not found",
            "Variable not found",
            "Error operator using",
            "This variable can be declared again",
        };

        public enum ThrowErrors : int
        {
            UNKNOWNSYBMOL,
            LSQNOTFOUND,
            INTYPEISSTRING,
            SPECIFICSTR,
            OPERATORERR,
            VARCANBEDECLARED,
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
                if (char.IsWhiteSpace(item) || IsBracket(item.ToString()) || IsSemicolon(item.ToString()) || IsDot(item.ToString()) || IsSign(item.ToString()))
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
