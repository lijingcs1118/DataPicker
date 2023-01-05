using System;
using System.Collections.ObjectModel;
using System.Linq;
using DataPicker.Core;
using DataPicker.Modules.CsvModule.Models;

namespace DataPicker.Modules.CsvModule.Services;

public class DataParserService:IDataParserService
{
    private short _datagramLength;
    private static int _datagramCounter;

    public byte[] ParseData(ObservableCollection<Column> columns)
    {
        //  计算报文总长度
        _datagramLength = (short) (columns[^1].Offset + PickerHelper.GetDataTypeLength(columns[^1].DataType));
        //  初始化返回结果
        byte[] datagram = new byte[_datagramLength];
        //  拼接报文头
        SetDatagramHeader(datagram, columns);
        //  拼接报文体
        SetDatagramBody(datagram, columns);
        return datagram;
    }

    private void SetDatagramHeader(byte[] datagram, ObservableCollection<Column> columns)
    {
        //表示偏移量
        int offset = 0;

        //moduleIndex将参数转化为字节流（short转字节流）
        byte[] moduleIndexBytes = BitConverter.GetBytes((short)SettingManager.Settings.CSV.ModuleIndex);
        //1字节流拷贝(源字节流，从第0个开始拷贝，目标字节流，拷贝到目标字节流第offset个，拷贝长度为原字节流长度)
        Buffer.BlockCopy(moduleIndexBytes, 0, datagram, offset, moduleIndexBytes.Length);
        offset += (short)moduleIndexBytes.Length;

        //将长度参数转化为字节流            
        byte[] lengthBytes = BitConverter.GetBytes(_datagramLength);
        //2报文长度拷贝
        Buffer.BlockCopy(lengthBytes, 0, datagram, offset, lengthBytes.Length);
        offset += (short)lengthBytes.Length;

        //将报文计数转化为字节流
        var countBytes = SettingManager.Settings.CSV.EnableTelCount ? BitConverter.GetBytes(_datagramCounter++) : BitConverter.GetBytes(-1);
        //3字节流拷贝
        Buffer.BlockCopy(countBytes, 0, datagram, offset, countBytes.Length);
    }

    private void SetDatagramBody(byte[] datagram, ObservableCollection<Column> columns)
    {
        int offset = 8;
        decimal parsedValue;
        foreach (var column in columns)
        {
            decimal.TryParse(column.Value, out parsedValue);
            byte[] valueBytes;
            switch (column.DataType)
            {
                case "REAL":
                    float realValue = Convert.ToSingle(parsedValue);
                    valueBytes = BitConverter.GetBytes(realValue);
                    Buffer.BlockCopy(valueBytes, 0, datagram, offset, valueBytes.Length);
                    offset += valueBytes.Length;
                    break;
                case "DWORD":
                case "DINT":
                    int dwordValue = Convert.ToInt32(parsedValue);
                    valueBytes = BitConverter.GetBytes(dwordValue);
                    Buffer.BlockCopy(valueBytes, 0, datagram, offset, valueBytes.Length);
                    offset += valueBytes.Length;
                    break;
                case "WORD":
                case "INT":
                    short wordValue = Convert.ToInt16(parsedValue);
                    valueBytes = BitConverter.GetBytes(wordValue);
                    Buffer.BlockCopy(valueBytes, 0, datagram, offset, valueBytes.Length);
                    offset += valueBytes.Length;
                    break;
                case "BYTE":
                    short byteValue = Convert.ToInt16(parsedValue);
                    valueBytes = BitConverter.GetBytes(byteValue);
                    Buffer.BlockCopy(valueBytes, 0, datagram, offset, valueBytes.Length);
                    offset += valueBytes.Length;
                    break;
            }
        }
    }
}