using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Video.Core.Interfaces;
using Video.iOS.Helpers;

namespace Video.PlatformSpecific
{
    public class MessageDialog : IMessageDialog
    {
        public void SendMessage(string message, string title = null)
        {
            UIHelpers.EnsureInvokedOnMainThread(() =>
            {
                var alertView = new UIAlertView(title ?? string.Empty, message, null, "OK", null);
                alertView.Show();
            });
        }
    }
}