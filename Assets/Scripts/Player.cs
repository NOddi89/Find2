using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    private Transform m_startingTile;
    private PlayerMovement m_playerMovement;
    #endregion

    #region Monobehaviour
    // Use this for initialization
    void Start ()
    {
        m_playerMovement = GetComponentInParent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(m_active)
        {
            if(GameManager.gameState == GameManager.GameState.ChooseStartTile)
            {

            }
        }	
	}
    #endregion

    #region Get/Set
    /// <summary>
    /// Set the starting tile for the player
    /// </summary>
    /// <param name="startingTile"></param>
    public void SetStartingTile(Transform startingTile)
    {
        m_startingTile = startingTile;
    }

    #endregion

    #region Properties

    public int m_playerId = 0;
    public int PlayerId
    {
        get { return m_playerId; }
        set { m_playerId = value; }
    }

    private bool m_active = false;
    public bool PlayerActive
    {
        get { return m_active; }
        set
        {
            m_active = value;

            if(GameManager.gameState == GameManager.GameState.Running)
            {
                m_playerMovement.CanMove = value;
            }  
        }
    }

    #endregion
}
