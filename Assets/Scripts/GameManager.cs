using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Transform tileManager;
    public Transform[] players;

    private TileManager m_tileManager;
    private List<ItemTile> m_playerStartingPositions;

    public void Awake()
    {
        m_tileManager = tileManager.GetComponent<TileManager>();
        m_playerStartingPositions = m_tileManager.GetPlayerStartingPositions();
    }
}