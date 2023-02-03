using System.Collections.Generic;
using tpl.Core;
using tpl.Engine.Core.Analysis;
using tpl.Engine.Core.Loader;
using tpl.Engine.Core.Parser;

namespace tpl.Engine
{
    public interface IEngine
    {
        string TokenToPackage(List<Token> list);
    }
}