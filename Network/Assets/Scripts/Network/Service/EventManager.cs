using System;
using System.Collections.Generic;

public struct NetworkMessage
{
    public uint id;
    public bool isRequire;

    public byte[] data;
}

public class EventManager
{
    private Dictionary<int, Action<NetworkMessage>> events = new Dictionary<int, Action<NetworkMessage>>();
    
    public void Process(int type, NetworkMessage message)
    {
        if (events.TryGetValue(type, out var action))
        {
            action.Invoke(message);
        }
    }

    public void On(int type, Action<NetworkMessage> action)
    {
        if (events.TryGetValue(type, out var act))
        {
            act += action;
        }
        else
        {
            events.Add(type, action);
        }
    }

    public void Off(int type, Action<NetworkMessage> action)
    {
        if (events.TryGetValue(type, out var act))
        {
            act -= action;
        }
    }
}