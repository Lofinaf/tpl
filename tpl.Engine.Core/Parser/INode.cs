using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpl.Engine.Core.Parser
{
    internal interface INode
    {
        string Literal { get; set; }
    }
}
