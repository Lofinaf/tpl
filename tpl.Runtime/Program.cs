using System;
using System.Linq;
using System.IO;
using tpl.Interpreter;

namespace tpl.Runtime
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var Argitem in args.Select((value, index) => (value, index)))
            {
                switch (Argitem.value)
                {
                    case "-run":
                        try
                        {
                            Compiler.RuninSafeMod(File.ReadAllText(args[Argitem.index+1]));
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("File not found");
                        }
                        break;

                    case "-debug":
                        try
                        {
                            Compiler.RuninSafeMod(File.ReadAllText(args[Argitem.index+1]));
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("File not found");
                        }
                        break;

                    default:
                        break;
                }
            }
            Compiler.RuninSafeMod(System.IO.File.ReadAllText("test.tpl"));
            Console.ReadKey(true);
        }
    }
}
