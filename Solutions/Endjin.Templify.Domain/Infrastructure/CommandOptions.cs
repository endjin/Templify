namespace Endjin.Templify.Domain.Infrastructure
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    using CommandLine;

    #endregion

    public class CommandOptions
    {
        private string rawMode;
        private string[] rawTokens;

        public CommandOptions()
        {
            this.Mode = Mode.NotSet;
        }

        [Option("a", "Author")]
        public string Author { get; set; }

        [Option("n", "Name")]
        public string Name { get; set; }

        public Mode Mode { get; set; }

        [Option("i", "Package Name")]
        public string PackageName { get; set; }

        [Option("p", "Path")]
        public string Path { get; set; }

        [Option("m", "Mode")]
        public string RawMode
        {
            get
            {
                return this.rawMode;
            }

            set
            {
                this.rawMode = value;

                switch (this.rawMode.ToLowerInvariant())
                {
                    case "c":
                        this.Mode = Mode.Create;
                        break;
                    case "d":
                        this.Mode = Mode.Deploy;
                        break;
                }
            }
        }

        [OptionArray("t", "Tokens")]
        public string[] RawTokens
        {
            get
            {
                return this.rawTokens;
            }

            set
            {
                this.rawTokens = value;
                this.Tokens = this.rawTokens.Where(t => !string.IsNullOrEmpty(t)).Select(t => t.Split('=')).ToDictionary(p => p[0], p => p[1]);
            }
        }

        public Dictionary<string, string> Tokens { get; set; }

        [Option("v", "Version")]
        public string Version { get; set; }
    }
}