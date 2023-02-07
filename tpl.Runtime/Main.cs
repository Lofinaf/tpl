using System;
using System.Linq;
using System.Text.Json;
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
            //Engine.RunScript("test.tpl", ScriptRunOptions.DEBUG_CODE);
            //TplEngine.parse_packet("package_test.tpkg", "package_test.tpkg_middlejsoncache");
            //var json = System.IO.File.ReadAllText("package_test.tpkg_middlejsoncache");
            //Token[] par = JsonSerializer.Deserialize<Token[]>("package_test.tpkg_middlejsoncache");
            //Console.WriteLine(par[0].Lit);
            // tests
            Console.ReadKey(true);
        }
    }
}
