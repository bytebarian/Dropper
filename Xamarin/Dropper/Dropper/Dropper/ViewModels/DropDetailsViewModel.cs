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

        public RelayCommand GenerateCodeCommand
        {
            get { return _generateCodeCommand ?? (_generateCodeCommand = new RelayCommand(async (args) => { await GenerateCode(); })); }
        }

        private async Task GenerateCode()
        {
            var credentials = new Credentials();
            credentials.Login = Guid.NewGuid().ToString();
            credentials.Password = Guid.NewGuid().ToString();

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
            barcode.BarcodeValue = string.Format("{0};{1}", credentials.Login, credentials.Password);

            var page = new CodeGeneratorView();
            page.Content = barcode;

            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
