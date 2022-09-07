using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace C22_Ex05_OmerSadeh_205964422_NoamFreund_302659875
{
    public class GameForm : Form
    {
        Game m_Game;
        TileForm[] m_BoardButtons;
        Dictionary<int, Image> m_Images;
        Label m_CurrentPlayerName;
        Label m_Player1Score;
        Label m_Player2Score;
        System.Windows.Forms.Timer m_BoardUpdateTimer;
        System.Windows.Forms.Timer m_ComputerTurnTimer;
        bool m_IsPlaying = false;
        int m_ComputerMoveNumber = 0;

        public GameForm(Game i_game)
        {
            m_Game = i_game;
            int length = m_Game.m_Board.GetLength(), width = m_Game.m_Board.GetWidth();
            m_BoardButtons = new TileForm[length * width];
            int currentTileIndex = 0;
            int j = 0;
            m_Images = initializwBoardImages();

            m_BoardUpdateTimer = new System.Windows.Forms.Timer();
            m_BoardUpdateTimer.Interval = 500;
            m_BoardUpdateTimer.Tick += updateBoard;
            m_ComputerTurnTimer = new System.Windows.Forms.Timer();
            m_ComputerTurnTimer.Interval = 1000;
            m_ComputerTurnTimer.Tick += timedComputerTurn;
            int tileSize = 90;

            m_Game.TileShow += showTile;
            m_Game.GameEnding += endGame;

            for (int i = 0; i < length; i++)
            {
                for (j = 0; j < width; j++)
                {
                    TileForm tile = new TileForm(m_Game.m_Board.GetTileValue(currentTileIndex), currentTileIndex);
                    tile.Size = new Size(tileSize, tileSize);
                    tile.Left = i * tileSize + 10 * (i + 1);
                    tile.Top = j * tileSize + 10 * (j + 1);
                    tile.BackgroundImageLayout = ImageLayout.Center;

                    m_BoardButtons[currentTileIndex] = tile;

                    Controls.Add(m_BoardButtons[currentTileIndex]);

                    m_BoardButtons[currentTileIndex].Click += new EventHandler(button_Click);
                    currentTileIndex++;
                }
            }

            m_CurrentPlayerName = new Label();
            if (i_game.m_TurnNumber % 2 == 0)
            {
                m_CurrentPlayerName.BackColor = Color.LightBlue;
            }
            else
            {
                m_CurrentPlayerName.BackColor = Color.LightGreen;
            }

            m_CurrentPlayerName.Text = string.Format("Current player: {0}", i_game.m_PlayerNames[i_game.m_TurnNumber % 2]);
            m_CurrentPlayerName.AutoSize = true;
            m_CurrentPlayerName.Left = 10;
            m_CurrentPlayerName.Top = j * tileSize + 10 * (j + 1);
            Controls.Add(m_CurrentPlayerName);

            m_Player1Score = new Label();
            m_Player1Score.BackColor = Color.LightBlue;
            m_Player1Score.Text = string.Format("{0}: {1} Pairs", i_game.m_PlayerNames[0], i_game.m_PlayerScores[0]);
            m_Player1Score.AutoSize = true;
            m_Player1Score.Left = 10;
            m_Player1Score.Top = m_CurrentPlayerName.Bottom + 10;
            Controls.Add(m_Player1Score);

            m_Player2Score = new Label();
            m_Player2Score.BackColor = Color.LightGreen;
            m_Player2Score.Text = string.Format("{0}: {1} Pairs", i_game.m_PlayerNames[1], i_game.m_PlayerScores[1]);
            m_Player2Score.AutoSize = true;
            m_Player2Score.Left = 10;
            m_Player2Score.Top = m_Player1Score.Bottom + 10;
            Controls.Add(m_Player2Score);

            this.Width = m_BoardButtons[m_BoardButtons.Length - 1].Right + 25;
            this.Height = m_Player2Score.Bottom + 50;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private Dictionary<int, Image> initializwBoardImages()
        {
            Dictionary<int, Image> boardImages = new Dictionary<int, Image>();
            for (int i = 0; i <= (m_BoardButtons.Length / 2) - 1; i++)
            {
                System.Net.WebClient WebbClient = new();
                byte[] bytes = WebbClient.DownloadData("https://picsum.photos/80");
                MemoryStream MemoryStream = new MemoryStream(bytes);
                Image img = Image.FromStream(MemoryStream);
                boardImages.Add(i, img);

            }
            return boardImages;
        }
    
        private void button_Click(object sender, EventArgs e)
        {
            if (!m_IsPlaying && (m_Game.m_TurnNumber % 2 == 0 || m_Game.m_NumberOfPlayers == 2))
            {
                m_IsPlaying = true;
                TileForm senderButton = sender as TileForm;

                m_Game.TileChosen(senderButton.Index);
                m_BoardUpdateTimer.Start();
                if (m_Game.m_NumberOfPlayers == 1 && m_Game.m_TurnNumber % 2 == 1)
                {
                    m_ComputerTurnTimer.Start();
                }
            }
        }

        private void timedComputerTurn(object sender, EventArgs e)
        {
            if (m_ComputerMoveNumber == 0)
            {
                m_Game.ComputerTurn();
                m_BoardUpdateTimer.Start();
                m_ComputerMoveNumber++;
            }
            else if (m_ComputerMoveNumber == 1)
            {
                m_Game.ComputerTurn();
                m_BoardUpdateTimer.Start();
                m_ComputerMoveNumber--;
                if (m_Game.m_TurnNumber % 2 == 0)
                {
                    m_ComputerTurnTimer.Stop();
                }
            }
        }

        private void updateBoard(object sender, EventArgs e)
        {
            for (int i = 0; i < m_BoardButtons.Length; i++)
            {
                switch (m_Game.m_Board.GetTileState(i))
                {
                    case eTileState.Shown:
                        m_BoardButtons[i].BackgroundImage = m_Images[m_BoardButtons[i].Value  - 'A'];
                        break;
                    case eTileState.Hidden:
                        m_BoardButtons[i].BackgroundImage = null;
                        break;
                    case eTileState.Found1:
                        m_BoardButtons[i].BackgroundImage = m_Images[m_BoardButtons[i].Value - 'A'];
                        m_BoardButtons[i].BackColor = Color.LightBlue;
                        break;
                    case eTileState.Found2:
                        m_BoardButtons[i].BackgroundImage = m_Images[m_BoardButtons[i].Value - 'A'];
                        m_BoardButtons[i].BackColor = Color.LightGreen;
                        break;
                }
            }

            m_Player1Score.Text = string.Format("{0}: {1} Pairs", m_Game.m_PlayerNames[0], m_Game.m_PlayerScores[0]);
            m_Player2Score.Text = string.Format("{0}: {1} Pairs", m_Game.m_PlayerNames[1], m_Game.m_PlayerScores[1]);
            m_CurrentPlayerName.Text = string.Format("Current player: {0}", m_Game.m_PlayerNames[m_Game.m_TurnNumber % 2]);
            
            if (m_Game.m_TurnNumber % 2 == 0)
            {
                m_CurrentPlayerName.BackColor = Color.LightBlue;
            }
            else
            {
                m_CurrentPlayerName.BackColor = Color.LightGreen;
            }

            m_IsPlaying = false;
            m_BoardUpdateTimer.Stop();
        }

        private void showTile(int i_TileIndex)
        {
            m_BoardButtons[i_TileIndex].BackgroundImage = m_Images[m_BoardButtons[i_TileIndex].Value - 'A'];
        }

        private void endGame(string i_WinningPlayerName, int i_WinningPlayerScore)
        {
            string message;
            string title;
            DialogResult result;

            if (i_WinningPlayerName == "TIE!")
            {
                message = string.Format("Game Over! Its a: {0}\nDo you want to play a new game?", i_WinningPlayerName);
                title = "Game Over!";
                MessageBoxButtons buttonsTie = MessageBoxButtons.YesNo;
                result = MessageBox.Show(message, title, buttonsTie);
            }
            else
            {
                message = string.Format("Game Over! Winning player: {0}, with score of: {1}!\nDo you want to play a new game?", i_WinningPlayerName, i_WinningPlayerScore);
                title = "Game Over!";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                result = MessageBox.Show(message, title, buttons);
            }
 
            if (result == DialogResult.No)
            {
                m_Game.m_RunningState = false;
            }

            this.Close();
        }
    }
}