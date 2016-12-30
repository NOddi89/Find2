using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTile : Tile
{
    private string m_name = "";
    private ItemType m_tileItemType;

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
        TargetItem
    }

    override public void Awake()
    {
        // Calculate the player position
        m_collider = GetComponentInChildren<Collider>();
        m_tilePlayerPos = m_collider.bounds.center + new Vector3(0f, 0.55f, 0f); // Adding some of the players hight to get him above the ground

        //Other setting
        TileID = -1;
        IsItemTile = true;

    }

    /// <summary>
    /// Property to holde the tiles name to display for the user
    /// </summary>
    public string Name
    {
        get { return m_name; }
        set { m_name = value; }
    }

    /// <summary>
    /// Item type of the tile
    /// </summary>
    public ItemType TileItemType
    {
        get { return m_tileItemType; }
        set { m_tileItemType = value; }
    }
}
