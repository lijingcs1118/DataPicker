using MediatR;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DataPicker.Modules.CsvModule.Services
{
    public class CsvFileSearcherService:IFileSearcherService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CsvFileSearcherService> _logger;

        public CsvFileSearcherService(IMediator mediator, ILogger<CsvFileSearcherService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        //public string FindLatestCsv(string directory, int maxRecursionDepth = 10)
        //{
        //    _logger.LogInformation("Start search latest csv file.");
        //    // 缓存已查找的文件夹
        //    var searchedFolders = new ConcurrentBag<string>();

        //    FileInfo latestCsv = null;

        //    void Search(string folder, int depth)
        //    {
        //        // 如果超过了递归深度限制，则停止搜索
        //        if (depth > maxRecursionDepth) return;

        //        // 如果已经查找过该文件夹，则停止搜索
        //        if (searchedFolders.Contains(folder)) return;
        //        searchedFolders.Add(folder);

        //        // 获取文件夹下的所有文件和文件夹
        //        var filesAndFolders = Directory.GetFileSystemEntries(folder);

        //        // 遍历所有文件和文件夹
        //        Parallel.ForEach(filesAndFolders, fileOrFolder =>
        //        {
        //            // 如果是文件夹，则递归查找
        //            if (Directory.Exists(fileOrFolder))
        //            {
        //                Search(fileOrFolder, depth + 1);
        //            }
        //            // 如果是 CSV 文件，则更新 latestCSV
        //            else if (Path.GetExtension(fileOrFolder) == ".csv")
        //            {
        //                var csv = new FileInfo(fileOrFolder);
        //                if (latestCsv == null || csv.LastWriteTime > latestCsv.LastWriteTime)
        //                {
        //                    latestCsv = csv;
        //                }
        //            }
        //        });
        //    }

        //    // 从给定文件夹开始搜索
        //    Search(directory, 0);

        //    _logger.LogInformation("Finish search latest csv file : {0}", latestCsv?.FullName);
        //    _mediator.Send(new FileName(latestCsv?.FullName));
        //    return latestCsv?.FullName; 
        //}

        public string FindLatestCsv(string directory)
        {
            _logger.LogInformation("Start search latest csv file.");
            var directoryInfo = new DirectoryInfo(directory).GetDirectories("*",
                SearchOption.AllDirectories).OrderByDescending(d => d.LastWriteTimeUtc).First();

            var latestFileName = directoryInfo.GetFiles("*.csv*").OrderByDescending(file => file.CreationTime).First().FullName;
            _mediator.Send(new FileName(latestFileName));
            _logger.LogInformation("Finish search latest csv file : {0}", latestFileName);
            return latestFileName;
        }
    }
}