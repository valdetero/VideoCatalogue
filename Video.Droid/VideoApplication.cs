using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Video.Droid
{
    [Application(Theme = "@android:style/Theme.Holo.Light")]
    public class VideoApplication : Application
    {
        public static Activity CurrentActivity { get; set; }
        public VideoApplication(IntPtr handle, global::Android.Runtime.JniHandleOwnership transer)
            : base(handle, transer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();
            Video.Core.ServiceRegistrar.Startup();
        }
    }
}

