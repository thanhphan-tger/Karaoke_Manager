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
    public partial class Form_QuanLyKaraoke : Form
    {
        public Form_QuanLyKaraoke()
        {
            InitializeComponent();

            btnP001.Click += btn_Click;
            btnP002.Click += btn_Click;
            btnP003.Click += btn_Click;
            btnP004.Click += btn_Click;
            btnP005.Click += btn_Click;
            btnP006.Click += btn_Click;
            btnP007.Click += btn_Click;
            btnP008.Click += btn_Click;
            btnP009.Click += btn_Click;
            btnP010.Click += btn_Click;
        }

        QL_Karaoke qlKr = new QL_Karaoke();

        private void menuAdmin_QL_Click(object sender, EventArgs e)
        {
            Form_Admin AD = new Form_Admin();
            this.Hide();
            AD.ShowDialog();
            this.Show();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            Form_HoaDon frmHD = new Form_HoaDon();
            frmHD.MaHD = qlKr.findMaHDNoPay(dtgvRoomInfo.Rows[0].Cells[0].Value.ToString());
            qlKr.updateGioRaForHD(frmHD.MaHD);
            this.Hide();
            frmHD.ShowDialog();

            Form_QuanLyKaraoke_Load(sender, e);
            btnThanhToan.Enabled = false;
            btnDatPhong_QL.Enabled = true;
            this.Show();
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Account AC = new Account();
            this.Hide();
            AC.ShowDialog();
            this.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_QuanLyKaraoke_Load(object sender, EventArgs e)
        {
            displayDMMon();
            showColor();
            btnDatPhong_QL.Enabled = false;
            btnThanhToan.Enabled = false;

            if (Form_Login.roles == "Admin")
                menuAdmin_QL.Visible = true;
        }

        void showColor()
        {
            for (int i = 1; i <= 10; i++)
            {
                string key;
                if (i >= 10)
                    key = "P0" + i;
                else
                    key = "P00" + i;
                
                switch (key)
                {
                    case "P001": panelP001.BackColor = checkStatus(key);
                        break;
                    case "P002":
                        panelP002.BackColor = checkStatus(key);
                        break;
                    case "P003":
                        panelP003.BackColor = checkStatus(key);
                        break;
                    case "P004":
                        panelP004.BackColor = checkStatus(key);
                        break;
                    case "P005":
                        panelP005.BackColor = checkStatus(key);
                        break;
                    case "P006":
                        panelP006.BackColor = checkStatus(key);
                        break;
                    case "P007":
                        panelP007.BackColor = checkStatus(key);
                        break;
                    case "P008":
                        panelP008.BackColor = checkStatus(key);
                        break;
                    case "P009":
                        panelP009.BackColor = checkStatus(key);
                        break;
                    case "P010":
                        panelP010.BackColor = checkStatus(key);
                        break;
                    default:
                        break;
                }
            }
        }

        Color checkStatus(string key)
        {
            if (qlKr.getRoomStatus(key) == "Sửa chữa")
                return Color.Red;
            if (qlKr.getRoomStatus(key)=="Đã đặt")
                return Color.Lime;
            return Color.PaleTurquoise;
        }

        void displayDMMon()
        {
            DataSet ds = new DataSet();
            qlKr.selectAll("DANHMUCHANG").Fill(ds, "DANHMUCHANG");


            cboDanhMucMon_QL.DisplayMember = "TENDM";
            cboDanhMucMon_QL.ValueMember = "MADM";
            cboDanhMucMon_QL.DataSource = ds.Tables[0];
        }

        private void comboBoxDanhMucMon_QL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();

                qlKr.selectTableByKey("MATHANG", "MADM", cboDanhMucMon_QL.SelectedValue.ToString()).Fill(ds, "MATHANG");

                cboMon_QL.DisplayMember = "TENMH";
                cboMon_QL.ValueMember = "MAMH";
                cboMon_QL.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void displayInfoRoom(string roomKey)
        {
            try
            {
                DataSet ds = new DataSet();

                qlKr.selectRoomByMaPH(roomKey).Fill(ds, "PHONG");
                dtgvRoomInfo.DataSource = ds.Tables[0];
                dtgvRoomInfo.Columns[0].HeaderCell.Value = "Mã phòng";
                dtgvRoomInfo.Columns[1].HeaderCell.Value = "Loại phòng";
                dtgvRoomInfo.Columns[2].HeaderCell.Value = "Giá phòng";
                dtgvRoomInfo.Columns[3].HeaderCell.Value = "Tình trạng phòng";
                dtgvRoomInfo.Columns[4].HeaderCell.Value = "Sức chứa";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string roomKey = btn.Name.Substring(btn.Name.Length - 4);

            displayInfoRoom(roomKey);


            string maHD = qlKr.findMaHDNoPay(roomKey);
            if (maHD == null)
            {
                lstChiTietHD.Items.Clear();
                btnThem_QL.Enabled = false;
                btnDatPhong_QL.Enabled = true;
                btnThanhToan.Enabled = false;
            }    
            else
            {
                displayCTMatHang(maHD);
                btnThem_QL.Enabled = true;
                btnDatPhong_QL.Enabled = false;
                btnThanhToan.Enabled = true;
            }    
        }

        private void btnThem_QL_Click(object sender, EventArgs e)
        {
            string maHD = qlKr.findMaHDNoPay(dtgvRoomInfo.Rows[0].Cells[0].Value.ToString());
            if (maHD != null)
            {
                qlKr.insertCTMatHang(cboMon_QL.SelectedValue.ToString(), maHD, Convert.ToInt32(numericUpDownSoLuong_QL.Value));
                displayCTMatHang(maHD);
            }
        }

        void displayCTMatHang(string maHD)
        {
            try
            {
                lstChiTietHD.Items.Clear();
                ListView listView = qlKr.selectCTMatHang(maHD);

                foreach (ListViewItem item in listView.Items)
                {
                    ListViewItem itemTemp = new ListViewItem();
                    itemTemp.Text = item.Text;

                    itemTemp.SubItems.Add(item.SubItems[1]);

                    lstChiTietHD.Items.Add(itemTemp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDatPhong_QL_Click(object sender, EventArgs e)
        {
            Form_DatPhong frm_DP = new Form_DatPhong();
            frm_DP.MaPhong = dtgvRoomInfo.Rows[0].Cells[0].Value.ToString();
            frm_DP.LoaiPhong = dtgvRoomInfo.Rows[0].Cells[1].Value.ToString();
            this.Hide();
            frm_DP.ShowDialog();

            Form_QuanLyKaraoke_Load(sender, e);
            
            this.Show();
        }
    }
}
