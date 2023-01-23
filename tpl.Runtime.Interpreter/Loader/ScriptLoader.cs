using System.Collections.Generic;
using System.IO;
using tpl.Core.Dynamic;
using tpl.Runtime.Results;

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
            return Result;
        }
    }
}
