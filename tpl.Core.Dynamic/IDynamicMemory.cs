using tpl.Core.Variables;

namespace tpl.Core.Dynamic
{
    public interface IDynamicMemory
    {
        Variable.Types GetVariableType(string name);
        void NewVariable(string name, string value, bool vstatic);
        void SetVariable(string name, string value);
        void TritonMemory();
    }
}