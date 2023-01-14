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
            LexerTpl.ReadTokens(LexerTpl.ToTokens(Source), ref Scope, ref Variables, Lexer.Param.def);
            return true;
        }
        public static bool RuninSafeModWithDebug(string Source)
        {
            LexerTpl.ReadTokens(LexerTpl.ToTokens(Source), ref Scope, ref Variables, Lexer.Param.debug);
            return true;
        }
        #endregion
    }
}
