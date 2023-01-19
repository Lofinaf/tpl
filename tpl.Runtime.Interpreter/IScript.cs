using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpl.Runtime.Interpreter
{
    internal interface IScript
    {
        string Version { get; set; }
        string Path { get; set; }
    }
}
