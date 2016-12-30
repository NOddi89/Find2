using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {

	public Transform m_tiles;

	private Graph m_tileGraph = new Graph();
	private int m_numOfAddedTiles = 0;

	// Use this for initialization
	void Awake () 
	{
		SetTileIDs();
		AddTilesToGraph();
        AddNeighborsToTiles(m_tiles);
	}
	
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
            Debug.Log("Adding tile " + tile.TileID + " to graph");
			m_tileGraph.AddNode(tile.TileID.ToString(), transform);

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
                Debug.Log("Adding edge between " + tile.GetComponent<Tile>().TileID + " to " + neighbor.TileID);
                m_tileGraph.AddUndirectedEdge(tile.GetComponent<Tile>().TileID.ToString(), neighbor.TileID.ToString());
            } 
        }
	}
	
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
}
