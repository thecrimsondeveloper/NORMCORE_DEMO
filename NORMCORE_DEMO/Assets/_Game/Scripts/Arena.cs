using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR    
using UnityEditor;
#endif


public class Arena : MonoBehaviour
{
    [SerializeField] Tile prefabTile;

    [SerializeField] int width = 10;
    [SerializeField] int height = 10;

    [SerializeField] float spacing = 1;


    [SerializeField] Transform leftParent;
    [SerializeField] Transform topParent;
    [SerializeField] Transform rightParent;
    [SerializeField] Transform bottomParent;
    [SerializeField] List<Tile> tiles = new List<Tile>();


#if UNITY_EDITOR

    [Button("Setup Tiles")]
    void SetupTiles()
    {
        int tryCount = 0;
        while (leftParent.childCount > 0 && tryCount++ < 1000)
        {
            DestroyImmediate(leftParent.GetChild(0).gameObject);
        }

        tryCount = 0;
        while (topParent.childCount > 0 && tryCount++ < 1000)
        {
            DestroyImmediate(topParent.GetChild(0).gameObject);
        }

        tryCount = 0;
        while (rightParent.childCount > 0 && tryCount++ < 1000)
        {
            DestroyImmediate(rightParent.GetChild(0).gameObject);
        }

        tryCount = 0;
        while (bottomParent.childCount > 0 && tryCount++ < 1000)
        {
            DestroyImmediate(bottomParent.GetChild(0).gameObject);
        }

        float horizontalOffset = (width - 1) * spacing / 2;

        //create a tile for each horizontal position for the top parent
        for (int x = 0; x < width; x++)
        {
            Tile tile = PrefabUtility.InstantiatePrefab(prefabTile, topParent) as Tile;

            tile.transform.localPosition = new Vector3(x * spacing - horizontalOffset, 0, 0);

            //set the z rotation to -90 so the tile is facing down
            tile.transform.localEulerAngles = new Vector3(0, 0, -90);

            tiles.Add(tile);
        }

        //create a tile for each horizontal position for the bottom parent
        for (int x = 0; x < width; x++)
        {
            Tile tile = PrefabUtility.InstantiatePrefab(prefabTile, bottomParent) as Tile;

            tile.transform.localPosition = new Vector3(x * spacing - horizontalOffset, 0, 0);

            //set the z rotation to 90 so the tile is facing up
            tile.transform.localEulerAngles = new Vector3(0, 0, 90);

            tiles.Add(tile);
        }

        float verticalOffset = (height - 1) * spacing / 2;

        //create a tile for each vertical position for the left parent
        for (int y = 0; y < height; y++)
        {
            Tile tile = PrefabUtility.InstantiatePrefab(prefabTile, leftParent) as Tile;

            tile.transform.localPosition = new Vector3(0, y * spacing - verticalOffset, 0);

            //set the z rotation to 0 so the tile is facing right
            tile.transform.localEulerAngles = new Vector3(0, 0, 0);

            tiles.Add(tile);
        }

        //create a tile for each vertical position for the right parent
        for (int y = 0; y < height; y++)
        {
            Tile tile = PrefabUtility.InstantiatePrefab(prefabTile, rightParent) as Tile;

            tile.transform.localPosition = new Vector3(0, y * spacing - verticalOffset, 0);

            //set the z rotation to 180 so the tile is facing left
            tile.transform.localEulerAngles = new Vector3(0, 0, 180);

            tiles.Add(tile);
        }

    }

#endif
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

public enum PlayerColor
{
    Red,
    Blue,
    Green,
    Yellow
}

