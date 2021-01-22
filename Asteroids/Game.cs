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

        private static Timer __Timer;
        private static Bullet __Bullet;
        private static SpaceShip __SpaceShip;

        public static int Width { get; set; }
        public static int Height { get; set; }


        public static void Initialize(Form GameForm)
        {
            Width = GameForm.Width;
            Height = GameForm.Height;

            __Context = BufferedGraphicsManager.Current;
            Graphics g = GameForm.CreateGraphics();
            __Buffer = __Context.Allocate(g, new Rectangle(0, 0, Width, Height));

            __Timer  = new Timer{Interval = 100};
            __Timer.Tick += OnTimerClick;
            __Timer.Start();

            GameForm.KeyDown += OnGameForm_KeyDown;
        }

        private static void OnGameForm_KeyDown(object Sender, KeyEventArgs E)
        {
            switch (E.KeyCode)
            {
                case Keys.ControlKey:
                    __Bullet = new Bullet(__SpaceShip.Rect.Y);
                    break;
                case Keys.Up:
                    __SpaceShip.MoveUp();
                    break;
                case Keys.Down:
                    __SpaceShip.MoveDown();
                    break;
            }
        }

        private static void OnTimerClick(object Sender, EventArgs E)
        {
            Update();
            Draw();
        }

        public static void Load()
        {
            var game_objects = new List<VisualObject>();

            var rnd = new Random();
            const int asteroid_cnt = 10;
            const int asteroid_size = 5;
            const int asteroidMax_speed = 20;
            for (var i = 0; i < asteroid_cnt; i++)
            {
                game_objects.Add(new Asteroid(
                    new Point(rnd.Next(0,Width), rnd.Next(0,Height)),
                    new Point(-rnd.Next(0,asteroidMax_speed), 0),
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

            __Bullet = new Bullet(200);
            __SpaceShip = new SpaceShip(new Point(10,400),
                new Point(5,5),
                new Size(20,10));
            __SpaceShip.Destroyed += OnShipDestroyed;
        }

        private static void OnShipDestroyed(object Sender, EventArgs E)
        {
            __Timer.Stop();
            var g = __Buffer.Graphics;
            g.Clear(Color.DarkBlue);
            g.DrawString("Game over!", new Font(FontFamily.GenericSerif, 60, FontStyle.Bold ), Brushes.Red,200,100 );
            __Buffer.Render();

        }

        public static void Draw()
        {
            Graphics g = __Buffer.Graphics;
            g.Clear(Color.Black);

            foreach (var game_object in __GameObjects)
                game_object.Draw(g);

            __SpaceShip.Draw(g);
            __Bullet?.Draw(g);

            if(!__Timer.Enabled) return;
            __Buffer.Render();
        }

        private static void Update()
        {
            foreach (var game_object in __GameObjects)
                game_object.Update();

            __Bullet?.Update();

            for (var i = 0; i < __GameObjects.Length; i++)
            {
                var obj = __GameObjects[i];
                if (obj is ICollision collision_obj)
                {
                    __SpaceShip.CheckCollision(collision_obj);

                    if(__Bullet?.CheckCollision(collision_obj) != true) continue;
                    __Bullet = null;
                    __GameObjects[i] = null;
                    System.Media.SystemSounds.Beep.Play();
                }
            }
            
        }
    }
}
