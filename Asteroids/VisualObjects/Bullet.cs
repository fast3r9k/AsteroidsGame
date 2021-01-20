using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.VisualObjects
{
    internal class Bullet : VisualObject, ICollision
    {
        private const int __BulletSizeX = 20;
        private const int __BulletSizeY = 5;
        private const int __BulletSpeed = 3;
        public Bullet(int Position) : base(new Point(0, Position), Point.Empty, new Size(__BulletSizeX,__BulletSizeY))
        {
        }

        public override void Update() => _Position.X += __BulletSpeed;

        public override void Draw(Graphics g)
        {
            var rect = Rect;
            g.FillEllipse(Brushes.Red, rect);
            g.DrawEllipse(Pens.White, rect);
        }

        public Rectangle Rect => new Rectangle(_Position,_Size);
        public bool CheckCollision(ICollision obj) => Rect.IntersectsWith(obj.Rect);
    }
}
