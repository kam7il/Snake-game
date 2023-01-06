using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Snake
{
    public class SnakeGame
    {
        public enum Direction { Up, Down, Left, Right }
        private Random generator = new Random();
        private int width;
        private int height;
        private Timer snakeSpeed = new Timer();
        private List<Point> snakeLocation = new List<Point>();
        private Direction snakeDirecion = Direction.Up;
        private Point apple;

        public int Width { get => width; private set => width = value; }
        public int Height { get => height; private set => height = value; }
        public List<Point> SnakeLocation { get => snakeLocation; private set => snakeLocation = value; }
        public Direction SnakeDirecion
        {
            get => snakeDirecion;
            set
            {
                if ((value == Direction.Down && snakeDirecion != Direction.Up) ||
                   (value == Direction.Up && snakeDirecion != Direction.Down) ||
                    (value == Direction.Left && snakeDirecion != Direction.Right) ||
                    (value == Direction.Right && snakeDirecion != Direction.Left))
                {
                    snakeDirecion = value;
                    SnakeSpeed_Tick(null, null);
                }
            }
        }
        public Point Apple { get => apple; set => apple = value; }

        public SnakeGame(int width, int height, int speed, int length)
        {
            this.Width = width;
            this.Height = height;
            this.snakeSpeed.Interval = speed;

            for (int i = 0; i < length; i++)
            {
                SnakeLocation.Add(new Point(width / 2, height - 1 + i));
            }

            genApple();

            this.snakeSpeed.Tick += SnakeSpeed_Tick;
            this.snakeSpeed.Start();
        }

        private void genApple()
        {
            do
            {
                Apple = new Point(generator.Next(Width), generator.Next(Height));
            } while (snakeLocation.Contains(Apple));
        }

        public delegate void bezargumentowa();
        public event bezargumentowa StateChanged;
        public event bezargumentowa GameEnded;

        private void SnakeSpeed_Tick(object sender, System.EventArgs e)
        {
            Point newHead = Point.Empty;
            switch (SnakeDirecion)
            {
                case Direction.Up:
                    newHead = new Point(SnakeLocation.First().X,
                                        SnakeLocation.First().Y - 1);
                    break;
                case Direction.Down:
                    newHead = new Point(SnakeLocation.First().X,
                                        SnakeLocation.First().Y + 1);
                    break;
                case Direction.Left:
                    newHead = new Point(SnakeLocation.First().X - 1,
                                        SnakeLocation.First().Y);
                    break;
                case Direction.Right:
                    newHead = new Point(SnakeLocation.First().X + 1,
                                        SnakeLocation.First().Y);
                    break;
            }
            newHead = new Point((newHead.X + Width) % Width, (newHead.Y + Height) % Height);

            if (SnakeLocation.Contains(newHead))
            {
                snakeSpeed.Stop();
                if (GameEnded != null)
                {
                    GameEnded();
                }
                return;
            }
            SnakeLocation.Insert(0, newHead);
            if (newHead == Apple)
            {
                genApple();
                snakeSpeed.Interval = (int)(snakeSpeed.Interval * 0.9);
            }
            else
            {
                SnakeLocation.Remove(SnakeLocation.Last());
            }

            if (StateChanged != null)
            {
                StateChanged();
            }

        }

    }
}