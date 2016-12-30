using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tile
{
    class SpecialItemTile : ItemTile
    {
        private SpecialType m_specialTypeValue;

        public enum SpecialType
        {
            Start,
            FirstPlayer,
            Blank
        }

        public SpecialType SpecialTypeValue
        {
            get { return m_specialTypeValue; }
            set { m_specialTypeValue = value; }
        }
    }
}
