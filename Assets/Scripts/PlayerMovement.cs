using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    public Transform m_SpawnPoint;
	public float m_Speed = 3f;
	public Transform m_TileManager;
	public int m_steps = 1;

	private Transform m_Transform;
	private bool m_isMoveing;  
	private Tile m_currentTile;
	private TileManager m_tileManagerScript;
	private bool m_hasValidTiles;
	private List<string> m_validTileIDs;
    #endregion

    #region Monobehaviour

    void Awake()
	{
		m_Transform = GetComponent<Transform> ();
		m_isMoveing = false;
        m_canMove = false;
		m_hasValidTiles = false;
		m_tileManagerScript = m_TileManager.GetComponent<TileManager>();
	}

	void Start () 
	{
		m_currentTile = m_SpawnPoint.GetComponent<Tile>();
		transform.position = m_currentTile.TilePlayerPos;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_canMove)
        {
            if (!m_hasValidTiles)
            {
                m_validTileIDs = m_tileManagerScript.GetAllValidTilesWithinNoOfStepsFromTile(m_currentTile, m_steps);
                m_hasValidTiles = true;
            }
            else
            {
                if (Input.GetButtonDown("Fire1") && !m_isMoveing)
                {
                    Tile newDestination = GetNewDestinationTile();
                    //Debug.Log("New destination is tile: " + newDestination.TileID + " and it is " + (m_validTileIDs.Contains(newDestination.TileID.ToString()) ? " valid." : "not valid."));

                    if (newDestination != null && m_validTileIDs.Contains(newDestination.TileID.ToString()))
                    {
                        //Debug.Log("Path to tile is: ");
                        List<string> path = m_tileManagerScript.GetPathBetweenTiles(m_currentTile, newDestination);
                        //string output = "";
                        //foreach (string s in path)
                        //{
                        //    output += (s + ", ");
                        //}
                        //Debug.Log(output);

                        StartCoroutine(walkPath(path));
                    }
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
    /// 
    /// </summary>
    private void NewRandomStep()
    {
        m_steps = Random.Range(1, 7);
        Debug.Log("Steps: " + m_steps);
        m_hasValidTiles = false;
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
            Tile destination = m_TileManager.GetComponent<TileManager>().GetTile(path[index]);
            yield return StartCoroutine(moveToTile(destination));

            index++;

            if(index == path.Count)
            {
                destinationReached = true;
            }

            yield return null;
        }

        m_isMoveing = false;

        NewRandomStep();
    }
    #endregion

    #region Properties
    private bool m_canMove;
    public bool CanMove
    {
        get { return m_canMove; }
        set { m_canMove = value; }
    }
    #endregion
}
