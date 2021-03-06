﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTile : Tile
{
    #region Variables

    /// <summary>
    /// Enum used to set the type of item the tile consist of
    /// </summary>
    public enum ItemType
    {
        ValueItem,
        MoneyItem,
        BuffItem,
        NerfItem,
        SpecialItem,
        StartItem,
        TargetItem
    }

    /// <summary>
    /// Referance to the child transform that holds the item visible for the player
    /// </summary>
    public Transform item;
    
    #endregion

    #region Monobehaviour

    override public void Awake()
    {
        // Calculate the player position
        m_collider = GetComponentInChildren<Collider>();
        m_tilePlayerPos = m_collider.bounds.center + new Vector3(0f, 0.55f, 0f); // Adding some of the players hight to get him above the ground

        //Other setting
        TileID = -1;
        IsItemTile = true;      
    }

    public void Start()
    {
        if (IsStartTile)
        {
            SetColor(Color.red);
        }
        else
        {
            SetColor(Color.blue);
        }
    }

    #endregion

    #region Get/Set

    /// <summary>
    /// Set the color of the tile
    /// </summary>
    /// <param name="color"></param>
    public void SetColor(Color color)
    {
        //m_tileItemType = ItemType.StartItem;

        Transform[] childs = GetComponentsInChildren<Transform>();

        foreach (Transform child in childs)
        {
            if (child.name == "Item")
            {
                child.GetComponent<MeshRenderer>().material.color = color;
            }
        }
    }

    #endregion

    #region Properties
    /// <summary>
    /// Property to holde the tiles name to display for the user
    /// </summary>
    public string locationName;
    public string Name
    {
        get { return locationName; }
        set { locationName = value; }
    }

    /// <summary>
    /// Item type of the tile
    /// </summary>
    private ItemType m_tileItemType;
    public ItemType TileItemType
    {
        get { return m_tileItemType; }
        set { m_tileItemType = value; }
    }

    /// <summary>
    /// True if the tile is a starting tile
    /// </summary>
    public bool m_isStartTile = false;
    public bool IsStartTile
    {
        get { return m_isStartTile; }
        set { m_isStartTile = value; }
    }

    /// <summary>
    /// What is cost to buy the tile
    /// </summary>
    private int m_tilePrize = 300;
    public int TilePrize
    {
        get { return m_tilePrize; }
        set { m_tilePrize = value; }
    }

    /// <summary>
    /// Does the item have the item, or is it already taken
    /// </summary>
    public bool m_hasItem = false;
    public bool HasItem
    {
        get { return m_hasItem; }
        set { m_hasItem = value; }
    }

    #endregion
}
