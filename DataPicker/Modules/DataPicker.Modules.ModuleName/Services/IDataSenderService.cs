using System.Net;

namespace DataPicker.Modules.CsvModule.Services;

public interface IDataSenderService
{
    void SendData(byte[] datagram,IPEndPoint remotePoint);
}