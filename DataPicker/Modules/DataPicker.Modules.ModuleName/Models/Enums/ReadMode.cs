using System.Xml.Serialization;

namespace DataPicker.Modules.CsvModule.Models.Enums;

public enum ReadMode
{
    [XmlEnum("依次读取所有未更新")] LastLine,
    [XmlEnum("读取平均值")] Average
}