namespace Endjin.Templify.CommandLine
{
    #region Using Directives

    using System;    

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