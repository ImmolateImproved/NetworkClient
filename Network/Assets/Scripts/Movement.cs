using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private float serverSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity * serverSpeed;
    }

    public void Move(Vector2 position)
    {
        if (Vector2.Distance(transform.position, position) < serverSpeed)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, position, lerpSpeed * Time.deltaTime));
        }
        else
        {
            rb.MovePosition(position);
        }
    }
}
