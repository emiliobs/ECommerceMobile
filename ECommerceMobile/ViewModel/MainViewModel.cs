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
            Menu = new ObservableCollection<MenuItemViewModel>();

            //Solo neceisto el login solo al entrar al servicio:
            NewLogin = new LoginViewModel();

            dataService = new DataService();

            //aqui me trae la propiedad fullname
            UserLoged = new UserViewModel();


            //traigo el nombre completo del usuario actual:
            LoadUser();


            //Menu
            LoadMenu();
        }



        #endregion


        #region Methods

        private void LoadUser()
        {
            var user = dataService.GetUser();

            ////aqui copio el user de la api(fullname properti de la clase User)
            ////y lo puedo bindiar desde userPage.xaml
            UserLoged.FullName = user.FullName;

            //para consumir la foto y bindiarla en el userpage:
            UserLoged.Photo = user.PhotoFullPath;


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
