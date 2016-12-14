using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceMobile.ViewModel
{
    public class MainViewModel
    {
        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }


        #endregion

        #region Constructor
        public MainViewModel()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();

            LoadMenu();
        }


        #endregion


        #region Methods
        private void LoadMenu()
        {
            Menu.Add(new MenuItemViewModel()
            {
                 Icon = "ic_action_product.png",
                PageName = "ProductsPage",
                Title = "Productos."
            });

            Menu.Add(new MenuItemViewModel
            {

                Icon = "ic_action_action_account_child.png",
                PageName = "CustomersPage",
                Title = "Clientes."
            });

            Menu.Add(new MenuItemViewModel
            {

                Icon = "ic_action_orders.png",
                PageName = "OrdersPage",
                Title = "Pedidos."
            });

            Menu.Add(new MenuItemViewModel
            {

                Icon = "ic_action_delivery.png",
                PageName = "DeliveriesPage",
                Title = "Entregas."
            });

            Menu.Add(new MenuItemViewModel
            {

                Icon = "ic_action_async.png",
                PageName = "SyncPage",
                Title = "Sincronizar."
            });

            Menu.Add(new MenuItemViewModel
            {

                Icon = "ic_action_setup.png",
                PageName = "SetupPage",
                Title = "Configuración."
            });

            Menu.Add(new MenuItemViewModel
            {

                Icon = "ic_action_exit.png",
                PageName = "LogOut",
                Title = "Salir."
            });

        }

        #endregion
    }
}
