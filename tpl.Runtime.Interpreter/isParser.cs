using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpl.Runtime.Interpreter
{
    public partial class Parser
    {
        public static bool IsPrint(string text)
        {
            return (text == "print");
        }

        public static bool IsSign(string text)
        {
            return (text == "=");
        }
        public static bool IsDot(string text)
        {
            return (text == ".");
        }
        public static bool IsSemicolon(string text)
        {
            return (text == ";");
        }
        public static bool IsOpenScope(string text)
        {
            return (text == "{");
        }
        public static bool IsCloseScope(string text)
        {
            return (text == "}");
        }
        public static bool IsOpenBracket(string text)
        {
            return (text == "(");
        }
        public static bool IsCloseBracket(string text)
        {
            return (text == "(");
        }

        public static bool IsKey(string text)
        {
            if (IsSign(text) || IsDot(text) || IsSemicolon(text) || IsOpenScope(text) || IsCloseScope(text) || IsOpenBracket(text) || IsCloseBracket(text))
            {
                return true;
            }
            return false;
        }
    }
}
