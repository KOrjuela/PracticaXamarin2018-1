using System;

[assembly: Xamarin.Forms.Dependency(typeof(App.iOS.Implementations.Config))]
namespace App.iOS.Implementations
{
    using System;
    using COrjuela.Utility.Interfaces;
    using SQLite.Net.Interop;

    public class Config : IConfig
    {
        private string directoryDB;
        private ISQLitePlatform platform;

        /// <summary>
        /// 
        /// </summary>
        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directoryDB))
                {
                    var directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    directoryDB = System.IO.Path.Combine(directory, "..", "Library");
                }

                return directoryDB;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ISQLitePlatform Platform
        {
            get
            {
                if (platform == null)
                {
                    platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
                }

                return platform;
            }
        }
    }

}