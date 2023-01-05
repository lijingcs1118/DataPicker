using System.Threading;
using System.Threading.Tasks;
using DataPicker.Modules.CsvModule.ViewModels;
using MediatR;

namespace DataPicker.Modules.CsvModule.Services;

public class FileNameHandler: IRequestHandler<FileName,string>
{
    private readonly HomeViewModel _homeViewModel;

    public FileNameHandler(HomeViewModel homeViewModel)
    {
        _homeViewModel = homeViewModel;
    }
    public Task<string> Handle(FileName request, CancellationToken cancellationToken)
    {
        _homeViewModel.FilePath = request.Message;
        return Task.FromResult(request.Message);
    }
}