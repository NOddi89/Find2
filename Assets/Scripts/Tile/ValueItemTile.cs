using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class ValueItemTile : ItemTile, ITransferableItem
{
    
    public enum ValueItem
    {
        Mobile = 500,
        Tablet = 1000,
        Laptop = 2500
    }

    public void TranferItemData(ItemTile itemTile)
    {
        throw new NotImplementedException();
    }

    private ValueItem m_typeOfValueItem;
    public ValueItem TypeOfValueItem
    {
        get { return m_typeOfValueItem; }
        set { m_typeOfValueItem = value; }
    }


}


