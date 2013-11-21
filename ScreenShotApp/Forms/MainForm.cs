using System.Linq;
using System.Windows.Forms;

namespace ScreenShotApp.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            usernameLbl.Text = Program.CurrentUser.Username;
            projectsCBox.Items.AddRange(Program.CurrentUser.Projects.ToArray());
        }

        private void startBtn_Click(object sender, System.EventArgs e)
        {

        }
    }
}
