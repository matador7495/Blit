using System;
using System.Windows.Forms;

namespace Blit
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
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
            new FrmCitys().ShowDialog();
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
    }
}