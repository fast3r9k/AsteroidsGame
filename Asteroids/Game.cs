using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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

            Timer timer = new Timer{Interval = 100};
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
            const int visualObjectsCount = 20;
            __GameObjects = new VisualObject[visualObjectsCount];

            for (var i = 0; i < __GameObjects.Length; i++)
            {
                __GameObjects[i] = new VisualObject(
                    new Point(600, i*20),
                    new Point(15-i, 20-i),
                    new Size(20, 20));
            }
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
