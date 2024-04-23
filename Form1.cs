using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, goUp, goDown, gameOver;
        string facing = "up";
        int playerHealth = 100;
        int speed = 10;
        int ammo = 10;
        int monsterSpeed = 3;
        Random randNum = new Random();
        List<PictureBox> monstersList = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (playerHealth > 1)
            {
                healthBar.Value = playerHealth;
            }
            else
            {
                gameOver = true;
                player.Image = Properties.Resources.playerDead;
                GameTimer.Stop();
            }

            if (goLeft == true && player.Left > 0)
            {
                player.Left -= speed;
            }
            if (goRight == true && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += speed;
            }
            if (goUp == true && player.Top > 0)
            {
                player.Top -= speed;
            }
            if (goDown == true && player.Top + player.Height < this.ClientSize.Height)
            {
                player.Top += speed;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "monster")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerHealth -= 1;
                    }

                    if (x.Left > player.Left)
                    {
                        x.Left -= monsterSpeed;
                        ((PictureBox)x).Image = Properties.Resources.monsterLeft;
                    }

                    if (x.Left < player.Left)
                    {
                        x.Left += monsterSpeed;
                        ((PictureBox)x).Image = Properties.Resources.monsterRight;
                    }

                    if (x.Top > player.Top)
                    {
                        x.Top -= monsterSpeed;
                        ((PictureBox)x).Image = Properties.Resources.monsterUp;
                    }

                    if (x.Top < player.Top)
                    {
                        x.Top += monsterSpeed;
                        ((PictureBox)x).Image = Properties.Resources.monsterDown;
                    }
                }

            }
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                player.Image = Properties.Resources.playerLeft;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                player.Image = Properties.Resources.PlayerRight;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                player.Image = Properties.Resources.playerUp;
            }

            if (e.KeyCode == Keys.Down )
            {
                goDown = true;
                facing = "down";
                player.Image = Properties.Resources.playerDown;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
                
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
                
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
                
            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
                
            }
        }

        private void MakeMonsters()
        {
            PictureBox monster = new PictureBox();
            monster.Tag = "monster";
            monster.Image = Properties.Resources.monsterDown;
            monster.Left = randNum.Next(0, 800);
            monster.Top = randNum.Next(0, 500);
            monster.SizeMode = PictureBoxSizeMode.AutoSize;
            monstersList.Add(monster);
            this.Controls.Add(monster);
            player.BringToFront();

        }
        private void RestartGame()
        {
            player.Image = Properties.Resources.playerUp;

            foreach (PictureBox i in monstersList)
            {
                this.Controls.Remove(i);
            }

            monstersList.Clear();

            for (int i = 0; i < 3; i++)
            {
                MakeMonsters();
            }

            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;
            gameOver = false;

            playerHealth = 100;

            GameTimer.Start();
        }

    }
}
