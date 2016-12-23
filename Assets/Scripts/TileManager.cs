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

		if(m_numOfAddedTiles > 1)
		{
			AddNeighborsToTiles();
		}

		//tileGraph.GetTileIDsNumOfStepFromNode(tileGraph.Nodes["4"], 2);
	}
	
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

	private void AddTilesToGraph()
	{
		foreach(Transform transform in m_tiles)
		{
			Tile tile = transform.GetComponent<Tile>();
			m_tileGraph.AddNode(tile.TileID.ToString(), transform);
            AddNeighborsToTile(transform);

		}
	}

	private void AddNeighborsToTile(Transform tile)
	{
		//for(int i = 1; i < m_numOfAddedTiles; i++)
		//{
		//	m_tileGraph.AddUndirectedEdge(i.ToString(), (i+1).ToString()); 
		//}


	}
	
	public List<string> GetAllValidTilesWithinNoOfStepsFromTile(Tile fromTile, int steps)
	{
		return m_tileGraph.GetTileIDsNumOfStepFromNode(m_tileGraph.Nodes[fromTile.m_TileId.ToString()], steps);
	}

    public List<string> GetPathBetweenTiles(Tile start, Tile goal)
    {
        return m_tileGraph.ConstructPath(m_tileGraph.Nodes[start.m_TileId.ToString()], m_tileGraph.Nodes[goal.m_TileId.ToString()]);
    }

    public Tile GetTile(string tileId)
    {
        int id;
        System.Int32.TryParse(tileId, out id);

        return m_tiles.GetChild(id - 1).GetComponent<Tile>();
    }
}
