using Connection_Class;
using System;
using System.Windows.Forms;
using Stimulsoft.Report;

namespace Blit
{
    public partial class frmListHavapeyma : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();
        string NameAgency;
        public frmListHavapeyma()
        {
            InitializeComponent();
        }
        void Display()
        {
            query.OpenConection();
            try
            {
                dgvListHavapeyma.DataSource = query.ShowData("select * from tblHavapeyma");

                var dr = query.DataReader("select NameAgency from tblSetting");
                if (dr.Read())
                {
                    NameAgency = dr["NameAgency"].ToString();
                }
                else
                {
                    MessageBox.Show("ابتدا در بخش تنظیمات مشخصات آژانس را وارد کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    new frmSetting().ShowDialog();
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("در اتصال به پایگاه داده خطایی رخ داده است ، لطفا مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }
        void Search()
        {
            query.OpenConection();
            try
            {
                dgvListHavapeyma.DataSource = query.ShowData(string.Format("select * from tblHavapeyma where NameHavapeyma like '%' + '{0}' + '%' AND NameGroup like '%' + '{1}' + '%'  ", txtName.Text, txtNameGroup.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("در اتصال به پایگاه داده خطایی رخ داده است ، لطفا مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }
        private void frmListHavapeyma_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void txtNameGroup_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport report = new StiReport();
            report.Load("Report/rptHavapeyma.mrt");
            report.Compile();
            report.ShowWithRibbonGUI();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                int x = Convert.ToInt32(dgvListHavapeyma.SelectedCells[0].Value);
                query.ExecuteQueries("delete from tblHavapeyma where id=" + x);
                MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Display();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }
    }
}