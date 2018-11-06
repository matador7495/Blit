using ClearClass;
using Connection_Class;
using System;
using System.Windows.Forms;

namespace Blit
{
    public partial class frmHarekat : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();
        public frmHarekat()
        {
            InitializeComponent();
        }

        private void frmHarekat_Load(object sender, EventArgs e)
        {
            //sakht shey az PersianCalendar
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            //meghdar dehi date tavasot P , agar month yekraghmi bod yek 0 gharar bde
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");

            try
            {
                cmbNameCity.DataSource = query.ShowData("select NameCity from tblCity");
                cmbNameCity.DisplayMember = "NameCity";
            }
            catch (Exception)
            {
                MessageBox.Show("در هنگام اتصال به بانک اطلاعاتی خطایی رخ داده است ، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //tarif sa@t jary system
            txtTime.Text = DateTime.Now.Hour.ToString("0#") + ":" + DateTime.Now.Minute.ToString("0#") + ":" + DateTime.Now.Second.ToString();
        }

        private void btnSearchCodeH_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                var dr = query.DataReader("select * from tblHavapeyma where ID=" + txtCodeHavapeyma.Text);
                if (dr.Read())
                {
                    txtCodeHavapeyma.Text = dr["ID"].ToString();
                    txtNameHavapeyma.Text = dr["NameHavapeyma"].ToString();
                }
                else
                {
                    MessageBox.Show("اطلاعاتی برای این کد پیدا نشد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodeHavapeyma.Text = string.Empty;
                    txtCodeHavapeyma.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            query.OpenConection();

            try
            {
                query.ExecuteQueries(string.Format("insert into tblHarekat values('{0}','{1}','{2}','{3}','{4}','{5}')", txtCodeHavapeyma.Text, txtNameHavapeyma.Text, mskTarikh.Text, txtTime.Text, cmbNameCity.Text, txtTedad.Text));
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
                var dr = query.DataReader("select * from tblHarekat where ID=" + txtCode.Text);
                if (dr.Read())
                {
                    txtCode.Text = dr["ID"].ToString();
                    txtCodeHavapeyma.Text = dr["ID_Havapeyma"].ToString();
                    txtNameHavapeyma.Text = dr["NameHavapeyma"].ToString();
                    mskTarikh.Text = dr["Tarikh"].ToString();
                    txtTime.Text = dr["Saat"].ToString();
                    cmbNameCity.Text = dr["NameCity"].ToString();
                    txtTedad.Text = dr["Tedad"].ToString();
                }
                else
                {
                    MessageBox.Show("اطلاعاتی برای این کد پیدا نشد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodeHavapeyma.Text = string.Empty;
                    txtCode.Focus();
                }
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
                query.ExecuteQueries("delete from tblHarekat where ID=" + txtCode.Text);
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
                query.ExecuteQueries("update tblHarekat set ID_Havapeyma='" + txtCodeHavapeyma.Text + "',NameHavapeyma='" + txtNameHavapeyma.Text + "',Tarikh='" + mskTarikh.Text + "',Saat='" + txtTime.Text + "',NameCity='" + cmbNameCity.Text + "',Tedad='" + txtTedad.Text + "' where ID=" + txtCode.Text);
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