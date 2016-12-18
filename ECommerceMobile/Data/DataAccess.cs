using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ECommerceMobile.Interface;
using ECommerceMobile.Models;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using Xamarin.Forms;

namespace ECommerceMobile.Data
{
    public class DataAccess : IDisposable
    {
        #region Attributes

        private SQLiteConnection connection;

        #endregion

        #region Constructor
        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();

            connection = new SQLiteConnection(config.Platform, Path.Combine(config.DirectoryDB, "ECommerce.db3"));

            connection.CreateTable<Category>();
            connection.CreateTable<City>();
            connection.CreateTable<Company>();
            connection.CreateTable<CompanyCustomer>();
            connection.CreateTable<Customer>();
            connection.CreateTable<Department>();
            connection.CreateTable<Inventory>();
            connection.CreateTable<Order>();
            connection.CreateTable<Product>();
            connection.CreateTable<Sale>();
            connection.CreateTable<Tax>();
            connection.CreateTable<User>();
        }

        #endregion


        #region Methods

        public void Insert<T>(T model)
        {
            connection.Insert(model);
        }

        public void Update<T>(T model)
        {
            connection.Update(model);
        }

        public void Delete<T>(T model)
        {
            connection.Delete(model);
        }

        //tbl relacionadas:

        public T First<T>(bool withChildren) where T : class
        {
            if (withChildren)
            {
                return connection.GetAllWithChildren<T>().FirstOrDefault();
            }
            else
            {
                return connection.Table<T>().FirstOrDefault();
            }
        }

        //get un lista:
        public List<T> GetList<T>(bool withChildren) where T : class
        {
            if (withChildren)
            {
                return connection.GetAllWithChildren<T>().ToList();
            }
            else
            {
                return connection.Table<T>().ToList();
            }
        }

        public T Find<T>(int pk, bool withChildren) where T : class
        {
            if (withChildren)
            {
                return connection.GetAllWithChildren<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            }
            else
            {
                return connection.Table<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            }
        }



        #endregion

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
