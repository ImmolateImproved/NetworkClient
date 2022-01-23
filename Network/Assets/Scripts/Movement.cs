using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float rotationSpeed;

    private Vector2 inputDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MouseLook();
    }

    private void FixedUpdate()
    {
        rb.velocity = inputDirection * moveSpeed;
    }

    private void MouseLook()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        var directionToMouse = (mouseWorldPos - transform.position).normalized;

        transform.up = directionToMouse;

        //var angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg - 90;
        //var rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    public void SetInputDirection(Vector2 direction)
    {
        inputDirection = direction.normalized;
    }

    public void Move(Vector2 position)
    {
        if (Vector2.Distance(transform.position, position) < moveSpeed)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, position, lerpSpeed * Time.deltaTime));
        }
        else
        {
            rb.MovePosition(position);
        }
    }

    public void Rotate(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}