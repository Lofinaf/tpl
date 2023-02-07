using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpl.Engine.Core.Parser.Statements;
using tpl.Engine.Core.Parser.Expressions;

namespace tpl.Engine.Core
{
    public abstract class Node
    {
        public abstract void Accept(Visit visit);
        public string State { get; set; }
    }

    public abstract class Visit
    {
        public abstract PrintStatement VisitElementUnaryPrint(PrintStatement printStatement);
        public abstract void VisitElementBinaryOperator(BinaryOperatorExpression binaryOperatorExpression);
    }

    public abstract class VisitorElement
    {
        public abstract void Accept();
    }

    public class Visitor : Visit
    {
        public override void VisitElementBinaryOperator(BinaryOperatorExpression binaryOperatorExpression) => binaryOperatorExpression.Some();
        public override PrintStatement VisitElementUnaryPrint(PrintStatement printStatement) => printStatement.Get();
    }

    public class AST
    {
        public List<Node> Nodes { get; set; } = new List<Node>();

        public void Add(Node node) => Nodes.Add(node);
        public void Remove(Node node) => Nodes.Remove(node);
        public void Accept(Visit visit)
        {
            foreach (var node in Nodes)
                node.Accept(visit);
        }
    }
}
