using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ECommerceMobile.Models;
using ECommerceMobile.Service;
using GalaSoft.MvvmLight.Command;

namespace ECommerceMobile.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Attributes

        private DataService dataService;

        public ApiService apiService;

        public NetService netService;

        //aqui ligo el public event PropertyChangedEventHandler PropertyChanged:
        private string filter;

        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public ObservableCollection<ProductsItemViewMOdel> Products { get; set; }

        public LoginViewModel NewLogin { get; set; }

        public UserViewModel UserLoged { get; set; }

        public string Filter
        {
            get { return filter; }
            set
            {

                if (filter != value)
                {
                    filter = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Filter"));

                    if (string.IsNullOrEmpty(filter))
                    {
                        //Aqui tomo los datos de forma locar, sin consumir el servicio
                        LoadLocalProduct();
                    }

                }
            }
        }



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

        public event PropertyChangedEventHandler PropertyChanged;

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

        private void LoadLocalProduct()
        {
            var productsList = dataService.GetProducts();


            //Metodo para recargar losdatos, para evitar copiar y pegar:
            ReloadProducts(productsList);




        }

        private void ReloadProducts(List<Product> productsList)
        {
            //lo limpio por si lo llamo de otro lado..
            Products.Clear();


            //Aqui hago la translación del objeto(paso todo de la api a las propiesdes de la clase ProductItemViewMOdel(en memoria)
            foreach (var product in productsList)
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

        private void SearchProduct()
        {
            //Aqui traigo los productos:
            var productsList = dataService.GetProducts(Filter);

            ReloadProducts(productsList);


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

            ReloadProducts(productasList);


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
