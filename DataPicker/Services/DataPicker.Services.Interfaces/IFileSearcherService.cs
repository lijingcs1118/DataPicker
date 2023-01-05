using System.IO;

namespace DataPicker.Services.Interfaces
{
    public interface IFileSearcherService
    {
        FileInfo FindLatestCsv(string directory, int maxRecursionDepth = 10);
    }
}