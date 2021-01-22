using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.VisualObjects
{
    internal class SpaceShip : VisualObject, ICollision
    {

        public event EventHandler Destroyed;
        private int _Energy = 20;

        public int Energy => _Energy;

        public SpaceShip(Point Position, Point Direction, Size Size) : base(Position, Direction, Size)
        {

        }

        public override void Draw(Graphics g)
        {
            var rect = Rect;
            g.FillEllipse(Brushes.Blue, rect);
            g.DrawEllipse(Pens.Yellow, rect);
        }

        public override void Update() {  }
        public Rectangle Rect => new Rectangle(_Position, _Size);

        public bool CheckCollision(ICollision obj)
        {
            var isCollision = Rect.IntersectsWith(obj.Rect);
            if (isCollision && obj is Asteroid asteroid)
            {
                ChangeEnergy(-asteroid.Power);
            }

            return isCollision;
        }

        public void ChangeEnergy(int delta)
        {
            _Energy += delta;
            if (_Energy < 0)
            {
                Destroyed?.Invoke(this, EventArgs.Empty);
            }
        }

        public void MoveUp()
        {
            if (_Position.Y > 0)
            {
                _Position.Y -= _Direction.Y;
            }
        }

        public void MoveDown()
        {
            if (_Position.Y - _Size.Height < Game.Height )
            {
                _Position.Y += _Direction.Y;
            }
        }
    }
}
