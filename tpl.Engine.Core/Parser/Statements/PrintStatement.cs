using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpl.Runtime.Results;

namespace tpl.Engine.Core.Parser.Statements
{
    public class PrintStatement : INode
    {
        public string NodeName { get; set; }
        public object LiteralToPrint { get; set; }
    }
}
