using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Systems/NetworkPlayersManager")]
public class NetworkPlayersManager : SystemBase
{
    [SerializeField]
    private NetworkPlayer playerPrefab;

    [SerializeField]
    private Color[] colors;

    private Dictionary<byte, NetworkPlayer> idToPlayerMap;

    public override void Init()
    {
        idToPlayerMap = new Dictionary<byte, NetworkPlayer>();
    }

    public void SpawnNetworkObject(byte id, bool isMine)
    {
        if (idToPlayerMap.ContainsKey(id))
        {
            Debug.Log($"Player {id} already joined");
            return;
        }

        Debug.Log($"Player {id} joined");

        var netPlayer = Instantiate(playerPrefab);

        var colorIndex = (id + 1) % colors.Length;

        netPlayer.Init(id, colors[colorIndex], isMine);

        idToPlayerMap.Add(id, netPlayer);
    }

    public bool TryGetNetPlayer(byte id, out NetworkPlayer networkPlayer)
    {
        return idToPlayerMap.TryGetValue(id, out networkPlayer);
    }
}