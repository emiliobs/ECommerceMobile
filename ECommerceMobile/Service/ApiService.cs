using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ECommerceMobile.Clasess;
using ECommerceMobile.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

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


        //Utilizo el Método Generico:

        //public async Task<List<Product>> GetProducts()
        //{
        //    try
        //    {

        //        var client = new HttpClient();
        //        client.BaseAddress = new Uri("http://zulu-software.com");
        //        var url = "/ECommerce/api/Products";

        //        var response = await client.GetAsync(url);


        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return null;
        //        }

        //        var result = await response.Content.ReadAsStringAsync();
        //        var products = JsonConvert.DeserializeObject<List<Product>>(result);


        //        return products.OrderBy(p => p.Description).ToList();
        //    }
        //    catch (Exception)
        //    {

        //        return null;
        //    }
        //}

        //Método Normal:
        //public async Task<List<Customer>> GetCustomers()
        //{
        //    try
        //    {

        //        var client = new HttpClient();
        //        client.BaseAddress = new Uri("http://zulu-software.com");
        //        var url = "/ECommerce/api/Customers";

        //        var response = await client.GetAsync(url);


        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return null;
        //        }

        //        var result = await response.Content.ReadAsStringAsync();
        //        var customers = JsonConvert.DeserializeObject<List<Customer>>(result);


        //        return customers.OrderBy(c => c.FirstName).ThenBy(c=>c.LastName).ToList();
        //    }
        //    catch (Exception)
        //    {

        //        return null;
        //    }
        //}

        //Método Generico:
        public async Task<List<T>> Get<T>(string controller)
        {
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://zulu-software.com");
                var url = $"/ECommerce/api/{controller}";

                var response = await client.GetAsync(url);


                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<T>>(result);


                return list;
            }
            catch (Exception)
            {

                return null;
            }
        }



        public async Task<Response> NewCustomer(Customer customer)
        {
            try
            {

                var request = JsonConvert.SerializeObject(customer);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://zulu-software.com");
                var url = "/ECommerce/api/Customers";
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response()
                    {

                        IsSuccess = false,
                        Message = response.StatusCode.ToString()
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newCustomer = JsonConvert.DeserializeObject<Customer>(result);

                return new Response()
                {
                    IsSuccess = true,
                    Message = "Cliente creado OK.",
                    Result = newCustomer
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

        #endregion

        public async  Task<Response> SetPhoto(int CustomerId, Stream stream)
        {
            try
            {
                var array = ReadFully(stream);

                var photoRequest = new PhotoRequest
                {
                    Id = CustomerId,
                    Array = array
                };

                var request = JsonConvert.SerializeObject(photoRequest);
                var body = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new  HttpClient();
                client.BaseAddress = new Uri("http://zulu-software.com");
                var url = "/ECommerce/api/Customers/SetPhoto";
                var response = await client.PostAsync(url, body);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response()
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString()
                    };
                }

                return new Response()
                {
                    IsSuccess = true,
                    Message = "Foto asignada OK."
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

        private static  byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream( ))
            {
                int read;

                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }

        }

        public async Task<Response> UpdateCustomer(Customer customer)
        {
            try
            {

                var request = JsonConvert.SerializeObject(customer);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://zulu-software.com");
                var url = $"/ECommerce/api/Customers/{customer.CustomerId}";
                var response = await client.PutAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response()
                    {

                        IsSuccess = false,
                        Message = response.StatusCode.ToString()
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newCustomer = JsonConvert.DeserializeObject<Customer>(result);

                return new Response()
                {
                    IsSuccess = true,
                    Message = "Cliente actualizado OK.",
                    Result = newCustomer
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
}
