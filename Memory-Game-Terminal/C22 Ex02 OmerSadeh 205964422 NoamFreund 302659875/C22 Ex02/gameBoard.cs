namespace Ex02
{
    public class GameBoard
    {
        private int m_Width = 0;
        private int m_Length = 0;
        private Tile[] m_Board;

        
        private char[] initializeBoardChar()
        {
            char[] boardChar = new char[m_Width * m_Length];
            char charToAdd = 'A';
            for (int i = 0; i < boardChar.Length-1; i++)
            {
                boardChar[i] = charToAdd;
                boardChar[i + 1] = charToAdd;
                charToAdd++;
                i++;
            }
            return boardChar;
        }

        
        public GameBoard(int i_Width, int i_Length)
        {
            m_Width = i_Width;
            m_Length = i_Length;
            char[] charsToShuffle = initializeBoardChar();
            int n = charsToShuffle.Length;
            char[] temp = new char[1];
            Random random = new Random();
            char[] shuffledChars = new char[charsToShuffle.Length];

            for (int i = charsToShuffle.Length; i >= 1; i--)
            {
                int randomnumber = random.Next(1, i + 1) - 1;
                shuffledChars[i - 1] = charsToShuffle[randomnumber];
                charsToShuffle[randomnumber] = charsToShuffle[i - 1];
            }
            m_Board = new Tile[m_Width * m_Length];
            for (int i = 0; i < shuffledChars.Length; i++)
            {
                m_Board[i] = new Tile(shuffledChars[i]);
            }
        }

        public int GetLength()
        {
            return m_Length;
        }

        public int GetWidth()
        {
            return m_Width;
        }

        public int GetSize()
        {
            return m_Width * m_Length;
        }

        public bool CheckWinState()
        {
            bool result = true;

            for (int i = 0; i < m_Board.Length; i++)
            {
                if(m_Board[i].GetTileState() == eTileState.Hidden)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public void SetTileState(int i_TileIndex, eTileState i_TileState)
        {
                m_Board[i_TileIndex].SetTileState(i_TileState);
        }

        public eTileState GetTileState(int i_TileIndex)
        {
            return m_Board[i_TileIndex].GetTileState();
        }

        public char GetTileValue(int i_TileIndex)
        {
            return m_Board[i_TileIndex].GetTileValue();
        }

        public bool CheckTilesMatch(int i_TileIndex1, int i_TileIndex2)
        {
            return m_Board[i_TileIndex1].GetTileValue() == m_Board[i_TileIndex2].GetTileValue();
        }
    }
}