namespace Ex02
{
    public enum eTileState
    {
        Hidden, Shown, Found
    }
    public class Tile
    {
        private char m_Value;
        private eTileState m_TileState;

        public Tile(char i_Value)
        {
            m_Value = i_Value;
            m_TileState = eTileState.Hidden;
        }

        public eTileState GetTileState()
        {
            return m_TileState;
        }

        public char GetTileValue()
        {
            return m_Value;
        }

        public void SetTileState(eTileState i_TileState)
        {
            m_TileState = i_TileState;
        }
    }
}
