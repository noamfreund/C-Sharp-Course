using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using static System.Windows.Forms.DataFormats;
using System.Diagnostics.Metrics;
using Microsoft.VisualBasic.Devices;

namespace C22_Ex05_OmerSadeh_205964422_NoamFreund_302659875
{
    public delegate void ActionSelectedHandler2();

    public class GameFormInitialize : Form
    {
        Button m_ButtonAgainstComp;
        Button m_StartGame;
        Button m_BoardSize;
        Label m_FirstPlayerLabel;
        Label m_SecondPlayerLabel;
        Label m_BoarsSizeLabel;
        TextBox m_FirstPlayerTextBox;
        TextBox m_SecondPlayerTextBox;
        int m_NumberOfPlayer = 2;
        string[] BoardSizeOptionArray = { "4X4", "4X5", "4X6", "5X4", "5X6", "6X4", "6X5", "6X6" };
        int m_Count = 0;
        bool m_True = false;



        public GameFormInitialize()       
        {
            this.Text = "Memory Game - Settings";

            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Size = new Size(420, 230);
            this.StartPosition = FormStartPosition.CenterScreen;

            m_FirstPlayerLabel = new Label();
            m_FirstPlayerLabel.Text = "First Player name:";
            m_FirstPlayerLabel.Width = 120;
            m_FirstPlayerLabel.Left = 10;
            m_FirstPlayerLabel.Top = 20;
            this.Controls.Add(m_FirstPlayerLabel);
            
            m_FirstPlayerTextBox = new TextBox();
            m_FirstPlayerTextBox.Text = "Enter name";
            m_FirstPlayerTextBox.Width = m_FirstPlayerLabel.Width;
            m_FirstPlayerTextBox.Left = m_FirstPlayerLabel.Right + 10;
            m_FirstPlayerTextBox.Top = 20;
            m_FirstPlayerTextBox.Height = 20;
            this.Controls.Add(m_FirstPlayerTextBox);

            m_SecondPlayerLabel = new Label();
            m_SecondPlayerLabel.Text = "Second player name:";
            m_SecondPlayerLabel.Width = m_FirstPlayerTextBox.Width;
            m_SecondPlayerLabel.Left = m_FirstPlayerLabel.Left;
            m_SecondPlayerLabel.Top = m_FirstPlayerLabel.Bottom + 20;
            this.Controls.Add(m_SecondPlayerLabel);
           
            m_SecondPlayerTextBox = new TextBox();
            m_SecondPlayerTextBox.Text = "Enter name";
            m_SecondPlayerTextBox.Width = m_FirstPlayerTextBox.Width;
            m_SecondPlayerTextBox.Left = m_FirstPlayerTextBox.Left;
            m_SecondPlayerTextBox.Top = m_SecondPlayerLabel.Top;
            m_FirstPlayerTextBox.Height = m_FirstPlayerTextBox.Height;
            this.Controls.Add(m_SecondPlayerTextBox);

            m_ButtonAgainstComp = new Button();
            m_ButtonAgainstComp.Text = "Against Computer";
            m_ButtonAgainstComp.Width = m_FirstPlayerLabel.Width;
            m_ButtonAgainstComp.Left = m_SecondPlayerTextBox.Right +10;
            m_ButtonAgainstComp.Top = m_SecondPlayerLabel.Top;
            m_ButtonAgainstComp.Height = m_FirstPlayerTextBox.Height;
            this.Controls.Add(m_ButtonAgainstComp);
            m_ButtonAgainstComp.Click += new EventHandler(ChooseNumberOfPlayers);

            m_BoarsSizeLabel = new Label();
            m_BoarsSizeLabel.Text = "Board Size:";
            m_BoarsSizeLabel.Width = m_FirstPlayerTextBox.Width;
            m_BoarsSizeLabel.Left = m_FirstPlayerLabel.Left;
            m_BoarsSizeLabel.Top = m_SecondPlayerLabel.Bottom + 10 ;
            this.Controls.Add(m_BoarsSizeLabel);

            m_BoardSize = new Button();
           m_BoardSize.BackColor = Color.AliceBlue;
            m_BoardSize.Text = BoardSizeOptionArray[m_Count];
            m_BoardSize.Width = m_FirstPlayerLabel.Width;
            m_BoardSize.Left = m_BoarsSizeLabel.Left;
            m_BoardSize.Top = m_BoarsSizeLabel.Bottom + 5;
            m_BoardSize.Height = 50;
            this.Controls.Add(m_BoardSize);
            m_BoardSize.Click += new EventHandler(ChoosenBoardSize);
            
            m_StartGame = new Button();
            m_StartGame.Text = "START!";
            m_StartGame.Width = m_FirstPlayerLabel.Width;
            m_StartGame.Left = m_ButtonAgainstComp.Left + m_ButtonAgainstComp.Width - m_StartGame.Width;
            m_StartGame.Top = m_BoardSize.Bottom - m_StartGame.Height;
            m_StartGame.Height = m_ButtonAgainstComp.Height;
            this.Controls.Add(m_StartGame);
            m_StartGame.Click += new EventHandler(StartGame);

        }

        public string GetFirstPlayerName()
        {
            return m_FirstPlayerTextBox.Text;
        }

        public string GetSecondPlayerName()
        {
            return m_SecondPlayerTextBox.Text;
        }

        public int ReturnNumberOfPlayer()
        {
            return m_NumberOfPlayer;
        }

        public string ReturnBoardSize()
        {
            return m_BoardSize.Text;
        }

        public bool ReturnIfToStartGame()
        {
            return m_True;
        }

        private void ChooseNumberOfPlayers(object sender, EventArgs e) 
        {
            m_SecondPlayerTextBox.Text = "Computer";
            m_ButtonAgainstComp.Text = "Against player";
            m_SecondPlayerTextBox.ReadOnly = true;
            m_ButtonAgainstComp.Click -= new EventHandler(ChooseNumberOfPlayers);
            m_ButtonAgainstComp.Click += new EventHandler(ChooseNumberOfPlayers2);
            m_NumberOfPlayer = 1;
        }

        private void ChooseNumberOfPlayers2(object? sender, EventArgs e)
        {
            m_SecondPlayerTextBox.Text = "Enter name";
            m_ButtonAgainstComp.Text = "Against Computer";
            m_SecondPlayerTextBox.ReadOnly = false;
            m_ButtonAgainstComp.Click -= new EventHandler(ChooseNumberOfPlayers2);
            m_ButtonAgainstComp.Click += new EventHandler(ChooseNumberOfPlayers);
            m_NumberOfPlayer = 2;
        }

        private void ChoosenBoardSize(object sender, EventArgs e)
        {
            m_Count = m_Count + 1;
            
            if(m_Count < BoardSizeOptionArray.Length)
            {
                m_BoardSize.Text = BoardSizeOptionArray[m_Count];
            }
            else
            {
                m_BoardSize.Text = BoardSizeOptionArray[0];
                m_Count = 0;
            }
        }

        private void StartGame(object sender, EventArgs e)
        {

            m_True = true;
            this.Close();
        }
    }
}
