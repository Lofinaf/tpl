using System.Collections.Generic;
using System.IO;
using tpl.Core.Dynamic;
using tpl.Runtime.Results;

namespace tpl.Engine.Core.Loader
{
    public class ScriptLoader
    {
        public List<Script> Module;
        public DynamicMemory Memory;

        public void Init()
        {
            Memory = new DynamicMemory();
            Module = new List<Script>();
        }
    }
}
