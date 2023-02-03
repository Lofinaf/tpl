using System;
using System.IO;
using System.Collections.Generic;
using tpl.Engine.Core.Loader;
using tpl.Engine.Core.Analysis;
using tpl.Core;
using System.Runtime.InteropServices;
using tpl.Engine.Core;
using tpl.Engine.Core.Parser;
using tpl.Engine.Core.Parser.Statements;

namespace tpl.Engine
{
    public sealed class TplEngine : IEngine
    {
        public ScriptLoader ScriptLoader { get; private set; }
        public Lexer Lexer { get; private set; }
        public Parser Parser { get; private set; }

        public TplEngine(Lexer lexer, Parser parser,ScriptLoader scriptLoader)
        {
            Lexer = lexer;
            Parser = parser;
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
            var SyntaxAnalysis = _engineSyntaxAnalysis(FrontendAnalysis);

            if (options is ScriptRunOptions.PACKAGE_BUILD)
            {
                File.WriteAllText($"package_{script}", TokenToPackage(FrontendAnalysis));
            }

            if (options is ScriptRunOptions.DEBUG_CODE)
            {
                Lexer.loaderErrors.InterpreterResult.FrontentDebug = FrontendAnalysis;
                Lexer.loaderErrors.InterpreterResult.BackendDebug = SyntaxAnalysis.Nodes;

                Console.WriteLine("Lexer returned:");
                foreach (var Token in FrontendAnalysis)
                {
                    Console.WriteLine($"Token type: {Token.Type}; Value: {Token.Value}; Literal: {Token.Lit}");
                }

                Console.WriteLine("Parser returned:");
                foreach (var Node in SyntaxAnalysis.Nodes)
                {
                    Console.WriteLine($"{Node.NodeName}");
                }
                return true;
            }
            return true;
        }

        public string TokenToPackage(List<Token> list)
        {
            string content = "";
            foreach (var item in list)
            {
                content += $"\n{{TYPE\"{item.Type}\" VALUE\"{item.Type}\" LIT\"{item.Lit}\" LINE\"{item.Line}\"}}";
            }
            return content;
        }

        private AST _engineSyntaxAnalysis(List<Token> tokens)
        {
            Parser.Tokens = tokens;
            return Parser.ScanTokens();
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
