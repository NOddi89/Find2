using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
    #region Variables

    public enum GameState
    {
        ChooseStartTile,
        StartGame,
        Running,
        EndGame
    }

    public Transform tileManager;
    public Canvas selectStartTileCanvas;
    public Canvas userInterface;
    public Transform[] players;
    public static GameState gameState;

    private TileManager m_tileManager;
    private Bank m_bank;
    

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        m_tileManager = tileManager.GetComponent<TileManager>();
        m_bank = new Bank(200, 100);

        int id = 1;

        foreach (Transform p in players)
        { 
            Player player = p.GetComponent<Player>();

            player.PlayerId = id;
            id++;

            player.MoneyBalance = m_bank.GetPlayerStartCapital();
        }

        // First player can select starting tile
        m_currentPlayer = FirstPlayer();
        gameState = GameState.ChooseStartTile;       
    }

    private void Update()
    {
        if(gameState == GameState.ChooseStartTile)
        {
            if(!selectStartTileCanvas.gameObject.activeSelf)
            {
                selectStartTileCanvas.gameObject.SetActive(true);
            }
        }
        else if(gameState == GameState.StartGame)
        {
            selectStartTileCanvas.gameObject.SetActive(false);

            // Selecting a player to start
            m_currentPlayer = FirstPlayer();
            gameState = GameState.Running;
        }
        else if(gameState == GameState.Running)
        {
            if(!m_currentPlayer.PlayerActive)
            {
                m_currentPlayer.PlayerActive = true;
            }
        }
        else if(gameState == GameState.EndGame)
        {

        }
    }

    private void OnEnable()
    {
        PlayerMovement.OnMoveDone += PlayerMovementDone;
    }

    private void OnDisable()
    {
        PlayerMovement.OnMoveDone -= PlayerMovementDone;
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Selecting the next player. If the the last player is current player
    /// the first player is selected
    /// </summary>
    private void NextPlayer()
    {
        m_currentPlayer.PlayerActive = false;

        if(m_currentPlayer.PlayerId < players.Length)
        {
            m_currentPlayer = players[m_currentPlayer.PlayerId].GetComponent<Player>();
            m_currentPlayer.PlayerActive = true;
        }
        else
        {
            m_currentPlayer = FirstPlayer();
        }
    }

    /// <summary>
    /// Get the first player
    /// </summary>
    /// <returns> the first player </returns>
    private Player FirstPlayer()
    {
        return players[0].GetComponent<Player>();
    }

    /// <summary>
    /// Call when the event OnMoveDone is raised from PlayerMovement
    /// </summary>
    private void PlayerMovementDone()
    {
        NextPlayer();
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Set the starting tile for current player
    /// </summary>
    /// <param name="startTile"></param>
    public void SetStartTileCurrentPlayer(Transform startTile)
    {
        //Debug.Log("Set start tile for player " + m_currentPlayer.PlayerId);

        ItemTile tile = startTile.GetComponent<ItemTile>();

        // A check to see that the tile selected as start tile on the button is a valid start tile.
        if(tile.IsStartTile)
        {
            m_currentPlayer.SetStartingTile(startTile);

            // If all players has selected starting tile the game can start
            if (m_currentPlayer.PlayerId == players.Length)
            {
                gameState = GameState.StartGame;
                Debug.Log("Start game!");
            }
            else
            {
                NextPlayer();
            }
        }
        else
        {
            Debug.LogError(String.Format("Tile {0} is not a start tile", tile.TileID));
        } 
    }

    #endregion

    #region Properties

    private Player m_currentPlayer;
    public Player CurrentPlayer
    {
        get { return m_currentPlayer; }
        set { m_currentPlayer = value; }
    }

    #endregion

}