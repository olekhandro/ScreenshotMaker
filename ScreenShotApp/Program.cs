using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ScreenShotApp.Forms;
using ScreenshotMakerLibrary;
using ScreenshotMakerLibrary.Domain;

namespace ScreenShotApp
{
    internal static class Program
    {
        public static MySQLBroker MySqlBroker { get; set; }

        public static User CurrentUser { get; set; }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            MySqlBroker = new MySQLBroker();
            ReadOptions();
            var loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MainForm());
            }

            return;
        }

        private static void ReadOptions()
        {
            var settingsFilePath = Application.StartupPath + @"\Settings.cfg";
            if (File.Exists(settingsFilePath))
            {
                var reader = new StreamReader(settingsFilePath);
                ConnectionOptions.ServerName = reader.ReadLine().Replace("ServerName=", "");
                ConnectionOptions.DatabaseName = reader.ReadLine().Replace("DatabaseName=", "");
                ConnectionOptions.Login = reader.ReadLine().Replace("Login=", "");
                ConnectionOptions.Pwd = reader.ReadLine().Replace("Password=", "");
            }
        }
    }
}
