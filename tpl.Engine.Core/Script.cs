namespace tpl.Engine.Core
{
    public class Script : IScript
    {
        public string Version { get; set; }
        public string Path { get; set; }

        public Script(string version, string path)
        {
            Version = version;
            Path = path;
        }
    }
}
