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
    private UserInterfaceManager m_userInterfaceManager;
    private Bank m_bank;
    private int m_roundNumber = 0;

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        m_tileManager = tileManager.GetComponent<TileManager>();
        m_userInterfaceManager = userInterface.GetComponent<UserInterfaceManager>();
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

                // Start info
                m_userInterfaceManager.SetInfoText("Select start location");
                m_userInterfaceManager.ShowInfoText();
            }
        }
        else if(gameState == GameState.StartGame)
        {
            selectStartTileCanvas.gameObject.SetActive(false);
            m_userInterfaceManager.HideInfoText();

            // Round number one
            NextRound();
            m_userInterfaceManager.ShowRoundNumberText();

            // Activating palyer info
            m_userInterfaceManager.ShowCurrentPlayerMoneyBalance();
            m_userInterfaceManager.ShowCurrentPlayerText();

            // Selecting a player to start
            m_currentPlayer = FirstPlayer();
            gameState = GameState.Running;
        }
        else if(gameState == GameState.Running)
        {
            if(!m_currentPlayer.PlayerActive)
            {
                m_currentPlayer.PlayerActive = true;
                ActivateCurrentPlayerUi();
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
        }
        else
        {
            m_currentPlayer = FirstPlayer();
            NextRound();
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

    private void ActivateCurrentPlayerUi()
    {
        m_userInterfaceManager.SetCurrentPlayerText(m_currentPlayer.name);
        m_userInterfaceManager.SetCurrentPlayerMoneyBalance(m_currentPlayer.MoneyBalance);
    }

    private void NextRound()
    {
        m_roundNumber++;
        m_userInterfaceManager.IncrementRoundNumber();
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Set the starting tile for current player
    /// </summary>
    /// <param name="startTile"></param>
    public void SetStartTileCurrentPlayer(Transform startTile)
    {
        ItemTile tile = startTile.GetComponent<ItemTile>();

        // A check to see that the tile selected as start tile on the button is a valid start tile.
        if(tile.IsStartTile)
        {
            m_currentPlayer.SetStartingTile(startTile);

            // If all players has selected starting tile the game can start
            if (m_currentPlayer.PlayerId == players.Length)
            {
                gameState = GameState.StartGame;
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