namespace Endjin.Templify.CommandLine
{
    #region Using Directives

    using System;
    using System.Diagnostics;
    using System.IO;

    #endregion

    public class Program
    {
        public static void Main(string[] args)
        {
            var client = new Client();
            client.Execute(args);
        }
    }
}