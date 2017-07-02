using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    public Canvas diceCanvas;

    public enum PlayerState
    {
        NotActive,
        RollDice,
        WaitingForDiceValue,
        SelectTile,
        Move,
        Moveing,
        TurnTile,
        WaitingForBuyTransaction,
        Done
    }

    private PlayerMovement m_playerMovement;
    private TileManager m_tileManager;
    private UserInterfaceManager m_userInterfaceManager;
    private GameManager m_gameManager;



    #endregion

    #region Events

    public delegate void PlayerDone();
    public static PlayerDone OnPlayerDone;

    #endregion

    #region Monobehaviour
    private void Awake()
    {
        m_tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        m_userInterfaceManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UserInterfaceManager>();
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
            if (m_playerState == PlayerState.RollDice)
            {
                //Debug.Log("Roll Dice");
                m_userInterfaceManager.ShowRollDiceButton();
                m_playerState = PlayerState.WaitingForDiceValue;
            }
            else if (m_playerState == PlayerState.SelectTile && m_diceRollValue > 0)
            {
                //Debug.Log("Select tile");
                m_userInterfaceManager.HideRollDiceButton();
                m_playerMovement.Steps = m_diceRollValue;
                m_playerState = PlayerState.Move;
                m_diceRollValue = 0;
            }
            else if (m_playerState == PlayerState.Move)
            {
                //Debug.Log("Move");
                m_playerMovement.CanMove = true;
                m_playerState = PlayerState.Moveing;
            }
            else if (m_playerState == PlayerState.TurnTile)
            {
                //Debug.Log("TurnTile");
                m_playerState = PlayerState.WaitingForBuyTransaction;
            }
            else if(m_playerState == PlayerState.WaitingForBuyTransaction)
            {
                //Debug.Log("Buy transaction");

            }
            else if (m_playerState == PlayerState.Done)
            {
                //Debug.Log("Done");
                m_playerState = PlayerState.NotActive;
                m_userInterfaceManager.HideBuyButton();
                m_userInterfaceManager.HideRollDiceButton();
                OnPlayerDone();
            }
        }	
	}

    #endregion


    #region Private Methods

    private void OnEnable()
    {
        PlayerMovement.MovementDone += PlayerMovementDone;
        Dice.OnDiceBuy += RollDiceBuyTile;
        UserInterfaceManager.OnBuyTileClick += BuyTile;
    }

    private void OnDisable()
    {
        PlayerMovement.MovementDone -= PlayerMovementDone;
        Dice.OnDiceBuy -= RollDiceBuyTile;
        UserInterfaceManager.OnBuyTileClick -= BuyTile;
    }

    /// <summary>
    /// When player movement is done check if the player is standing on an item tile.
    /// If there is an item there check if the player can buy it. Activate ui buttons
    /// to buy or roll.
    /// </summary>
    private void PlayerMovementDone()
    {
        if (m_playerMovement.CurrentTile.IsItemTile)
        {
            ItemTile itemTile = (ItemTile)m_playerMovement.CurrentTile;

            if(itemTile.HasItem)
            {
                bool canAfford = CanAffordTile(itemTile.TilePrize);

                if (canAfford)
                {
                    m_userInterfaceManager.ShowBuyButton();
                }
            }

            m_userInterfaceManager.ShowRollDiceButton();
            m_playerState = PlayerState.TurnTile;
        }
        else
        {
            m_playerState = PlayerState.Done;
        }    
    }

    /// <summary>
    /// If user is rolling for the tile check if the dice value is equal to 4 or greater.
    /// Retrive item if true.
    /// </summary>
    /// <param name="diceValue"></param>
    private void RollDiceBuyTile(int diceValue)
    {
        if (m_gameManager.CurrentPlayer.PlayerId == this.PlayerId)
        {
            if (diceValue >= 4)
            {
                RetriveTileItem();
            }

            m_playerState = PlayerState.Done;
        }
    }

    /// <summary>
    /// If the user is buying the tile deposit money for the tile then retrive the item.
    /// </summary>
    private void BuyTile()
    {
        if(m_gameManager.CurrentPlayer.PlayerId == this.PlayerId)
        {
            bool depositOkey = m_gameManager.Bank.DepositMoney(300);

            if (depositOkey)
            {
                RetriveTileItem();
            }

            m_playerState = PlayerState.Done;
        }       
    }

    /// <summary>
    /// Get the tile item and save the value in player inventory
    /// </summary>
    private void RetriveTileItem()
    {
        ItemTile itemTile = m_playerMovement.CurrentItemTile;
        itemTile.HasItem = false;
        itemTile.SetColor(Color.gray);  
        ItemTile.ItemType itemType = itemTile.TileItemType; 

        switch (itemType)
        {
            case ItemTile.ItemType.MoneyItem:
                Debug.Log("Itemtype = MoneyItem");
                MoneyItemTile moneyItemTile = (MoneyItemTile)itemTile;
                int value = moneyItemTile.Value;  
                m_gameManager.Bank.WithdrawMoney(value);
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Return true if the player can afford to buy an item tile.
    /// </summary>
    /// <param name="tilePrize"></param>
    /// <returns></returns>
    bool CanAffordTile(int tilePrize)
    {
        return MoneyBalance >= tilePrize;
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

    private int m_moneyBalance = 0;
    public int MoneyBalance
    {
        get { return m_moneyBalance; }
        set
        {
            m_moneyBalance = value;
            m_userInterfaceManager.SetCurrentPlayerMoneyBalance(m_moneyBalance);
        }
    }

    private PlayerState m_playerState = PlayerState.NotActive;
    public PlayerState CurrentPlayerState
    {
        get { return m_playerState; }
    }

    #endregion
}
