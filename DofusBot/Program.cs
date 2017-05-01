using DofusBot.Interface;
using System;
using System.Windows.Forms;

namespace DofusBot
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Main main = new Main();
            main.FormClosing += MainFormClosing;
            Application.Run(main);
            Console.WriteLine("I AM AT THE END OF THE MAIN");
        }

        public static void MainFormClosing(object source, FormClosingEventArgs e)
        {
            //
        }
    }
}
