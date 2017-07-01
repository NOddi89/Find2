using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bank : MonoBehaviour
{
    public int playerStartCapital;
    public int totalMoney;

    private GameManager m_gameManager;

    public void Awake()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// Withdraw 'value' amount of money from the bank if the total
    /// amount of money in the bank is big enough
    /// </summary>
    /// <param name="value"></param>
    /// <returns>-1 if there is no more money in the bank</returns>
    public int WithdrawMoney(int value)
    {
        int withdraw = -1;

        if((totalMoney - value) >= 0)
        {
            withdraw = value;
            totalMoney -= withdraw;
            m_gameManager.CurrentPlayer.MoneyBalance += withdraw;
        }

        return withdraw;
    }

    /// <summary>
    /// Deposit money into the bank
    /// </summary>
    /// <param name="deposit"></param>
    /// <returns></returns>
    public bool DepositMoney(int deposit)
    {
        bool transactionOk = false;

        if(deposit > 0)
        {
            totalMoney += deposit;
            m_gameManager.CurrentPlayer.MoneyBalance -= deposit;
            transactionOk = true;
        }

        return transactionOk;
    }

    /// <summary>
    /// Returning the amout of money a player should start the game with
    /// </summary>
    /// <returns></returns>
    public int GetPlayerStartCapital()
    {
        int capital = -1;

        if(totalMoney - playerStartCapital >= 0)
        {
            capital = playerStartCapital;
            totalMoney -= capital;
        }
        return playerStartCapital;
    }
}
