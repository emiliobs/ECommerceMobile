using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ECommerceMobile.Models;
using ECommerceMobile.Service;
using GalaSoft.MvvmLight.Command;

namespace ECommerceMobile.ViewModel
{
    public class MainViewModel
    {
        #region Attributes

        private DataService dataService;

        public ApiService apiService;

        public NetService netService;

        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public ObservableCollection<ProductsItemViewMOdel> Products { get; set; }

        public LoginViewModel NewLogin { get; set; }

        public UserViewModel UserLoged { get; set; }

        public string Filter { get; set; }


        #endregion

        #region Constructor
        public MainViewModel()
        {

            //


            //aqui ya tengo el objeto instanciado(usted es la instancia)
            //singleton
            instance = this;


            //create observable collection
            Menu = new ObservableCollection<MenuItemViewModel>();
            Products = new ObservableCollection<ProductsItemViewMOdel>();


           //Instance service
            dataService = new DataService();
            apiService = new ApiService();
            netService = new NetService();

            //Create views
            //Solo neceisto el login solo al entrar al servicio:
            NewLogin = new LoginViewModel();
            //aqui me trae la propiedad fullname
            UserLoged = new UserViewModel();


            //load data:
            //traigo el nombre completo del usuario actual:
            //LoadUser();
            //Menu
            LoadMenu();
            LoadProduct();
        }



        #endregion

        #region Events

        #endregion


        #region Singleton

        //propiesda privatda estatica de ella misma:
        private static MainViewModel instance;


        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }

        #endregion


        #region Commands

        public ICommand searchProductCommand
        {
            get { return  new RelayCommand(SearchProduct);}
        }



        #endregion

        #region Methods

        private void SearchProduct()
        {
            //Aqui traigo los productos:
            var productsList = dataService.GetProducts(Filter);

            Products.Clear();

            foreach (var product in productsList)
            {
                Products.Add(new ProductsItemViewMOdel()
                {
                    BarCode = product.BarCode,
                    Category = product.Category,
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    CompanyId = product.CompanyId,
                    Tax = product.Tax,
                    Company = product.Company,
                    TaxId = product.TaxId,
                    ProductId = product.ProductId,
                    Price = product.Price,
                    Stock = product.Stock,
                    Inventories = product.Inventories,
                    Image = product.Image,
                    Remarks = product.Remarks


                });
            }


        }

        public async void LoadProduct()
        {
          var productasList = new List<Product>();



            //aqui pregunto si hay conexion a internet?:
            if (netService.IsConnected())
            {
                productasList = await apiService.GetProducts();

                //como hay conexion los gusdo en la db
                //para luego ustilizar una 2da instacia y  utilizar los datos si no hay conexion:
                dataService.SaveProducts(productasList);


            }
            else
            {
                productasList = dataService.GetProducts();
            }

            //lo limpio por si lo llamo de otro lado..
            Products.Clear();


            //Aqui hago la translación del objeto(paso todo de la api a las propiesdes de la clase ProductItemViewMOdel(en memoria)
            foreach (var product in productasList)
            {
                Products.Add(new ProductsItemViewMOdel()
                {
                    BarCode = product.BarCode,
                    Category = product.Category,
                    CategoryId = product.CategoryId,
                    Company = product.Company,
                    CompanyId = product.CompanyId,
                    Tax = product.Tax,
                    Description = product.Description,
                    Image = product.Image,
                    Inventories = product.Inventories,
                    Price = product.Price,
                    ProductId = product.ProductId,
                    Remarks = product.Remarks,
                    Stock = product.Stock,
                    TaxId = product.TaxId
                });
            }
        }



        public void LoadUser(User user)
        {
            UserLoged.FullName = user.FullName;
            UserLoged.Photo = user.PhotoFullPath;

            // var user = dataService.GetUser();

            ////Aqui valido si hay usuarios:
            //if (user != null)
            //{
            //    ////aqui copio el user de la api(fullname properti de la clase User)
            //    ////y lo puedo bindiar desde userPage.xaml
            //    UserLoged.FullName = user.FullName;

            //    //para consumir la foto y bindiarla en el userpage:
            //    UserLoged.Photo = user.PhotoFullPath;
            //}





        }


        private void LoadMenu()
        {
            Menu.Add(new MenuItemViewModel()
            {
                 Icon = "products.png",
                PageName = "ProductsPage",
                Title = "Productos."
            });

            Menu.Add(new MenuItemViewModel
            {

                Icon = "customer.png",
                PageName = "CustomersPage",
                Title = "Clientes."
            });

            Menu.Add(new MenuItemViewModel
            {

                Icon = "orders.png",
                PageName = "OrdersPage",
                Title = "Pedidos."
            });

            Menu.Add(new MenuItemViewModel
            {

                Icon = "delivery.png",
                PageName = "DeliveriesPage",
                Title = "Entregas."
            });

            Menu.Add(new MenuItemViewModel
            {

                Icon = "sync.png",
                PageName = "SyncPage",
                Title = "Sincronizar."
            });

            Menu.Add(new MenuItemViewModel
            {

                Icon = "setup.png",
                PageName = "SetupPage",
                Title = "Configuración."
            });

            Menu.Add(new MenuItemViewModel
            {

                Icon = "exit.png",
                PageName = "LogOutPape",
                Title = "Cerrar Sesión."
            });

        }

        #endregion
    }
}
