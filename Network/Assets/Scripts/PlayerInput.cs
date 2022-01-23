using ClientMessages;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public static readonly Dictionary<Vector2, vector> directionMap = new Dictionary<Vector2, vector>
    {
        {new Vector2(0, 0) , vector.Zero},
        {new Vector2(0, 1) , vector.Up},
        {new Vector2(1, 1) , vector.UpRight},
        {new Vector2(1, 0), vector.Right},
        {new Vector2(1, -1) , vector.DownRight},
        {new Vector2(0, -1) , vector.Down},
        {new Vector2(-1, -1) , vector.DownLeft},
        {new Vector2(-1, 0) , vector.Left},
        {new Vector2(-1, 1) , vector.UpLeft}
    };

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public Vector2 GetInput()
    {
        var h = Input.GetAxisRaw(Horizontal);
        var v = Input.GetAxisRaw(Vertical);

        var direction = new Vector2(h, v);

        return direction;
    }

    public Vector2 GetMousePosition()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
