using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpl.Core;
using tpl.Runtime.Results;

namespace tpl.Engine.Core.Parser.Expressions
{
    public class BinaryOperatorExpression : Node
    {
        public string NodeName { get; set; }

        public Token Right { get; set; }
        public Token Operator { get; set; }
        public Token Left { get; set; }

        public override void Accept(Visit visit)
        {
            visit.VisitElementBinaryOperator(this);
        }

        public void Some() { }
    }
}
