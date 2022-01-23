using Cinemachine;
using MEC;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NetworkPlayer : MonoBehaviour
{
    private NetworkMovementManager networkMovementManager;
    private NetworkWeponManager networkWeponManager;

    private PlayerInput playerInput;
    private Movement movement;
    private Weapon weapon;

    private int id;

    private bool isMine;

    private CoroutineHandle sendInputCoroutine;

    private SpriteRenderer sr;

    [SerializeField]
    private GameObject crossHair;

    private void Awake()
    {
        networkMovementManager = SystemsManager.GetSystem<NetworkMovementManager>();

        playerInput = GetComponent<PlayerInput>();
        movement = GetComponent<Movement>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        Timing.KillCoroutines(sendInputCoroutine);
    }

    private void Update()
    {
        StartShot();
    }

    public void Init(int id, Color color, bool isMine)
    {
        this.id = id;

        this.isMine = isMine;

        sr.color = color;

        if (isMine)
        {
            FindObjectOfType<CinemachineVirtualCamera>().Follow = transform;
            sendInputCoroutine = Timing.RunCoroutine(SendInputRoutine());
        }
        else
        {
            movement.enabled = false;
            crossHair.SetActive(false);
        }
    }

    public void ReceivePosition(Vector2 position, float angle)
    {
        movement.Move(position);

        if (!isMine)
        {
            movement.Rotate(angle);
        }
    }

    public void SendInput()
    {
        var direction = playerInput.GetInput();

        movement.SetInputDirection(direction);

        networkMovementManager.SendInput(direction, transform.eulerAngles.z);
    }

    public void StartShot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.StartShot();
            networkWeponManager.SendShotEvent(playerInput.GetMousePosition());
        }
    }

    public void Shot(Vector2 targetPosition)
    {
        weapon.Shot(targetPosition);
    }

    private IEnumerator<float> SendInputRoutine()
    {
        var delay = 1 / networkMovementManager.SendRate;

        while (true)
        {
            SendInput();

            yield return Timing.WaitForSeconds(delay);
        }
    }
}