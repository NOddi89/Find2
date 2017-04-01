using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTileDeployment
{
    #region Variables

    private int m_percentageOfMoneyitems = 100;
    private int m_percentageOfHundreds = 30;
    private int m_percentageOfTwohundreds = 30;
    private int m_percentageOfFivehundreds = 20;
    private int m_percentageOfThounsands = 20;

    // TODO: Implement when the map is bigger
    //private int numberOfValueItems = 0;
    //private int percentageOfPhones = 0;
    //private int percentageOfTablets = 0;
    //private int percentageOfLaptops = 0;

    //private int numberOfBuffItems = 0;
    //private int percentageOfDoubleDiceValue = 0;
    //private int percentageOfDiceCurrencyvalue = 0;

    //private int numberOfNerfItems = 0;
    //private int percentageOfOneStepNextTurn = 0;
    //private int percentageOfTheifs = 0;

    //private int numberOfSpecialItems = 0;
    //private int percentageOfBlanks = 0;
    //private int percentageOfFirstPlayer = 0;

    private int m_numberOfItems = 0;


    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="totalNumerOfItemTiles"></param>
    public ItemTileDeployment(int totalNumerOfItemTiles)
    {
        m_numberOfItems = totalNumerOfItemTiles;
        CalculateItems();
    }

    /// <summary>
    /// Calculate how many items we should deploy of each type
    /// </summary>
    private void CalculateItems()
    {
        CreateMoneyItems();
    }

    /// <summary>
    /// Create money items
    /// </summary>
    private void CreateMoneyItems()
    {
        int numberOfMoneyItemTiles = PercentageOfNumber(m_percentageOfMoneyitems, m_numberOfItems);

        m_moneyItemsDeployment.Add("100", PercentageOfNumber(m_percentageOfHundreds, numberOfMoneyItemTiles));
        m_moneyItemsDeployment.Add("200", PercentageOfNumber(m_percentageOfTwohundreds, numberOfMoneyItemTiles));
        m_moneyItemsDeployment.Add("500", PercentageOfNumber(m_percentageOfFivehundreds, numberOfMoneyItemTiles));
        m_moneyItemsDeployment.Add("1000", PercentageOfNumber(m_percentageOfThounsands, numberOfMoneyItemTiles));
    }

    /// <summary>
    /// Calculate the percentage of a given number and return the result rounded to nearest integer
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="numberOfItems"></param>
    /// <returns></returns>
    private int PercentageOfNumber(int percent, int numberOfItems)
    {
        float percentageOf = ((percent * numberOfItems) / 100.0f);
        return (int)Mathf.Round(percentageOf);
    }


    #region Properties

    private Dictionary<string, int> m_moneyItemsDeployment = new Dictionary<string, int>();
    public Dictionary<string, int> MoneyItemDeployment
    {
        get { return m_moneyItemsDeployment; }
    }

    #endregion

}
