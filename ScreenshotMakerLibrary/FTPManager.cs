﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BytesRoad.Net.Ftp;
using ScreenshotMakerLibrary.Domain;

namespace ScreenshotMakerLibrary
{
    public class FTPManager
    {
        private const string FTPADDRESS = "ftp.screenshot.mdei.info";
        private const string SCREENSHOTPATH = "";
        private const string LOGIN = "timer@screenshot.mdei.info";
        private const string PASSWORD = "screenshot123";
        private const int TIMEOUTFTP = 120000;

        public static string UploadFile(User user, string filename)
        {
            var client = ConnectToServer();
            var directoryList = client.GetDirectoryList(TIMEOUTFTP, SCREENSHOTPATH);
            if (!directoryList.Any(x => x.Name == user.Username))
            {
                client.CreateDirectory(TIMEOUTFTP, SCREENSHOTPATH + @"/" + user.Username);
            }
            client.PutFile(TIMEOUTFTP,
                SCREENSHOTPATH + @"/" + user.Username + @"/" + filename.Substring(filename.LastIndexOf(@"\")+1),
                filename);
            client.Disconnect(TIMEOUTFTP);
            return SCREENSHOTPATH + @"/" + user.Username + @"/" + filename.Substring(filename.LastIndexOf(@"\")+1);
        }

        public static FtpClient ConnectToServer()
        {
            var client = new FtpClient();
            int TimeoutFTP = TIMEOUTFTP;
            string FTP_SERVER = FTPADDRESS;
            int FTP_PORT = 21;
            string FTP_USER = LOGIN;
            string FTP_PASSWORD = PASSWORD;
            client.Connect(TimeoutFTP, FTP_SERVER, FTP_PORT);
            client.Login(TimeoutFTP, FTP_USER, FTP_PASSWORD);
            return client;
        }
    }
}
