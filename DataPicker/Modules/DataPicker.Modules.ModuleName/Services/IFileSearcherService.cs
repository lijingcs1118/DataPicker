using System.IO;

namespace DataPicker.Modules.CsvModule.Services
{
    public interface IFileSearcherService
    {
        string FindLatestCsv(string directory);
    }
}