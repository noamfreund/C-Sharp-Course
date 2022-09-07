using System.Text;

namespace Ex02
{
    internal class UI
    {
        public static Game InitializeGame()
        {
            string firstPlayerField = getPlayerName("your");
            string secondPlayerField;
            int numberOfPlayer  = getNumberOfPlayers();

            if (numberOfPlayer == 2)
            {
                secondPlayerField = getPlayerName("the second player's");
            }
            else
            {
                secondPlayerField = "Computer";
            }

            int[] sizeOfBoard  = getBoardSize();
            int boardLength = sizeOfBoard[0];
            int boardWidth = sizeOfBoard[1];

            return new Game(numberOfPlayer, firstPlayerField, secondPlayerField, boardLength, boardWidth);
        }
       
        public static GameBoard ResetBoard()
        {
            int[] sizeOfBoard = getBoardSize();
            int boardLength = sizeOfBoard[0];
            int boardWidth = sizeOfBoard[1];

            return new GameBoard(boardLength, boardWidth);
        }

        public static void ShowBoard(GameBoard i_CurrentGameBoard)
        {
            int boardLength = i_CurrentGameBoard.GetLength();
            int boardWidth = i_CurrentGameBoard.GetWidth();
            int amountOfEqualSigns = (boardWidth * 4) + 1;
            string equalSignsLine = string.Format("  {0}", new string('=', amountOfEqualSigns));
            int currentTileIndex = 0;

            ConsoleUtils.Screen.Clear();
            printBoardTopRow(boardWidth);
            for (int i = 0; i < boardLength; i++)
            {
                Console.WriteLine(equalSignsLine);
                string beginningOfRow = string.Format("{0} |", i + 1);
                Console.Write(beginningOfRow);
                for (int j = 0; j < boardWidth; j++)
                {
                    eTileState currentTileState = i_CurrentGameBoard.GetTileState(currentTileIndex);
                    char currentTileValue = i_CurrentGameBoard.GetTileValue(currentTileIndex);
                    string cellToPrint = string.Format(" {0} |", (currentTileState == eTileState.Hidden) ? ' ' : currentTileValue);
                    currentTileIndex++;
                    Console.Write(cellToPrint);
                }
                
                Console.WriteLine();
            }

            Console.WriteLine(equalSignsLine);
        }

        public static bool NewGame()
        {
            bool loopRunning = true;
            bool answerRecieved = true;
            string playerAnswer;

            while (loopRunning)
            {
                Console.WriteLine("Do you wish to play again? (Y/N)");
                playerAnswer = Console.ReadLine();
                if (playerAnswer.Equals("Y") || playerAnswer.Equals("y"))
                {
                    answerRecieved = true;
                    loopRunning = false;
                }
                else if (playerAnswer.Equals("N") || playerAnswer.Equals("n"))
                {
                    answerRecieved = false;
                    loopRunning = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid answer!");
                }
            }

            return answerRecieved;
        } 

        private static int getNumberOfPlayers()
        {
            bool waitingForNumberOfPlayers = true;
            int numberChoosen = 0;
            string numberOfPlayerschoosen;

            while (waitingForNumberOfPlayers)
            {
                Console.WriteLine("How many players are playing 1/2?");
                numberOfPlayerschoosen = Console.ReadLine();
                int.TryParse(numberOfPlayerschoosen, out numberChoosen);

                if (numberChoosen == 1 || numberChoosen == 2)
                {
                    waitingForNumberOfPlayers = false;
                }
                else
                {
                    Console.WriteLine("Please enter 1 or 2");
                }
            }
            return numberChoosen;
        }

