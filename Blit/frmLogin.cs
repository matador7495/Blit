using System;
using System.Windows.Forms;
using Connection_Class;

namespace Blit
{
    public partial class frmLogin : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;//check kardane mojod bodan Username
                query.OpenConection();
                var qe = query.ExecuteScaler("Select Count(*) from tblUsers where Uname ='" + txtUserName.Text + "' And Pass  ='" + txtPassword.Text + "'");
                i = (int)qe.ExecuteScalar();//chon dar database taghiri eijad nemishe
                if (i > 0)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("نام کاربری یا کلمه عبور صحیح نمی باشد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("در هنگام اتصال به بانک اطلاعاتی خطایی رخ داده است ، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            analogClockControl1.Value = DateTime.Now;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}