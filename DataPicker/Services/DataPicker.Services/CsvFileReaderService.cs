using System.IO;
using System.Linq;
using DataPicker.Services.Interfaces;

namespace DataPicker.Services
{
    public class CsvFileReaderService :IFileReaderService
    {
        public string[] ReadLastLine(string csvFilePath, int[] indices)
        {
            string[] lines = File.ReadLines(csvFilePath).ToArray();
            if (lines.Length <= 1) 
                return new string[] { };
            string[] lastLine = lines[^1].Split(',');
            string[] selectedColumnsData = indices.Select(index => lastLine[index]).ToArray();
            return selectedColumnsData;
        }

        //public Co
    }
}