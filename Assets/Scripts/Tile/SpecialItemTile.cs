using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class SpecialItemTile : ItemTile, ITransferableItem
{
    
    public enum SpecialType
    {
        FirstPlayer,
        Blank
    }

    public void TranferItemData(ItemTile itemTile)
    {
        throw new NotImplementedException();
    }

    private SpecialType m_specialTypeValue;
    public SpecialType SpecialTypeValue
    {
        get { return m_specialTypeValue; }
        set { m_specialTypeValue = value; }
    }
}
