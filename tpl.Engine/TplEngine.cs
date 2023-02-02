using System;
using System.IO;
using System.Collections.Generic;
using tpl.Engine.Core.Loader;
using tpl.Engine.Core.Analysis;
using tpl.Core;

namespace tpl.Engine
{
    public sealed class TplEngine : IEngine
    {
        public ScriptLoader ScriptLoader { get; private set; }
        public Lexer Lexer { get; private set; }

        public TplEngine(Lexer lexer, ScriptLoader scriptLoader)
        {
            Lexer = lexer;
            ScriptLoader = scriptLoader;
            ScriptLoader.Init();
        }

        public void RegistryScript(string path) => ScriptLoader.Module.Add(new Core.Script("1.0.0.0", path));

        public bool RunScript(string script, ScriptRunOptions options)
        {
            if (!File.Exists(script))
            {
                Lexer.loaderErrors.Throw($"File don`t found in the path {script}", ConsoleColor.Red);
                return false;
            }
            var FrontendAnalysis = _engineFrontendAnalysis(File.ReadAllText(script));

            if (options is ScriptRunOptions.PACKAGE_BUILD)
            {
                File.WriteAllText($"package_{script}", _tokenToPackage(FrontendAnalysis));
            }

            if (options is ScriptRunOptions.DEBUG_CODE)
            {
                Lexer.loaderErrors.InterpreterResult.FrontentDebug = FrontendAnalysis;
                foreach (var Token in FrontendAnalysis)
                {
                    Console.WriteLine($"Token type: {Token.Type}; Value: {Token.Value}; Literal: {Token.Lit}");
                }
                return true;
            }
            return true;
        }

        private string _tokenToPackage(List<Token> list)
        {
            string content = "";
            foreach (var item in list)
            {
                content += $"\n{{TYPE\"{item.Type}\" VALUE\"{item.Type}\" LIT\"{item.Lit}\" LINE\"{item.Line}\"}}";
            }
            return content;
        }

        private List<Token> _packageToToken(string tokens)
        {

        }

        private List<Token> _engineFrontendAnalysis(string source)
        {
            Lexer.Source = source;
            return Lexer.ScanSource();
        }

        ~TplEngine()
        {

        }
    }
}
