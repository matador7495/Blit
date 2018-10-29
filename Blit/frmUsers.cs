using ClearClass;
using Connection_Class;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Blit
{
    public partial class frmUsers : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query q = new Connection_Query();

        public frmUsers()
        {
            InitializeComponent();
        }

        void Display()
        {
            dgvUsers.DataSource = q.ShowDataInGridView("Select * from tblUsers");
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                q.OpenConection();
                q.ExecuteQueries(string.Format("insert into tblusers values('{0}','{1}')", txtUserName.Text, txtPassword.Text));
                MessageBox.Show("عملیات با موفقیت انجام شد", "Bilit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls.ClearTextBoxes(this);
                Display();
                q.CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Bilit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvUsers.SelectedCells[0].Value);//id cell entekhab shode
            q.OpenConection();
            q.ExecuteQueries("Delete from tblUsers where ID=" + x);
            MessageBox.Show("عملیات با موفقیت انجام شد", "Bilit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Display();
        }
    }
}