using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tile
{
    class NerfItemTile : ItemTile
    {
        private NerfType m_nerfTypeValue;

        public enum NerfType
        {
            OneStep,
            Theif
        }

        public NerfType NerfTypeValue
        {
            get { return m_nerfTypeValue; }
            set { m_nerfTypeValue = value; }
        }
    }
}
