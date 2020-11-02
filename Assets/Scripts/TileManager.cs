using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private float DefaultSize;

    [SerializeField] private GameObject TilePrefab;

    [SerializeField] private Vector2 GridSize;
    // Start is called before the first frame update
    void Start()
    {
        // float sizeMultiplier = DefaultSize * 10f;
        // for (int x = 0; x < GridSize.x; x++)
        // {
        //     for (int y = 0; y < GridSize.y; y++)
        //     {
        //         GameObject.Instantiate(TilePrefab, new Vector3(x * sizeMultiplier, 0, y * sizeMultiplier),
        //             Quaternion.identity);
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
