namespace Endjin.Templify.Domain.Domain.Packager.Processors
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;

    using Endjin.Templify.Domain.Contracts.Packager.Processors;

    #endregion

    [Export(typeof(IArtefactProcessor))]
    public class FileSystemArtefactProcessor : IArtefactProcessor
    {
        public virtual IEnumerable<string> RetrieveFiles(string path)
        {
            return this.RetrieveFiles(path, "*.*");
        }

        public IEnumerable<string> RetrieveFiles(string path, string filter)
        {
            return Flatten(path, Directory.GetDirectories).SelectMany(dir => Directory.EnumerateFiles(dir, filter));
        }

        public void RemoveFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public virtual IEnumerable<string> RetrieveDirectories(string path)
        {
            return Flatten(path, Directory.EnumerateDirectories);
        }

        private static IEnumerable<T> Flatten<T>(T item, Func<T, IEnumerable<T>> next)
        {
            yield return item;

            foreach (T flattenedChild in next(item).SelectMany(child => Flatten(child, next)))
            {
                yield return flattenedChild;
            }
        }
    }
}