using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceMobile.Data;
using ECommerceMobile.Models;

namespace ECommerceMobile.Service
{

    public class DataService
    {

        #region Methods


        //aqui actualizo el usuario en la bd:
        public Response UpdateUser(User user)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    da.Update(user);
                }

                return new Response()
                {
                    IsSuccess = true,
                    Message = "Usuario Insertado, OK",
                    Result = user
                };
            }
            catch (Exception ex)
            {


                return  new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        //Metodo que nos deviulve el usario:
        public User GetUser()
        {
            using (var da = new DataAccess())
            {
                return da.First<User>(true);
            }


        }

        public Response InsertUser(User user)
        {
            try
            {




                using (var da = new DataAccess())
                {

                    //verifico que no exista un usuario ya logiado:, y si existe lo borro de la memoria.
                    var oldUser = da.First<User>(false);

                    if (oldUser != null)
                    {
                        da.Delete(oldUser);
                    }

                    da.Insert(user);
                }


                return new Response()
                {
                    IsSuccess = true,
                    Message = "Usuario Insertado, OK.!",
                    Result = user
                };
            }
            catch (Exception ex)
            {

                return new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public void SaveProducts(List<Product> productasList)
        {
            using (var da = new DataAccess())
            {
                //primero borro los datos viejos:
                var oldProducts = da.GetList<Product>(false);

                foreach (var product in oldProducts)
                {
                    da.Delete(product);
                }

                //aqui guardo los produxtos nuevos en la bd:
                foreach (var product in productasList)
                {
                    da.Insert(product);
                }

            }
        }

        public List<Product> GetProducts()
        {
            using (var da = new DataAccess())
            {
                return da.GetList<Product>(true);
            }
        }
    }


    #endregion
}
