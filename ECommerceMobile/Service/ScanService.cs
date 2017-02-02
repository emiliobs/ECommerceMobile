using System;
using System.Threading.Tasks;
using ECommerceMobile.Interface;
using Xamarin.Forms;

namespace ECommerceMobile.Service
{
    public class ScanService
    {
        public async Task<string> Scanner()
        {
            try
            {
                var scanner = DependencyService.Get<IQrCodeScanningService>();
                var result = await scanner.ScanAsync();

                return result.ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();

                return string.Empty;
            }
        }
    }
}
