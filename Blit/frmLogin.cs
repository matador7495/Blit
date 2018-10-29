using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Connection_Class;

namespace Blit
{
    public partial class frmLogin : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query q = new Connection_Query();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            int i = 0;//check kardane mojod bodan Username
            q.OpenConection();
            var qe = q.ExecuteScaler("Select Count(*) from tblUsers where Uname ='"+txtUserName.Text+"' And Pass  ='"+txtPassword.Text+"'");
            i = (int)qe.ExecuteScalar();//chon dar database taghiri eijad nemishe
            if (i > 0)
            {
                new frmMain().ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("نام کاربری یا کلمه عبور صحیح نمی باشد", "Bilit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            analogClockControl1.Value = DateTime.Now;
          
        }
    }
}