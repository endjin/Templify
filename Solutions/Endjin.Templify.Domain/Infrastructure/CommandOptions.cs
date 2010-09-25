namespace Endjin.Templify.Domain.Infrastructure
{
    using System.Collections.Generic;

    public class CommandOptions
    {
        public string Author { get; set; }

        public string Name { get; set; }

        public Mode Mode { get; set; }

        public string PackagePath { get; set; }

        public string Version { get; set; }

        public Dictionary<string, string> Tokens { get; set; }
    }
}