        private static int[] getBoardSize()
        {
            bool waitingForLength = true;
            bool waitingForWidth = true;
            bool matrixIsOdd = true;
            string numberChoosenByPlayer;
            int numberChoosen;
            int[] lenghtAndWidthArray = new int[2];

            while (waitingForLength)
            {
                Console.WriteLine("Choose a length between 4 and 6:");
                numberChoosenByPlayer = Console.ReadLine();
                int.TryParse(numberChoosenByPlayer, out numberChoosen);

                if (numberChoosen >= 4 && numberChoosen <= 6)
                {
                    lenghtAndWidthArray[0] = numberChoosen;
                    waitingForLength = false;
                }
                else
                {
                    Console.WriteLine("Not a valid number!");
                }
            }

            while (waitingForWidth)
            {
                Console.WriteLine("Choose a width between 4 and 6:");
                numberChoosenByPlayer = System.Console.ReadLine();
                int.TryParse(numberChoosenByPlayer, out numberChoosen);

                if (numberChoosen >= 4 && numberChoosen <= 6)
                {
                    while (matrixIsOdd)
                    {
                        if (numberChoosen == 5 && lenghtAndWidthArray[0] == 5)
                        {
                            Console.WriteLine("Please choose 4 or 6 but not 5:");
                            numberChoosenByPlayer = System.Console.ReadLine();
                            int.TryParse(numberChoosenByPlayer, out numberChoosen);
                        }
                        else
                        {
                            matrixIsOdd = false;
                        }
                    }

                    lenghtAndWidthArray[1] = numberChoosen;
                    waitingForWidth = false;
                }
                else
                {
                    Console.WriteLine("Not a valid number!");
                }
            }

            return lenghtAndWidthArray;
        }

        private static string getPlayerName(string i_PlayerNumber)
        {
            bool waitingForName = true;
            string nameOfPlayer = "";

            while (waitingForName)
            {
                Console.WriteLine("Enter " + i_PlayerNumber + " name:");
                nameOfPlayer = Console.ReadLine();
                if (nameOfPlayer.Equals(""))
                {
                    Console.WriteLine("Please enter a name!");
                }
                else
                {
                    waitingForName = false;
                }
            }

            return nameOfPlayer;
        }

        public static int ChooseTile(GameBoard i_Board)
        {
            bool waitingForInput = true;
            string tileChoosenByPlayer =""; 
            char convertedLetter;
            int convertedDigit;
            int finalTilechoosen = 0;

            while (waitingForInput)
            {
                Console.WriteLine("Choose a Tile or Press Q to Quit:");
                tileChoosenByPlayer = Console.ReadLine();
                if (tileChoosenByPlayer == "Q" || tileChoosenByPlayer == "q")
                {
                    finishGame();
                    
                }
                else if (tileChoosenByPlayer.Length != 2)
                {
                    Console.WriteLine("Your Input must be 2 characters long!");
                    Console.WriteLine("Example: A1 or B1");
                }
                else
                {
                    convertedLetter = char.ToUpper(tileChoosenByPlayer[0]);
                    convertedDigit = (int)char.GetNumericValue(tileChoosenByPlayer[1]);
                    
                    if (convertedLetter >= 65 && convertedLetter <= 65 + i_Board.GetWidth())
                    {
                        if (convertedDigit >= 1 && convertedDigit <= i_Board.GetWidth())
                        {
                            finalTilechoosen = convertedLetter - 65 + i_Board.GetWidth() * (convertedDigit - 1);
                            if (TileHasBeenUsed(i_Board, finalTilechoosen) == false)
                            {
                                waitingForInput = false;
                            }
                            else
                            {
                                Console.WriteLine("This Tile has been choosen allready please choose a different one");
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Second character should be an integer from 1-" + i_Board.GetLength() + "!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("First character should be a letter from A-" + (char)(i_Board.GetWidth() + 64) + "!");
                    }
                }
                
            }

            return finalTilechoosen;
        }

        public static bool TileHasBeenUsed(GameBoard i_Board, int i_TileChoosen )
        {
            bool tileHasBeenUsed = true;
            if (i_Board.GetTileState(i_TileChoosen) == eTileState.Hidden)
            {
                tileHasBeenUsed = false;
            }
            return tileHasBeenUsed;
        }
        
        public static void ShowGameResults(string i_WinningPlayerName, int i_WinningPlayerScore)
        {
            Console.WriteLine("The Winner for this game is " + i_WinningPlayerName + " and his/her score is " + i_WinningPlayerScore + ".");
        }

        private static void printBoardTopRow (int i_LengthOfRow)
        {
            StringBuilder topRowPrint = new StringBuilder(" ");

            for (int i = 0; i < i_LengthOfRow; i++)
            {
                topRowPrint.Append(string.Format("   {0}", (char)(i + 'A')));
            }

            Console.WriteLine(topRowPrint.ToString());
        }

        private static void finishGame()
        {
            Console.WriteLine("Goodbye!");
            Thread.Sleep(1000);
            Environment.Exit(0);
        }

    }
}

