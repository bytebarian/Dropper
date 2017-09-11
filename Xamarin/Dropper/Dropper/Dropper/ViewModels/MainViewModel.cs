using Dropper.Models;
using Dropper.Services;
using Dropper.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace Dropper.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private RelayCommand _syncCommand;
        private RelayCommand _generateCodeCommand;
        private RelayCommand _pickFileCommand;
        private string _dropName;
        private IDatabaseController _databaseController;
        private IFileService _fileService;

        private List<FileModel> _files;

        public MainViewModel()
        {
            _databaseController = DependencyService.Get<IDatabaseController>();
            _fileService = new FileService();
            _files = new List<FileModel>();
        }

        public RelayCommand SyncCommand
        {
            get { return _syncCommand ?? (_syncCommand = new RelayCommand(async (args) => { await ScanCode(); })); }
        }

        public RelayCommand GenerateCodeCommand
        {
            get { return _generateCodeCommand ?? (_generateCodeCommand = new RelayCommand(async (args) => { await GenerateCode(); })); }
        }

        public RelayCommand PickFileCommand
        {
            get { return _pickFileCommand ?? (_pickFileCommand = new RelayCommand(async (args) => { await PickFile(); })); }
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

        private async Task GenerateCode()
        {
            string ipaddress = DependencyService.Get<IIPAddressService>().GetIPAddress();

            if (string.IsNullOrEmpty(ipaddress)
                || string.IsNullOrEmpty(_dropName)
                || !_files.Any()) return;

            var credentials = new Credentials();
            credentials.Login = Guid.NewGuid().ToString();
            credentials.Password = Guid.NewGuid().ToString();
            credentials.Port = 5432;
            credentials.SyncGatewayUrl = string.Format("http://{0}:{1}/{2}", ipaddress, credentials.Port, _dropName);

            var receiver = DependencyService.Get<IReceiver>();
            receiver.Initialize(credentials);
            _databaseController.DatabaseChanged += async (sender, args) => await DatabaseChanged(args);

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

        private async Task DatabaseChanged(DatabaseChangedEventArgs e)
        {
            var file = await _databaseController.GetDoc(e.DocumentId);
            await _fileService.SaveFileAsync(file);
            _files.Add(file);
        }

        private async Task Save()
        {
            if (string.IsNullOrEmpty(_dropName)) return;
            await _databaseController.Init(_dropName);
            foreach (var file in _files)
            {
                await _databaseController.Add(file);
            }
        }

        private async Task PickFile()
        {
            var file = await _fileService.PickFileAsync();
            _files.Add(file);
        }
    }
}
