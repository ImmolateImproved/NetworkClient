using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float serverSpeed;

    public void Move(Vector2 position)
    {
        if (Vector2.Distance(transform.position, position) < serverSpeed)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = position;
        }
    }
}
