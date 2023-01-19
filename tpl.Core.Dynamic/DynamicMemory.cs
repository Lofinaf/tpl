using System;
using System.Collections.Generic;
using tpl.Core.Variables;

using static tpl.Core.Variables.Variable;

namespace tpl.Core.Dynamic
{
    public class DynamicMemory : IDynamicMemory
    {
        public Dictionary<string, Variable> Variables;

        public void NewVarible(string name, string value, bool vstatic)
        {
            if (!Variables.ContainsKey(name))
            {
                Variables.Add(name, new Variable(value, vstatic));
                return;
            }
            throw new Exception("Такая переменая имеется");
        }

        public Types GetVaribleType(string name)
        {
            if (!Variables.ContainsKey(name))
            {
                throw new Exception("Такая переменая не существует");
            }
            return Variables[name].Type;
        }

        public void SetVarible(string name, string value)
        {
            if (!Variables.ContainsKey(name))
            {
                throw new Exception("Такая переменая не существует");
            }
            Variables[name] = new Variable(value);
        }

        public void TritonMemory()
        {
            Variables = new Dictionary<string, Variable>();
        }
    }
}
