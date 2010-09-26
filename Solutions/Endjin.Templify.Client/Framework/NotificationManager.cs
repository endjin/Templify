namespace Endjin.Templify.Client.Framework
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Windows.Controls.Primitives;

    using Endjin.Templify.Client.Contracts;
    using Endjin.Templify.Client.View;

    using Hardcodet.Wpf.TaskbarNotification;

    #endregion

    [Export(typeof(INotificationManager))]
    public class NotificationManager : INotificationManager
    {
        public void ShowNotification(string title, string message)
        {
            var balloon = new FancyBalloon { TextBody = message, TextTitle = title };

            // show balloon and close it after 4 seconds
            new TaskbarIcon().ShowCustomBalloon(balloon, PopupAnimation.Slide, 4000);
        }
    }
}