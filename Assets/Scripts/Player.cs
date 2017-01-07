using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    public Canvas diceCanvas;

    private PlayerMovement m_playerMovement;
    private PlayerState m_playerState = PlayerState.NotActive;
    private TileManager m_tileManager;

    private enum PlayerState
    {
        NotActive,
        RollDice,
        WaitingForDiceValue,
        SelectTile,
        Move,
        Moveing
    }

    #endregion

    #region Monobehaviour
    private void Awake()
    {
        m_tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
    }

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
            if(m_playerState == PlayerState.RollDice)
            {
                diceCanvas.gameObject.SetActive(true);
                m_playerState = PlayerState.WaitingForDiceValue;
            }
            else if(m_playerState == PlayerState.SelectTile && m_diceRollValue > 0)
            {
                diceCanvas.gameObject.SetActive(false);
                m_playerMovement.Steps = m_diceRollValue;
                m_playerState = PlayerState.Move;
                m_diceRollValue = 0;
            }
            else if(m_playerState == PlayerState.Move)
            {
                m_playerMovement.CanMove = true;
                m_playerState = PlayerState.Moveing;
            }
        }	
	}

    #endregion

    #region Get/Set
    /// <summary>
    /// Set the starting tile/spawn point for the player
    /// </summary>
    /// <param name="startingTile"></param>
    public void SetStartingTile(Transform startingTile)
    {
        m_playerMovement.SpawnPoint = startingTile;
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

            if(m_active && !(GameManager.gameState == GameManager.GameState.ChooseStartTile))
            {
                m_playerState = PlayerState.RollDice;
            }
            else
            {
                m_playerState = PlayerState.NotActive;
                m_playerMovement.CanMove = false;
            }
            
        }
    }

    private int m_diceRollValue = 0;
    public int DiceRollValue
    {
        set
        {
            m_diceRollValue = value;
            m_playerState = PlayerState.SelectTile;
        }
    }

    #endregion
}
