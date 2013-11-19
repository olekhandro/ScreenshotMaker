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
                if (Program.MySqlBroker.Login(usernameTBox.Text, passwordTBox.Text))
                {
                    DialogResult= DialogResult.OK;
                }
            }
        }
    }
}
