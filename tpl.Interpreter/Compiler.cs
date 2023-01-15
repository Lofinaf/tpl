using System;
using System.Collections.Generic;
using tpl.LibraryContent;
using tpl.Runtime.Results;

namespace tpl.Interpreter
{
    public class Compiler
    {
        public static Stack<string> Scope = new Stack<string>();
        public static Dictionary<string, string> Variables = new Dictionary<string, string>();

        #region RunAndDebug 
        public static InterpreterResult RuninSafeMod(string Source)
        {
            return RunTpl.ReadTokens(RunTpl.ToTokens(Source), ref Scope, ref Variables, RunTpl.Param.def);
        }
        public static InterpreterResult RuninSafeModWithDebug(string Source)
        {
            return RunTpl.ReadTokens(RunTpl.ToTokens(Source), ref Scope, ref Variables, RunTpl.Param.debug);
        }
        #endregion
    }
}
