namespace Endjin.Templify.Domain.Framework.Loggers
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Text;

    using Endjin.Templify.Domain.Contracts.Framework.Loggers;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    [Export(typeof(IErrorLogger))]
    public class FileSystemErrorLogger : IErrorLogger
    {
        public void Log(Exception exception)
        {
            this.EnsureErrorLogsPathExists();

            string filePath = this.GetErrorFilePath();
            string errorMessage = this.GenerateErrorMessage(exception);

            File.WriteAllText(filePath, errorMessage, Encoding.UTF8);
        }

        private void EnsureErrorLogsPathExists()
        {
            var errorLogsPath = new DirectoryInfo(FilePaths.ErrorLogs);

            if (!errorLogsPath.Exists)
            {
                errorLogsPath.Create();
            }
        }

        private string GenerateErrorMessage(Exception exception)
        {
            var sb = new StringBuilder();
            sb.AppendLine(exception.Message);
            sb.Append(exception.StackTrace);

            return sb.ToString();
        }

        private string GetErrorFilePath()
        {
            string dateTime = DateTime.Now.ToString("s").Replace(":", "-");
            string fileName = string.Format("error-{0}-{1}.txt", dateTime, Guid.NewGuid());
            return Path.Combine(FilePaths.ErrorLogs, fileName);
        }
    }
}