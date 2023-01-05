using System.Collections.ObjectModel;
using DataPicker.Modules.CsvModule.Models;

namespace DataPicker.Modules.CsvModule.Services
{
    public interface IFileReaderService
    {
        ObservableCollection<Column> GetSelectedColumnsValue(string csvFilePath, ObservableCollection<Models.Column> observableColumnCollection);
    }
}