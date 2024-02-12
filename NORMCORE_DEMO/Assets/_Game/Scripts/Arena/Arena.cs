using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Normal.Realtime;
using Unity.VisualScripting;



#if UNITY_EDITOR
using UnityEditor;
#endif


public class Arena : RealtimeComponent<ArenaModel>
{
    static List<Player> players = new List<Player>();
    static List<Tile> gameTiles = new List<Tile>();

    [ShowInInspector] public List<Player> Players => players;
    [ShowInInspector] public PlayerColor availableColor => Arena.GetFirstAvailableColor();
    [SerializeField] Tile prefabTile;


    //onvaluechanged
#if UNITY_EDITOR
    [OnValueChanged(nameof(SetupTiles))]
#endif
    [SerializeField] int width = 10;

#if UNITY_EDITOR
    [OnValueChanged(nameof(SetupTiles))]
#endif
    [SerializeField] int height = 10;

#if UNITY_EDITOR
    [OnValueChanged(nameof(SetupTiles))]
#endif
    [SerializeField] float spacing = 1;


    [SerializeField] Transform leftParent;
    [SerializeField] Transform topParent;
    [SerializeField] Transform rightParent;
    [SerializeField] Transform bottomParent;
    [SerializeField] List<Tile> tiles = new List<Tile>();

    private void Awake()
    {
        gameTiles = tiles;
    }


    public static void SetColorOfClosestTile(Vector3 pos, PlayerColor color)
    {
        //get the closest tile
        Tile closest = GetClosestTile(pos);

        //if the closest tile is not null
        if (closest != null)
        {
            //set the color of the tile to the color of the ball
            closest.SetColor(color);
        }
    }


    public static Tile GetClosestTile(Vector3 pos)
    {
        Tile closest = null;
        float closestDist = float.MaxValue;

        foreach (var tile in gameTiles)
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
    public static bool AnyPlayerHasColor(PlayerColor color)
    {
        foreach (var player in players)
        {
            if (player.color == color) return true;
        }
        return false;
    }

    public static PlayerColor GetFirstAvailableColor()
    {
        //return the first color that a player does not have
        if (!AnyPlayerHasColor(PlayerColor.Red)) return PlayerColor.Red;
        if (!AnyPlayerHasColor(PlayerColor.Blue)) return PlayerColor.Blue;
        if (!AnyPlayerHasColor(PlayerColor.Green)) return PlayerColor.Green;
        if (!AnyPlayerHasColor(PlayerColor.Yellow)) return PlayerColor.Yellow;

        return PlayerColor.Default;
    }

    public static void AddPlayer(Player player)
    {
        if (players.Contains(player)) return;
        players.Add(player);
    }

    public static void RemovePlayer(Player player)
    {
        if (!players.Contains(player)) return;
        players.Remove(player);
    }


#if UNITY_EDITOR




    [Button("Setup Tiles")]
    void SetupTiles()
    {
        //if applciation is playing, return
        if (Application.isPlaying) return;


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

        //clear the list of tiles
        tiles.Clear();

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
}


