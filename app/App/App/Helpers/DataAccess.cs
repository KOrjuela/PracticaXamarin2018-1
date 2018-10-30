using App.Model.Entities;
using COrjuela.Utility.Interfaces;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace App.Helpers
{
    public class DataAccess : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private SQLiteConnection connection;

        /// <summary>
        /// Constrctor
        /// </summary>
        public DataAccess()
        {
            /// Obtenemos configuración de SQLite segun Config de cada plataforma
            var config = DependencyService.Get<IConfig>();

            /// Creamos base de datos "Lands.db3"
            this.connection = new SQLiteConnection(config.Platform, Path.Combine(config.DirectoryDB, "Lands.db3"));

            /// Creamos tabla con la entidad "UserLocal"
            connection.CreateTable<UserLocal>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void Insert<T>(T model)
        {
            this.connection.Insert(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void Update<T>(T model)
        {
            this.connection.Update(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void Delete<T>(T model)
        {
            this.connection.Delete(model);
        }

        /// <summary>
        /// Retorna el primer registro de la tabla 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WithChildren">Con hijos o sin hijos</param>
        /// <Eje>Trae todas las relaciones en base de datos relacionadas a ese primer registro</Eje>
        /// <returns></returns>
        public T First<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection.GetAllWithChildren<T>().FirstOrDefault();
            }
            else
            {
                return connection.Table<T>().FirstOrDefault();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WithChildren"></param>
        /// <returns></returns>
        public List<T> GetList<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection.GetAllWithChildren<T>().ToList();
            }
            else
            {
                return connection.Table<T>().ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pk"></param>
        /// <param name="WithChildren"></param>
        /// <returns></returns>
        public T Find<T>(int pk, bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection.GetAllWithChildren<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            }
            else
            {
                return connection.Table<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            }
        }

        /// <summary>
        /// Cierra la conexion a Base de datos
        /// </summary>
        public void Dispose()
        {
            connection.Dispose();
        }
    }

}
