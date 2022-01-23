using Cinemachine;
using System.Collections;
using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    private NetworkObjectManager networkObjectManager;

    private PlayerInput playerInput;
    private Movement movement;

    private int id;

    private bool isMine;

    [SerializeField]
    private float sendRate;

    private WaitForSeconds sendRateHandel;

    private Coroutine receiveMovementCoroutine;

    [SerializeField]
    private GameObject crossHair;

    private void Awake()
    {
        networkObjectManager = SystemsManager.GetSystem<NetworkObjectManager>();

        playerInput = GetComponent<PlayerInput>();
        movement = GetComponent<Movement>();

        sendRateHandel = new WaitForSeconds(1 / sendRate);

        receiveMovementCoroutine = StartCoroutine(SendInputRoutine());
    }

    public void Init(int id, bool isMine)
    {
        this.id = id;

        this.isMine = isMine;

        if (isMine)
        {
            FindObjectOfType<CinemachineVirtualCamera>().Follow = transform;
        }
        else
        {
            movement.enabled = false;
            crossHair.SetActive(false);
            StopCoroutine(receiveMovementCoroutine);
        }
    }

    public void ReceivePosition(Vector2 position, float angle)
    {
        movement.Move(position);

        if (isMine)
            movement.Rotate(angle);
    }

    private IEnumerator SendInputRoutine()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            var direction = playerInput.GetInput();

            movement.SetVelocity(direction);

            networkObjectManager.SendInput(direction, transform.eulerAngles.z);

            yield return sendRateHandel;
        }
    }
}