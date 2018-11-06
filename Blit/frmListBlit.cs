using Connection_Class;
using System;
using System.Windows.Forms;
using Stimulsoft.Report;

namespace Blit
{
    public partial class frmListBlit : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();
        public frmListBlit()
        {
            InitializeComponent();
        }

        void Display()
        {
            query.OpenConection();
            try
            {
                dgvBlit.DataSource = query.ShowData(string.Format("select ID,Tarikh,Saat,NameHavapeyma,FName +' '+LName as FullName,TedadBlit,Gheymat,GheymatKol from tblBlit where Tarikh Between '{0}' AND '{1}'", mskTarikhAz.Text, mskTarikhTa.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("در اتصال به پایگاه داده خطایی رخ داده است ، لطفا مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void frmListBlit_Load(object sender, EventArgs e)
        {
            //sakht shey az PersianCalendar
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            //meghdar dehi date tavasot P , agar month yekraghmi bod yek 0 gharar bde
            mskTarikhAz.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            mskTarikhTa.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");

            Display();
        }

        private void mskTarikhAz_TextChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void mskTarikhTa_TextChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                int x = Convert.ToInt32(dgvBlit.SelectedCells[0].Value);
                query.ExecuteQueries("delete from tblBlit where ID=" + x);
                MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Display();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport report = new StiReport();
            report.Load("Report/rptListBlit.mrt");
            report.Compile();
            report["tarikh_az"] = mskTarikhAz.Text;
            report["tarikh_ta"] = mskTarikhTa.Text;
            report.ShowWithRibbonGUI();
        }
    }
}