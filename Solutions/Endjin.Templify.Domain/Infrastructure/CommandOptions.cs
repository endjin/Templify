namespace Endjin.Templify.Domain.Infrastructure
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using CommandLine;
    using CommandLine.Text;

    #endregion

    public class CommandOptions
    {
        private string rawMode;
        private string[] rawTokens;

        public CommandOptions()
        {
            this.Mode = Mode.NotSet;
            this.Tokens = new Dictionary<string, string>();
            this.PackageRepositoryPath = FilePaths.PackageRepository;
        }

        [Option("a", "author",
                HelpText = "Used when creating a package to set the author's name.")]
        public string Author { get; set; }

        [Option("n", "name",
                HelpText = "Name of a package to be deployed - must exist in repository.")]
        public string Name { get; set; }

        public Mode Mode { get; set; }

        [Option("i", "package",
                HelpText = "Name of the package to be created.")]
        public string PackageName { get; set; }

        [Option("p", "path",
                Required = true,
                HelpText = "Source path to be used when creating a package, or the destination path when deploying.")]
        public string Path { get; set; }

        [Option("r", "repository",
                HelpText = @"Alternative location of the Templify repository (Default: '%AppData%\Endjin\Templify\repo').")]
        public string PackageRepositoryPath { get; set; }

        [Option("m", "mode",
                Required = true,
                HelpText = "Specifies whether to (c)reate/(d)eploy a package or (s)how all tokens inside a package")]
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

        [OptionArray("t", "tokens",
                     HelpText = "Specifies name/value pairs to be used as part of token replacement.")]
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

        [Option("v", "version",
                HelpText = "Used when creating a package to set the version number.")]
        public string Version { get; set; }

        [HelpOption(HelpText = "Display this help text.")]
        public string GetUsage()
        {
            var help = new HelpText(new HeadingInfo("Templify",
                                                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()));

            help.AdditionalNewLineAfterOption = false;
            help.MaximumDisplayWidth = Console.WindowWidth;
            help.Copyright = new CopyrightInfo("Endjin", 2010);
            help.AddPreOptionsLine("Usage:");
            help.AddPreOptionsLine("    TemplifyCmd.exe -m c -p C:\\MySolution -i MyTemplate -t \"__NAME__=MySolution\" -a \"AN Other\" -v 1.0");
            help.AddPreOptionsLine("    TemplifyCmd.exe -m d -n MyTemplate -p C:\\NewSol -t \"__NAME__=NewSol\" ");
            help.AddPreOptionsLine("    TemplifyCmd.exe -m s -n MyTemplate");
            help.AddOptions(this);

            return help;
        }

    }
}