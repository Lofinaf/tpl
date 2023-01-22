using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpl.Runtime.Expressions
{
    public class PrintExpression
    {
        protected string Value { get; private set; }

        public void AddValue(string value) => Value += value;
        public void Output()
        {
            Console.WriteLine(Value);
        }
    }
}
