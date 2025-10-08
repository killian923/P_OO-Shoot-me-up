using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones
{
    public partial class Shoot
    {
        private double realX;
        private double realY;

        public int X { get { return (int)realX; } }
        public int Y { get { return (int)realY; } }

        public Image Texture { get; private set; }
        public float Speed { get; private set; }

        public float TargetX { get; private set; }
        public float TargetY { get; private set; }

        private double vx;
        private double vy;

        public Shoot(int xInitial, int yInitial, Image texture, float speed, float targetX, float targetY)
        {
            realX = xInitial;
            realY = yInitial;
            Texture = texture;
            Speed = speed;

            double dx = targetX - realX;
            double dy = targetY - realY;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance == 0) distance = 1;

            vx = Speed * dx / distance;
            vy = Speed * dy / distance;
        }
        public void Update()
        {
            realX += vx;
            realY += vy;
        }
        public Rectangle GetRectangle()
        {
            return new Rectangle(X, Y, 50, 50);
        }
    }
}