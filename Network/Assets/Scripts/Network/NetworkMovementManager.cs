using ClientMessages;
using FlatBuffers;
using Main_message;
using messages_shot;
using ServerMessages;
using UnityEngine;

public struct LocalPlayer
{
    public byte id;
    public Vector2 position;
    public float angle;
}

[CreateAssetMenu(menuName = "ScriptableObjects/Systems/NetworkMovementManager")]
public class NetworkMovementManager : SystemBase
{
    private NetworkManager networkManager;

    private NetworkPlayersManager networkPlayerManager;

    private FlatBufferBuilder fb;

    [field: SerializeField]
    public float SendRate { get; private set; }

    private uint lastReceiveMessageId;

    public override void Init()
    {
        networkManager = SystemsManager.GetSystem<NetworkManager>();
        networkPlayerManager = SystemsManager.GetSystem<NetworkPlayersManager>();

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
        var directionData = SerializeDirection(direction, angle);

        var data = networkManager.CreateMainMessage(fb, message_type.Move, directionData, false);

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

            if (networkPlayerManager.TryGetNetPlayer(localObject.id, out var netOjbect))
            {
                netOjbect.ReceivePosition(localObject.position, localObject.angle);
            }
            else
            {
                networkPlayerManager.SpawnNetworkObject(localObject.id, false);
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