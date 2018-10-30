/// <summary name="Xamarin.Forms.Dependency">
/// Directivas de compilador
/// </summary>
/// <Description>
/// Permite a las aplicaciones llamar a funciones específicas de plataforma, desde el código compartido. 
/// Esta funcionalidad permite a las aplicaciones de Xamarin.Forms, hacer todo lo que podria hacer una aplicación nativa.
/// </Description>
[assembly: Xamarin.Forms.Dependency(typeof(App.Droid.Implementations.Config))]
namespace App.Droid.Implementations
{    
    using COrjuela.Utility.Interfaces;
    using SQLite.Net.Interop;

    public class Config : IConfig
    {
        /// <summary>
        /// 
        /// </summary>
        private string directoryDB;

        /// <summary>
        /// 
        /// </summary>
        private ISQLitePlatform platform;

        /// <summary>
        /// Obtiene la ruta a la carpeta especial del sistema que se identifica por la enumeración especificada.
        /// </summary>
        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directoryDB))
                {
                    /// Opción = "SpecialFolder.Personal" carpeta especial del proyecto (directorio privado de la aplicación)
                    directoryDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }

                return directoryDB;
            }
        }

        /// <summary>
        /// Obtiene la libreria SQLite en Android
        /// </summary>
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