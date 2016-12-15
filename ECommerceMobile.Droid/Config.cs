using ECommerceMobile.Interface;
using SQLite.Net.Interop;
using Xamarin.Forms;


[assembly: Dependency(typeof(ECommerceMobile.Droid.Config))]

namespace ECommerceMobile.Droid
{
    public class Config : IConfig
    {

        private string directoryDb;
        private ISQLitePlatform platform;
        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directoryDb))
                {
                    directoryDb = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }

                return directoryDb;
            }


        }

        public ISQLitePlatform Platform
        {
            get
            {
                if (platform == null)
                {
                    platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }

                return platform;
            }
        }
    }
}