using Dropper.Services;
using Dropper.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace Dropper.ViewModels
{
    public class DropDetailsViewModel : ViewModelBase
    {
        private RelayCommand _generateCodeCommand;
        private string _dropName;
        private IDatabaseController _databaseController;

        public RelayCommand GenerateCodeCommand
        {
            get { return _generateCodeCommand ?? (_generateCodeCommand = new RelayCommand(async (args) => { await GenerateCode(); })); }
        }

        private async Task GenerateCode()
        {
            string ipaddress = DependencyService.Get<IIPAddressService>().GetIPAddress();

            if (string.IsNullOrEmpty(ipaddress) || string.IsNullOrEmpty(_dropName)) return;

            var credentials = new Credentials();
            credentials.Login = Guid.NewGuid().ToString();
            credentials.Password = Guid.NewGuid().ToString();
            credentials.Port = 5432;
            credentials.SyncGatewayUrl = string.Format("http://{0}:{1}/{2}", ipaddress, credentials.Port, _dropName);

            var receiver = DependencyService.Get<IReceiver>();
            receiver.Initialize(credentials);

            var barcode = new ZXingBarcodeImageView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingBarcodeImageView",
            };
            barcode.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            barcode.BarcodeOptions.Width = 600;
            barcode.BarcodeOptions.Height = 600;
            barcode.BarcodeOptions.Margin = 10;
            barcode.BarcodeValue = string.Format("{0};{1};{2}", credentials.SyncGatewayUrl, credentials.Login, credentials.Password);

            var page = new CodeGeneratorView();
            page.Content = barcode;

            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        private void StartListening()
        {
            
        }
    }
}
