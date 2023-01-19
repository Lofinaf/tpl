namespace tpl.Core.Variables
{
    public interface IVariable
    {
        bool IsStatic { get; set; }
        Variable.Types Type { get; set; }
        string Value { get; set; }

        void SetType(string str);
    }
}