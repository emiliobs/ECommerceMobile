using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceMobile.Interface;
using Xamarin.Forms;

namespace ECommerceMobile.Service
{
    public class NetService
    {

        public bool IsConnected()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();

            networkConnection.CheckNetworkConnection();

            return networkConnection.IsConnected;

        }

    }
}
