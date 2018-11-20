using System;
using System.Windows.Forms;
using Connection_Class;

namespace Blit
{
    public partial class frmMain : Form
    {
        Connection_Query query = new Connection_Query();
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            new frmLogin().ShowDialog();

            //sakht shey az PersianCalendar
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            //meghdar dehi date tavasot P , agar month yekraghmi bod yek 0 gharar bde
            lblDate.Text = p.GetYear(DateTime.Now).ToString() + "/" + p.GetMonth(DateTime.Now).ToString("0#") + "/" + p.GetDayOfMonth(DateTime.Now).ToString("0#");

            //namayesh roz haye hafte ba Switch Case
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    lblDay.Text = "یکشنبه";
                    break;
                case DayOfWeek.Monday:
                    lblDay.Text = "دوشبنه";
                    break;
                case DayOfWeek.Tuesday:
                    lblDay.Text = "سه شنبه";
                    break;
                case DayOfWeek.Wednesday:
                    lblDay.Text = "چهار شنبه";
                    break;
                case DayOfWeek.Thursday:
                    lblDay.Text = "پنج شنبه";
                    break;
                case DayOfWeek.Friday:
                    lblDay.Text = "جمعه";
                    break;
                case DayOfWeek.Saturday:
                    lblDay.Text = "شنبه";
                    break;
                default:
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //tarif sa@t jary system
            lblTime.Text = DateTime.Now.Hour.ToString("0#") + ":" + DateTime.Now.Minute.ToString("0#") + ":" + DateTime.Now.Second.ToString();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            new frmSetting().ShowDialog();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            new frmUsers().ShowDialog();
        }

        private void btnGroups_Click(object sender, EventArgs e)
        {
            new frmGroups().ShowDialog();
        }

        private void btnListGroups_Click(object sender, EventArgs e)
        {
            new frmListGroups().ShowDialog();
        }

        private void btnCitys_Click(object sender, EventArgs e)
        {
            new frmCitys().ShowDialog();
        }

        private void btnListCity_Click(object sender, EventArgs e)
        {
            new frmListCitys().ShowDialog();
        }

        private void btnMosafer_Click(object sender, EventArgs e)
        {
            new frmMosafer().ShowDialog();
        }

        private void btnListMosafer_Click(object sender, EventArgs e)
        {
            new frmListMosafer().ShowDialog();
        }

        private void btnHavapeyma_Click(object sender, EventArgs e)
        {
            new frmHavapeyma().ShowDialog();
        }

        private void btnListHavapeyma_Click(object sender, EventArgs e)
        {
            new frmListHavapeyma().ShowDialog();
        }

        private void btnNooBlit_Click(object sender, EventArgs e)
        {
            new frmNooBlit().ShowDialog();
        }

        private void btnListNooBlit_Click(object sender, EventArgs e)
        {
            new frmListNooBlit().ShowDialog();
        }

        private void btnBlit_Click(object sender, EventArgs e)
        {
            new frmBlit().ShowDialog();
        }

        private void btnHesab_Click(object sender, EventArgs e)
        {
            new frmHesab().ShowDialog();
        }

        private void btnListBlit_Click(object sender, EventArgs e)
        {
            new frmListBlit().ShowDialog();
        }

        private void btnHarekat_Click(object sender, EventArgs e)
        {
            new frmHarekat().ShowDialog();
        }

        private void btnListHarekat_Click(object sender, EventArgs e)
        {
            new frmListHarekat().ShowDialog();
        }

        private void btnReportHavapeyma_Click(object sender, EventArgs e)
        {
            new frmReportHavapeyma().ShowDialog();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            new frmReport().ShowDialog();
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            new frmNote().ShowDialog();
        }

        public void BackUpDB(string FileName)
        {
            try
            {
                query.OpenConection();
                query.ExecuteQueries(@"BACKUP DATABASE [Blit] TO  DISK='" + FileName + "' WITH NOFORMAT, NOINIT, NAME = N'Blit-Full Database Backup', SKIP, NOREWIND, NOUNLOAD");
                MessageBox.Show("عملیات پشتیبان گیری با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                query.CloseConnection();
            }
        }

        public void RestoreDB(string FileName)
        {
            try
            {
                query.OpenConection();
                query.ExecuteQueries(@"Alter DATABASE [Blit] SET SINGLE_USER with ROLLBACK IMMEDIATE " + "USE master " + " RESTORE DATABASE [Blit] FROM DISK =N'" + FileName + "' with RECOVERY,REPLACE");
                MessageBox.Show("عملیات بازیابی با موفقیت انجام شد", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی رخ داده است، مجددا تلاش کنید", "Blit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                query.CloseConnection();
            }
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true;
            sfd.Filter = @"SQL BackUp FIles ALL Files (*.*) |*.*| (*.Bak)|*.Bak";
            sfd.DefaultExt = "Bak";
            sfd.FilterIndex = 1;
            sfd.FileName = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
            sfd.Title = "BackUp SQL Files";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                BackUpDB(sfd.FileName);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = @"SQL BackUp FIles ALL Files (*.*) |*.*| (*.Bak)|*.Bak";
            ofd.FilterIndex = 1;
            ofd.Title = "BackUp SQL Files";
            ofd.FileName = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                RestoreDB(ofd.FileName);
            }
        }
    }
}