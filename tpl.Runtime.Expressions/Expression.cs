using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpl.Runtime.Expressions
{
    public abstract class Expression
    {
        public List<string> Input; // Input Tokens
        public Stack<string> OperatorStack; // Stack for operators
        public Queue<string> Output; // Output Expression

        public abstract int HowMany();
    }
}
