using System;
using DataPicker.Modules.CsvModule;
using DataPicker.Modules.CsvModule.Models;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace DataPicker.Modules.CsvModule.ViewModels
{
    public class AppSettingViewModel : BindableBase, IDialogAware
    {
        public AppSettingViewModel()
        {

        }

        public CSV CSV
        {
            get => SettingManager.Settings.CSV;
            set => SetProperty(ref SettingManager.Settings.CSV, value);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

        public string Title { get; }
        public event Action<IDialogResult> RequestClose;
    }
}
