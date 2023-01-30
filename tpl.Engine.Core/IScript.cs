namespace tpl.Engine.Core
{
    internal interface IScript
    {
        string Version { get; set; }
        string Path { get; set; }
    }
}
