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
    }


    #endregion
}
