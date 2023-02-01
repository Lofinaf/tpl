using System;
using System.Linq;
using System.IO;
using tpl.Runtime.Results;
using tpl.Engine;
using tpl.Engine.Core;
using tpl.Engine.Core.Analysis;
using tpl.Engine.Core.Loader;

namespace tpl.Runtime
{
    class MainClass
    {
        protected static TplEngine Engine = new TplEngine(new Lexer(""), new ScriptLoader());

        public static void Main(string[] args)
        {
            foreach (var Argitem in args.Select((value, index) => (value, index)))
            {
                if (Argitem.value is "-file")
                {
                    Engine.RegistryScript(args[Argitem.index + 1]);
                }
            }
            Engine.RunScript("test.tpl", ScriptRunOptions.DEBUG_LEXICAL_ANALYSIS);
            Console.ReadKey(true);
        }
    }
}
