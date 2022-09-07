namespace Ex02
{
    public class Game
    {

        private GameBoard m_Board;
        private bool m_RunningState;
        private string[] m_PlayerNames = new string[2];
        private int[] m_PlayerScores = new int[2];
        private int m_NumberOfPlayers;
        private int m_TurnNumber;

        public static void Main()
        {
            StartGame();
        }

        public static void StartGame()
        {
            Game theGame = UI.InitializeGame();
            UI.ShowBoard(theGame.m_Board);

            while (theGame.m_RunningState)
            {
                if (theGame.m_Board.CheckWinState())
                {
                    theGame.EndGame();
                }
                else
                {
                    if ((theGame.m_TurnNumber % 2 == 1) && (theGame.m_NumberOfPlayers < 2))
                    {
                        theGame.computerTurn();
                    }
                    else
                    {
                        theGame.playerTurn(theGame.m_TurnNumber % 2);
                    }
                    theGame.m_TurnNumber++;
                }
            }
            Ex02.ConsoleUtils.Screen.Clear();
            System.Console.WriteLine("GoodBye");
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

        private void computerTurn()
        {
            bool computerTurn = true;

            while (computerTurn && !m_Board.CheckWinState())
            {
                Thread.Sleep(1000);
                int firstTileToShow = chooseRandomAvailableTile();
                m_Board.SetTileState(firstTileToShow, eTileState.Shown);
                UI.ShowBoard(m_Board);
                Thread.Sleep(1000);
                int secondTileToShow = chooseRandomAvailableTile();
                m_Board.SetTileState(secondTileToShow, eTileState.Shown);
                UI.ShowBoard(m_Board);
                computerTurn = checkTiles(firstTileToShow, secondTileToShow, 1);
                UI.ShowBoard(m_Board);
            }
        }

        private void playerTurn(int i_PlayerNumber)
        {
            bool playerTurn = true;

            while (playerTurn && !m_Board.CheckWinState())
            {
                int firstTileToShow = UI.ChooseTile(m_Board);
                m_Board.SetTileState(firstTileToShow, eTileState.Shown);
                UI.ShowBoard(m_Board);
                int secondTileToShow = UI.ChooseTile(m_Board);
                m_Board.SetTileState(secondTileToShow, eTileState.Shown);
                UI.ShowBoard(m_Board);
                playerTurn = checkTiles(firstTileToShow, secondTileToShow, i_PlayerNumber);
                UI.ShowBoard(m_Board);
            }
        }

        private int chooseRandomAvailableTile()
        {
            List<int> availableIndexes = new List<int>();
            Random random = new Random();

            for (int i = 0; i < m_Board.GetSize(); i++)
            {
                if(m_Board.GetTileState(i) == eTileState.Hidden)
                {
                    availableIndexes.Add(i);
                }
            }

            return availableIndexes[random.Next(availableIndexes.Count)];
        }

        public  void EndGame()
        {
            string winningPlayer;
            int winningPoints;

            if(m_PlayerScores[0] >= m_PlayerScores[1])
            {
                winningPlayer = m_PlayerNames[0];
                winningPoints = m_PlayerScores[0];
            }
            else
            {
                winningPlayer = m_PlayerNames[1];
                winningPoints = m_PlayerScores[1];
            }
            UI.ShowGameResults(winningPlayer, winningPoints);
            if(UI.NewGame())
            {
                m_Board = UI.ResetBoard();
                m_PlayerScores[0] = 0;
                m_PlayerScores[1] = 0;
                m_TurnNumber = 0;
                UI.ShowBoard(m_Board);
            }
            else
            {
                m_RunningState = false;
            }
        }

        private bool checkTiles(int i_Tile1, int i_Tile2, int i_PlayerNumber)
        {
            bool matchResult = true;

            if(m_Board.CheckTilesMatch(i_Tile1, i_Tile2))
            {
                m_Board.SetTileState(i_Tile1, eTileState.Found);
                m_Board.SetTileState(i_Tile2, eTileState.Found);
                m_PlayerScores[i_PlayerNumber]++;
            }
            else
            {
                Thread.Sleep(2000);
                m_Board.SetTileState(i_Tile1, eTileState.Hidden);
                m_Board.SetTileState(i_Tile2, eTileState.Hidden);
                matchResult = false;
            }

            return matchResult;
        }
    }
}
