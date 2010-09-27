namespace Endjin.Templify.Domain.Infrastructure
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using CommandLine;

    using Endjin.Templify.Domain.Contracts.Infrastructure;

    #endregion

    [Export(typeof(ICommandLineProcessor))]
    public class CommandLineProcessor : ICommandLineProcessor
    {
        public CommandOptions Process(string[] args)
        {
            var options = new CommandOptions();
            var parser = new CommandLineParser();
            var parsedArgs = new List<string>();

            foreach (string arg in args)
            {
                parsedArgs.AddRange(arg.Split(Convert.ToChar(" ")));
            }

            parser.ParseArguments(parsedArgs.ToArray(), options);

            return options;
        }
    }
}