using ClearClass;
using Connection_Class;
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

namespace Blit
{
    public partial class frmNooBlit : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();
        public frmNooBlit()
        {
            InitializeComponent();
        }

        private void frmNooBlit_Load(object sender, EventArgs e)
        {
            query.OpenConection();
            cmbNameCity.DataSource = query.ShowData("select NameCity from tblCity");
            cmbNameCity.DisplayMember = "NameCity";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                query.OpenConection();
                query.ExecuteQueries(string.Format("insert into tblNooBlit values('{0}','{1}','{2}','{3}')", txtNooBlit.Text, cmbNameCity.Text, txtGheymat.Text, txtTozihat.Text));
                query.CloseConnection();
                MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls.ClearTextBoxes(this);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                query.OpenConection();
                query.ExecuteQueries("delete from tblNooBlit where id=" + txtCode.Text);
                query.CloseConnection();
                MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls.ClearTextBoxes(this);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                query.OpenConection();
                var dr = query.DataReader("select * from tblNooBlit where id=" + txtCode.Text);

                if (dr.Read())
                {
                    txtCode.Text = dr["ID"].ToString();
                    txtNooBlit.Text = dr["NooBlit"].ToString();
                    cmbNameCity.Text = dr["NameCity"].ToString();
                    txtGheymat.Text = dr["Gheymat"].ToString();
                    txtTozihat.Text = dr["Tozihat"].ToString();
                }
                else
                {
                    MessageBox.Show("اطلاعاتی برای این کد پیدا نشد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearControls.ClearTextBoxes(this);
                    txtCode.Focus();
                }
                query.CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                query.OpenConection();
                query.ExecuteQueries("update tblNooBlit set NooBlit='" + txtNooBlit.Text + "',NameCity='" + cmbNameCity.Text + "',Gheymat='" + txtGheymat.Text + "',Tozihat='" + txtTozihat.Text + "' where id=" + txtCode.Text);
                query.CloseConnection();
                MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls.ClearTextBoxes(this);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}