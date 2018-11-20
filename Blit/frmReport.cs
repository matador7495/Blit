using Connection_Class;
using System;
using System.Windows.Forms;
using Stimulsoft.Report;

namespace Blit
{
    public partial class frmReport : DevComponents.DotNetBar.Office2007Form
    {
        Connection_Query query = new Connection_Query();
        string NameAgency, TelA, AddressA;


        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            query.OpenConection();
            try
            {
                var q = query.ExecuteScaler("select COUNT(ID) from tblCity");
                lblCity.Text = Convert.ToString((int)q.ExecuteScalar());
                //****************************************************************
                var q1 = query.ExecuteScaler("select COUNT(ID) from tblHavapeyma");
                lblHavapeyma.Text = Convert.ToString((int)q1.ExecuteScalar());
                //****************************************************************
                var q2 = query.ExecuteScaler("select COUNT(ID) from tblHarekat");
                lblParvaz.Text = Convert.ToString((int)q2.ExecuteScalar());
                //****************************************************************
                var q3 = query.ExecuteScaler("select COUNT(ID) from tblBlit");
                lblTedadBlit.Text = Convert.ToString((int)q3.ExecuteScalar());
                //****************************************************************
                var q4 = query.ExecuteScaler("select SUM(Mojodi) from tblHesab");
                lblMojodi.Text = Convert.ToString((int)q4.ExecuteScalar());
                //****************************************************************
                var q5 = query.ExecuteScaler("select Count(ID) from tblGroups");
                lblTedadGroupha.Text = Convert.ToString((int)q5.ExecuteScalar());

                //****************************************************************
                var dr = query.DataReader("Select * from tblsetting where id=1");
                if (dr.Read())
                {
                    NameAgency = dr["NameAgency"].ToString();
                    TelA = dr["TelA"].ToString();
                    AddressA = dr["AddressA"].ToString();
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
                
                MessageBox.Show("اطلاعاتی برای موجودی حساب وجود ندارد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            query.CloseConnection();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport report = new StiReport();
            report.Load("Report/rptReportALL.mrt");
            report.Compile();
            report["NameAgency"] = NameAgency;
            report["TelA"] = TelA;
            report["AddressA"] = AddressA;
            report["TCity"] = lblCity.Text;
            report["THavapeyma"] = lblHavapeyma.Text;
            report["TParvaz"] = lblParvaz.Text;
            report["TBlit"] = lblTedadBlit.Text;
            report["TGroup"] = lblTedadGroupha.Text;
            report["Mojodi"] = lblMojodi.Text;
            report.ShowWithRibbonGUI();
        }
    }
}