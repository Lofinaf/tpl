using System;

namespace tpl.Core.Variables
{
    public class Variable : IVariable
    {
        public enum Types
        {
            Float,
            Int,
            String,
            Bool
        }

        public string Value { get; set; }

        public bool IsStatic { get; set; }
        public Types Type { get; set; }

        public Variable(string Source, bool Static)
        {
            Value = Source;
            SetType(Source);
            IsStatic = Static;
        }
        public Variable(string Source)
        {
            Value = Source;
            if (!IsStatic)
            {
                SetType(Source);
            }
        }
        public void SetType(string str)
        {
            if (str.IndexOf("'") != -1)
            {
                if (str.IndexOf("'", str.IndexOf("'")) == -1) return;
                Type = Types.String;
                return;
            }
            if (str == "true" || str == "false")
            {
                Type = Types.Bool;
                return;
            }
            if (str.IndexOf(".") != -1)
            {
                if (double.TryParse(str, out double DoubleParseResult))
                {
                    Type = Types.Float;
                    return;
                }
                throw new Exception("Ошибка приведения к float");
            }
            if (int.TryParse(str, out int IntegerParseResult))
            {
                Type = Types.Int;
                return;
            }
            throw new Exception("Ошибка приведения к int");
        }
    }
}

