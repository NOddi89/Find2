using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dice : MonoBehaviour {

    private GameManager m_gameManager;

    private void Awake()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// Call when the roll dice button on the Dice gameobject is clicked
    /// </summary>
    public void RollDice()
    {
        System.Random rand = new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        int diceValue = rand.Next(1, 7);
        Debug.Log("Dice value: " + diceValue);
        m_gameManager.GetComponent<GameManager>().CurrentPlayer.DiceRollValue = diceValue;
    }
}
