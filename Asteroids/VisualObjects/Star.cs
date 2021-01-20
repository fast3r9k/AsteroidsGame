using System.Drawing;

namespace Asteroids.VisualObjects
{
     class Star : VisualObject
    {
        public Star(Point Position, Point Direction, int Size) : base(Position, Direction,new Size(Size,Size))
        {

        }

        public override void Draw(Graphics g)
        {
            g.DrawLine(Pens.WhiteSmoke,
                (int) _Position.X,(int) _Position.Y,
                (int) (_Position.X + _Size.Width), (int) (_Position.Y + _Size.Width));

            g.DrawLine(Pens.WhiteSmoke,
                (int) (_Position.X+ _Size.Width), (int) _Position.Y,
                (int) _Position.X,(int) (_Position.Y + _Size.Width));
        }

        public override void Update()
        {
            _Position.X += _Direction.X;
            if (_Position.X < 0)
                _Position.X = Game.Width + _Size.Width;

        }
    }

}
