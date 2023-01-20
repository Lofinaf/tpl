using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using tpl.Core;
using tpl.Core.Variables;
using tpl.Core.Dynamic;
using tpl.Runtime.Results;

using static tpl.Runtime.Interpreter.Parser;

namespace tpl.Runtime.Interpreter.Loader
{
    public class ScriptLoader
    {
        public List<Script> Module = new List<Script>();

        public DynamicMemory Memory = new DynamicMemory();

        public bool LoadScript(string path)
        {
            if (File.Exists(path))
            {
                Module.Add(new Script("1.0.0.0", path, new ScriptOptions(false, false)));
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
                        var LineDivideWord = Divide(Line).Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in LineDivideWord)
                        {
                            Console.WriteLine(item);
                        }
                        foreach (var Word in LineDivideWord.Select((v, i) => (v, i)))
                        {
                            if (IsPrint(Word.v))
                            {
                                //Console.WriteLine(LineDivideWord[Word.i]);
                            }
                        }
                    }
                    continue;
                }
                Result.ErrorsList.Add(new LoaderErrors().IdAbouts["TPL1"]);
            }
            return Result;
        }
    }
}
