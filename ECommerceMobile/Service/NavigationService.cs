using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceMobile.Pages;

namespace ECommerceMobile.Service
{
    public class NavigationService
    {
        #region Methods
        public async Task Navigate(string pageName)
        {
            //Para el cirre de la ventana lateral despues del click:
            App.Master.IsPresented = false;

            switch (pageName)
            {
                case "CustomersPage":
                    await App.Navigator.PushAsync(new CustomersPage());
                    break;
                case "DeliveriesPage":
                    await App.Navigator.PushAsync(new DeliveriesPage());
                    break;
                case "OrdersPage":
                    await App.Navigator.PushAsync(new OrdersPage());
                    break;
                case "ProductsPage":
                    await App.Navigator.PushAsync(new ProductsPage());
                    break;
                case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    break;
                case "SyncPage":
                    await App.Navigator.PushAsync(new SyncPage());
                    break;
                case "UsersPage":
                    await App.Navigator.PushAsync(new UsersPage());
                    break;

                default:
                    break;

            }
        }

        public void SetMainPage()
        {
            App.Current.MainPage = new MasterPage();
        }
        #endregion
    }
}
