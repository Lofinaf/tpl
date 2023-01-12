using System;
using tpl.Interplitator;

namespace tpl
{
    class Tools
    {
        private protected enum _argstype
        {
            c, // Run File
            argtoi // Path to file
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Compiler.RuninSafeMod(System.IO.File.ReadAllText("test.tpl"));
            Console.ReadKey(true);
        }
    }
}
