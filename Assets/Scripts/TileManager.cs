using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    #region Variables

    public Transform m_tiles;

	private Graph m_tileGraph = new Graph();
	private int m_numOfAddedTiles = 0;
    private int m_numOfAddedItemTiles = 0;
    private List<Transform> m_itemTiles = new List<Transform>();

    #endregion

    #region Monobehaviour

    // Use this for initialization
    void Awake () 
	{
		SetTileIDs();
		AddTilesToGraph();
        AddNeighborsToTiles(m_tiles);
        AddItemTileTypes();
    }

    private void Start()
    {
        
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Goes through all tiles asign to m_tiles and setting a ID.
    /// </summary>
    private void SetTileIDs()
	{
		int id = 0;

		foreach(Transform transform in m_tiles)
		{
			id++;
			Tile tile = transform.GetComponent<Tile>();
			tile.TileID = id;
		}

		if( id != 0)
		{
			m_numOfAddedTiles = id;
		}
	}

    /// <summary>
    /// Step one of createing a graph. This method adding a node to the grap
    /// for each tile in m_tiles.
    /// </summary>
	private void AddTilesToGraph()
	{
		foreach(Transform transform in m_tiles)
		{
			Tile tile = transform.GetComponent<Tile>();
			m_tileGraph.AddNode(tile.TileID.ToString(), transform);

            if(tile.IsItemTile)
            {
                ItemTile itemTile = (ItemTile)tile;

                if (!itemTile.IsStartTile)
                {
                    itemTile.HasItem = true;
                    m_itemTiles.Add(transform);
                    m_numOfAddedItemTiles++;
                }  
            }
		}
	}

    /// <summary>
    /// Step two of creating a graph. This function is checking each neighbor
    /// in tiles, and then adding an undirected edge between the tile and every
    /// neighbor.
    /// </summary>
    /// <param name="tiles"></param>
	private void AddNeighborsToTiles(Transform tiles)
	{     
        foreach(Transform tile in tiles)
        {
            Transform[] neighbors = tile.GetComponent<Tile>().NeighborTiles;

            foreach(Transform t in neighbors)
            {
                Tile neighbor = t.GetComponent<Tile>();
                //Debug.Log("Adding edge between " + tile.GetComponent<Tile>().TileID + " to " + neighbor.TileID);
                m_tileGraph.AddUndirectedEdge(tile.GetComponent<Tile>().TileID.ToString(), neighbor.TileID.ToString());
            } 
        }
	}

    /// <summary>
    /// TODO
    /// </summary>
    private void AddItemTileTypes()
    {
        //Debug.Log("No. item tiles = " + m_numOfAddedItemTiles);

        // Create item deployment minus one of the total of item tiles. That one is used for the vr googles
        ItemTileDeployment itemTileDeployment = new ItemTileDeployment(m_numOfAddedItemTiles - 1); 

        List<int> itemTileIDs = new List<int>();      

        // Get all item tile id's
        foreach (Transform t in m_itemTiles)
        {
            Tile tile = t.GetComponent<Tile>();
            itemTileIDs.Add(tile.TileID);
        }

        // Shuffle the id's so items is not added at the same tiles each game
        System.Random rnd = new System.Random();
        itemTileIDs.Shuffle(rnd);

        int currentItemIdIndex = 0;
        int numOfItems = itemTileDeployment.MoneyItemDeployment.Count;

        foreach (KeyValuePair<string, int> kvp in itemTileDeployment.MoneyItemDeployment)
        {
            int moneyValue;
            Int32.TryParse(kvp.Key, out moneyValue);

            for (int i = 0; i < kvp.Value; i++)
            {
                //Transform t = m_tiles.GetChild(itemTileIDs[currentItemIdIndex]);
                Tile tile = GetTile(itemTileIDs[currentItemIdIndex].ToString());

                if (tile.IsItemTile)
                {
                    ItemTile itemTile = tile.GetComponent<ItemTile>();
                    MoneyItemTile moneyItemTile = tile.gameObject.AddComponent<MoneyItemTile>();
                    moneyItemTile.TranferItemData(itemTile);
                    moneyItemTile.Value = moneyValue;
                    moneyItemTile.TilePrize = moneyItemTile.Value;
                    //Debug.Log("TileID: " + moneyItemTile.TileID);
                    Destroy(itemTile);
                }

                currentItemIdIndex++;
            }        
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Returning a list of tiles id's that is clickable a number of steps away in every direction from
    /// where the player is standing.
    /// </summary>
    /// <param name="fromTile"></param>
    /// <param name="steps"></param>
    /// <returns></returns>
    public List<string> GetAllValidTilesWithinNoOfStepsFromTile(Tile fromTile, int steps)
	{
		return m_tileGraph.GetTileIDsNumOfStepFromNode(m_tileGraph.Nodes[fromTile.TileID.ToString()], steps);
	}

    /// <summary>
    /// Returning a list of tile id's that is showing the path where the player need to go
    /// to get from start to goal tile.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="goal"></param>
    /// <returns></returns>
    public List<string> GetPathBetweenTiles(Tile start, Tile goal)
    {
        return m_tileGraph.ConstructPath(m_tileGraph.Nodes[start.TileID.ToString()], m_tileGraph.Nodes[goal.TileID.ToString()]);
    }

    /// <summary>
    /// Returning a tile with a spesific id.
    /// </summary>
    /// <param name="tileId"></param>
    /// <returns></returns>
    public Tile GetTile(string tileId)
    {
        int id;
        System.Int32.TryParse(tileId, out id);

        return m_tiles.GetChild(id - 1).GetComponent<Tile>();
    }

    #endregion
}
