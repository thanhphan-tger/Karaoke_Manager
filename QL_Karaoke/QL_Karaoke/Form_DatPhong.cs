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
    public partial class Form_DatPhong : Form
    {
        public Form_DatPhong()
        {
            InitializeComponent();

            txtCCCD_DP.KeyPress += txt_KeyPress;
            txtPhone_DP.KeyPress += txt_KeyPress;
        }

        QL_Karaoke qlDP = new QL_Karaoke();

        string loaiPhong, maPhong;

        public string LoaiPhong { get => loaiPhong; set => loaiPhong = value; }
        public string MaPhong { get => maPhong; set => maPhong = value; }

        private void Form_DatPhong_Load(object sender, EventArgs e)
        {
            txtLoaiPhong_DP.Text = loaiPhong;
            txtPhong_DP.Text = maPhong;
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtCCCD_DP_TextChanged(object sender, EventArgs e)
        {
            if (txtCCCD_DP.Text.Length != 10)
            {
                errorProvider1.SetError(txtCCCD_DP, "Bạn phải nhập đủ 10 kí tự số");
                btnFind.Enabled = false;
            }
            else
            {
                errorProvider1.Clear();
                btnFind.Enabled = true;
            }
        }

        private void txtPhone_DP_TextChanged(object sender, EventArgs e)
        {
            if (txtPhone_DP.Text.Length != 10)
                errorProvider1.SetError(txtPhone_DP, "Bạn phải nhập đủ 10 kí tự số");
            else
                errorProvider1.Clear();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            ListView listView = qlDP.findKHByCCCD(txtCCCD_DP.Text);

            if (listView.Items.Count != 0)
            {
                txtMaKH.Text = listView.Items[0].Text;
                txtCusName_DP.Text = listView.Items[0].SubItems[2].Text;
                dtBirthday_DP.Text = listView.Items[0].SubItems[3].Text;
                txtPhone_DP.Text = listView.Items[0].SubItems[4].Text;
                txtAddress_DP.Text = listView.Items[0].SubItems[6].Text;
                
                btnCreateMaKH.Enabled = false;

                lblLoaiKH_DP.Visible = true;
                txtLoaiKH.Visible = true;
                txtLoaiKH.Text = listView.Items[0].SubItems[5].Text;

                lblDTLuy.Visible = true;
                txtDTLuy.Visible = true;
                txtDTLuy.Text = listView.Items[0].SubItems[7].Text;
            }
            else
            {
                MessageBox.Show("Thông tin khách không tồn tại", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtMaKH.Clear();
                txtCusName_DP.Clear();
                dtBirthday_DP.Text = DateTime.Now.ToShortDateString();
                txtAddress_DP.Clear();
                txtPhone_DP.Clear();
                btnCreateMaKH.Enabled = true;

                lblLoaiKH_DP.Visible = false;
                txtLoaiKH.Visible = false;

                lblDTLuy.Visible = false;
                txtDTLuy.Visible = false;
            }
        }

        private void btnCreateMaKH_Click(object sender, EventArgs e)
        {
            int count = qlDP.countRowsInTable("KHACHHANG");
            count++;
            txtMaKH.Text = "KH" + string.Format("{0:D5}", count);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDat_Click(object sender, EventArgs e)
        {
            try
            {
                qlDP.insertCustomer(txtMaKH.Text, txtCCCD_DP.Text,
                                txtCusName_DP.Text, dtBirthday_DP.Text,
                                txtPhone_DP.Text, txtLoaiKH.Text,
                                txtAddress_DP.Text, Convert.ToInt32(txtDTLuy.Text));

                qlDP.updateRoomStatus(txtPhong_DP.Text, txtTTPhong_DP.Text);

                string maPH = qlDP.createMaPhieuDP();
                qlDP.insertPhieuDP(maPH, txtMaKH.Text, txtPhong_DP.Text);

                string maNV = qlDP.findMaNV(Form_Login.UserName);
                int diem = Convert.ToInt32(txtDTLuy.Text);
                double giamGia = 0;
                if (diem > 0 && diem < 20)
                    giamGia = 0.05;
                else
                {
                    if (diem < 50)
                        giamGia = 0.1;
                    else
                        giamGia = 0.15;
                }
                qlDP.insertHD(qlDP.createMaHD(), maPH, maNV, giamGia);

                DialogResult r = MessageBox.Show("Đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (r == DialogResult.OK)
                    this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Đặt phòng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
