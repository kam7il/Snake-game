using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class FormSnake : Form
    {
        SnakeGame mySnake;
        Graphics graphics;
        const int fieldSize = 50;

        public FormSnake()
        {
            InitializeComponent();

            mySnake = new SnakeGame(15, 10, 200, 3);
            mySnake.StateChanged += Repaint;
            mySnake.GameEnded += MySnake_GameEnded;

            pbVisualization.Image = new Bitmap(mySnake.Width * fieldSize, mySnake.Height * fieldSize);
            graphics = Graphics.FromImage(pbVisualization.Image);

            Repaint();
        }

        private void MySnake_GameEnded()
        {
            MessageBox.Show("Gra skonczona!");
        }

        private void Repaint()
        {
            graphics.Clear(Color.White);
            /*for(int x=0; x<=mySnake.Width; x++)
            {
                graphics.DrawLine(new Pen(Color.Brown, 2),
                                 x * fieldSize, 0,
                                 x * fieldSize, mySnake.Height * fieldSize);
            }
            for (int y = 0; y <= mySnake.Height; y++)
            {
                graphics.DrawLine(new Pen(Color.Brown, 2),
                                 0, y * fieldSize, 
                                 mySnake.Width * fieldSize, y*fieldSize);
            }*/
            for (int x = 0; x < mySnake.Width; x++)
            {
                for (int y = 0; y < mySnake.Height; y++)
                {
                    graphics.DrawRectangle(new Pen(Color.Brown, 2),
                                           x * fieldSize, y * fieldSize,
                                           fieldSize, fieldSize);
                }
            }
            foreach (Point s in mySnake.SnakeLocation)
            {
                if (s == mySnake.SnakeLocation.First())
                {
                    graphics.DrawImage(Properties.Resources.ImageSnakeHead,
                                        s.X * fieldSize, s.Y * fieldSize,
                                        fieldSize - 1, fieldSize - 1);
                }
                else
                {
                    graphics.FillEllipse(new SolidBrush(Color.Brown),
                                         s.X * fieldSize, s.Y * fieldSize,
                                         fieldSize - 1, fieldSize - 1);
                }
            }
            graphics.DrawImage(Properties.Resources.ImageApple,
                                     mySnake.Apple.X * fieldSize, mySnake.Apple.Y * fieldSize,
                                     fieldSize - 1, fieldSize - 1);
            pbVisualization.Refresh();
        }

        private void FormSnake_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    mySnake.SnakeDirecion = SnakeGame.Direction.Up;
                    break;
                case Keys.Down:
                case Keys.S:
                    mySnake.SnakeDirecion = SnakeGame.Direction.Down;
                    break;
                case Keys.Left:
                case Keys.A:
                    mySnake.SnakeDirecion = SnakeGame.Direction.Left;
                    break;
                case Keys.Right:
                case Keys.D:
                    mySnake.SnakeDirecion = SnakeGame.Direction.Right;
                    break;
            }
        }
    }
}
