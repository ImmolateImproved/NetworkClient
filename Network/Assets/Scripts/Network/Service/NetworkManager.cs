using FlatBuffers;
using Main_message;
using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Systems/NetworkManager")]
public class NetworkManager : SystemBase
{
    [SerializeField]
    private NetworkSocket socket;

    private EventManager eventManager;

    private CoroutineHandle receiveMessageHandle;

    public event Action OnConnect;

    public uint LastSendMessageId { get; private set; }

    public byte NetworkID { get; set; }

    public override void Init()
    {
        eventManager = new EventManager();
    }

    public override void MyOnEnable()
    {
        receiveMessageHandle = Timing.RunCoroutine(Receive());
    }

    public override void MyOnDisable()
    {
        Timing.KillCoroutines(receiveMessageHandle);
        socket?.Close();
    }

    public override void MyOnDestroy()
    {
        socket = null;
    }

    private IEnumerator<float> Receive()
    {
        while (true)
        {
            if (socket.TryGetMessage(out var data))
            {
                var bufer = new ByteBuffer(data);

                var mainMessage = main.GetRootAsmain(bufer);

                var type = (byte)mainMessage.MesType;
                data = mainMessage.GetDataArray();

                var networkMessage = new NetworkMessage { data = data, id = mainMessage.MessageId, isRequire = mainMessage.IsReq };
                eventManager.Process(type, networkMessage);
            }

            yield return Timing.WaitForOneFrame;
        }
    }

    public void Connect(string ip, int port)
    {
        socket.Connect(ip, port);

        OnConnect?.Invoke();
    }

    public void Send(byte[] data)
    {
        LastSendMessageId++;
        socket.Send(data);
    }

    public void On(int type, Action<NetworkMessage> action)
    {
        eventManager.On(type, action);
    }

    public void Off(int type, Action<NetworkMessage> action)
    {
        eventManager.Off(type, action);
    }

    public byte[] CreateMainMessage(FlatBufferBuilder fb, message_type message_Type, byte[] data, bool isRequire)
    {
        fb.Clear();

        var dataOffset = main.CreateDataVector(fb, data);

        var offset = main.Createmain(fb, LastSendMessageId, isRequire, message_Type, dataOffset);
        main.FinishmainBuffer(fb, offset);

        return fb.SizedByteArray();
    }
}