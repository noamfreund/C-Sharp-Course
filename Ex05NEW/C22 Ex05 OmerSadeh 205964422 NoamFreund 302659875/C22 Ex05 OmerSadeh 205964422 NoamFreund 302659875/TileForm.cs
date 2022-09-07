using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C22_Ex05_OmerSadeh_205964422_NoamFreund_302659875
{
    internal class TileForm : Button
    {
        private char m_Value;
        private int m_Index;
        public TileForm(char i_Value, int i_Index)
        {
            m_Value = i_Value;
            m_Index = i_Index;
        }

        public char Value
        {
            get { return m_Value; }
        }

        public int Index
        {
            get { return m_Index; }
        }
    }
}