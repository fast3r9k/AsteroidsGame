﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.VisualObjects
{
    internal class Asteroid : ImageObject
    {
        //private static readonly Image __Image = Image.FromFile("Images/Asteroid.png");

        public  int Power { get; set; }

        public Asteroid(Point Position,Point Direction, int Size) : base (Position, Direction, new Size(Size,Size), Properties.Resources.Asteroid) 
        {
            
        }

        public override void Draw(Graphics g) { throw new NotImplementedException(); }
    }
}
