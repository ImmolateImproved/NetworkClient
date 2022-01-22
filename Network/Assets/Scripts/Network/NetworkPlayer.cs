using System.Collections;
using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    private NetworkObjectManager networkObjectManager;

    private PlayerInput playerInput;
    private Movement movement;

    private int id;

    [SerializeField]
    private float sendRate;

    private WaitForSeconds sendRateHandel;

    private void Awake()
    {
        networkObjectManager = SystemsManager.GetSystem<NetworkObjectManager>();

        playerInput = GetComponent<PlayerInput>();
        movement = GetComponent<Movement>();

        sendRateHandel = new WaitForSeconds(1 / sendRate);

        StartCoroutine(SendInputRoutine());
    }

    public void Init(int id, bool isMine)
    {
        this.id = id;

        playerInput.enabled = isMine;

        if (isMine)
            Camera.main.transform.SetParent(transform);
    }

    public void ReceivePosition(Vector2 position)
    {
        movement.Move(position);
    }

    private IEnumerator SendInputRoutine()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            var direction = playerInput.GetInput();

            movement.SetVelocity(direction);

            networkObjectManager.SendInput(direction);

            yield return sendRateHandel;
        }
    }
}