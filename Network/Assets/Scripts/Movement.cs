using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        var directionToMouse = (mouseWorldPos - transform.position).normalized;

        transform.up = directionToMouse;
    }

    public void SetVelocity(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed;
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
