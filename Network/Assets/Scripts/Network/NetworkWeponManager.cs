using FlatBuffers;
using Main_message;
using messages_shots;
using UnityEngine;

public struct LocalShot
{
    public byte playerID;

    public Vector2 targetPosition;
    public weapon_type weaponType;
}

[CreateAssetMenu(menuName = "ScriptableObjects/Systems/NetworkWeponManager")]
public class NetworkWeponManager : SystemBase
{
    private NetworkManager networkManager;

    private NetworkPlayersManager networkPlayerManager;

    private FlatBufferBuilder fb;

    private uint answerID;

    public override void Init()
    {
        networkManager = SystemsManager.GetSystem<NetworkManager>();
        networkPlayerManager = SystemsManager.GetSystem<NetworkPlayersManager>();

        fb = new FlatBufferBuilder(1);
    }

    public override void MyOnEnable()
    {
        networkManager.On((int)message_type.Answer, ReceiveShotAnswer);
    }

    public override void MyOnDisable()
    {
        networkManager.Off((int)message_type.Answer, ReceiveShotAnswer);
    }

    public void SendShotEvent(Vector2 targetPosition)
    {
        answerID = networkManager.LastSendMessageId;

        var shootMessage = SerializeShotMessage(messages_shot.weapon_type.laser, targetPosition);

        var data = networkManager.CreateMainMessage(fb, message_type.Shot, shootMessage, true);

        networkManager.Send(data);
    }

    private void ReceiveShotAnswer(NetworkMessage networkMessage)
    {
        if (answerID != networkMessage.id)
            return;

        var data = networkMessage.data;

        var bufer = new ByteBuffer(data);

        var serverMessage = Shots.GetRootAsShots(bufer);

        for (int i = 0; i < serverMessage.ObjectsLength; i++)
        {
            var shot = ToLocalShot(serverMessage.Objects(i).Value);

            if (networkPlayerManager.TryGetNetPlayer(shot.playerID, out var netPlayer))
            {
                netPlayer.Shot(shot.targetPosition);
            }
        }
    }

    private byte[] SerializeShotMessage(messages_shot.weapon_type weapon_Type, Vector2 targetPosition)
    {
        fb.Clear();

        var offset = messages_shot.Shot.CreateShot(fb, weapon_Type, targetPosition.x, targetPosition.y);
        messages_shot.Shot.FinishShotBuffer(fb, offset);

        var data = fb.SizedByteArray();

        return data;
    }

    private LocalShot ToLocalShot(Shot shot)
    {
        return new LocalShot
        {
            playerID = shot.PId,
            targetPosition = new Vector2(shot.X, shot.Y),
            weaponType = shot.WType
        };
    }
}