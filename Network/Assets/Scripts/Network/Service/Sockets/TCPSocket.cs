using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System;
using UnityEngine;

[Serializable]
public struct UrlData
{
    public string ip;
    public int port;
}

[CreateAssetMenu(menuName = "ScriptableObjects/Network/TCPSocket")]
public class TCPSocket : NetworkSocket
{
    private TcpClient connection;
    private NetworkStream stream;

    public override async void Connect()
    {
        connection = new TcpClient();

        connection.Connect(ip, port);
        stream = connection.GetStream();

        await Task.Run(() => ReceiveMessage());
    }

    public override void Close()
    {
        if (stream != null)
            stream.Close();
        if (connection != null)
            connection.Close();
    }

    public override void Send(byte[] data)
    {
        stream.Write(data, 0, data.Length);
    }

    private void ReceiveMessage()
    {
        while (true)
        {
            var data = new byte[64];
            do
            {
                stream.Read(data, 0, data.Length);
            }
            while (stream.DataAvailable);

            networkMessages.Enqueue(data);
        }
    }
}