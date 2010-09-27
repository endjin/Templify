namespace Endjin.Templify.Domain.Infrastructure
{
    #region Using Directives

    using System;
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
            this.Tokens = new Dictionary<string, string>();
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
                    case "s":
                        this.Mode = Mode.ShowTokens;
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

                var tokens = this.rawTokens.Where(t => !string.IsNullOrEmpty(t)).Select(t => t.Split('='));

                foreach (var token in tokens)
                {
                    if (token.Length == 2)
                    {
                        if (string.IsNullOrEmpty(token[1]))
                        {
                            throw new ArgumentException("Token is Malformed");
                        }

                        if (this.Tokens.ContainsKey(token[0]))
                        {
                            this.Tokens[token[0]] = token[1];
                        }
                        else
                        {
                            this.Tokens.Add(token[0], token[1]);
                        }
                        
                    }
                }
            }
        }

        public Dictionary<string, string> Tokens { get; set; }

        [Option("v", "Version")]
        public string Version { get; set; }
    }
}