using tpl.Engine;
using tpl.Engine.Core.Loader;

namespace tpl.Engine
{
    public interface IEngine
    {
        LoaderErrors LoaderErrors { get; }
        ScriptLoader ScriptLoader { get; }
    }
}