using Connection_Class;
using System;
using System.Windows.Forms;
using Stimulsoft.Report;

namespace Blit
{
    public partial class frmListNooBlit : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();
        public frmListNooBlit()
        {
            InitializeComponent();
        }
        public void Display()
        {
            query.OpenConection();
            try
            {
                dgvListNooBlit.DataSource = query.ShowData("select * from tblNooBlit");
            }
            catch (Exception)
            {
                MessageBox.Show("در اتصال به پایگاه داده خطایی رخ داده است ، لطفا مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }
        public void Search()
        {
            query.OpenConection();
            try
            {
                dgvListNooBlit.DataSource = query.ShowData(string.Format("select * from tblNooBlit where NooBlit like '%' + '{0}' + '%' AND NameCity like '%' + '{1}' + '%' ", txtNooBlit.Text, txtNameCity.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("در اتصال به پایگاه داده خطایی رخ داده است ، لطفا مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }

        private void frmListNooBlit_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void txtNooBlit_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void txtNameCity_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                int x = Convert.ToInt32(dgvListNooBlit.SelectedCells[0].Value);
                query.ExecuteQueries("delete from tblNooBlit where id=" + x);
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
            if (string.IsNullOrEmpty(txtPrintNameCity.Text))
            {
                MessageBox.Show("لطفا نام شهر را جهت پرینت وارد کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                StiReport report = new StiReport();
                report.Load("Report/rptNooBlit.mrt");
                report.Compile();
                report["strCity"] = txtPrintNameCity.Text;
                report.ShowWithRibbonGUI();
            }
        }
    }
}