using ClientMessages;
using FlatBuffers;
using Main_message;
using ServerMessages;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Systems/NetworkMovementManager")]
public class NetworkMovementManager : SystemBase
{
    private NetworkManager networkManager;

    private NetworkObjectManager networkObjectManager;

    private FlatBufferBuilder fb;

    private uint lastSendMessageId;
    private uint lastReceiveMessageId;

    public override void Init()
    {
        networkManager = SystemsManager.GetSystem<NetworkManager>();
        networkObjectManager = SystemsManager.GetSystem<NetworkObjectManager>();

        fb = new FlatBufferBuilder(1);
    }

    public override void MyOnEnable()
    {
        networkManager.On((int)message_type.Move, ReceiveMovement);
    }

    public override void MyOnDisable()
    {
        networkManager.Off((int)message_type.Move, ReceiveMovement);
    }

    public void SendInput(Vector2 direction, float angle)
    {
        var data = CreateInputMessage(direction, angle, lastSendMessageId);

        lastSendMessageId++;

        networkManager.Send(data);
    }

    private void ReceiveMovement(NetworkMessage networkMessage)
    {
        if (networkMessage.id <= lastReceiveMessageId)
            return;

        lastReceiveMessageId = networkMessage.id;

        var data = networkMessage.data;

        var bufer = new ByteBuffer(data);

        var serverMessage = Message.GetRootAsMessage(bufer);

        for (int i = 0; i < serverMessage.ObjectsLength; i++)
        {
            var localObject = ToLocalObject(serverMessage.Objects(i).Value);

            if (networkObjectManager.TryGetNetObject(localObject.id, out var netOjbect))
            {
                netOjbect.ReceivePosition(localObject.position, localObject.angle);
            }
            else
            {
                networkObjectManager.SpawnNetworkObject(localObject.id, false);
            }
        }
    }

    private LocalPlayer ToLocalObject(Game_Object netObject)
    {
        return new LocalPlayer
        {
            id = netObject.Id,
            position = new Vector2(netObject.X, netObject.Y),
            angle = netObject.Ang
        };
    }

    private byte[] CreateInputMessage(Vector2 direction, float angle, uint lastMessageId)
    {
        var data = SerializeDirection(direction, angle);

        fb.Clear();

        var dataOffset = main.CreateDataVector(fb, data);

        var offset = main.Createmain(fb, lastMessageId, false, message_type.Move, dataOffset);
        main.FinishmainBuffer(fb, offset);

        return fb.SizedByteArray();
    }

    private byte[] SerializeDirection(Vector2 direction, float angle)
    {
        fb.Clear();

        var input = PlayerInput.directionMap[direction];

        var msgOffset = Move.CreateMove(fb, input, angle);
        Move.FinishMoveBuffer(fb, msgOffset);

        var data = fb.SizedByteArray();

        return data;
    }

}