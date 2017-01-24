using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ECommerceMobile.ViewModel;
using Xamarin.Forms;

namespace ECommerceMobile.Pages
{
    public partial class CustomersPage : ContentPage
    {
        public CustomersPage()
        {
            InitializeComponent();

            //esto es para actualizar el index despues de agregar un cliente nuevo:

            var main = (MainViewModel) BindingContext;

            Appearing += (object SendOrPostCallback, EventArgs e) =>
            {
                main.RefreshCustomersCommand.Execute(this);
            };
        }
    }
}
