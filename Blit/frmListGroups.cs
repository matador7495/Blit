using System;
using System.Windows.Forms;
using Connection_Class;
using Stimulsoft.Report;

namespace Blit
{
    public partial class frmListGroups : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();
        string NameAgency, TelA, AddressA;

        public frmListGroups()
        {
            InitializeComponent();
        }
        void Display()
        {
            query.OpenConection();
            try
            {
                dgvGroups.DataSource = query.ShowData("select * from tblGroups");

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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                StiReport report = new StiReport();//sakhte Shey
                report.Load("Report/rptGroups.mrt");//masir file MRT report
                report.Compile();
                //ersal Variable ha b Stimulsoft
                report["strNameAgency"] = NameAgency;
                report["strTelA"] = TelA;
                report["strAddressA"] = AddressA;
                report.ShowWithRibbonGUI();
            }
            catch (Exception)
            {
                MessageBox.Show("در هنگام گزارش گیری خطایی رخ داده است لطفا مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                int x = Convert.ToInt32(dgvGroups.SelectedCells[0].Value);
                query.ExecuteQueries("delete from tblGroups where ID=" + x);
                MessageBox.Show("عملیات با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Display();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }
        private void frmListGroups_Load(object sender, EventArgs e)
        {
            Display();
        }
    }
}