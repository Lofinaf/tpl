using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpl.Runtime.Interpreter
{
    public class Script : IScript
    {
        public string Version { get; set; }
        public string Path { get; set; }

        public ScriptOptions ScriptOptions { get; set; }

        public Script(string version, string path, ScriptOptions scriptOptions)
        {
            Version = version;
            Path = path;
        }
    }
}
