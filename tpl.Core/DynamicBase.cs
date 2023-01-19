using System;
using System.Collections.Generic;
using tpl.Core.Variables;

namespace tpl.Core
{
    public class DynamicBase
    {
        protected internal List<Variable> Varibles;
        public virtual void Add(string value) => Varibles.Add(new Variable(value));
        public virtual void Remove(string index) => Varibles.RemoveAt(Convert.ToInt32(index));
        public virtual string Get(string index) => Varibles[Convert.ToInt32(index)].Value;
        public virtual void Set(string index, string value) => Varibles[Convert.ToInt32(index)].Value = value;
    }
}
