using System;
using System.Collections.Generic;

using static tpl.LibraryContent.Exception;

namespace tpl.LibraryContent
{
    public partial class Run
    {
        public static List<string> ToTokens(string Source)
        {
            List<string> tokens = new List<string>();

            var codetocompare = Lex(Source).Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (int pos = 0; pos < codetocompare.Length; pos++)
            {
                // Console.WriteLine(codetocompare[pos]);
                switch (codetocompare[pos])
                {
                    case "\n":
                        tokens.Add(TokenTypes.s_space.ToString());
                        break;

                    case "var":
                        tokens.Add(TokenTypes.k_var.ToString());
                        break;

                    case "=":
                        tokens.Add(TokenTypes.o_sign.ToString());
                        break;
                    case "+":
                        tokens.Add(TokenTypes.o_plus.ToString());
                        break;

                    case "print":
                        tokens.Add(TokenTypes.PRINT.ToString());
                        break;

                    case "(":
                        tokens.Add(TokenTypes.lsq.ToString());
                        break;

                    case ")":
                        tokens.Add(TokenTypes.rsq.ToString());
                        break;

                    case "\"":
                        tokens.Add(TokenTypes.s_symbol.ToString());
                        break;

                    case "\'":
                        tokens.Add(TokenTypes.s_symbol.ToString());
                        break;

                    case "exit":
                        break;

                    case "int":
                        break;

                    default:
                        if (codetocompare[pos - 1] != "int")
                        {
                            tokens.Add(codetocompare[pos]);
                            break;
                        }
                        tokens.Add($"N{codetocompare[pos]}");
                        break;
                }
            }
            return tokens;
        }
    }
}
