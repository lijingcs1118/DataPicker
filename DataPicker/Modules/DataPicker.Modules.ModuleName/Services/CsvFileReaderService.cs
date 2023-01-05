using System.Collections.Generic;
using DataPicker.Modules.CsvModule.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace DataPicker.Modules.CsvModule.Services;

public class CsvFileReaderService : IFileReaderService
{
    private string[] _values;
    private string _oldCsvFilePath;
    private int _lineNumber;

    public ObservableCollection<Column> GetSelectedColumnsValue(string csvFilePath, ObservableCollection<Column> observableColumnCollection)
    {
        ReadCurrentLine(csvFilePath);
        return MakeDataFormat(observableColumnCollection);
    }
    public void ReadCurrentLine(string currentCsvFilePath)
    {
        if (currentCsvFilePath != _oldCsvFilePath)
        {
            _lineNumber = 2;
            _oldCsvFilePath = currentCsvFilePath;
        }
            
        using var stream = new FileStream(currentCsvFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var reader = new StreamReader(stream);
        string currentLine = "";
        int lineNumber = 0;
        while (reader.ReadLine() is { } line)
        {
            lineNumber++;
            if (lineNumber == _lineNumber)
            {
                currentLine = line;
                _lineNumber++;
                break;
            }
        }
        if (string.IsNullOrEmpty(currentLine)) return;

        _values = currentLine.Split(',');
    }

    private ObservableCollection<Column> MakeDataFormat(ObservableCollection<Column> observableColumnCollection)
    {
        //var columns = new ObservableCollection<Column>(SettingManager.Settings.CSV.Columns);
        var columns = new ObservableCollection<Column>(observableColumnCollection.Select(column => new Column
        {
            DataType = column.DataType,
            No = column.No,
            Offset = column.Offset,
            SignalIndex = column.SignalIndex,
            SignalName = column.SignalName,
            StringLength = column.StringLength,
            Value = _values[column.No]
        }));
        return columns;
    }
}