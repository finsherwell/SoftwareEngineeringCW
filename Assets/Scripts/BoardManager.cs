using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    [SerializeField] public List<Tile> tiles;

    public int TotalTiles => tiles.Count;

    public Tile GetTile(int id)
    {
        Tile foundTile = tiles.Find(tile => tile.GetID() == id);
        if (foundTile == null)
        {
            Debug.LogWarning($"No tile found with ID: {id}");
        }
    return foundTile;
    }    
    public void Start()
    {
       FindTiles();
    }
   public void FindTiles()
{
    GameObject[] tileObjects = GameObject.FindGameObjectsWithTag("Tile");
    List<Tile> unsortedTiles = new List<Tile>();

    foreach (GameObject tileObject in tileObjects)
    {
        Tile tile = tileObject.GetComponent<Tile>();
        if (tile != null)
        {
            unsortedTiles.Add(tile);
        }
    }

    // Sort the tiles by ID
    unsortedTiles.Sort((a, b) => a.GetID().CompareTo(b.GetID()));

    // Add sorted tiles to the main list
    tiles = new List<Tile>();
    foreach (Tile tile in unsortedTiles)
    {
        if (tiles.Count < 40)
        {
            tiles.Add(tile);
        }
        else
        {
            break;
        }
    }
    
    Debug.Log($"Found and sorted {tiles.Count} tiles.");
}
}
