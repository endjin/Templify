namespace Endjin.Templify.Domain.Contracts.Framework.Loggers
{
    using System;

    public interface IErrorLogger
    {
        void Log(Exception exception);
    }
}