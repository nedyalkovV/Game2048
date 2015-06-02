using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Runtime.Remoting.Contexts;
namespace teamProject2048
{
    
    public partial class Game : Form
    {
        //-------------------------------
        //  Main Parameters
        //-------------------------------
        static int randomRow;
        static int randomCol;
        static int[,] board = new int[4, 4];
        static List<Button> boardDIsplay = new List<Button>();
        private static int moveCounter = 0;
        private static int score = 0;
        private static byte SoundNumber = 1;
        private StreamReader reader = new StreamReader("BestScoreRecord.txt");
        private static int highScore = 0;
        private SoundPlayer play = new SoundPlayer();

        //-------------------------------
        //  Start Game Main
        //-------------------------------
      
        public Game()
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            AddButtonsToLits();
            InitializeBoard();
            Spawn();
            Spawn();
            DisplayBoard();
            PlaySound();
            highScore = GetBestScore(reader);
            BestScore.Text = string.Format("Best Score:\n{0}", highScore);
            Up.PreviewKeyDown += new PreviewKeyDownEventHandler(Arrow_PreviewKeyDown);
            Up.KeyDown += new KeyEventHandler(Arrow_KeyDown);
            Down.PreviewKeyDown += new PreviewKeyDownEventHandler(Arrow_PreviewKeyDown);
            Down.KeyDown += new KeyEventHandler(Arrow_KeyDown);
            Left.PreviewKeyDown += new PreviewKeyDownEventHandler(Arrow_PreviewKeyDown);
            Left.KeyDown += new KeyEventHandler(Arrow_KeyDown);
            Right.PreviewKeyDown += new PreviewKeyDownEventHandler(Arrow_PreviewKeyDown);
            Right.KeyDown += new KeyEventHandler(Arrow_KeyDown);
        }

        //-------------------------------
        //  Game Logic Algorithum
        //-------------------------------

