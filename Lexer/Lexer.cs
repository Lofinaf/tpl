using System;
using System.Collections.Generic;

namespace tpl.LibraryContent
{
    public class Lexer
    {

        public enum Param
        {
            safed,
            unsafed,
        }

        public enum TokenTypes
        {
            // k - keyword, s - string && integer, UPPER - Func Branch
            PRINT, // print()
            lsq, // (
            rsq, // )
            s_symbol, // "
            s_symbol2, // '
            k_import, // import
            s_symbolunk, // Unknown Symbol
            EXIT, // exit()
            function, // defined func
            variable, // defined var
            constant, // define constant
            k_var, // var
            k_const, // const
            k_func, // function
        }

        public static List<string> RunSource(string Source, ref Stack<string> StackOfLanguange, ref Dictionary<string,string> Variables, Param param)
        {
            List<string> tokens = new List<string>{""};
            int index;
            bool jumptofunction;

            var codetocompare = Source.Split(new char[] {' ', '(', ')', '"'});
            for (int pos = 0; pos <= codetocompare.Length; pos++)
            {
                Console.WriteLine(codetocompare[pos]);
            }

            return tokens;
        }
    }
}
