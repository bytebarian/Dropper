using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace Dropper.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private RelayCommand _syncCommand;

        public RelayCommand SyncCommand
        {
            get { return _syncCommand ?? (_syncCommand = new RelayCommand(async (args) => { await ScanCode(); })); }
        }

        public async Task ScanCode()
        {
            var scanPage = new ZXingScannerPage();

            scanPage.OnScanResult += (result) => {
                // Stop scanning
                scanPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(() => {
                    Application.Current.MainPage.Navigation.PopAsync();
                });
            };

            // Navigate to our scanner page
            await Application.Current.MainPage.Navigation.PushAsync(scanPage);
        }
    }
}
