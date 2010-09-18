namespace Endjin.Templify.Domain.Domain.Packages
{
    using System;

    public class PackageProgressEventArgs : EventArgs
    {
        public PackageProgressEventArgs(int maxValue, int currentValue)
        {
            this.MaxValue = maxValue;
            this.CurrentValue = currentValue;
        }

        public int MaxValue { get; set; }

        public int CurrentValue { get; set; }
    }
}