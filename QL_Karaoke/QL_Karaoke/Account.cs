using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_Karaoke
{
    public partial class Account : Form
    {
        public Account()
        {
            InitializeComponent();
        }

        QL_Karaoke qlAC = new QL_Karaoke();
        private void Account_Load(object sender, EventArgs e)
        {
            string userName = Form_Login.UserName;
            try
            {
                ListView listView = qlAC.selectInfoUser(userName);

                foreach (ListViewItem item in listView.Items)
                {
                    lblShowHoTen_AC.Text = item.Text;
                    lblShowGioiTinh_AC.Text = item.SubItems[1].Text;
                    lblShowNSinh_AC.Text = item.SubItems[2].Text;
                    lblShowCCCD_AC.Text = item.SubItems[3].Text;
                    lblShowSDT_AC.Text = item.SubItems[4].Text;
                    lblShowDChi_AC.Text = item.SubItems[5].Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
