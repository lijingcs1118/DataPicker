namespace DataPicker.Core;

public class PickerHelper
{
    public static int GetDataTypeLength(string dataType)
    {
        var length = dataType switch
        {
            "BOOL" => 1,
            "BYTE" => 8,
            "WORD" => 16,
            "DWORD" => 32,
            "INT" => 16,
            "DINT" => 32,
            "REAL" => 32,
            _ => 0
        };
        return length / 8;
    }
}