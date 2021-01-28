using System;
using System.Windows.Forms;

namespace Asteroids
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var gameForm = new Form();
            
            gameForm.Show();
            gameForm.Width = 800;
            gameForm.Height = 600;


            Game.Initialize(gameForm);
            Game.Load();
            Game.Draw();

            Application.Run(gameForm);
        }
    }
}
