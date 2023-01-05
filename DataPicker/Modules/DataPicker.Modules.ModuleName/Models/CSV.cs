using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using DataPicker.Modules.CsvModule.Models.Enums;
using PropertyTools.DataAnnotations;
using BooleanConverter = DataPicker.Modules.CsvModule.Converter.BooleanConverter;

namespace DataPicker.Modules.CsvModule.Models;

public class CSV : INotifyDataErrorInfo
{
    private readonly Dictionary<string, ValidationResult> errors = new();

    private string ip;

    [XmlIgnore]
    [PropertyTools.DataAnnotations.Browsable(false)]
    public string BasePath { get; private set; }

    [PropertyTools.DataAnnotations.Browsable(false)]
    [XmlIgnore]
    public string Filter { get; private set; }

    public CSV()
    {
        Items = new List<string> {"BOOL", "BYTE", "WORD", "INT", "REAL", "DINT", "DWORD"};
        this.BasePath = Directory.GetCurrentDirectory();
        this.Filter = "Text files (Csv files (*.csv)|*.csv";
    }

    [PropertyTools.DataAnnotations.Browsable(false)]
    [XmlIgnore]
    public List<string> Items { get; set; }

    [PropertyTools.DataAnnotations.Category("文件")]
    [PropertyTools.DataAnnotations.DisplayName("文件路径")]
    [PropertyTools.DataAnnotations.Description("CSV所在文件夹路径")]
    [DirectoryPath]
    [AutoUpdateText]
    public string FilePath { get; set; }

    [PropertyTools.DataAnnotations.DisplayName("模板文件")]
    [PropertyTools.DataAnnotations.Description("CSV文件模板，用来读取文件列头信息")]
    [InputFilePath(".csv")]
    [FilterProperty("Filter")]
    [AutoUpdateText]
    public string FileTemplate { get; set; }

    [PropertyTools.DataAnnotations.DisplayName("IP地址")]
    [DefaultValue("127.0.0.1")]
    [PropertyTools.DataAnnotations.Description("FDAA服务器IP地址")]
    [PropertyTools.DataAnnotations.Category("属性")]
    public string IP
    {
        get => ip;
        set
        {
            ip = value;
            Validate("IP", IsValidIP(ip), "非法的IP地址！");
        }
    }

    [PropertyTools.DataAnnotations.Category("属性")]
    [PropertyTools.DataAnnotations.DisplayName("端口号")]
    [PropertyTools.DataAnnotations.Description("FDAA采集服务UDP接收端口号")]
    public int Port { get; set; }

    [PropertyTools.DataAnnotations.Category("属性")]
    [PropertyTools.DataAnnotations.DisplayName("模块索引号")]
    [PropertyTools.DataAnnotations.Description("模块索引号在UDP通信接口中应保持每个模块唯一，它相当于UDP报文的ID号，用于识别不同的UDP报文。\n允许范围：0 - 65535.")]
    public int ModuleIndex { get; set; }

    [PropertyTools.DataAnnotations.Category("属性")]
    [PropertyTools.DataAnnotations.DisplayName("发送周期")]
    [PropertyTools.DataAnnotations.Description("向FDAA发送数据的周期,最小为100毫秒")]
    public int Sendrate { get; set; }


    [ItemsSourceProperty("Items")]
    [PropertyTools.DataAnnotations.Category("属性")]
    [PropertyTools.DataAnnotations.DisplayName("数据类型")]
    [PropertyTools.DataAnnotations.Description("当前信号的数据类型")]
    public string DataType { get; set; }

    [XmlAttribute("EnableTelCount")]
    [TypeConverter(typeof(BooleanConverter))]
    [PropertyTools.DataAnnotations.DisplayName("报文计数器")]
    [PropertyTools.DataAnnotations.Description("是否开启报文计数器")]
    [PropertyTools.DataAnnotations.Category("属性")]
    public bool EnableTelCount { get; set; }

    [PropertyTools.DataAnnotations.Category("属性")]
    [PropertyTools.DataAnnotations.DisplayName("读取模式")]
    [PropertyTools.DataAnnotations.Description("依次读取所有未更新还是读取所有未更新平均值")]
    public ReadMode ReadMode { get; set; }

    [PropertyTools.DataAnnotations.Browsable(false)]
    public string SelectedColumnOutput { get; set; }

    [PropertyTools.DataAnnotations.Browsable(false)]
    public Column[] Columns;

    public IEnumerable GetErrors(string propertyName)
    {
        if (errors.ContainsKey(propertyName)) yield return errors[propertyName];
    }

    [PropertyTools.DataAnnotations.Browsable(false)]
    public bool HasErrors => errors.Count > 0;

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    private void Validate(string propertyName, bool isValid, string message)
    {
        if (!isValid == errors.ContainsKey(propertyName)) return;

        if (!isValid)
            errors.Add(propertyName, new ValidationResult(message));
        else
            errors.Remove(propertyName);

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    public static bool IsValidIP(string ip)
    {
        if (Regex.IsMatch(ip, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}"))
        {
            var ips = ip.Split('.');
            if (ips.Length == 4 || ips.Length == 6)
                try
                {
                    var condition = int.Parse(ips[0]) < 256 && (int.Parse(ips[1]) < 256) & (int.Parse(ips[2]) < 256) &
                        (int.Parse(ips[3]) < 256);
                    if (condition)
                        return true;
                    return false;
                }
                catch (Exception e)
                {
                    return false;
                }

            return false;
        }

        return false;
    }
}