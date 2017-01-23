using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;

namespace ECommerceMobile.Service
{
   public class GeolocatorService
    {

        public double  Latitude { get; set; }

        public double Longitud { get; set; }

        public async Task getLocation()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var location = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

                Latitude = location.Latitude;
                Longitud = location.Longitude;
            }
            catch (Exception ex)
            {

                ex.ToString();

                return;
            }
        }

    }
}
