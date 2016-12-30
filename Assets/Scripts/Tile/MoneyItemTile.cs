using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tile
{
    class MoneyItemTile : ItemTile
    {
        private int m_value;

        public int Value
        {
            get { return m_value; }
            set { m_value = value; }
        }
    }
}
