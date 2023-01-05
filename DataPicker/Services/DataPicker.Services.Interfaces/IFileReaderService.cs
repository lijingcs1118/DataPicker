namespace DataPicker.Services.Interfaces
{
    public interface IFileReaderService
    {
        string[] ReadLastLine(string csvFilePath, int[] indices);
    }
}