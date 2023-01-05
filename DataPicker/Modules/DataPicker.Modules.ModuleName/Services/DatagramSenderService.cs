using System;
using System.Net;
using System.Net.Sockets;

namespace DataPicker.Modules.CsvModule.Services;

public class DatagramSenderService:IDataSenderService
{
    private readonly UdpClient _client;

    public DatagramSenderService()
    {
        _client = new UdpClient();
    }

    public void SendData(byte[] datagram, IPEndPoint remotePoint)
    {
        _client.Send(datagram, datagram.Length, remotePoint);
    }
}