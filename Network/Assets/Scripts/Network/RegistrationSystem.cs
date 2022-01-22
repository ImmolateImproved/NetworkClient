using FlatBuffers;
using Join_game;
using Main_message;
using MEC;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Systems/RegistrationSystem")]
public class RegistrationSystem : SystemBase
{
    private NetworkManager networkManager;

    private NetworkObjectManager networkObjectManager;

    private FlatBufferBuilder fb;

    private bool joined;

    private uint joinGameMessageID = 1;

    public override void Init()
    {
        networkManager = SystemsManager.GetSystem<NetworkManager>();
        networkObjectManager = SystemsManager.GetSystem<NetworkObjectManager>();

        fb = new FlatBufferBuilder(1);
    }

    public override void MyOnEnable()
    {
        networkManager.OnConnect += NetworkManager_OnConnect;

        networkManager.On((int)message_type.Register, JoinGameResponse);
    }

    public override void MyOnDisable()
    {
        networkManager.OnConnect -= NetworkManager_OnConnect;

        networkManager.Off((int)message_type.Register, JoinGameResponse);
    }

    private void NetworkManager_OnConnect()
    {
        Debug.Log($"Connect");
        Timing.RunCoroutine(SendJoinGameRequestRoutine());
    }

    private void JoinGameResponse(NetworkMessage message)
    {
        var buffer = new ByteBuffer(message.data);
        var join = Join.GetRootAsJoin(buffer);
        networkManager.NetworkID = join.PlayerId;

        joined = true;

        networkObjectManager.SpawnNetworkObject(join.PlayerId, true);
    }

    private IEnumerator<float> SendJoinGameRequestRoutine()
    {
        var joinGameMessage = CreateJoinGameRequest();

        while (!joined)
        {
            networkManager.Send(joinGameMessage);

            yield return Timing.WaitForSeconds(1);
        }
    }

    private byte[] CreateJoinGameRequest()
    {
        fb.Clear();

        var dataOffset = main.CreateDataVector(fb, new byte[0]);

        var offset = main.Createmain(fb, joinGameMessageID, true, message_type.Register, dataOffset);
        main.FinishmainBuffer(fb, offset);

        var data = fb.SizedByteArray();

        return data;
    }
}