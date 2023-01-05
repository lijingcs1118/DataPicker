using DataPicker.Views;
using Microsoft.Extensions.Logging;
using Prism.Mvvm;

namespace DataPicker.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        private readonly ILogger<MainWindowViewModel> _logger;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public MainWindowViewModel(ILogger<MainWindowViewModel> logger)
        {
            _logger = logger;
            _logger.LogInformation("Hello");
        }
    }
}
