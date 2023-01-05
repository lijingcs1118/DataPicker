using DataPicker.Core.Mvvm;
using System.Xml.Serialization;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace DataPicker.Modules.CsvModule.Models;

public class Column: ViewModelBase
{
    private int _no;
    public int No
    {
        get => _no;
        set => SetProperty(ref _no, value);
    }

    private string _dataType;
    public string DataType
    {
        get => _dataType;
        set => SetProperty(ref _dataType, value);
    }

    private int _signalIndex;
    public int SignalIndex
    {
        get => _signalIndex;
        set => SetProperty(ref _signalIndex, value);
    }

    private string _signalName;
    public string SignalName
    {
        get => _signalName;
        set => SetProperty(ref _signalName, value);
    }

    private int _offset;
    public int Offset
    {
        get => _offset;
        set => SetProperty(ref _offset, value);
    }

    //public int Offset { get; set; }
    public int StringLength { get; set; }

    [XmlIgnore]
    public string Value { get; set; }

}