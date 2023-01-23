using System;
using System.Collections.Generic;

namespace tpl.Runtime.Interpreter.Loader
{
    public class LoaderErrors
    {
        public Dictionary<string, string> IdAbouts = new Dictionary<string, string>()
        {
            {"TPL1", "File not found"},
            {"TPL2", "Unknown symbol"},
        };

        public void Throw(string text, ConsoleColor consoleColor)
        {
            Console.BackgroundColor = consoleColor;
            Console.WriteLine(text);
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
