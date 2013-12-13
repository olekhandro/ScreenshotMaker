using System;
using System.Linq;
using System.Windows.Forms;
using ScreenshotMakerLibrary;
using ScreenshotMakerLibrary.Domain;

namespace ScreenShotApp.Forms
{
    public partial class MainForm : Form
    {
        private bool _isStarted = false;

        public MainForm()
        {
            InitializeComponent();
            usernameLbl.Text = Program.CurrentUser.Username;
            projectsCBox.Items.AddRange(Program.CurrentUser.Projects.ToArray());
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            ScreenshotManagerClass.OnScreenshotUploaded+= UpdateInterface;
        }
        
        private void UpdateInterface(object sender, EventArgs e)
        {
            screenshotPictureBox.Image = ScreenshotManagerClass.LastSavedScreenshot;
            screenshotTakingTimeLbl.Text = DateTime.UtcNow.ToString();
        }

        private void startBtn_Click(object sender, System.EventArgs e)
        {
            if (!_isStarted)
            {
                startBtn.Text = "Stop";
                ScreenshotManagerClass.Work(Program.CurrentUser, (Project) projectsCBox.SelectedItem,
                    Program.MySqlBroker);
                _isStarted = true;
            }
            else
            {
                startBtn.Text = "Start";
                ScreenshotManagerClass.Stop();
                _isStarted = false;
            }
        }

        private void chatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var chatForm = new ChatForm();
            chatForm.Show();
        }
    }
}
