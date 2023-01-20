using tpl.Runtime.Interpreter.Loader;

namespace tpl.Engine
{
    public interface IEngine
    {
        LoaderErrors LoaderErrors { get; }
        ScriptLoader ScriptLoader { get; }
    }
}