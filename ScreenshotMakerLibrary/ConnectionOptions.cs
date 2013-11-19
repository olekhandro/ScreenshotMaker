namespace ScreenshotMakerLibrary
{
    /// <summary>
    ///     Опции соединения
    /// </summary>
    public static class ConnectionOptions
    {
        #region Properties

        private static string _serverName = "";
        private static string _databaseName = "";
        private static string _login = "";
        private static string _pwd = "";

        public static string ServerName
        {
            get { return _serverName; }
            set { _serverName = value; }
        }

        public static string DatabaseName
        {
            get { return _databaseName; }
            set { _databaseName = value; }
        }

        public static string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public static string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        #endregion

        #region Public voids

        public static void Init(string serverName, string databaseName, string login, string pwd)
        {
            ServerName = serverName;
            DatabaseName = databaseName;
            Login = login;
            Pwd = pwd;
        }

        #endregion
    }
}