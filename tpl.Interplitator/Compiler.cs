using System;
using System.Collections.Generic;
using tpl.LibraryContent;

namespace tpl.Interplitator
{
    public class Compiler
    {
        public static Stack<string> Scope = new Stack<string>();
        public static Dictionary<string, string> Variables = new Dictionary<string, string>();

        #region RunAndDebug 
        public static bool RuninSafeMod(string Source)
        {
            RunTpl.ReadTokens(RunTpl.ToTokens(Source), ref Scope, ref Variables, RunTpl.Param.def);
            return true;
        }
        public static bool RuninSafeModWithDebug(string Source)
        {
            RunTpl.ReadTokens(RunTpl.ToTokens(Source), ref Scope, ref Variables, RunTpl.Param.debug);
            return true;
        }
        #endregion
    }
}
