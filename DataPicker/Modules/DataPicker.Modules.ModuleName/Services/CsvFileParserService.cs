using System;
using System.IO;
using System.Linq;

namespace DataPicker.Modules.CsvModule.Services
{
    public class CsvFileParserService:IFileParserService
    {
        public string[] ParseColumns(string filePath)
        {
            // 读取 CSV 文件的第一行，这应该包含列名
            var header = File.ReadLines(filePath).First() ?? throw new ArgumentNullException("空文件，无法识别列头");
            string[] columnNames = header.Split(',');

            // 返回列名数组
            return columnNames;
        }
    }
}