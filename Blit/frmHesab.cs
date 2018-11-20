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
using Stimulsoft.Report;

namespace Blit
{
    public partial class frmHesab : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();
        public frmHesab()
        {
            InitializeComponent();
        }
        void Display()
        {
            query.OpenConection();
            try
            {
                dgvHesab.DataSource = query.ShowData("select * from tblHesab");
            }
            catch (Exception)
            {
                MessageBox.Show("در اتصال به پایگاه داده خطایی رخ داده است ، لطفا مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void frmHesab_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                query.ExecuteQueries(string.Format("insert into tblHesab values ('{0}','{1}','{2}','{3}')", txtNameHesab.Text, txtShomareHesab.Text, txtMojodi.Text, txtTozihat.Text));
                Display();
                MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls.ClearTextBoxes(this);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                int x = Convert.ToInt32(dgvHesab.SelectedCells[0].Value);
                query.ExecuteQueries("delete from tblHesab where ID=" + x);
                Display();
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
                var dr = query.DataReader("select * from tblHesab where ID=" + txtCode.Text);
                if (dr.Read())
                {
                    txtCode.Text = dr["ID"].ToString();
                    txtNameHesab.Text = dr["NameHesab"].ToString();
                    txtShomareHesab.Text = dr["ShomareHesab"].ToString();
                    txtMojodi.Text = dr["Mojodi"].ToString();
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

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                query.ExecuteQueries("update tblHesab set NameHesab='" + txtNameHesab.Text + "', ShomareHesab='" + txtShomareHesab.Text + "', Mojodi='" + txtMojodi.Text + "', Tozihat='" + txtTozihat.Text + "' where ID=" + txtCode.Text);
                Display();
                MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls.ClearTextBoxes(this);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void dgvHesab_MouseUp(object sender, MouseEventArgs e)
        {
            txtCode.Text = dgvHesab[0, dgvHesab.CurrentRow.Index].Value.ToString();
            txtNameHesab.Text = dgvHesab[1, dgvHesab.CurrentRow.Index].Value.ToString();
            txtShomareHesab.Text = dgvHesab[2, dgvHesab.CurrentRow.Index].Value.ToString();
            txtMojodi.Text = dgvHesab[3, dgvHesab.CurrentRow.Index].Value.ToString();
            txtTozihat.Text = dgvHesab[4, dgvHesab.CurrentRow.Index].Value.ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport report = new StiReport();
            report.Load("Report/rptHesab.mrt");
            report.Compile();
            report.ShowWithRibbonGUI();
        }
    }
}