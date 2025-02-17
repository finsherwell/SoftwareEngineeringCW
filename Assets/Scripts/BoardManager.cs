using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private List<Tile> tiles;

    public int TotalTiles => tiles.Count;

    public Tile GetTile(int index)
    {
        if (index < 0 || index >= TotalTiles) return null;
        return tiles[index];
    }

}
