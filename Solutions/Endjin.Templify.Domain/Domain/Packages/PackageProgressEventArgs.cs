namespace Endjin.Templify.Domain.Domain.Packages
{
    using System;

    public class PackageProgressEventArgs : EventArgs
    {
        public PackageProgressEventArgs(ProgressStage progressStage, int maxValue, int currentValue)
        {
            this.ProgressStage = progressStage;
            this.MaxValue = maxValue;
            this.CurrentValue = currentValue;
        }

        public int MaxValue { get; set; }

        public int CurrentValue { get; set; }

        public ProgressStage ProgressStage { get; set; }
    }
}