using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using tpl.Runtime.Interpreter.Loader;
using tpl.Runtime.Results;
using tpl.Runtime.Interpreter.Analysis;
using tpl.Core;
using tpl.Runtime.Interpreter;

namespace tpl.Engine
{
    public sealed class TplEngine : IEngine
    {
        public ScriptLoader ScriptLoader { get; private set; }
        public LoaderErrors LoaderErrors { get; private set; }

        public TplEngine(ScriptLoader scriptLoader, LoaderErrors loaderErrors)
        {
            ScriptLoader = scriptLoader;
            LoaderErrors = loaderErrors;
        }

        public void RegistryScript(string path) => ScriptLoader.LoadScript(path);
        public InterpreterResult RunAllScripts() => ScriptLoader.RunAllScriptsInModule();

        public bool RunScript(string script)
        {
            if (!File.Exists(script))
            {
                LoaderErrors.Throw($"File dont found in the path {script}", ConsoleColor.Red);
                return false;
            }
            _engineFrontendAnalysis(File.ReadAllText(script));
            return true;
        }

        private void _engineFrontendAnalysis(string source)
        {
            var Scanner = new Lexer(source);
            var Tokens = Scanner.ScanSource();

            foreach (var Token in Tokens)
            {
                Console.WriteLine(Token.Type + " " + Token.Lit);
            }
        }

        ~TplEngine()
        {

        }
    }
}
