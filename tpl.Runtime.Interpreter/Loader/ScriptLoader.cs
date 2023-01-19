using System.Collections.Generic;
using System.IO;
using System.Linq;
using tpl.Core;
using tpl.Core.Variables;
using tpl.Core.Dynamic;
using tpl.Runtime.Results;

namespace tpl.Runtime.Interpreter.Loader
{
    class ScriptLoader
    {
        public List<Script> Module = new List<Script>();

        public DynamicMemory Memory = new DynamicMemory();

        public bool LoadScript(string path)
        {
            if (File.Exists(path))
            {
                Module.Add(new Script("1.0.0.0", File.ReadAllText(path), new ScriptOptions(false, false)));
                return true;
            }
            return false;
        }

        public InterpreterResult RunAllScriptsInModule()
        {
            var Result = new InterpreterResult();

            foreach (var Script in Module)
            {
                if (File.Exists(Script.Path))
                {
                    foreach (var Line in File.ReadLines(Script.Path))
                    {
                        var LineDivideWord = DivideToWord.Lex(Line).Split();

                        foreach (var Word in LineDivideWord)
                        {
                            if (Word is "var")
                            {
                                try
                                {

                                }
                                catch (System.Exception)
                                {

                                    throw;
                                }
                            }
                        }
                    }
                    continue;
                }
                // Throw Error
            }
            return Result;
        }
    }
}
