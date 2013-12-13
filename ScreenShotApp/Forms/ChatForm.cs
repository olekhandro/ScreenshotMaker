using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ScreenshotMakerLibrary.Domain;

namespace ScreenShotApp.Forms
{
    public partial class ChatForm : Form
    {
        private User _user = Program.CurrentUser;
        private Project _project;
        private static User _curentReceiver = null;
        private Thread _messagePullingThread;

        public ChatForm()
        {
            InitializeComponent();
            InitComboboxes();
            _messagePullingThread = new Thread(new ThreadStart(delegate
            {
                RefreshChatRTB();
            }));
            _messagePullingThread.Start();
        }

        private void RefreshChatRTB()
        {
            while (true)
            {
                if (_curentReceiver != null)
                {
                    var messages = Program.MySqlBroker.GetMessagesForUser(_user, _curentReceiver);
                    foreach (var message in messages)
                    {
                        chatRTB.Invoke(
                            new Action(
                                () =>
                                    chatRTB.AppendText(message.Sender.Username + " (" + message.SendTime.ToString() +
                                                       ")" +
                                                       System.Environment.NewLine +
                                                       message.Text + Environment.NewLine)));
                        Program.MySqlBroker.Delete(message);
                    }
                }
                Thread.Sleep(2000);
            }
        }

        private void InitComboboxes()
        {
            projectsCBox.DataSource = _user.Projects.ToList();
            if (_user.Projects.Any())
            {
                projectsCBox.SelectedIndex = 0;
            }
        }

        private void messageRTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            var message = new ScreenshotMakerLibrary.Domain.Message()
            {
                SendTime = DateTime.Now,
                Receiver = _curentReceiver,
                Sender = _user,
                Text = messageRTB.Text
            };
            chatRTB.AppendText(_user.Username + " (" + message.SendTime.ToString() + ")" +
                               System.Environment.NewLine +
                               message.Text + Environment.NewLine);
            Program.MySqlBroker.Save(message);
        }

        private void projectsCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _project = (Project) projectsCBox.SelectedItem;
            userCBox.DataSource = _project.Users.Where(x=>x!= _user).ToList();
            if (_project.Users.Any())
            {
                userCBox.SelectedIndex = 0;
            }
        }

        private void userCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedUser = (User) userCBox.SelectedItem;
            if (_curentReceiver != null)
                chatRTB.SaveFile(Program.CurrentHistoryDirectory + @"\" + _curentReceiver.Username + ".rtf");
            _curentReceiver = selectedUser;
            var fileInfo =
                new FileInfo(Program.CurrentHistoryDirectory + @"\" + selectedUser.Username + ".rtf");
            if (fileInfo.Exists)
                chatRTB.LoadFile(fileInfo.FullName);
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_curentReceiver != null)
                chatRTB.SaveFile(Program.CurrentHistoryDirectory + @"\" + _curentReceiver.Username + ".rtf");
            _messagePullingThread.Abort();
        }
    }
}
