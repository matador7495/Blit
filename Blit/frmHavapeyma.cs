using ClearClass;
using Connection_Class;
using System;
using System.Windows.Forms;

namespace Blit
{
    public partial class frmHavapeyma : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();
        public frmHavapeyma()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                query.ExecuteQueries(string.Format("insert into tblHavapeyma values('{0}','{1}','{2}','{3}')", txtName.Text, cmbGroup.Text, txtTedad.Text, txtTozihat.Text));
                MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls.ClearTextBoxes(this);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                var dr = query.DataReader("select * from tblHavapeyma where id=" + txtCode.Text);
                if (dr.Read())
                {
                    txtCode.Text = dr["ID"].ToString();
                    txtName.Text = dr["NameHavapeyma"].ToString();
                    cmbGroup.Text = dr["NameGroup"].ToString();
                    txtTedad.Text = dr["Tedad"].ToString();
                    txtTozihat.Text = dr["Tozihat"].ToString();
                }
                else
                {
                    MessageBox.Show("اطلاعاتی برای این کد پیدا نشد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearControls.ClearTextBoxes(this);
                    txtCode.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("در هنگام اتصال به بانک اطلاعاتی خطایی رخ داده است ، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void frmHavapeyma_Load(object sender, EventArgs e)
        {
            try
            {
                cmbGroup.DataSource = query.ShowData("select NameG from tblGroups");
                cmbGroup.DisplayMember = "NameG";//namayesh barasase col NameGroup
            }
            catch (Exception)
            {
                MessageBox.Show("در هنگام اتصال به بانک اطلاعاتی خطایی رخ داده است ، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                query.ExecuteQueries("delete from tblHavapeyma where id=" + txtCode.Text);
                MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls.ClearTextBoxes(this);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                query.ExecuteQueries("update tblHavapeyma set NameHavapeyma='" + txtName.Text + "',NameGroup='" + cmbGroup.Text + "',Tedad='" + txtTedad.Text + "',Tozihat='" + txtTozihat.Text + "' where id=" + txtCode.Text);
                MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls.ClearTextBoxes(this);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }
    }
}