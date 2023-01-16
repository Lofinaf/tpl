using System;
using System.Collections.Generic;

using static tpl.LibraryContent.Exception;

namespace tpl.LibraryContent
{
    public partial class Run
    {

        public enum TokenTypes
        {
            // k - keyword, s - string && integer param, UPPER - Func Branch, o - operator, f - field
            PRINT, // print()
            lsq, // (
            rsq, // )
            s_symbol, // "
            s_symbol2, // '
            k_import, // import
            k_var, // var
            o_sign, // =
            o_plus, // +
            o_minus, // -
            o_div, // /
            o_sub, // *
            k_const, // const
            k_func, // function
            k_stop, // stop program with exception
            s_newline // Space symbol define
        }

        public static List<string> ToTokens(string Source)
        {
            List<string> tokens = new List<string>();

            var codetocompare = Lex(Source).Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (int pos = 0; pos < codetocompare.Length; pos++)
            {
                // Console.WriteLine(codetocompare[pos]);
                switch (codetocompare[pos])
                {

                    case "cvar":
                        tokens.Add(TokenTypes.k_const.ToString());
                        break;

                    case "\n":
                        tokens.Add(TokenTypes.s_newline.ToString());
                        break;

                    case "-":
                        tokens.Add(TokenTypes.o_minus.ToString());
                        break;

                    case "*":
                        tokens.Add(TokenTypes.o_sub.ToString());
                        break;

                    case "/":
                        tokens.Add(TokenTypes.o_div.ToString());
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

                    default:
                        tokens.Add(codetocompare[pos]);
                        break;
                }
            }
            return tokens;
        }
    }
}
