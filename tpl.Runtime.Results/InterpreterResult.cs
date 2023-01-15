using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpl.Runtime.Results
{
    public unsafe class InterpreterResult
    {
        public List<string> ErrorsList = new List<string>();
        public List<string> WarningsList = new List<string>();
    }
}
