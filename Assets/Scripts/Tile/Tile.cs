using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{
    public Transform[] neighborTiles;

	protected Vector3 m_tilePlayerPos;
	protected Collider m_collider;
    private int m_tileId;
    private bool m_isItemTile;


	public virtual void Awake()
	{
        // Calculate the player position
		m_collider = GetComponentInChildren<Collider> ();
		m_tilePlayerPos = m_collider.bounds.center + new Vector3(0f, 0.55f, 0f); // Adding some of the players hight to get him above the ground

        //Other setting
        m_tileId = -1;
        m_isItemTile = false;

    }

    /// <summary>
    /// The tile ID number
    /// </summary>
	public int TileID
	{
		get{ return m_tileId; }
		set{ m_tileId = value; }
	}

    /// <summary>
    /// The position where the player is supposed to stand
    /// </summary>
    /// <returns></returns>
	public Vector3 TilePlayerPos
	{
		get { return m_tilePlayerPos; }
	}

    /// <summary>
    /// Array of neighbor tiles
    /// </summary>
    /// <returns></returns>
    public Transform[] NeighborTiles
    {
        get { return neighborTiles; }
    }

    /// <summary>
    /// Is the tile an item or a regular tile
    /// </summary>
    /// <returns></returns>
    public bool IsItemTile
    {
        get { return m_isItemTile; }
        set { m_isItemTile = value; }
    }

}
