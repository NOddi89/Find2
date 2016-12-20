using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{

	public int m_TileId = -1;

	private Vector3 m_TilePlayerPos;
	private Collider m_Collider;


	private void Awake()
	{
		m_Collider = GetComponentInChildren<Collider> ();
		m_TilePlayerPos = m_Collider.bounds.center + new Vector3(0f, 0.55f, 0f); // Adding some of the players hight to get him above the ground
	}

	public int TileID
	{
		get{ return m_TileId; }
		set{ m_TileId = value; }
	}

	public Vector3 GetTilePlayerPos()
	{
		return m_TilePlayerPos;
	}

}
