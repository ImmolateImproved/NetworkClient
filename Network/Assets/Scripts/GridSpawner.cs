using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject cellPrefab;

    [SerializeField]
    private Vector2 offset;

    [SerializeField]
    private int spacing, width, height;

    void Start()
    {
        //Spawn();
    }

    public void Spawn()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var position = new Vector3(x, y) * spacing + (Vector3)offset;

                Instantiate(cellPrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
