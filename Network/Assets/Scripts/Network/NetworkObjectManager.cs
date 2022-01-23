using System.Collections.Generic;
using UnityEngine;

public struct LocalPlayer
{
    public byte id;
    public Vector2 position;
    public float angle;
}

[CreateAssetMenu(menuName = "ScriptableObjects/Systems/NetworkObjectManager")]
public class NetworkObjectManager : SystemBase
{
    [SerializeField]
    private NetworkPlayer playerPrefab;

    [SerializeField]
    private Color[] colors;

    private Dictionary<byte, NetworkPlayer> players;

    public override void Init()
    {
        players = new Dictionary<byte, NetworkPlayer>();
    }

    public bool TryGetNetObject(byte id, out NetworkPlayer networkPlayer)
    {
        return players.TryGetValue(id, out networkPlayer);
    }

    public void SpawnNetworkObject(byte id, bool isMine)
    {
        if (players.ContainsKey(id))
        {
            Debug.Log($"Player {id} already joined");
            return;
        }

        Debug.Log($"Player {id} joined");

        var localPlayer = Instantiate(playerPrefab);

        var colorIndex = (id + 1) % colors.Length;

        localPlayer.Init(id, colors[colorIndex], isMine);

        players.Add(id, localPlayer);
    }
}