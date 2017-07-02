using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class MoneyItemTile : ItemTile, ITransferableItem
{
    /// <summary>
    /// Transfer itemTile data to moneyItemTile
    /// </summary>
    /// <param name="itemTile"></param>
    public void TranferItemData(ItemTile itemTile)
    {
        this.TileID = itemTile.TileID;
        this.TilePlayerPos = itemTile.TilePlayerPos;
        this.neighborTiles = itemTile.neighborTiles;
        this.item = itemTile.item;
        this.IsItemTile = true;
        this.locationName = itemTile.locationName;
        this.TileItemType = ItemTile.ItemType.MoneyItem;
        this.IsStartTile = false;
        this.HasItem = itemTile.HasItem;
    }

    private int m_value;
    public int Value
    {
        get { return m_value; }
        set { m_value = value; }
    }
}

