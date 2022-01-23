using Cinemachine;
using System.Collections;
using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    private NetworkMovementManager networkMovementManager;

    private PlayerInput playerInput;
    private Movement movement;

    [SerializeField]
    private float sendRate;

    private int id;

    private bool isMine;

    private Coroutine sendInputCoroutine;

    private SpriteRenderer sr;

    [SerializeField]
    private GameObject crossHair;

    private void Awake()
    {
        networkMovementManager = SystemsManager.GetSystem<NetworkMovementManager>();

        playerInput = GetComponent<PlayerInput>();
        movement = GetComponent<Movement>();
        sr = GetComponent<SpriteRenderer>();

        sendInputCoroutine = StartCoroutine(SendInputRoutine());
    }

    public void Init(int id, Color color, bool isMine)
    {
        this.id = id;

        this.isMine = isMine;

        sr.color = color;

        if (isMine)
        {
            FindObjectOfType<CinemachineVirtualCamera>().Follow = transform;
        }
        else
        {
            movement.enabled = false;
            crossHair.SetActive(false);
            StopCoroutine(sendInputCoroutine);
        }
    }

    public void ReceivePosition(Vector2 position, float angle)
    {
        movement.Move(position);

        if (!isMine)
            movement.Rotate(angle);
    }

    private IEnumerator SendInputRoutine()
    {
        var sendRateHandel = new WaitForSeconds(1 / sendRate);

        while (true)
        {
            var direction = playerInput.GetInput();

            movement.SetVelocity(direction);

            networkMovementManager.SendInput(direction, transform.eulerAngles.z);

            yield return sendRateHandel;
        }
    }
}