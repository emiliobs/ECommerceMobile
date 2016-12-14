using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceMobile.Service
{
    public class DialogService
    {

        public async Task ShowMessage(string title, string message)
        {
            //si hay error toma la pagina actual y muestra el mensaje.
          await  App.Current.MainPage.DisplayAlert(title, message, "Aceptar");
        }
    }
}
