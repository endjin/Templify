namespace Endjin.Templify.Domain.Domain.Packager.Processors
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.IO;
    using System.Text;

    using Endjin.Templify.Domain.Contracts.Packager.Processors;

    #endregion

    [Export(typeof(IFileContentProcessor))]
    public class FileContentProcessor : IFileContentProcessor
    {
        public string ReadContents(string path)
        {
            var fileEncoding = GetFileEncoding(path);

            return File.ReadAllText(path, fileEncoding);
        }

        public void WriteContents(string path, string content)
        {
            this.MakeWritable(path);

            var fileEncoding = GetFileEncoding(path);

            var hidden = this.IsHidden(path);

            if (hidden)
            {
                this.Unhide(path);
            }

            File.WriteAllText(path, content, fileEncoding);

            if (hidden)
            {
                this.Hide(path);
            }
        }

        private void Hide(string path)
        {
            File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);
        }

        private void Unhide(string path)
        {
            File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Hidden);
        }

        private bool IsHidden(string path)
        {
            return File.GetAttributes(path).HasFlag(FileAttributes.Hidden);
        }

        private void MakeWritable(string path)
        {
            File.SetAttributes(path, File.GetAttributes(path) & ~(FileAttributes.Archive | FileAttributes.ReadOnly));
        }

        /// <summary>
        /// Detects the byte order mark of a file and returns
        /// an appropriate encoding for the file.
        /// </summary>
        /// <param name="srcFile"></param>
        /// <returns></returns>
        private Encoding GetFileEncoding(string srcFile)
        {
            // *** Use Default of Encoding.Default (Ansi CodePage)
            Encoding enc = Encoding.Default;

            // *** Detect byte order mark if any - otherwise assume default
            var buffer = new byte[5];
            var file = new FileStream(srcFile, FileMode.Open);
            file.Read(buffer, 0, 5);
            file.Close();

            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
            {
                enc = Encoding.UTF8;
            }
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
            {
                enc = Encoding.Unicode;
            }
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
            {
                enc = Encoding.UTF32;
            }
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
            {
                enc = Encoding.UTF7;
            }

            return enc;
        }
    }
}