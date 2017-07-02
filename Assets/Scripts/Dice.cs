using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dice : MonoBehaviour
{

    private GameManager m_gameManager;

    #region Events

    //Implement later
    //public delegate void RollDiceMove();
    //public static event RollDiceMove OnRollDiceMove;

    public delegate void DiceBuy(int diceValue);
    public static event DiceBuy OnDiceBuy;

    #endregion

    private void Awake()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    #region Private Methods

    /// <summary>
    /// Call when the roll dice button on the Dice gameobject is clicked
    /// </summary>
    private void PlayerMove()
    {
        int diceValue = NewDiceValue();
        Debug.Log("Dice value: " + diceValue);
        m_gameManager.CurrentPlayer.DiceRollValue = diceValue;
    }

    /// <summary>
    /// Call when the player click on the roll button to try getting the tile for free
    /// </summary>
    private void RollDiceBuy()
    {
        int diceValue = NewDiceValue();

        if(OnDiceBuy != null)
        {
            OnDiceBuy(diceValue);
        }
    }

    /// <summary>
    /// Returns a random number from 1 to 6
    /// </summary>
    /// <returns></returns>
    private int NewDiceValue()
    {
        System.Random rand = new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        return rand.Next(1, 7);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Decide if the current user is rolling for a move or tile
    /// </summary>
    public void RollDice()
    {
        if(m_gameManager.CurrentPlayer.CurrentPlayerState == Player.PlayerState.WaitingForBuyTransaction)
        {
            RollDiceBuy();
        }
        else
        {
            PlayerMove();
        }
    }

    #endregion

}
