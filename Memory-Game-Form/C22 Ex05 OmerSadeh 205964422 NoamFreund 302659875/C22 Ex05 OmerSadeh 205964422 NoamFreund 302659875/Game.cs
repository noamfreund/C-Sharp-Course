namespace C22_Ex05_OmerSadeh_205964422_NoamFreund_302659875
{
    public delegate void TileActions(int i_TileIndex);
    public delegate void GameEndActions(string i_WinningPlayerName, int i_WinningPlayerScore);
    public class Game
    {
        public GameBoard m_Board;
        public bool m_RunningState;
        public string[] m_PlayerNames = new string[2];
        public int[] m_PlayerScores = new int[2];
        public int m_NumberOfPlayers;
        public int m_TurnNumber;
        private int m_PairPartsChosen = 0;
        private int m_FirstPairChoiceIndex;
        public event TileActions TileShow;
        public event GameEndActions GameEnding;

        public static void StartGame()
        {
            bool running = true;

            while (running)
            {
                GameFormInitialize initGame = new GameFormInitialize();
                initGame.MaximizeBox = false;
                initGame.MinimizeBox = false;
                initGame.ShowDialog();

                if (initGame.ReturnIfToStartGame())
                {
                    int[] sizeOfBoard = GetBoardSize(initGame.ReturnBoardSize());
                    int boardLength = sizeOfBoard[0];
                    int boardWidth = sizeOfBoard[1];

                    Game theGame = new Game(initGame.ReturnNumberOfPlayer(), initGame.GetFirstPlayerName(), initGame.GetSecondPlayerName(), boardLength, boardWidth);
                    GameForm gameRun = new GameForm(theGame);
                    gameRun.MaximizeBox = false;
                    gameRun.MinimizeBox = false;
                    gameRun.StartPosition = FormStartPosition.CenterScreen;
                    gameRun.ShowDialog();
                    if (!theGame.m_RunningState)
                    {
                        running = false;
                    }
                }
                else
                {
                    running = false;
                }
            }
        }

        public Game(int i_NumberOfPlayers, string i_PlayerName1, string i_PlayerName2, int i_BoardWidth, int i_BoardLength)
        {
            m_PlayerNames[0] = i_PlayerName1;
            m_PlayerNames[1] = i_PlayerName2;
            m_Board = new GameBoard(i_BoardWidth, i_BoardLength);
            m_RunningState = true;
            m_PlayerScores[0] = 0;
            m_PlayerScores[1] = 0;
            m_NumberOfPlayers = i_NumberOfPlayers;
            m_TurnNumber = 0;
        }

        public void ComputerTurn()
        {
            TileChosen(chooseRandomAvailableTile());
        }

        public void TileChosen(int i_TileIndex)
        {
            if (m_Board.GetTileState(i_TileIndex) == eTileState.Hidden)
            {
                m_Board.SetTileState(i_TileIndex, eTileState.Shown);
                TileShow.Invoke(i_TileIndex);
                m_PairPartsChosen++;
                if (m_PairPartsChosen == 1)
                {
                    m_FirstPairChoiceIndex = i_TileIndex;
                }
                else if (m_PairPartsChosen == 2)
                {
                    bool playerTurn = checkTiles(m_FirstPairChoiceIndex, i_TileIndex, m_TurnNumber % 2);
                    m_PairPartsChosen = 0;

                    if (!playerTurn)
                    {
                        m_TurnNumber++;
                    }
                }

                if (m_Board.CheckWinState())
                {
                    EndGame();
                }
            }
        }

        private int chooseRandomAvailableTile()
        {
            List<int> availableIndexes = new List<int>();
            Random random = new Random();

            for (int i = 0; i < m_Board.GetSize(); i++)
            {
                if (m_Board.GetTileState(i) == eTileState.Hidden)
                {
                    availableIndexes.Add(i);
                }
            }

            return availableIndexes[random.Next(availableIndexes.Count)];
        }

        public void EndGame()
        {
            string winningPlayer;
            int winningPoints;

            if (m_PlayerScores[0] > m_PlayerScores[1])
            {
                winningPlayer = m_PlayerNames[0];
                winningPoints = m_PlayerScores[0];
            }
            else if (m_PlayerScores[0] < m_PlayerScores[1])
            {
                winningPlayer = m_PlayerNames[1];
                winningPoints = m_PlayerScores[1];
            }
            else
            {
                winningPlayer = "TIE!";
                winningPoints = m_PlayerScores[0];
            }

            GameEnding.Invoke(winningPlayer, winningPoints);
        }

        private bool checkTiles(int i_Tile1, int i_Tile2, int i_PlayerNumber)
        {
            bool matchResult = true;

            if (m_Board.CheckTilesMatch(i_Tile1, i_Tile2))
            {
                if (i_PlayerNumber == 0)
                {
                    m_Board.SetTileState(i_Tile1, eTileState.Found1);
                    m_Board.SetTileState(i_Tile2, eTileState.Found1);
                }
                else
                {
                    m_Board.SetTileState(i_Tile1, eTileState.Found2);
                    m_Board.SetTileState(i_Tile2, eTileState.Found2);
                }
                m_PlayerScores[i_PlayerNumber]++;
            }
            else
            {
                m_Board.SetTileState(i_Tile1, eTileState.Hidden);
                m_Board.SetTileState(i_Tile2, eTileState.Hidden);
                matchResult = false;
            }

            return matchResult;
        }

        private static int[] GetBoardSize(string i_UserInput)
        {
            int[] BoardSize = new int[2];
            char[] charArr = i_UserInput.ToCharArray();

            BoardSize[0] = (int)charArr[0] - '0';
            BoardSize[1] = (int)charArr[2] - '0';

            return BoardSize;
        }
    }
}