namespace DataPicker.Services.Interfaces
{
    public interface IFileParserService
    {
        string[] ParseColumns(string filePath);
    }
}