using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ECommerceMobile.Models;
using Newtonsoft.Json;

namespace ECommerceMobile.Service
{
    public class ApiService
    {
        #region Methods

        public async Task<Response> Login(string email, string password)
        {
            try
            {
                //armo el objeto body para porder consumir nuestro api:
                var loginRequest = new LoginRequest()
                {
                    Email = email,
                    Password = password
                };

                //aqui convierto el contenido del objeto loginRequest a json:
                var request = JsonConvert.SerializeObject(loginRequest);

                var content = new StringContent(request, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://zulu-software.com");
                var url = "/ECommerce/api/Users/Login";

                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response()
                    {

                        IsSuccess = false,
                        Message = "Usuario o Contraseña incorrectos.",


                    };
                }

                //si llega hasta aqui , la respuesta hay convertir de json a strin:

                var result = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(result);


                return new Response()
                {
                    IsSuccess = true,
                    Message = "Login, OK.!",
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


        public async Task<List<Product>> GetProducts()
        {
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://zulu-software.com");
                var url = "/ECommerce/api/Products";

                var response = await client.GetAsync(url);


                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<Product>>(result);


                return products;
            }
            catch (Exception)
            {

                return null;
            }
        }

        #endregion


    }
}
