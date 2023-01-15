using System;
using System.Collections.Generic;
using tpl.Runtime.Expressions;

namespace tpl.Runtime.Expressions
{
    public unsafe sealed class MathExpression : IExpression
    {
        public Stack<string> Three { get; set; }

        public int Analysis()
        {
            int ExprValueResult = 0;

            foreach (var item in Three)
            {

            }

            if (int.TryParse(Three.Pop(), out int Value))
            {

            }

            return ExprValueResult;
        }
    }
}
