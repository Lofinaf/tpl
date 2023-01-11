using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Compiler.RuninSafeMod("Тест тест2 звезда");
            Console.ReadKey(true);
        }
    }
}
