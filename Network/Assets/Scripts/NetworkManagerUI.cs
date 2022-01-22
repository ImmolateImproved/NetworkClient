using TMPro;
using UnityEngine;

public class NetworkManagerUI : MonoBehaviour
{
    private NetworkManager networkManager;

    public TMP_InputField ipInput;
    public TMP_InputField portInput;

    private void Awake()
    {
        networkManager = SystemsManager.GetSystem<NetworkManager>();
    }

    public void Connect()
    {
        var ip = ipInput.text;
        var port = int.Parse(portInput.text);

        networkManager.Connect(ip, port);
    }
}