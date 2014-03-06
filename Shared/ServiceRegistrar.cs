using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Video.Core.Helpers;
using Video.Core.Interfaces;
using Video.Core.Services;
using Video.Core.ViewModels;

namespace Video.Core
{
    public static class ServiceRegistrar
    {
        public static void Startup()
        {
#if __MOBILE__
            SQLite.Net.SQLiteConnection connection = null;
            SQLite.Net.Interop.ISQLitePlatform platform = null;
            string dbLocation = "videoDB.db3";
#endif

#if XAMARIN_ANDROID
            var library = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dbLocation = Path.Combine(library, dbLocation);
            platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
#elif XAMARIN_IOS
            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var library = Path.Combine(docsPath, "../Library/");
            dbLocation = Path.Combine(library, dbLocation);
            platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
#elif WINDOWS_PHONE
            platform = new SQLite.Net.Platform.WindowsPhone8.SQLitePlatformWP8();
#elif NETFX_CORE
            platform = new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
#endif

            ServiceContainer.Register<IMovieService>(() => new MovieService());
#if __MOBILE__
            connection = new SQLite.Net.SQLiteConnection(platform, dbLocation);
            ServiceContainer.Register<IStorageService>(() => new StorageService(connection));
            ServiceContainer.Register<IMessageDialog>(() => new Video.PlatformSpecific.MessageDialog());
#endif
            ServiceContainer.Register<MoviesViewModel>();
            ServiceContainer.Register<MovieViewModel>();
        }
    }
}
