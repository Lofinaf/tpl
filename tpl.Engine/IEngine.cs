using tpl.Engine.Core.Analysis;
using tpl.Engine.Core.Loader;

namespace tpl.Engine
{
    public interface IEngine
    {
        Lexer Lexer { get; }
        ScriptLoader ScriptLoader { get; }

        void RegistryScript(string path);
        bool RunScript(string script, ScriptRunOptions options);
    }
}