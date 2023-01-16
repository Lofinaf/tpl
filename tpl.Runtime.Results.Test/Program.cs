using System;
using System.IO;

// Tpl namespace`s
using tpl.Interpreter;

namespace tpl.Runtime.Results.Test
{
    unsafe class Program
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
