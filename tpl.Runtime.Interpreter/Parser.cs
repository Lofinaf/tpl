using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpl.Runtime.Interpreter
{
    public partial class Parser
    {
        public static string Divide(string text)
        {
            string ret = "";
            string word = "";
            foreach (var item in text)
            {
                if (char.IsWhiteSpace(item) || IsKey(item.ToString()))
                {
                    word += $" {item}";
                    ret += $" {word}";
                    word = "";
                    continue;
                }
                word += $"{item}";
                continue;
            }
            return ret;
        }
    }
}
