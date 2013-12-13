using System.IO;
using System.Windows.Forms;
using ScreenshotMakerLibrary.Domain;

namespace ScreenShotApp.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(usernameTBox.Text))
            {
                MessageBox.Show("Username cannot be empty.");
            }
            else
            {
                var user = Program.MySqlBroker.Login(usernameTBox.Text, passwordTBox.Text);
                if (user !=null)
                {
                    Program.CurrentUser = user;
                    DialogResult= DialogResult.OK;
                    var directory = new DirectoryInfo(Application.StartupPath + @"\History\" + user.Username);
                    if (!directory.Exists)
                    {
                        directory.Create();
                    }
                    Program.CurrentHistoryDirectory = directory.FullName;
                }
            }
        }
    }
}
