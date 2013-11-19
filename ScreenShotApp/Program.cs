using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScreenShotApp.Forms;
using ScreenshotMakerLibrary;

namespace ScreenShotApp
{
    static class Program
    {
        public static MySQLBroker MySqlBroker { get; set; }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MySqlBroker = new MySQLBroker();
            var loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MainForm());
            }
            return;
        }
    }
}
