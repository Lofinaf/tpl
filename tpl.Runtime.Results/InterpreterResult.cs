using System.Collections.Generic;
using tpl.Core;

namespace tpl.Runtime.Results
{
    public unsafe class InterpreterResult
    {
        public List<string> ErrorsList = new List<string>(); // not property!
        public List<Token> FrontentDebug = new List<Token>(); // not property!
        public List<INode> BackendDebug = new List<INode>(); // not property!
    }
}
