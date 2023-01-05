using MediatR;

namespace DataPicker.Modules.CsvModule.Services;

public class FileName: IRequest<string>
{
    public FileName(string message)
    {
        Message = message;
    }

    public string Message { get; }
}