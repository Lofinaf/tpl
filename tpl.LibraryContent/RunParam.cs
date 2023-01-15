using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpl.LibraryContent
{
    public partial class Run
    {
        private const int _maxWords = 10000;

        public enum Param
        {
            def,
            debug,
        }

        public enum TokenTypes
        {
            // k - keyword, s - string && integer param, UPPER - Func Branch, o - operator, f - field
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
            o_sign, // =
            o_plus, // +
            k_const, // const
            k_func, // function
            k_stop, // stop program with exception
            s_space, // Space symbol define
            f_int, // integer
        }
    }
}
