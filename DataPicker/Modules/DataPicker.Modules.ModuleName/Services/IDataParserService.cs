using System.Collections.ObjectModel;
using DataPicker.Modules.CsvModule.Models;

namespace DataPicker.Modules.CsvModule.Services;

public interface IDataParserService
{
    byte[] ParseData(ObservableCollection<Column> columns);
}