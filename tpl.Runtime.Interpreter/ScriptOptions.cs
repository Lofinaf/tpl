using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpl.Runtime.Interpreter
{
    public class ScriptOptions
    {
        public bool Debug { get; private set; }
        public bool OptimizationSourceCodeBeforeRead { get; private set; }

        public ScriptOptions(bool debug, bool optimizationSourceCodeBeforeRead)
        {
            Debug = debug;
            OptimizationSourceCodeBeforeRead = optimizationSourceCodeBeforeRead;
        }
    }
}
