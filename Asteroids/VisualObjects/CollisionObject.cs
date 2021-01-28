using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.VisualObjects
{
    internal abstract class CollisionObject : VisualObject, ICollision
    {
        public CollisionObject(Point Position, Point Direction, Size Size) : base(Position, Direction, Size)
        {
        }

        public Rectangle Rect => new Rectangle(_Position, _Size);
        public bool CheckCollision(ICollision obj) => Rect.IntersectsWith(obj.Rect);
    }
}
