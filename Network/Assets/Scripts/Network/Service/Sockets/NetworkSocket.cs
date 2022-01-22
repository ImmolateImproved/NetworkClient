using System;
using System.Collections.Concurrent;
using UnityEngine;

public abstract class NetworkSocket : ScriptableObject
{
    public string ip;
    public int port;

    protected ConcurrentQueue<byte[]> networkMessages = new ConcurrentQueue<byte[]>();

    public void Connect(string ip, int port)
    {
        this.ip = ip;
        this.port = port;

        Connect();
    }
    public abstract void Connect();
    public abstract void Send(byte[] data);
    public abstract void Close();

    public bool TryGetMessage(out byte[] data)
    {
        return networkMessages.TryDequeue(out data);
    }
}