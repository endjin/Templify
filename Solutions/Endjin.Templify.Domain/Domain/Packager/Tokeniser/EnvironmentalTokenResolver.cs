namespace Endjin.Templify.Domain.Domain.Packager.Tokeniser
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;

    using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;

    #endregion

    [Export(typeof(IEnvironmentalTokenResolver))]
    public class EnvironmentalTokenResolver : IEnvironmentalTokenResolver
    {
        public string Resolve(string item)
        {
            var resolvedString = item;

            // Assume any remaining tokens are Environment variables
            while (resolvedString.IndexOf("$(") != -1)
            {
                int start = resolvedString.IndexOf("$(");
                int end = resolvedString.IndexOf(")", start);

                string envVar = resolvedString.Substring(start + 2, end - (start + 2));
                string envVarValue = Environment.GetEnvironmentVariable(envVar);

                if (!String.IsNullOrEmpty(envVarValue))
                {
                    resolvedString = resolvedString.Replace(string.Format("$({0})", envVar), envVarValue);
                }
            }

            return resolvedString;
        }
    }
}