using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpl.Interplitator
{
    [Serializable]
    public class UnknownException : Exception
    {
        public UnknownException() { }
        public UnknownException(string message) : base(message) { }
        public UnknownException(string message, Exception inner) : base(message, inner) { }
        protected UnknownException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
