namespace Endjin.Templify.CommandLine
{
    using System;

    #region Using Directives

    

    #endregion

    public class Program
    {
        public static void Main(string[] args)
        {
            var client = new Client();
            client.Execute(args);

            Console.ReadKey();
        }
    }
}