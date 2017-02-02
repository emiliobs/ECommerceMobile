using System.Threading.Tasks;
using ECommerceMobile.Interface;
using ZXing.Mobile;

using Xamarin.Forms;


[assembly: Dependency(typeof(ECommerceMobile.Droid.QrCodeScanningService))]

namespace ECommerceMobile.Droid
{
   public  class QrCodeScanningService: IQrCodeScanningService
    {
        public async Task<string> ScanAsync()
        {
            var options = new MobileBarcodeScanningOptions();
            var scanner = new MobileBarcodeScanner();
            var scanResults = await scanner.Scan(options);

            return scanResults.Text;
        }
    }
}