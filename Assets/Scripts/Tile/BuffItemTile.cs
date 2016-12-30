using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tile
{
    class BuffItemTile : ItemTile
    {
        private BuffType m_buffTypeValue;

        public enum BuffType
        {
            DoubleDiceValue,
            DiceTimesValue
        }

        public BuffType BuffTypeValue
        {
            get { return m_buffTypeValue; }
            set { m_buffTypeValue = value; }
        }
    }
}
