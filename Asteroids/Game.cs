using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Asteroids.VisualObjects;

namespace Asteroids
{
    static class Game
    {
        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;

        private static VisualObject[] __GameObjects;

        public static int Width { get; set; }
        public static int Height { get; set; }


        public static void Initialize(Form GameForm)
        {
            Width = GameForm.Width;
            Height = GameForm.Height;

            __Context = BufferedGraphicsManager.Current;
            Graphics g = GameForm.CreateGraphics();
            __Buffer = __Context.Allocate(g, new Rectangle(0, 0, Width, Height));

            var timer = new Timer{Interval = 100};
            timer.Tick += OnTimerClick;
            timer.Start();
        }

        private static void OnTimerClick(object Sender, EventArgs E)
        {
            Update();
            Draw();
        }

        public static void Load()
        {
            var game_objects = new List<VisualObject>();
            for (var i = 0; i < 15; i++)
            {
                game_objects.Add(new Asteroid(
                    new Point(600, i * 20),
                    new Point(15 - i, 20 - i),
                     20));
            }

            for (var i = 0; i < 15; i++)
            {
                game_objects.Add(new Star(
                    new Point(600, (int)(i / 2.0 * 20)),
                    new Point(-i, 0),
                    10));
            }

            __GameObjects = game_objects.ToArray();
        }

        public static void Draw()
        {
            Graphics g = __Buffer.Graphics;
            g.Clear(Color.Black);

            foreach (var game_object in __GameObjects)
                game_object.Draw(g);

            __Buffer.Render();
        }

        private static void Update()
        {
            foreach (var game_object in __GameObjects)
                game_object.Update();
            
        }
    }
}
