using System.Net.Sockets;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Network/UDPSocket")]
public class UDPSocket : NetworkSocket
{
    private UdpClient connection;

    public override void Connect()
    {
        connection = new UdpClient(ip, port);
        Receive();
    }

    public override void Close()
    {
        connection?.Close();
        connection?.Dispose();
    }

    public override void Send(byte[] data)
    {
        connection.Send(data, data.Length);
    }

    private async void Receive()
    {
        while (true)
        {
            var result = await connection.ReceiveAsync();

            var receiveBytes = result.Buffer;

            networkMessages.Enqueue(receiveBytes);
        }
    }
}