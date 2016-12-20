using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {

	public Transform m_Tiles;

	private Graph tileGraph = new Graph();
	private int numOfAddedTiles = 0;

	// Use this for initialization
	void Awake () 
	{
		SetTileIDs();
		AddTilesToGraph();

		if(numOfAddedTiles > 1)
		{
			AddNeighborsToTiles();
		}

		//tileGraph.GetTileIDsNumOfStepFromNode(tileGraph.Nodes["4"], 2);
	}
	
	private void SetTileIDs()
	{
		int id = 0;

		foreach(Transform transform in m_Tiles)
		{
			id++;
			Tile tile = transform.GetComponent<Tile>();
			tile.TileID = id;
		}

		if( id != 0)
		{
			numOfAddedTiles = id;
		}
	}

	private void AddTilesToGraph()
	{
		foreach(Transform transform in m_Tiles)
		{
			Tile tile = transform.GetComponent<Tile>();
			tileGraph.AddNode(tile.TileID.ToString(), transform);
		}
	}

	private void AddNeighborsToTiles()
	{
		for(int i = 1; i < numOfAddedTiles; i++)
		{
			tileGraph.AddUndirectedEdge(i.ToString(), (i+1).ToString()); 
		}
	}
	
	public List<string> GetAllValidTilesWithinNoOfStepsFromTile(Tile fromTile, int steps)
	{
		return tileGraph.GetTileIDsNumOfStepFromNode(tileGraph.Nodes[fromTile.m_TileId.ToString()], steps);
	}
}
