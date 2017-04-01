using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{
    #region Variables

    protected Collider m_collider;

    #endregion

    #region Monobehaviour

    public virtual void Awake()
	{
        // Calculate the player position
		m_collider = GetComponentInChildren<Collider> ();
		m_tilePlayerPos = m_collider.bounds.center + new Vector3(0f, 0.55f, 0f); // Adding some of the players hight to get him above the ground

        //Other setting
        m_tileId = -1;
        m_isItemTile = false;
    }

    #endregion

    #region Properties

    /// <summary>
    /// The tile ID number
    /// </summary>
    public int m_tileId;
    public int TileID
	{
		get{ return m_tileId; }
		set{ m_tileId = value; }
	}

    /// <summary>
    /// The position where the player is supposed to stand
    /// </summary>
    /// <returns></returns>
    protected Vector3 m_tilePlayerPos;
    public Vector3 TilePlayerPos
	{
		get { return m_tilePlayerPos; }
        set { m_tilePlayerPos = value; }
	}

    /// <summary>
    /// Array of neighbor tiles
    /// </summary>
    /// <returns></returns>
    public Transform[] neighborTiles;
    public Transform[] NeighborTiles
    {
        get { return neighborTiles; }
        set { neighborTiles = value; }
    }

    /// <summary>
    /// Is the tile an item or a regular tile
    /// </summary>
    /// <returns></returns>
    private bool m_isItemTile;
    public bool IsItemTile
    {
        get { return m_isItemTile; }
        set { m_isItemTile = value; }
    }

    #endregion

}
