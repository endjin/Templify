namespace Endjin.Templify.Domain.Infrastructure
{
    using System.Linq;

    using CommandLine;

    public class InternalCommandOptions
    {
        [Option("c", "Create Package")]
        public string createPackagePath;

        [Option("d", "Deploy Package")]
        public string deployPackagePath;

        [OptionArray("t", "Tokens")]
        public string[] tokens;

        [Option("a", "Author")]
        public string author;

        [Option("n", "Name")]
        public string name;

        [Option("v", "Version")]
        public string version;

        public CommandOptions ToCommandOptions()
        {
            return new CommandOptions
                        {
                            Author = this.author,
                            Mode = !string.IsNullOrEmpty(this.createPackagePath) ? Mode.Create : Mode.Deploy,
                            Name = this.name, 
                            PackagePath = this.createPackagePath ?? this.deployPackagePath,
                            Tokens = this.tokens.Select(t => t.Split('=')).ToDictionary(p => p[0], p => p[1]),
                            Version = this.version,
                        };
        }
    }
}