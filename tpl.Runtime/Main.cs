﻿using System;
using System.Linq;
using System.IO;
using tpl.Runtime.Results;
using tpl.Engine;
using tpl.Runtime.Interpreter.Loader;
using tpl.Runtime.Interpreter.Analysis;

namespace tpl.Runtime
{
    class MainClass
    {
        protected static TplEngine Engine = new TplEngine(new ScriptLoader(), new LoaderErrors(new InterpreterResult()));

        public static void Main(string[] args)
        {
            foreach (var Argitem in args.Select((value, index) => (value, index)))
            {
                if (Argitem.value is "-file")
                {
                    // Engine.RegistryScript(args[Argitem.index + 1]);
                }
                var Lexer = new Lexer(File.ReadAllText("test.tpl"), Engine.LoaderErrors);

                foreach (var item in Lexer.ScanSource())
                {
                    Console.WriteLine(item);
                }
            }
            // Engine.RegistryScript("test.tpl");
            Console.ReadKey(true);
        }
    }
}
