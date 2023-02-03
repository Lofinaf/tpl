using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpl.Engine.Core.Parser;
using tpl.Runtime.Results;

namespace tpl.Engine.Core
{
    public class AST
    {
        public List<INode> Nodes { get; set; } = new List<INode>();

        public void Print()
        {
            var ASTPrinted = new StringBuilder("<TPL>: Abstract Syntax Three");
            foreach (var node in Nodes)
            {

            }
        }
    }
}
