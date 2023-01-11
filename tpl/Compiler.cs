using System;
using System.Collections.Generic;
using tpl.LibraryContent;

namespace tpl.Interplitator
{

    public class LexerTpl : Lexer
    {
        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => base.ToString();
    }

    public class Compiler
    {
        public static Stack<string> Scope = new Stack<string>();
        public static Dictionary<string, string> Variables = new Dictionary<string, string>();

        #region RunAndDebug 
        public static bool RuninSafeMod(string Source)
        {
            try
            {
                var Tokens = Lexer.RunSource(Source, ref Scope, ref Variables, Lexer.Param.safed);
                return true;
            }
            catch (Exception)
            {
                throw new UnknownException("Unknown error with run source");
            }
        }
        #endregion

        #region ErrorThrowingParam
        private protected static string[] _aboutErrorsEN =
        {
            "Unknown Symbol",
            "Uncorrect file!"
        };

        public enum ThrowErrors : int
        {
            UNKNOWNSYBMOL = 0
        }

        public static void ThrowError(ThrowErrors Error, int Line, int Word)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Unhandled Exception, id: {Error}, line: {Line}, char: {Word}; {_aboutErrorsEN[Convert.ToInt32(Error)]}");
            Console.ResetColor();
        }
        public static void ThrowError(ThrowErrors Error, int Line)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Unhandled Exception, id: {Error}, line: {Line}; {_aboutErrorsEN[Convert.ToInt32(Error)]}");
            Console.ResetColor();
        }
        public static void ThrowError(ThrowErrors Error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Unhandled Exception, id: {Error}; {_aboutErrorsEN[Convert.ToInt32(Error)]}");
            Console.ResetColor();
        }
        #endregion
    }
}
