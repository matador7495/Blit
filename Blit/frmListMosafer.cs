using Connection_Class;
using Stimulsoft.Report;
using System;
using System.Windows.Forms;

namespace Blit
{
    public partial class frmListMosafer : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();
        string NameAgency;

        public frmListMosafer()
        {
            InitializeComponent();
        }
        void Display()
        {
            query.OpenConection();
            try
            {
                dgvListMosafer.DataSource = query.ShowData("select * from tblMosafer");

                var dr = query.DataReader("select NameAgency from tblSetting");
                if (dr.Read())
                {
                    NameAgency = dr["NameAgency"].ToString();
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
        void Search()
        {
            query.OpenConection();
            try
            {
                dgvListMosafer.DataSource = query.ShowData(string.Format("select * from tblMosafer where FName like '%' + '{0}' + '%' AND LName like '%' + '{1}' + '%'  ", txtFName.Text, txtLName.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("در اتصال به پایگاه داده خطایی رخ داده است ، لطفا مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }
        private void ListMosafer_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void txtFName_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                StiReport report = new StiReport();//sakhte Shey
                report.Load("Report/rptMosafer.mrt");//masir file MRT report
                report.Compile();
                //ersal Variable ha b Stimulsoft
                report["strNameAgency"] = NameAgency;
                report.ShowWithRibbonGUI();
            }
            catch (Exception)
            {
                MessageBox.Show("در هنگام گزارش گیری خطایی رخ داده است لطفا مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtLName_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                int x = Convert.ToInt32(dgvListMosafer.SelectedCells[0].Value);
                query.ExecuteQueries("delete from tblMosafer where id=" + x);
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