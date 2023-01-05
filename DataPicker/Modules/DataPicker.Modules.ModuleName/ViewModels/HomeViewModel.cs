using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DataPicker.Core;
using DataPicker.Core.Mvvm;
using DataPicker.Modules.CsvModule.Models;
using DataPicker.Modules.CsvModule.Services;
using GongSolutions.Wpf.DragDrop;
using ImTools;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using IFileParserService = DataPicker.Modules.CsvModule.Services.IFileParserService;
using IFileReaderService = DataPicker.Modules.CsvModule.Services.IFileReaderService;
using IFileSearcherService = DataPicker.Modules.CsvModule.Services.IFileSearcherService;

namespace DataPicker.Modules.CsvModule.ViewModels;

public class HomeViewModel : RegionViewModelBase, IDropTarget
{
    public struct Column
    {
        public int Index { get; set; }
        public string Value { get; set; }
    }

    private readonly IDialogService _dialogService;
    private readonly IFileSearcherService _fileSearcherService;
    private readonly IFileReaderService _fileReaderService;
    private readonly IDataParserService _dataParserService;
    private readonly IDataSenderService _dataSenderService;


    private ObservableCollection<Column> _columns;
    public ObservableCollection<Column> Columns
    {
        get => _columns;
        set  => SetProperty(ref _columns, value);
    }

    private ObservableCollection<Models.Column> _dataFormatsList;
    public ObservableCollection<Models.Column> DataFormatsList
    {
        get => _dataFormatsList;
        set => SetProperty(ref _dataFormatsList, value);
    }

    private string _filePath;
    public string FilePath
    {
        get => _filePath;
        set => SetProperty(ref _filePath, value);
    }
    
    private bool _isEnabled = true;
    public bool IsEnabled
    {
        get => _isEnabled;
        set => SetProperty(ref _isEnabled, value);
    }
    
    private bool _isReadOnly = false;
    public bool IsReadOnly
    {
        get => _isReadOnly;
        set => SetProperty(ref _isReadOnly, value);
    }
    
    private bool _isRunning;
    public bool IsRunning
    {
        get => _isRunning;
        set => SetProperty(ref _isRunning, value);
    }

    public HomeViewModel(
        IRegionManager regionManager, 
        IDialogService dialogService,
        IFileParserService fileParserService, 
        IFileSearcherService fileSearcherService, 
        IFileReaderService fileReaderService, 
        IDataParserService dataParserService, 
        IDataSenderService dataSenderService) :
        base(regionManager)
    {
        _dialogService = dialogService;
        _fileSearcherService = fileSearcherService;
        _fileReaderService = fileReaderService;
        _dataParserService = dataParserService;
        _dataSenderService = dataSenderService;

        string[] columns = fileParserService.ParseColumns(SettingManager.Settings.CSV.FileTemplate);
        _columns = new ObservableCollection<Column>(columns.Select((s, index) => new Column { Index = index, Value = s })) ;
        _dataFormatsList = new ObservableCollection<Models.Column>(SettingManager.Settings.CSV.Columns);
    }

    public override void OnNavigatedTo(NavigationContext navigationContext)
    {
        //do something
    }

    private DelegateCommand _showDialogCommand;
    public DelegateCommand ShowDialogCommand =>
        _showDialogCommand ??= new DelegateCommand(ShowDialog);

    private void ShowDialog()
    {
        _dialogService.ShowDialog("AppSettingView");
    }

    private DelegateCommand<Models.Column> _deleteCommand;
    public DelegateCommand<Models.Column> DeleteCommand =>
        _deleteCommand ??= new DelegateCommand<Models.Column>(RemoveRow);


    private void RemoveRow(Models.Column column)
    {
        DataFormatsList.Remove(column);
    }

    private DelegateCommand _startStopCommand;
    public DelegateCommand StartStopCommand =>
        _startStopCommand ??= new DelegateCommand(ExecuteStartStop);

    private async void ExecuteStartStop()
    {
        IsEnabled = !IsEnabled;
        IsReadOnly = !IsReadOnly;
        IsRunning = !IsRunning;
        await Start();
    }

    private async Task Start()
    {
        while (IsRunning)
        {
            string latestCsvName = _fileSearcherService.FindLatestCsv(SettingManager.Settings.CSV.FilePath);
            DataFormatsList = _fileReaderService.GetSelectedColumnsValue(latestCsvName, _dataFormatsList);
            byte[] datagram = _dataParserService.ParseData(DataFormatsList);
            _dataSenderService.SendData(datagram, GetRemotePoint());

            await Task.Delay(1000);
        }
    }

    private IPEndPoint GetRemotePoint()
    {
        IPAddress remoteIp = IPAddress.Parse(SettingManager.Settings.CSV.IP);
        int remotePort = SettingManager.Settings.CSV.Port;
        IPEndPoint remotePoint = new IPEndPoint(remoteIp, remotePort);
        return remotePoint;
    }

    public void DragOver(IDropInfo dropInfo)
    {
        dropInfo.Effects = DragDropEffects.Move;
        dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
    }

    public void Drop(IDropInfo dropInfo)
    {
        if (dropInfo.Data is Column column)
        {
            var dataFormat = new Models.Column
            {
                DataType = SettingManager.Settings.CSV.DataType,
                No = column.Index,
                SignalName = column.Value
            };
            dropInfo.Data = dataFormat;
            
        }

        if (dropInfo.Data is IEnumerable columnList)
        {
            var dataFormatList = (from Column c in columnList
                select new Models.Column
                    {DataType = SettingManager.Settings.CSV.DataType, No = c.Index, SignalName = c.Value}).ToList();
            dropInfo.Data = dataFormatList;
        }

        GongSolutions.Wpf.DragDrop.DragDrop.DefaultDropHandler.Drop(dropInfo);
        for (var i = 0; i < DataFormatsList.Count; i++)
        {
            DataFormatsList[i].SignalIndex = i;
            var previousOffset = i == 0 ? 8 : DataFormatsList[i - 1].Offset;
            var previousDataTypeLength = i == 0 ? 0 : PickerHelper.GetDataTypeLength(DataFormatsList[i - 1].DataType);
            DataFormatsList[i].Offset = previousOffset + previousDataTypeLength;
        }
    }
}