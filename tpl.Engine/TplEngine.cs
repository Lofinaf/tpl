using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpl.Runtime.Interpreter.Loader;
using tpl.Runtime.Results;

namespace tpl.Engine
{
    public sealed class TplEngine : IEngine
    {
        public ScriptLoader ScriptLoader { get; private set; }
        public LoaderErrors LoaderErrors { get; private set; }

        public Stack<string> EngineStack { get; private set; }

        public TplEngine(ScriptLoader scriptLoader, LoaderErrors loaderErrors)
        {
            ScriptLoader = scriptLoader;
            LoaderErrors = loaderErrors;
        }

        public void RegistryScript(string path) => ScriptLoader.LoadScript(path);
        public InterpreterResult RunAllScripts() => ScriptLoader.RunAllScriptsInModule();

        ~TplEngine()
        {

        }
    }
}
