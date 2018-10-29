using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Connection_Class;
using ClearClass;

namespace Blit
{
    public partial class frmSetting : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query q = new Connection_Query();

        public frmSetting()
        {
            InitializeComponent();
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                q.OpenConection();
                q.ExecuteQueries(string.Format("Insert into tblsetting values('{0}','{1}','{2}')", txtName.Text, txtTel.Text, txtAddress.Text));
                q.CloseConnection();
                MessageBox.Show("عملیات با موفقیت انجام شد", "Bilit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls.ClearTextBoxes(this);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Bilit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                q.OpenConection();
                q.ExecuteQueries("Delete from tblsetting where ID = " + txtCode.Text);
                q.CloseConnection();
                MessageBox.Show("عملیات با موفقیت انجام شد", "Bilit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls.ClearTextBoxes(this);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Bilit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                q.OpenConection();
                SqlDataReader dr = q.DataReader("Select * from tblsetting where id=" + txtCode.Text);

                if (dr.Read())
                {
                    txtCode.Text = dr["ID"].ToString();
                    txtName.Text = dr["NameAgency"].ToString();
                    txtTel.Text = dr["TelA"].ToString();
                    txtAddress.Text = dr["AddressA"].ToString();
                }
                else
                {
                    txtCode.Text = "";
                    MessageBox.Show("اطلاعاتی برای این کد پیدا نشد", "Bilit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                q.CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Bilit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                q.OpenConection();
                q.ExecuteQueries("Update tblsetting set NameAgency='" + txtName.Text + "',TelA='" + txtTel.Text + "',AddressA='" + txtAddress.Text + "'where ID=" + txtCode.Text);
                q.CloseConnection();
                MessageBox.Show("عملیات با موفقیت انجام شد", "Bilit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls.ClearTextBoxes(this);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Bilit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}