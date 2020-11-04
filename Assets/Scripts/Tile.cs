using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool walkable;
    public Vector3 GetTilePosition()
    {
        return transform.position;
    }

    public bool IsWalkable
    {
        get => walkable;
    }
}
