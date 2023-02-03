using System;
using System.Collections.Generic;
using tpl.Runtime.Results;

namespace tpl.Engine.Core.Loader
{
    public class LoaderErrors
    {
        public InterpreterResult InterpreterResult { get; private set; }

        public LoaderErrors(InterpreterResult interpreterResult)
        {
            InterpreterResult = interpreterResult;
        }

        public Dictionary<string, string> IdAbouts = new Dictionary<string, string>()
        {
            {"TPL1", "File not found"},
            {"TPL2", "Unknown symbol"},
        };

        public void Throw(string text, ConsoleColor consoleColor)
        {
            InterpreterResult.ErrorsList.Add(text);
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
