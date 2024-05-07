//Aaron Marchanton
//May 7, 2024
//Square Chaser Game

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace SquareChaser
{
    public partial class Form1 : Form
    {
        Rectangle player1 = new Rectangle(190, 175, 20, 20);
        Rectangle player2 = new Rectangle(290, 175, 20, 20);
        Rectangle border = new Rectangle(29, 29, 444, 444);
        Rectangle playarea = new Rectangle(32, 32, 438, 438);
        Rectangle ball = new Rectangle(243, 350, 14, 14);
        Rectangle booster = new Rectangle(244, 80, 12, 12);

        int player1Score = 0;
        int player2Score = 0;

        int ballpoint1 = 0;
        int ballpoint2 = 0;
        int boosterpoint1 = 0;
        int boosterpoint2 = 0;

        Random randbp = new Random();

        int player1Speed = 4;
        int player2Speed = 4;

        bool wPressed = false;
        bool sPressed = false;
        bool upPressed = false;
        bool downPressed = false;
        bool aPressed = false;
        bool dPressed = false;
        bool leftPressed = false;
        bool rightPressed = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush redBrush = new SolidBrush(Color.DarkRed);
        SolidBrush orangeBrush = new SolidBrush(Color.DarkOrange);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush yellowbrush = new SolidBrush(Color.Yellow);

        SoundPlayer winSound = new SoundPlayer(Properties.Resources.winSound);
        SoundPlayer p1PointSound = new SoundPlayer(Properties.Resources.p1PointSound);
        SoundPlayer p2PointSound = new SoundPlayer(Properties.Resources.p2PointSound);
        SoundPlayer boosterSound = new SoundPlayer(Properties.Resources.boosterSound);

        public Form1()
        {
            InitializeComponent();

            ball.X = randbp.Next(33, 451);
            ball.Y = randbp.Next(33, 451);
            booster.X = randbp.Next(33, 451);
            booster.Y = randbp.Next(33, 451);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            PlayerMovement();
            BallPlayerIntersections();
            PlayerWins();
            PlayerBoosterInteractions();
            
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(blueBrush, border);
            e.Graphics.FillRectangle(blackBrush, playarea);
            e.Graphics.FillRectangle(whiteBrush, ball);
            e.Graphics.FillRectangle(redBrush, player1);
            e.Graphics.FillRectangle(orangeBrush, player2);
            e.Graphics.FillEllipse(yellowbrush, booster);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.D:
                    dPressed = true;
                    break;
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
            }
        }

        private void p1BoosterTimer_Tick(object sender, EventArgs e)
        {
            player1Speed = 4;
            p1BoosterTimer.Enabled = false;
        }

        private void p2BoosterTimer_Tick(object sender, EventArgs e)
        {
            player2Speed = 4;
            p1BoosterTimer.Enabled = false;
        }

        public void PlayerMovement()
        {
            //move player 1
            if (wPressed == true && player1.Y > 34)
            {
                player1.Y = player1.Y - player1Speed;
            }
            if (sPressed == true && player1.Y < 450)
            {
                player1.Y = player1.Y + player1Speed;
            }
            if (aPressed == true && player1.X > 33)
            {
                player1.X = player1.X - player1Speed;
            }
            if (dPressed == true && player1.X < 450)
            {
                player1.X = player1.X + player1Speed;
            }

            //move player 2
            if (upPressed == true && player2.Y > 34)
            {
                player2.Y = player2.Y - player2Speed;
            }
            if (downPressed == true && player2.Y < 450)
            {
                player2.Y = player2.Y + player2Speed;
            }
            if (leftPressed == true && player2.X > 33)
            {
                player2.X = player2.X - player2Speed;
            }
            if (rightPressed == true && player2.X < 450)
            {
                player2.X = player2.X + player2Speed;
            }
        }

        public void BallPlayerIntersections()
        {
            //player 1 scores a point
            if (player1.IntersectsWith(ball))
            {
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";

                ballpoint1 = randbp.Next(33, 451);
                ballpoint2 = randbp.Next(33, 451);

                ball = new Rectangle(ballpoint1, ballpoint2, 14, 14);

                p1PointSound.Play();
            }

            //player 2 scores a point
            if (player2.IntersectsWith(ball))
            {
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";

                ballpoint1 = randbp.Next(33, 451);
                ballpoint2 = randbp.Next(33, 451);

                ball = new Rectangle(ballpoint1, ballpoint2, 14, 14);

                p2PointSound.Play();
            }
        }

        public void PlayerWins()
        {
            //player 1 wins (3 points)
            if (player1Score == 3)
            {
                winLabel.Text = "Player 1 wins";
                gameTimer.Stop();

                winSound.Play();
            }

            //player 2 wins (3 points)
            if (player2Score == 3)
            {
                winLabel.Text = "Player 2 wins";
                gameTimer.Stop();

                winSound.Play();
            }
        }

        public void PlayerBoosterInteractions()
        {
            //player 1 gets booster
            if (player1.IntersectsWith(booster))
            {
                player1Speed = 6;
                p1BoosterTimer.Enabled = true;

                boosterpoint1 = randbp.Next(33, 451);
                boosterpoint2 = randbp.Next(33, 451);

                booster = new Rectangle(boosterpoint1, boosterpoint2, 12, 12);

                boosterSound.Play();
            }

            //player 2 gets booster
            if (player2.IntersectsWith(booster))
            {
                player2Speed = 6;
                p2BoosterTimer.Enabled = true;

                boosterpoint1 = randbp.Next(33, 451);
                boosterpoint2 = randbp.Next(33, 451);

                booster = new Rectangle(boosterpoint1, boosterpoint2, 12, 12);

                boosterSound.Play();
            }
        }
    }
}