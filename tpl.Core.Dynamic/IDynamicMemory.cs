using tpl.Core.Variables;

namespace tpl.Core.Dynamic
{
    public interface IDynamicMemory
    {
        Variable.Types GetVaribleType(string name);
        void NewVarible(string name, string value, bool vstatic);
        void SetVarible(string name, string value);
        void TritonMemory();
    }
}