        //add buttons to list
        private void AddButtonsToLits()
        {
            boardDIsplay.Add(B1);
            boardDIsplay.Add(B2);
            boardDIsplay.Add(B3);
            boardDIsplay.Add(B4);
            boardDIsplay.Add(B5);
            boardDIsplay.Add(B6);
            boardDIsplay.Add(B7);
            boardDIsplay.Add(B8);
            boardDIsplay.Add(B9);
            boardDIsplay.Add(B10);
            boardDIsplay.Add(B11);
            boardDIsplay.Add(B12);
            boardDIsplay.Add(B13);
            boardDIsplay.Add(B14);
            boardDIsplay.Add(B15);
            boardDIsplay.Add(B16);
        }        
        //Initialize the board
        static void InitializeBoard()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col] = 0;
                }
            }
        }   
        //Spawn random
        static void Spawn()
        {
            Random rndm = new Random();
            while (true)
            {
                randomRow = rndm.Next(0, board.GetUpperBound(0) + 1);
                randomCol = rndm.Next(0, board.GetUpperBound(1) + 1);
                if (board[randomRow, randomCol] == 0)
                {
                    board[randomRow, randomCol] = 2;
                    return;
                }
            }
        }
        
        //Display the board
        static void DisplayBoard()
        {
            int n = 0;
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row,col] != 0)
                    {
                        boardDIsplay[n].Text = Convert.ToString(board[row,col]);
                        int num = board[row, col]; 
                        switch (num % 13)
                        {
                            case 2: boardDIsplay[n].BackColor = Color.MediumSeaGreen; break;
                            case 3: boardDIsplay[n].BackColor = Color.LightSalmon; break;
                            case 4: boardDIsplay[n].BackColor = Color.MediumPurple; break;
                            case 5: boardDIsplay[n].BackColor = Color.Cyan; break;
                            case 6: boardDIsplay[n].BackColor = Color.DodgerBlue; break;
                            case 8: boardDIsplay[n].BackColor = Color.SkyBlue; break;
                            case 9: boardDIsplay[n].BackColor = Color.LightSeaGreen; break;
                            case 10: boardDIsplay[n].BackColor = Color.DarkKhaki; break;
                            case 11: boardDIsplay[n].BackColor = Color.SteelBlue; break;
                            case 12: boardDIsplay[n].BackColor = Color.DarkCyan; break;
                            default: boardDIsplay[n].BackColor = Color.LightGray; break;
                        }
                    }
                    else
                    {
                        boardDIsplay[n].Text = "";
                        boardDIsplay[n].BackColor = Color.DarkGray;
                    }
                    n++;
                }
            }
        }        
        //Check for equal neighbours
        static bool IsThereEqualNeighbours()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (row == 0)
                    {
                        if (col == 0)
                        {
                            if (board[row, col] == board[row + 1, col] ||
                                board[row, col] == board[row, col + 1])
                            {
                                return true;
                            }
                        }
                        else if (col == board.GetUpperBound(1))
                        {
                            if (board[row, col] == board[row + 1, col] ||
                                board[row, col] == board[row, col - 1])
                            {
                                return true;
                            }
                        }
                        else 
                        {
                            if (board[row, col] == board[row + 1, col] ||
                                board[row, col] == board[row, col + 1] ||
                                board[row, col] == board[row, col - 1])
                            {
                                return true;
                            }
                        }

                    }
                    else if(row == board.GetUpperBound(0))
                    {
                        if (col == 0)
                        {
                            if (board[row, col] == board[row - 1, col] ||
                                board[row, col] == board[row, col + 1])
                            {
                                return true;
                            }
                        }
                        else if (col == board.GetUpperBound(1))
                        {
                            if (board[row, col] == board[row - 1, col] ||
                                board[row, col] == board[row, col - 1])
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (board[row, col] == board[row - 1, col] ||
                                board[row, col] == board[row, col - 1] ||
                                board[row, col] == board[row, col + 1])
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (col == 0)
                        {
                            if (board[row, col] == board[row - 1, col] ||
                                board[row, col] == board[row + 1, col] ||
                                board[row, col] == board[row, col + 1])
                            {
                                return true;
                            }
                        }
                        else if (col == board.GetUpperBound(1))
                        {
                            if (board[row, col] == board[row + 1, col] ||
                                board[row, col] == board[row - 1, col] ||
                                board[row, col] == board[row, col - 1])
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (board[row, col] == board[row - 1, col] ||
                                board[row, col] == board[row + 1, col] ||
                                board[row, col] == board[row, col - 1] ||
                                board[row, col] == board[row, col + 1])
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }     
        //Check if the board is full
        static bool IsFull()
        {
            int counter = 0;
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] != 0)
                    {
                        counter++;
                    }
                }
            }
            if (counter == 16)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Check for zeros or equal neighbours
        static bool IsThereZerosOrEqualNeighbours(string input)
        {
            if (input == "right")
            {
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int col = 0; col < board.GetUpperBound(1); col++)
                    {
                        if ((board[row, col] != 0 && board[row, col + 1] == 0) || 
                            (board[row, col] != 0 && board[row, col] == board[row, col + 1]))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            else if (input == "left")
            {
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int col = 1; col < board.GetLength(1); col++)
                    {
                        if ((board[row, col] != 0 && board[row, col - 1] == 0) || 
                            (board[row, col] != 0 && board[row, col] == board[row, col - 1]))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            else if (input == "down")
            {
                for (int row = 0; row < board.GetUpperBound(0); row++)
                {
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        if ((board[row, col] != 0 && board[row + 1, col] == 0) || 
                            (board[row, col] != 0 && board[row, col] == board[row + 1, col]))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            else
            {
                for (int row = 1; row < board.GetLength(0); row++)
                {
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        if ((board[row, col] != 0 && board[row - 1, col] == 0) || 
                            (board[row, col] != 0 && board[row, col] == board[row - 1, col]))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        
        //-----------------------------
        // Movement methods 
        //-----------------------------

        private void MoveRight()
        {
            moveCounter++;
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = board.GetUpperBound(1) - 1; col >= 0; col--)
                {
                    if (board[row, col + 1] == 0 || board[row, col + 1] == board[row, col])
                    {
                        if (board[row, col + 1] == board[row, col])
                        {
                            IncrementScore(board[row, col] * 2);
                        }
                        board[row, col + 1] += board[row, col];
                        board[row, col] = 0;
                    }
                }
            }
        }
        private void MoveDown()
        {
            moveCounter++;
            for (int row = board.GetUpperBound(0) - 1; row >= 0; row--)
            {
                for (int col = 0; col < board.GetLength(0); col++)
                {
                    if (board[row + 1, col] == 0 || board[row + 1, col] == board[row, col])
                    {
                        if (board[row + 1, col] == board[row, col])
                        {
                            IncrementScore(board[row, col] * 2);
                        }
                        board[row + 1, col] += board[row, col];
                        board[row, col] = 0;
                    }
                }
            }
        }
        private void MoveLeft()
        {
            moveCounter++;
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 1; col < board.GetLength(1); col++)
                {
                    if (board[row, col - 1] == 0 || board[row, col - 1] == board[row, col])
                    {
                        if (board[row, col - 1] == board[row, col])
                        {
                            IncrementScore(board[row, col] * 2);
                        }
                        board[row, col - 1] += board[row, col];
                        board[row, col] = 0;
                    }
                }
            }
        }
        private void MoveUp()
        {
            moveCounter++;
            for (int row = 1; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row - 1, col] == 0 || board[row - 1, col] == board[row, col])
                    {
                        if (board[row - 1, col] == board[row, col])
                        {
                            IncrementScore(board[row, col] * 2);
                        }
                        board[row - 1, col] += board[row, col];
                        board[row, col] = 0;
                    }
                }
            }
        }

        //-------------------------------
        // Event listeners
        //-------------------------------

        private void Right_Click(object sender, EventArgs e)
        {
            while (IsThereZerosOrEqualNeighbours("right"))
            {
                MoveRight();
            }
            if (moveCounter > 0)
            {
                Spawn();
                DisplayBoard();
                if (IsFull() && !IsThereEqualNeighbours())
                {
                    EndGame();
                }
                moveCounter = 0;
            }
        }
        private void Down_Click(object sender, EventArgs e)
        {
            while (IsThereZerosOrEqualNeighbours("down"))
            {
                MoveDown();
            }
            if (moveCounter > 0)
            {
                Spawn();
                DisplayBoard();
                if (IsFull() && !IsThereEqualNeighbours())
                {
                    EndGame();
                }
                moveCounter = 0;
            }
        }
        private void Left_Click(object sender, EventArgs e)
        {
            while (IsThereZerosOrEqualNeighbours("left"))
            {
                MoveLeft();
            }
            if (moveCounter > 0)
            {
                Spawn();
                DisplayBoard();
                if (IsFull() && !IsThereEqualNeighbours())
                {
                    EndGame();
                }
                moveCounter = 0;
            }
        }
        private void Up_Click(object sender, EventArgs e)
        {
            while (IsThereZerosOrEqualNeighbours("up"))
            {
                MoveUp();
            }
            if (moveCounter > 0)
            {
                Spawn();
                DisplayBoard();
                if (IsFull() && !IsThereEqualNeighbours())
                {
                    EndGame();
                }
                moveCounter = 0;
            }
        }
        //W A S D
        private void Game_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)//switch the pressed key as a char
            {
                case (char)119: Up_Click(sender, e); break;
                case (char)115: Down_Click(sender, e); break;
                case (char)97: Left_Click(sender, e); break;
                case (char)100: Right_Click(sender, e); break;
                default:
                    break;
            }
        }
        //Arrows
        void Arrow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    Down_Click(sender, e); 
                    break;
                case Keys.Up:
                    Up_Click(sender, e); 
                    break;
                case Keys.Right:
                    Right_Click(sender, e); 
                    break;
                case Keys.Left:
                    Left_Click(sender, e); 
                    break;
            }
        }
        private void Arrow_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                case Keys.Right:
                case Keys.Left:
                    e.IsInputKey = true;
                    break;
            }
        }

        //-------------------------------
        // Other Buttons and Labels
        //-------------------------------

        private void NewGame_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void ExitGame_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void About_Click(object sender, EventArgs e)
        {
            string team = "Team Aitarus C# Advance Project : 2048 \n\nTsvetan Topalov (dpainter)\nNikolay Iliev (spoooon)" +
                          "\nPetya Djanovska-Yancheva (PetyaDjanovska)\nVasil Nedyalkov (nedyalkov_v)" +
                          "\nTodor Tachev (PsychoSphere)\nViktor Terziev (viktor4130)";
            MessageBox.Show(team);
        }
        private void PlaySoundBut_Click(object sender, EventArgs e)
        {
            PlaySound();
        }
        private void cbMute_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMute.Checked)
            {
                play.Stop();
            }
            else
            {
                play.PlayLooping();
            }
        }
        private void ResetBestScore_Click(object sender, EventArgs e)
        {
            using (StreamWriter resetScore = new StreamWriter("BestScoreRecord.txt"))
            {
                resetScore.Write(0);
            }
            highScore = 0;
            BestScore.Text = string.Format("Best Score:\n{0}", highScore);
        }

        //-------------------------------
        // Score Calculators
        //-------------------------------

        private int GetBestScore(StreamReader reader)
        {
            int score = 0;
           
            string line = reader.ReadLine();
            score = int.Parse(line);
            
            reader.Close();
            reader.Dispose();
            return score;
        }
        private void IncrementScore(int value)
        {
            score += value;
            YourScore.Text = string.Format("Your Score:\n{0}", score);
        }

        //-------------------------------
        // Game END method
        //-------------------------------

        private void EndGame()
        {
            Up.Enabled = false;
            Down.Enabled = false;
            Right.Enabled = false;
            Left.Enabled = false;
            SoundPlayer GameOverSound = new SoundPlayer();
            GameOverSound.SoundLocation = @"GameOver.wav";
            GameOverSound.Play();

            if (score <= highScore)
            {
                MessageBox.Show("Game Over!\nYour Score is :" + score, "Game Over!");
                Application.Restart();
            }
            else if (score > highScore)
            {
                StreamWriter writer = new StreamWriter("BestScoreRecord.txt", false);
                using (writer)
                {
                    writer.WriteLine(Convert.ToString(score));
                }
                MessageBox.Show("Game Over!\nDAMN! You set a new Recored :" + score,"Game Over!");
                Application.Restart();
            }
        }

        //-------------------------------
        // Sound Player
        //-------------------------------

        private void PlaySound()
        {
            switch (SoundNumber)
            {
                case 1: { play.SoundLocation = @"GameBoy.wav"; SoundNumber = 2; break; }
                case 2: { play.SoundLocation = @"Chickens.wav"; SoundNumber = 3; break; }
                case 3: { play.SoundLocation = @"Super_Mario.wav"; SoundNumber = 1; break; }
                default:
                    break;
            }

            play.PlayLooping();
        }

    }
}