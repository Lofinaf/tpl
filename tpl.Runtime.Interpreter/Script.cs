namespace tpl.Runtime.Interpreter
{
    public class Script : IScript
    {
        public string Version { get; set; }
        public string Path { get; set; }

        public ScriptOptions ScriptOptions { get; set; }

        public Script(string version, string path)
        {
            Version = version;
            Path = path;
        }
    }
}
