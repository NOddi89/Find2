using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tile
{
    class ValueItemTile : ItemTile
    {
        private ValueItem m_typeOfValueItem;

        public enum ValueItem
        {
            Mobile = 500,
            Tablet = 1000,
            Laptop = 2500
        }

        public ValueItem TypeOfValueItem
        {
            get { return m_typeOfValueItem; }
            set { m_typeOfValueItem = value; }
        }
    }
}
