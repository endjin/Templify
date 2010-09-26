namespace Endjin.Templify.CommandLine
{
    using System;

    public static class ConsoleProgress
    {
        private static int lastProgress = -1;
        private static float lastPercent = 0;
        private static string lastStage = string.Empty;

        private static bool reset = true;
        private static bool operationCanceled = false;

        public static bool Canceled
        {
            get
            {
                return operationCanceled;
            }
            set
            {
                operationCanceled = value;
                Update();
            }
        }

        #region Methods
        public static void Reset()
        {
            reset = true;
            operationCanceled = false;
        }

        public static void Update(int currentPosition, int maximum, string stage)
        {
            if (currentPosition == maximum)
            {
                Reset();
            }
            else
            {
                Update((float)currentPosition / maximum, stage);
            }
        }

        public static void Update(float percent, string stage)
        {
            // Reserved for ' [' ']', a space and percent text
            const int width = 3 + 6;
            int reserved = stage.Length + width;

            // Sanity check
            if (percent < 0 || percent > 1)
            {
                throw new ArgumentOutOfRangeException("percent", "Invalid progress value!");
            }

            // Calculate the number of dashes and white spaces we need.
            int capacity = Console.BufferWidth;
            int dashes = (int)(percent * (capacity - reserved));
            int spaces = capacity - reserved - dashes;

            lastPercent = percent;
            lastStage = stage;

            // Only update the progress bar when there is a full percent change
            // Otherwise, there might be performance hits
            if (lastProgress != dashes || operationCanceled || reset)
            {
                string progressText = string.Format(
                    "{0} [{1}{2}] {3}",
                    stage,
                    new string('-', dashes),
                    new string(' ', spaces),
                    operationCanceled ? "Abort" : string.Format("{0,5:P0}", percent));

                Console.Clear();
                Console.SetCursorPosition(0, 0);

                Console.Write(progressText);

                // Save the new state
                lastProgress = dashes;
                reset = false;
            }
        }

        private static void Update()
        {
            Update(lastPercent, lastStage);
        }

        #endregion
    }
}