using System.Collections.Generic;
using System.IO;
using tpl.Core.Dynamic;
using tpl.Runtime.Results;
using tpl.Runtime.Interpreter.Analysis;

namespace tpl.Runtime.Interpreter.Loader
{
    public class ScriptLoader
    {
        public List<Script> Module;
        public DynamicMemory Memory;

        private Script _local;

        public bool LoadScript(string path)
        {
            if (File.Exists(path))
            {
                _local = new Script("1.0.0.0", path);
                Module.Add(_local);
                return true;
            }
            return false;
        }

        public void Init()
        {
            Memory = new DynamicMemory();
            Module = new List<Script>();
        }

        public InterpreterResult RunAllScriptsInModule()
        {
            var Result = new InterpreterResult();

            return Result;
        }
    }
}
