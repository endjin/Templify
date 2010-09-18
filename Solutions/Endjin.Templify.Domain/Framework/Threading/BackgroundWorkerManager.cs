namespace Endjin.Templify.Domain.Framework.Threading
{
    #region Using Directives

    using System;
    using System.ComponentModel;

    #endregion

    public static class BackgroundWorkerManager
    {
        public static void RunBackgroundWork(Action work, Action<RunWorkerCompletedEventArgs> complete = null)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += delegate { work.Invoke(); };

            if (complete != null)
            {
                worker.RunWorkerCompleted += (sender, args) => complete.Invoke(args);
            }

            worker.RunWorkerAsync();
        }
    }
}