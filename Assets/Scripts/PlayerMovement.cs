using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    
	public float m_Speed = 3f;
	
	private Transform m_transform;
	 
	private Tile m_currentTile;
	private TileManager m_tileManager;
	private bool m_hasValidTiles = false;
    private List<string> m_validTileIDs;

    #endregion

    #region Events

    public delegate void MoveDone();
    public static event MoveDone OnMoveDone;

    #endregion

    #region Monobehaviour

    void Awake()
	{
		m_transform = GetComponent<Transform> ();
        m_tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
	}

	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_canMove && m_hasValidTiles)
        {      
            if (Input.GetButtonDown("Fire1") && !m_isMoveing)
            {
                Tile newDestination = GetNewDestinationTile();

                if (newDestination != null && m_validTileIDs.Contains(newDestination.TileID.ToString()))
                {
                    List<string> path = m_tileManager.GetPathBetweenTiles(m_currentTile, newDestination);
                    StartCoroutine(walkPath(path));
                } 
            }
        }	
	}

    #endregion

    #region Private methods
    /// <summary>
    /// Casting a ray form mouse position to get the tile the mouse is pointing at
    /// </summary>
    /// <returns> A tile clicked by a player </returns>
    private Tile GetNewDestinationTile() 
	{
		Tile destination = null;

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if(Physics.Raycast(ray, out hit, 1000))
		{
			destination = hit.collider.GetComponentInParent<Tile> ();
		}

		return destination;
	}

    /// <summary>
    /// Find all tiles that the player can go to from the current tile
    /// to all tiles a number of steps away. If there is one or more
    /// valid tile the m_hasValidTiles is true
    /// </summary>
    private void FindValidTiles()
    {
        m_validTileIDs = m_tileManager.GetAllValidTilesWithinNoOfStepsFromTile(m_currentTile, m_steps);

        if(m_validTileIDs.Count > 0)
        {
            m_hasValidTiles = true;
        }
    }

    #endregion

    #region Co-routines
    /// <summary>
    /// Co-routine
    /// Animation of movement between the current tile of the player and
    /// the destination
    /// </summary>
    /// <param name="destination"></param>
    /// <returns></returns>
    private IEnumerator moveToTile(Tile destination)
    {
        Vector3 newPos = destination.TilePlayerPos;
        Vector3 startPos = transform.position;
        float startTime = Time.time;
        float journeyTime = 1.0f;

        while (Vector3.Distance(transform.position, newPos) > 0.05f)
        {
            Vector3 center = (startPos + newPos) * 0.5f;
            center -= new Vector3(0, 1, 0);
            Vector3 startRelCenter = startPos - center;
            Vector3 newPosRelCenter = newPos - center;
            float fracComplete = (Time.time - startTime) / journeyTime;
            transform.position = Vector3.Slerp(startRelCenter, newPosRelCenter, fracComplete);
            transform.position += center;

            yield return null;
        }

        m_currentTile = destination;
    }

    /// <summary>
    /// Co-routine
    /// Starting a moveToTile for each tile in the list until the
    /// path is walked
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private IEnumerator walkPath(List<string> path)
    {
        bool destinationReached = false;
        int index = 1;
        m_isMoveing = true;

        while (!destinationReached)
        {
            Tile destination = m_tileManager.GetTile(path[index]);
            yield return StartCoroutine(moveToTile(destination));

            index++;

            if(index == path.Count)
            {
                destinationReached = true;
            }

            yield return null;
        }

        m_isMoveing = false;
        
        if(OnMoveDone != null)
        {
            OnMoveDone();
        }
    }
    #endregion

    #region Properties
    private bool m_canMove = false;
    public bool CanMove
    {
        get { return m_canMove; }
        set
        {
            m_canMove = value;

            if(!m_canMove)
            {
                m_hasValidTiles = false;
                m_canSelectTile = false;
                m_steps = -1;
            }
        }
    }

    private bool m_isMoveing = false;
    public bool IsMoveing
    {
        get { return m_isMoveing; }
        set { m_isMoveing = value; }
    }

    private bool m_canSelectTile;
    public bool CanSelectTile
    {
        get { return m_canSelectTile; }
        set { m_canSelectTile = value; }
    }

    private int m_steps = -1;
    public int Steps
    {
        get { return m_steps; }
        set
        {
            m_steps = value;
            FindValidTiles();
        }
    }

    private Transform m_spawnPoint;
    public Transform SpawnPoint
    {
        get { return m_spawnPoint; }
        set
        {
            m_spawnPoint = value;
            m_currentTile = m_spawnPoint.GetComponent<Tile>();
            m_transform.position = m_currentTile.TilePlayerPos;
        }
    }

    #endregion
}
