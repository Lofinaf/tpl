using System;
using System.Linq;
using tpl.Engine;
using tpl.Engine.Core.Analysis;
using tpl.Engine.Core.Loader;
using System.Collections.Generic;
using tpl.Core;
using tpl.Engine.Core.Parser;

namespace tpl.Runtime
{
    class MainClass
    {
        protected static TplEngine Engine = new TplEngine(new Lexer(""), new Parser(new List<Token>()), new ScriptLoader());

        public static void Main(string[] args)
        {
            foreach (var Argitem in args.Select((value, index) => (value, index)))
            {
                if (Argitem.value is "-file")
                {
                    Engine.RegistryScript(args[Argitem.index + 1]);
                }
            }
            Engine.RunScript("test.tpl", ScriptRunOptions.DEBUG_CODE);
            Console.ReadKey(true);
        }
    }
}
