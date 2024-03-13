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
    public partial class Form_HoaDon : Form
    {
        public Form_HoaDon()
        {
            InitializeComponent();
        }

        QL_Karaoke qlHD = new QL_Karaoke();
        string maHD;

        public string MaHD { get => maHD; set => maHD = value; }

        void displayCTMatHang(string maHD)
        {
            try
            {
                lstCTMHang.Items.Clear();
                ListView listView = qlHD.selectCTMatHangForHD(maHD);

                foreach (ListViewItem item in listView.Items)
                {
                    ListViewItem itemTemp = new ListViewItem();
                    itemTemp.Text = item.Text;

                    
                    itemTemp.SubItems.Add(item.SubItems[1]);
                    itemTemp.SubItems.Add(item.SubItems[2]);
                    itemTemp.SubItems.Add(item.SubItems[3]);
                    itemTemp.SubItems.Add(item.SubItems[4]);

                    lstCTMHang.Items.Add(itemTemp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form_HoaDon_Load(object sender, EventArgs e)
        {
            displayCTMatHang(maHD);
            displayCTPhong(maHD);
            txtSumMoney.Text = qlHD.sumMoney(maHD).ToString();
        }

        void displayCTPhong(string maHD)
        {
            try
            {
                lstCTPhong.Items.Clear();
                ListView listView = qlHD.selectCTPhongForHD(maHD);

                foreach (ListViewItem item in listView.Items)
                {
                    ListViewItem itemTemp = new ListViewItem();
                    itemTemp.Text = item.Text;


                    itemTemp.SubItems.Add(item.SubItems[1]);
                    itemTemp.SubItems.Add(item.SubItems[2]);
                    itemTemp.SubItems.Add(item.SubItems[3]);
                    itemTemp.SubItems.Add(item.SubItems[4]);
                    itemTemp.SubItems.Add(item.SubItems[5]);
                    lstCTPhong.Items.Add(itemTemp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            Form_InHĐ frm = new Form_InHĐ();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
    }
}
