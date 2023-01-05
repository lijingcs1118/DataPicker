using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DataPicker.Services.Interfaces;

namespace DataPicker.Services
{
    public class CsvFileSearcherService:IFileSearcherService
    {
        public FileInfo FindLatestCsv(string directory, int maxRecursionDepth = 10)
        {
            // 缓存已查找的文件夹
            var searchedFolders = new HashSet<string>();

            FileInfo latestCsv = null;

            void Search(string folder, int depth)
            {
                // 如果超过了递归深度限制，则停止搜索
                if (depth > maxRecursionDepth) return;

                // 如果已经查找过该文件夹，则停止搜索
                if (searchedFolders.Contains(folder)) return;
                searchedFolders.Add(folder);

                // 获取文件夹下的所有文件和文件夹
                var filesAndFolders = Directory.GetFileSystemEntries(folder);

                // 遍历所有文件和文件夹
                Parallel.ForEach(filesAndFolders, fileOrFolder =>
                {
                    // 如果是文件夹，则递归查找
                    if (Directory.Exists(fileOrFolder))
                    {
                        Search(fileOrFolder, depth + 1);
                    }
                    // 如果是 CSV 文件，则更新 latestCSV
                    else if (Path.GetExtension(fileOrFolder) == ".csv")
                    {
                        var csv = new FileInfo(fileOrFolder);
                        if (latestCsv == null || csv.LastWriteTime > latestCsv.LastWriteTime) latestCsv = csv;
                    }
                });
            }

            // 从给定文件夹开始搜索
            Search(directory, 0);

            return latestCsv;
        }
    }
}