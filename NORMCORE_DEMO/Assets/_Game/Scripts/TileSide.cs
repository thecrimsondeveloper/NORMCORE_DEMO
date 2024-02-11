using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSide : MonoBehaviour
{
    [SerializeField] Tile[] tiles;

    private void OnValidate()
    {
        tiles = GetComponentsInChildren<Tile>(true);
    }

    public Tile GetClosestTile(Vector3 pos)
    {
        Tile closest = null;
        float closestDist = float.MaxValue;

        foreach (var tile in tiles)
        {
            float dist = Vector3.Distance(tile.transform.position, pos);
            if (dist < closestDist)
            {
                closest = tile;
                closestDist = dist;
            }
        }

        return closest;
    }



}
