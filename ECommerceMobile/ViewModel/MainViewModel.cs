using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceMobile.Models;
using ECommerceMobile.Service;

namespace ECommerceMobile.ViewModel
{
    public class MainViewModel
    {
        #region Attributes

        private DataService dataService;

        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public LoginViewModel NewLogin { get; set; }

        public UserViewModel UserLoged { get; set; }



        #endregion

        #region Constructor
        public MainViewModel()
        {
            //aqui ya tengo el objeto instanciado(usted es la instancia)
            //singleton
            instance = this;


            //create observable collection
            Menu = new ObservableCollection<MenuItemViewModel>();


           //Instance service
            dataService = new DataService();


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
        }



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

        #region Methods

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
