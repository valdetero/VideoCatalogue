using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Video.Core.Interfaces;
using Video.Droid;

namespace Video.PlatformSpecific
{
    public class MessageDialog : IMessageDialog
    {
        public void SendMessage(string message, string title = null)
        {
            if (VideoApplication.CurrentActivity == null)
                return;

            VideoApplication.CurrentActivity.RunOnUiThread(() =>
            {
                var builder = new AlertDialog.Builder(VideoApplication.CurrentActivity);
                builder.SetMessage(message)
                  .SetTitle(title ?? string.Empty)
                  .SetPositiveButton("OK", delegate { });
                var dialog = builder.Create();
                dialog.Show();
            });
        }
    }
}