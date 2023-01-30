using System;
using System.IO;
using tpl.Engine.Core.Loader;
using tpl.Engine.Core.Analysis;

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
            ScriptLoader.Init();
        }

        public void RegistryScript(string path) => ScriptLoader.Module.Add(new Core.Script("1.0.0.0", path));

        public bool RunScript(string script)
        {
            if (!File.Exists(script))
            {
                LoaderErrors.Throw($"File don`t found in the path {script}", ConsoleColor.Red);
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
