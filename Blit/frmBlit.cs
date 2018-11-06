using ClearClass;
using Connection_Class;
using System;
using System.Windows.Forms;
using Stimulsoft.Report;

namespace Blit
{
    public partial class frmBlit : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();
        string NameAgency, TelA, AddressA;

        public frmBlit()
        {
            InitializeComponent();
        }
        void Display()
        {
            query.OpenConection();
            try
            {
                var dr = query.DataReader("select * from tblSetting");
                if (dr.Read())
                {
                    NameAgency = dr["NameAgency"].ToString();
                    TelA = dr["TelA"].ToString();
                    AddressA = dr["AddressA"].ToString();
                }
                else
                {
                    MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("در اتصال به پایگاه داده خطایی رخ داده است ، لطفا مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
            query.CloseConnection();
        }

        private void btnSearchHavapeyma_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                var dr = query.DataReader("select * from tblHavapeyma where ID=" + txtCodeHavapeyma.Text);
                if (dr.Read())
                {
                    txtCodeHavapeyma.Text = dr["ID"].ToString();
                    txtNameHavapeyma.Text = dr["NameHavapeyma"].ToString();
                    txtTedadSandali.Text = dr["Tedad"].ToString();
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

        private void btnSearchNooBlit_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                var dr = query.DataReader("select * from tblNooBlit where ID=" + txtCodeNooBlit.Text);
                if (dr.Read())
                {
                    txtCodeNooBlit.Text = dr["ID"].ToString();
                    txtNooBlit.Text = dr["NooBlit"].ToString();
                    txtNameCity.Text = dr["NameCity"].ToString();
                    txtGheymat.Text = dr["Gheymat"].ToString();
                }
                else
                {
                    MessageBox.Show("اطلاعاتی برای این کد پیدا نشد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodeNooBlit.Text = string.Empty;
                    txtCodeNooBlit.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void btnSearchCodeMosafer_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                var dr = query.DataReader("select * from tblMosafer where ID=" + txtCodeMosafer.Text);

                if (dr.Read())
                {
                    txtCodeMosafer.Text = dr["ID"].ToString();
                    txtFName.Text = dr["FName"].ToString();
                    txtLName.Text = dr["LName"].ToString();
                }
                else
                {
                    MessageBox.Show("اطلاعاتی برای این کد پیدا نشد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodeMosafer.Text = string.Empty;
                    txtCodeMosafer.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void frmBlit_Load(object sender, EventArgs e)
        {
            Display();
            //sakht shey az PersianCalendar
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            //meghdar dehi date tavasot P , agar month yekraghmi bod yek 0 gharar bde
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
       
            cmbShomareHesab.DataSource = query.ShowData("SELECT Convert (nvarchar(50),ShomareHesab)  + ' - ' + NameHesab  AS Hesab ,ShomareHesab AS shomare_h FROM tblHesab");
            cmbShomareHesab.DisplayMember = "Hesab";
            cmbShomareHesab.ValueMember = "shomare_h";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //tarif sa@t jary system
            lblTime.Text = DateTime.Now.Hour.ToString("0#") + ":" + DateTime.Now.Minute.ToString("0#") + ":" + DateTime.Now.Second.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                int tedad_sandali;
                int tedad_blit;
                var q = query.ExecuteScaler("select Tedad from tblHavapeyma where ID=" + txtCodeHavapeyma.Text);
                tedad_sandali = (int)q.ExecuteScalar();
                tedad_blit = Convert.ToInt32(txtTedadBlit.Text);
                if (tedad_blit > tedad_sandali)
                {
                    MessageBox.Show("مقدار تعداد بلیط های وارد شده از حد مجاز ظرفیت صندلی های این هواپیما بیشتر است", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int reserve = tedad_sandali - tedad_blit;
                    query.ExecuteQueries("update tblHavapeyma set Tedad='" + reserve + "' where ID=" + txtCodeHavapeyma.Text);
                    query.ExecuteQueries(string.Format("insert into tblBlit values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", mskTarikh.Text, lblTime.Text, txtCodeHavapeyma.Text, txtNameHavapeyma.Text, txtTedadSandali.Text, txtNooBlit.Text, txtNameCity.Text, txtGheymat.Text, txtFName.Text, txtLName.Text, txtTedadBlit.Text, txtNumSandali.Text, txtGheymatKol.Text, txtTozihat.Text));
                    MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearControls.ClearTextBoxes(this);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void btnSearchBlit_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                var dr = query.DataReader("select * from tblBlit where ID=" + txtCode.Text);
                if (dr.Read())
                {
                    txtCode.Text = dr["ID"].ToString();
                    mskTarikh.Text = dr["Tarikh"].ToString();
                    lblTime.Text = dr["Saat"].ToString();
                    txtCodeHavapeyma.Text = dr["ID_Havapeyma"].ToString();
                    txtNameHavapeyma.Text = dr["NameHavapeyma"].ToString();
                    txtTedadSandali.Text = dr["TedadSandali"].ToString();
                    txtNooBlit.Text = dr["NooBlit"].ToString();
                    txtNameCity.Text = dr["NameCity"].ToString();
                    txtGheymat.Text = dr["Gheymat"].ToString();
                    txtFName.Text = dr["FName"].ToString();
                    txtLName.Text = dr["LName"].ToString();
                    txtTedadBlit.Text = dr["TedadBlit"].ToString();
                    txtNumSandali.Text = dr["NumSandali"].ToString();
                    txtGheymatKol.Text = dr["GheymatKol"].ToString();
                    txtTozihat.Text = dr["Tozihat"].ToString();
                }
                else
                {
                    MessageBox.Show("اطلاعاتی برای این کد پیدا نشد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCode.Text = string.Empty;
                    txtCode.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("در هنگام اتصال به بانک اطلاعاتی خطایی رخ داده است ، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                query.ExecuteQueries("delete from tblBlit where ID=" + txtCode.Text);
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
                query.ExecuteQueries("update tblBlit set Tarikh='" + mskTarikh.Text + "',Saat='" + lblTime.Text + "',ID_Havapeyma='" + txtCodeHavapeyma.Text + "',NameHavapeyma='" + txtNameHavapeyma.Text + "',Tedadsandali='" + txtTedadSandali.Text + "',NooBlit='" + txtNooBlit.Text + "',NameCity='" + txtNameCity.Text + "',Gheymat='" + txtGheymat.Text + "',FName='" + txtFName.Text + "',LName='" + txtLName.Text + "',TedadBlit='" + txtTedadBlit.Text + "',NumSandali='" + txtNumSandali.Text + "',GheymatKol='" + txtGheymatKol.Text + "',Tozihat='" + txtTozihat.Text + "' where ID=" + txtCode.Text);
                MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls.ClearTextBoxes(this);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                StiReport report = new StiReport();
                report.Load("Report/rptBlit.mrt");
                report.Compile();
                report["Code"] =Convert.ToInt32( txtCode.Text);
                report["strNameAgency"] = NameAgency;
                report["strAddressA"] = AddressA;
                report["strTelA"] = TelA;

                report.ShowWithRibbonGUI();
            }
            catch (Exception)
            {
                MessageBox.Show("در هنگام گزارش گیری خطایی رخ داده است لطفا مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            int gheymat, tedad_blit, sum = 0;

            gheymat = Convert.ToInt32(txtGheymat.Text);
            tedad_blit = Convert.ToInt32(txtTedadBlit.Text);
            sum = gheymat * tedad_blit;
            txtGheymatKol.Text = sum.ToString();
        }

        private void btnPardakht_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                int mojodi_hesab;
                int mablagh_kol;
                var q = query.ExecuteScaler("select Mojodi from tblHesab where ShomareHesab=" + cmbShomareHesab.SelectedValue.ToString());
                mojodi_hesab = ((int)q.ExecuteScalar());
                mablagh_kol = Convert.ToInt32(txtGheymatKol.Text);
                int variz = mojodi_hesab + mablagh_kol;
                query.ExecuteQueries("update tblHesab set Mojodi=" + variz + " where ShomareHesab=" + cmbShomareHesab.SelectedValue.ToString());
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