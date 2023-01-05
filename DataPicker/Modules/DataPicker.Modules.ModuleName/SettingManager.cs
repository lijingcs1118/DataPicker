using System;
using System.IO;
using System.Xml.Serialization;
using DataPicker.Modules.CsvModule.Models;

namespace DataPicker.Modules.CsvModule;

public class SettingManager
{
    private const string ApplicationConfigFileName = "CSVPickerConfig.xml";
    private static string ApplicationConfigFilePath => Path.Combine(Environment.CurrentDirectory, ApplicationConfigFileName);

    public static Configuration Settings;
    public static void LoadInitialSettings()
    {
        // 创建 XML serializer 实例
        XmlSerializer serializer = new XmlSerializer(typeof(Configuration));

        // 使用 XML serializer 的 Deserialize 方法读取配置文件，并将其反序列化为 Configuration 类的实例
        using var reader = new StreamReader(ApplicationConfigFilePath);
        Settings = (Configuration)serializer.Deserialize(reader);
    }

    public static void SaveSettings()
    {
        // 创建 XML serializer 实例
        XmlSerializer serializer = new XmlSerializer(typeof(Configuration));

        // 使用 XML serializer 的 Serialize 方法将配置信息序列化为 XML 字符串
        using var writer = new StringWriter();
        serializer.Serialize(writer, Settings);
        var xml = writer.ToString();
        // 在这里使用 xml 字符串保存配置信息
    }
}