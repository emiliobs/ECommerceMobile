using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceMobile.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ECommerceMobile.Pages
{
    public partial class CustomerDetailPage : ContentPage
    {
        public CustomerDetailPage()
        {
            InitializeComponent();


            //GoogleMap:
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.GetGeolocation();

            foreach (var pin in mainViewModel.Pins)
            {
                myMap.Pins.Add(pin);

            }


            Locator();

        }

        private async void Locator()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var location = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            var position = new Position(location.Latitude, location.Longitude);
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(.3)));
        }
    }
}
