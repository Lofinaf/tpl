using System;
using System.IO;

// Tpl namespace`s
using tpl.Interpreter;
using tpl.Runtime.Results;

namespace tpl.Runtime.Results.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var Result = Compiler.RuninSafeMod(File.ReadAllText("Test.tpl"));

            foreach (var item in Result.ErrorsList)
            {
                Console.WriteLine($"Throwed error! {item}");
            }
            Console.ReadKey(true);
        }
    }
}
