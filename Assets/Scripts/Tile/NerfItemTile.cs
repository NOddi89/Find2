using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class NerfItemTile : ItemTile, ITransferableItem
{
    public enum NerfType
    {
        OneStep,
        Theif
    }

    public void TranferItemData(ItemTile itemTile)
    {
        throw new NotImplementedException();
    }

    private NerfType m_nerfTypeValue;
    public NerfType NerfTypeValue
    {
        get { return m_nerfTypeValue; }
        set { m_nerfTypeValue = value; }
    }

}

