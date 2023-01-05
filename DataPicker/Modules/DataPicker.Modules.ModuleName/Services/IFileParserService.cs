namespace DataPicker.Modules.CsvModule.Services
{
    public interface IFileParserService
    {
        string[] ParseColumns(string filePath);
    }
}