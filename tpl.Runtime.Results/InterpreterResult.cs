using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpl.Core;

namespace tpl.Runtime.Results
{
    public unsafe class InterpreterResult
    {
        public List<string> ErrorsList = new List<string>(); // not property!
        public List<Token> FrontentDebug = new List<Token>(); // not property!
    }
}
