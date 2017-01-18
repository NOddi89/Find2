using System.Collections;
using System.Collections.Generic;

public class Bank
{
    private int m_playerStartCapital;
    private int m_totalMoney;

    public Bank(int bankVaultTotal, int playerStartCapital)
    {
        m_totalMoney = bankVaultTotal;
        m_playerStartCapital = playerStartCapital;
    }

    /// <summary>
    /// Withdraw 'value' amount of money from the bank if the total
    /// amount of money in the bank is big enough
    /// </summary>
    /// <param name="value"></param>
    /// <returns>-1 if there is no more money in the bank</returns>
    public int Withdraw(int value)
    {
        int withdraw = -1;

        if((m_totalMoney - value) >= 0)
        {
            withdraw = value;
            m_totalMoney -= withdraw;
        }

        return withdraw;
    }

    /// <summary>
    /// Returning the amout of money a player should start the game with
    /// </summary>
    /// <returns></returns>
    public int GetPlayerStartCapital()
    {
        int capital = -1;

        if(m_totalMoney - m_playerStartCapital >= 0)
        {
            capital = m_playerStartCapital;
            m_totalMoney -= capital;
        }
        return m_playerStartCapital;
    }
}
