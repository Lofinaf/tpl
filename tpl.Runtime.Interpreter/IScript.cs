namespace tpl.Runtime.Interpreter
{
    internal interface IScript
    {
        string Version { get; set; }
        string Path { get; set; }
    }
}
