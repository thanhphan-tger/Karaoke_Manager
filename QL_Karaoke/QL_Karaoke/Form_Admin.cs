using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace QL_Karaoke
{
    public partial class Form_Admin : Form
    {
        public Form_Admin()
        {
            InitializeComponent();
        }

        QL_Karaoke qlAD = new QL_Karaoke();

        public System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo { NumberGroupSeparator = "." };
        private void Form_Admin_Load(object sender, EventArgs e)
        {
            load_Admin();
        }

        /*--------------------------------------------------------------------*/
        /*|                           FORM_DOANHTHU                          |*/
        /*--------------------------------------------------------------------*/

        void load_Admin()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da = qlAD.selectAll("HOADON");

            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var item = new ListViewItem(new[] { dt.Rows[i][0].ToString(), dt.Rows[i][3].ToString(), qlAD.CheckStatus((int)dt.Rows[i][7]), Double.Parse(dt.Rows[i][6].ToString()).ToString("#,##", nfi) + " vnđ" });
                lstDSHoaDon_AD.Items.Add(item);
            }

            foreach (ListViewItem item in lstChiTietHD_AD.Items)
            {
                item.SubItems.Add("");
            }
            rdo_theongay.Checked = true;
            date_theothang.Enabled = false;
            date_theothang.CustomFormat = "MM/yyyy";
        }

        private void lstDSHoaDon_AD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem x = lstDSHoaDon_AD.FocusedItem;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da = qlAD.TimHoaDon(x.SubItems[0].Text);

                DataTable dt = new DataTable();
                DataTable value = new DataTable();
                da.Fill(dt);
                da = qlAD.TinhTienDV(x.SubItems[0].Text);
                da.Fill(value);
                lstChiTietHD_AD.Columns[1].Text = x.SubItems[0].Text;

                foreach (ListViewItem item in lstChiTietHD_AD.Items)
                {
                    if (item.Text == "Ngày lập:")
                    {
                        item.SubItems[1].Text = dt.Rows[0][1].ToString();
                    }
                    if (item.Text == "Giờ vào:")
                    {
                        item.SubItems[1].Text = dt.Rows[0][2].ToString();
                    }
                    if (item.Text == "Giờ ra:")
                    {
                        item.SubItems[1].Text = dt.Rows[0][3].ToString();
                    }
                    if (item.Text == "Tiền DV:")
                    {
                        item.SubItems[1].Text = value.Rows[0][0].ToString();
                    }
                    if (item.Text == "Giảm giá:")
                    {
                        item.SubItems[1].Text = dt.Rows[0][4].ToString();
                    }
                    if (item.Text == "Trạng thái:")
                    {
                        item.SubItems[1].Text = qlAD.CheckStatus((int)dt.Rows[0][5]);
                    }
                    if (item.Text == "Tổng tiền:")
                    {
                        item.SubItems[1].Text = Double.Parse(dt.Rows[0][6].ToString()).ToString("#,##", nfi) + " vnđ";
                    }
                }
            }
            catch
            {
                MessageBox.Show("Không tìm thấy!");
            }
        }

        private void btnTimHD_AD_Click(object sender, EventArgs e)
        {

            SqlDataAdapter da = new SqlDataAdapter();

            if (rdo_theongay.Checked == true)
                da = qlAD.SearchHoaDonTheoNgay(date_theongay.Text.ToString());
            else
            {
                if (rdo_theothang.Checked == true)
                    da = qlAD.SearchHoaDonTheoThang(date_theothang.Text.ToString());
                else
                    MessageBox.Show("Vui lòng chọn cách tính tổng doanh thu! (theo ngày hay theo tháng)");
            }

            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count <= 0)
                MessageBox.Show("Lỗi! Không tìm thấy hóa đơn!");
            else
            {
                double sum = 0;
                lstDSHoaDon_AD.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new ListViewItem(new[] { dt.Rows[i][0].ToString(), dt.Rows[i][3].ToString(), qlAD.CheckStatus((int)dt.Rows[i][7]), Double.Parse(dt.Rows[i][6].ToString()).ToString("#,##", nfi) + " vnđ" });
                    lstDSHoaDon_AD.Items.Add(item);

                    sum += Double.Parse(dt.Rows[i].ItemArray[6].ToString());
                }

                txt_Total_Icome.Text = sum.ToString("#,##", nfi) + " vnđ";
            }
        }

        public void Refesh(int kq)
        {
            if (kq == 0)
                MessageBox.Show("Cập nhật không thành công");
            else
            {
                MessageBox.Show("Cập nhật thành công");
                this.Refresh();
                lstDSHoaDon_AD.Items.Clear();
                lstChiTietHD_AD.Columns[1].Text = "";
                foreach (ListViewItem item in lstChiTietHD_AD.Items)
                {
                    item.SubItems[1].Text = "";
                }

                load_Admin();
            }
        }

        public void Refesh()
        {
            this.Refresh();
            lstDSHoaDon_AD.Items.Clear();
            lstChiTietHD_AD.Columns[1].Text = "";
            foreach (ListViewItem item in lstChiTietHD_AD.Items)
            {
                item.SubItems[1].Text = "";
            }
            load_Admin();
        }

        private void btnRefeshHD_AD_Click(object sender, EventArgs e)
        {
            Refesh();
        }

        private void rdo_theongay_CheckedChanged(object sender, EventArgs e)   
        {
            if (rdo_theongay.Checked == true)
            {
                date_theongay.Enabled = true;
                date_theothang.Enabled = false;
            }
            else
            {
                date_theothang.Enabled = true;
                date_theongay.Enabled = false;
            }
        }

        /*--------------------------------------------------------------------*/
        /*|                           FORM_MATHANG                           |*/
        /*--------------------------------------------------------------------*/

        private void tabAdmin_AD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabAdmin_AD.SelectedTab.Text == "Kho hàng")
            {
                lstDSHang_AD.Items.Clear();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                da = qlAD.selectAllMatHang();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem(new[] { dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), Double.Parse(dt.Rows[i][2].ToString()).ToString("#,##", nfi) + " vnđ", dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString() });
                    lstDSHang_AD.Items.Add(item);
                }
                LoadComboxDanhMuc();
                LoadComboxDVT();
            }
            else if (tabAdmin_AD.SelectedTab.Text == "Nhân viên")
            {
                loadListNhanVien();
                loadQuyenDN();
            }
        }

        private void RefeshlstMatHang()
        {
            lstDSHang_AD.Items.Clear();

            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            da = qlAD.selectAllMatHang();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(new[] { dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), Double.Parse(dt.Rows[i][2].ToString()).ToString("#,##", nfi) + " vnđ", dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString() });
                lstDSHang_AD.Items.Add(item);
            }
            txtMaMatHang_AD.Text = "";
            txtTenMatHang_AD.Text = "";
            comb_DVT.SelectedIndex = 0;
            comboBoxDanhMuc_AD.SelectedIndex = 0;
            txtDonGia_AD.Text = "";
        }

        public void LoadComboxDanhMuc()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da = qlAD.selectAll("DANHMUCHANG");
            da.Fill(ds, "DANHMUCHANG");
            comboBoxDanhMuc_AD.DisplayMember = "TENDM";
            comboBoxDanhMuc_AD.ValueMember = "MADM";
            comboBoxDanhMuc_AD.DataSource = ds.Tables[0];
        }

        public void LoadComboxDVT()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da = qlAD.selectAll("DOVITINH");
            da.Fill(ds, "DOVITINH");
            comb_DVT.DisplayMember = "TENDVT";
            comb_DVT.ValueMember = "MADVT";
            comb_DVT.DataSource = ds.Tables[0];
        }

        private void lstDSHang_AD_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable ds = new DataTable();

            da = qlAD.TimMatHang(lstDSHang_AD.FocusedItem.SubItems[0].Text);
            da.Fill(ds);
            txtMaMatHang_AD.Text = lstDSHang_AD.FocusedItem.SubItems[0].Text;
            txtTenMatHang_AD.Text = lstDSHang_AD.FocusedItem.SubItems[1].Text;
            int i = comboBoxDanhMuc_AD.FindString(lstDSHang_AD.FocusedItem.SubItems[3].Text);
            comboBoxDanhMuc_AD.SelectedIndex = i;
            txtDonGia_AD.Text = ds.Rows[0][4].ToString();

            int j = comb_DVT.FindString(lstDSHang_AD.FocusedItem.SubItems[4].Text);
            comb_DVT.SelectedIndex = j;
        }

        private void btn_TaoMa_Click(object sender, EventArgs e)
        {
            txtMaMatHang_AD.Text = qlAD.TaoMaMH();
            txtTenMatHang_AD.Text = "";
            comb_DVT.SelectedIndex = 0;
            comboBoxDanhMuc_AD.SelectedIndex = 0;
            txtDonGia_AD.Text = "";
        }

        private void btnThem_AD_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            da = qlAD.selectAll("MATHANG");
            string error = "";
            da.Fill(dt);


            if (txtMaMatHang_AD.Text.Length == 0)
                error += "Chưa nhập mã mặt hàng!\n";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Trim() == txtMaMatHang_AD.Text)
                        error += "Mã mặt hàng đã tồn tại!\n";
                }
            }

            if (txtTenMatHang_AD.Text.Length == 0)
                error += "Chưa nhập tên mặt hàng!\n";
            else
                if (txtTenMatHang_AD.Text.Length <= 2)
                error += "Tên mặt hàng phải dài hơn 2 kí tự!\n";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][3].ToString().Trim() == txtTenMatHang_AD.Text)
                        error += "Tên mặt hàng đã tồn tại!\n";
                }
            }

            if (txtDonGia_AD.Text.Length == 0)
                error += "Chưa nhập đơn giá!\n";
            else
                if (Double.Parse(txtDonGia_AD.Text) <= 5000)
                error += "Đơn giá phải lớn hơn 5.000 v\n";
            if (error.Length > 0)
                MessageBox.Show(error);
            else
            {
                qlAD.insertMH(txtMaMatHang_AD.Text, comboBoxDanhMuc_AD.SelectedValue.ToString(), comb_DVT.SelectedValue.ToString(), txtTenMatHang_AD.Text, Double.Parse(txtDonGia_AD.Text));
                MessageBox.Show("Yeah! Thành công rồi nè hehe!");
                RefeshlstMatHang();
            }
        }

        private void btnXoa_AD_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            da = qlAD.selectAll("MATHANG");
            string error = "";
            da.Fill(dt);
            if (txtMaMatHang_AD.Text.Length == 0)
                error += "Chưa nhập mã mặt hàng!\n";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Trim() == txtMaMatHang_AD.Text)
                    {
                        qlAD.deleteMH(txtMaMatHang_AD.Text);
                        RefeshlstMatHang();
                        MessageBox.Show("Xóa thành công!");
                        return;
                    }
                }
                error += "Mã mặt hàng không tồn tại!\n";

            }
            MessageBox.Show(error);
        }

        private void btnSua_AD_Click(object sender, EventArgs e)
        {
            string id = "";
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            da = qlAD.selectAll("MATHANG");
            string error = "";
            da.Fill(dt);
            if (txtMaMatHang_AD.Text.Length == 0)
            {
                error += "Chưa chọn mã mặt hàng muốn sửa!\n";
                MessageBox.Show(error);
                return;
            }
            else
            {
                int flag = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Trim() == txtMaMatHang_AD.Text)
                    {
                        flag = 1;
                    }
                }
                if (flag == 0)
                    error += "Mã mặt hàng không tồn tại!\n";
            }

            if (txtTenMatHang_AD.Text.Length == 0)
                error += "Chưa nhập tên mặt hàng!\n";
            else if (txtTenMatHang_AD.Text.Length <= 2)
                error += "Tên mặt hàng phải dài hơn 2 kí tự!\n";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][3].ToString().Trim() == txtTenMatHang_AD.Text && dt.Rows[i][0].ToString().Trim() != txtMaMatHang_AD.Text)
                    {
                        error += "Tên mặt hàng đã tồn tại!\n";
                        break;
                    }

                }
            }
            if (txtDonGia_AD.Text.Length == 0)
                error += "Chưa nhập đơn giá!\n";
            else
                if (Double.Parse(txtDonGia_AD.Text) <= 5000)
                error += "Đơn giá phải lớn hơn 5.000 vnđ\n";
            if (error.Length == 0)
            {
                qlAD.updateMH(txtMaMatHang_AD.Text, comboBoxDanhMuc_AD.SelectedValue.ToString(), comb_DVT.SelectedValue.ToString(), txtTenMatHang_AD.Text, Double.Parse(txtDonGia_AD.Text));
                RefeshlstMatHang();
                MessageBox.Show("Sửa thành công!");
                return;
            }
            MessageBox.Show(error);
        }

        private void btnRefresh_AD_Click(object sender, EventArgs e)
        {
            RefeshlstMatHang();
        }

        private void btnTimMatHang_AD_Click(object sender, EventArgs e)
        {
            if (txt_SearchHang.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập thông tinh mặt hàng cần tìm!");
                return;
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da = qlAD.SearchMH(txt_SearchHang.Text);
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy mặt hàng cần tìm!");
                    return;
                }
                lstDSHang_AD.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem(new[] { dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), Double.Parse(dt.Rows[i][2].ToString()).ToString("#,##", nfi) + " vnđ", dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString() });
                    lstDSHang_AD.Items.Add(item);
                }
            }
        }

        private void txtDonGia_AD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        /*--------------------------------------------------------------------*/
        /*|                           FORM_NHANVIEN                           |*/
        /*--------------------------------------------------------------------*/

        public void loadListNhanVien()
        {
            lstDSNV.Items.Clear();

            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            da = qlAD.selectAll("NHANVIEN");
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(new[] { dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), DateTime.Parse(dt.Rows[i][2].ToString()).ToString("dd/MM/yyyy"), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString(), dt.Rows[i][7].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][9].ToString() });
                lstDSNV.Items.Add(item);
            }
            txtTenNV_AD.Text = "";
            txtMaNV_AD.Text = "";
            radioButtonNam_AD.Checked = false;
            radioButtonNu_AD.Checked = false;
            txtDiaChi_AD.Text = "";
            txtUserName_AD.Text = "";
            txtPassWord_AD.Text = "";
            txt_SoDienThoai.Text = "";
        }

        public void loadQuyenDN()
        {
            List<string> quyen = new List<string>(new[] { "Admin", "Account" });
            combo_Quyen_AD.DataSource = quyen;
        }

        private void lstDSNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable ds = new DataTable();

            da = qlAD.timNV(lstDSNV.FocusedItem.SubItems[0].Text);
            da.Fill(ds);
            txtMaNV_AD.Text = ds.Rows[0][0].ToString();
            txtTenNV_AD.Text = ds.Rows[0][1].ToString();
            txt_NgaySinh_NV.Text = ds.Rows[0][2].ToString();
            if (ds.Rows[0][3].ToString() == "Nam")
            {
                radioButtonNam_AD.Checked = true;
                radioButtonNu_AD.Checked = false;
            }
            else
            {
                radioButtonNam_AD.Checked = false;
                radioButtonNu_AD.Checked = true;
            }
            txtDiaChi_AD.Text = ds.Rows[0][4].ToString();
            txtUserName_AD.Text = ds.Rows[0][5].ToString();
            txtPassWord_AD.Text = ds.Rows[0][6].ToString();
            combo_ChucVu.SelectedItem = ds.Rows[0][8].ToString();
            combo_Quyen_AD.SelectedItem = ds.Rows[0][7].ToString();
            txt_SoDienThoai.Text = ds.Rows[0][9].ToString();
        }

        private void btn_ThemNV_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            da = qlAD.selectAll("NHANVIEN");
            string error = "";
            da.Fill(dt);
            if (txtMaNV_AD.Text.Length == 0)
                error += "Chưa chọn mã nhân viên!\n";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Trim() == txtMaNV_AD.Text)
                    {
                        error += "Mã nhân viên đã tồn tại!\n";
                        break;
                    }
                }
            }
            if (txtTenNV_AD.Text.Length <= 5)
                error += "Nhập tên nhân viên phải dài hơn 5 kí tự!\n";
            if (DateTime.Now.Year - DateTime.Parse(txt_NgaySinh_NV.Text).Year <= 18)
                error += "Tuổi của nhân viên phải lớn hơn hoặc bằng 18 tuổi!\n";
            if (radioButtonNam_AD.Checked == false && radioButtonNu_AD.Checked == false)
                error += "Vui lòng chọn giới tính cho nhân viên!\n";

            if (txtUserName_AD.Text.Length == 0 || txtUserName_AD.Text.Length >= 13)
                error += "CCCD không thể có độ dài bằng không hoặc lớn 12 ký tự!\n";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][5].ToString().Trim() == txtUserName_AD.Text.Trim())
                    {
                        error += "CCCD đã tồn tại!\n";
                        break;
                    }
                }
            }
            if (txtPassWord_AD.Text.Length <= 3)
                error += "Vui lòng nhập mật khẩu dài hơn 3 ký tự!\n";
            if (txt_SoDienThoai.Text.Length >= 12)
                error += "Vui lòng nhập số điện thoại dài 10 đến 11 ký tự!\n";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][9].ToString().Trim() == txt_SoDienThoai.Text.Trim())
                    {
                        error += "Số điện thoại đã tồn tại!\n";
                        break;
                    }
                }
            }
            if (error.Length != 0)
                MessageBox.Show(error);
            else
            {
                string gioitinh = "";
                if (radioButtonNam_AD.Checked == true)
                    gioitinh = radioButtonNam_AD.Text;
                else
                    gioitinh = radioButtonNu_AD.Text;
                qlAD.insertNV(txtMaNV_AD.Text, txtTenNV_AD.Text, txt_NgaySinh_NV.Text, gioitinh, txtDiaChi_AD.Text, txtUserName_AD.Text, txtPassWord_AD.Text, combo_Quyen_AD.SelectedItem.ToString(), combo_ChucVu.SelectedItem.ToString(), txt_SoDienThoai.Text);
                MessageBox.Show("Thêm thành công!");
                loadListNhanVien();
            }
        }

        private void btn_XoaNhanVien_Click(object sender, EventArgs e)
        {
            if (lstDSNV.FocusedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Nhân viên muốn xóa!");
                return;
            }
            if (lstDSNV.FocusedItem.SubItems[0].Text == "NV0001")
            {
                MessageBox.Show("Không thể xóa nhân viên này!");
                return;
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da = qlAD.selectAll("NHANVIEN");
                string error = "";
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (lstDSNV.FocusedItem.SubItems[0].Text == dt.Rows[i][0].ToString())
                    {
                        qlAD.deleteNV(lstDSNV.FocusedItem.SubItems[0].Text);
                        MessageBox.Show("Xóa thành công!");
                        loadListNhanVien();
                        return;
                    }
                }
            }
        }

        private void btn_SuaNV_Click(object sender, EventArgs e)
        {
            loadListNhanVien();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            string id = "";
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            da = qlAD.selectAll("NHANVIEN");
            string error = "";
            da.Fill(dt);
            if (txtMaNV_AD.Text.Length == 0)
                error += "Vui lòng chọn nhân viên muốn sửa!\n";
            else
            {
                int flag = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Trim() == txtMaNV_AD.Text.Trim())
                    {
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                    error += "Mã nhân viên không tồn tại!\n";

            }
            if (txtTenNV_AD.Text.Length <= 5)
                error += "Nhập tên nhân viên phải dài hơn 5 kí tự!\n";
            if (DateTime.Now.Year - DateTime.Parse(txt_NgaySinh_NV.Text).Year <= 18)
                error += "Tuổi của nhân viên phải lớn hơn hoặc bằng 18 tuổi!\n";
            if (radioButtonNam_AD.Checked == false && radioButtonNu_AD.Checked == false)
                error += "Vui lòng chọn giới tính cho nhân viên!\n";

            if (txtUserName_AD.Text.Length == 0 || txtUserName_AD.Text.Length >= 13)
                error += "CCCD không thể có độ dài bằng không hoặc lớn 12 ký tự!\n";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][5].ToString().Trim() == txtUserName_AD.Text.Trim() && txtMaNV_AD.Text != dt.Rows[i][0].ToString().Trim())
                    {
                        error += "CCCD đã tồn tại!\n";
                    }
                }
            }
            if (txtPassWord_AD.Text.Length <= 3)
                error += "Vui lòng nhập mật khẩu dài hơn 3 ký tự!\n";
            if (txt_SoDienThoai.Text.Length >= 12)
                error += "Vui lòng nhập số điện thoại dài 10 đến 11 ký tự!\n";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][9].ToString().Trim() == txt_SoDienThoai.Text.Trim() && txtMaNV_AD.Text != dt.Rows[i][0].ToString().Trim())
                    {
                        error += "Số điện thoại đã tồn tại!\n";
                        break;
                    }
                }
            }
            if (error.Length == 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (txtMaNV_AD.Text == dt.Rows[i][0].ToString())
                    {
                        string gioitinh = "";
                        if (radioButtonNam_AD.Checked == true)
                            gioitinh = radioButtonNam_AD.Text;
                        else
                            gioitinh = radioButtonNu_AD.Text;
                        qlAD.updateNV(txtMaNV_AD.Text, txtTenNV_AD.Text, txt_NgaySinh_NV.Text, gioitinh, txtDiaChi_AD.Text, txtUserName_AD.Text, txtPassWord_AD.Text, combo_ChucVu.SelectedItem.ToString(), combo_Quyen_AD.SelectedItem.ToString(), txt_SoDienThoai.Text);
                        MessageBox.Show("Sửa thành công!");
                        loadListNhanVien();
                        break;
                    }
                }

            }
            else
                MessageBox.Show(error);
        }

        private void btnTimNV_AD_Click(object sender, EventArgs e)
        {
            if (txtTimNhanVien_AD.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập thông tin nhân viên cần tìm!");
                return;
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da = qlAD.SearchNV(txtTimNhanVien_AD.Text);
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy nhân viên cần tìm!");
                    return;
                }
                lstDSNV.Items.Clear();
                loadListNhanVien(dt);
            }
        }

        public void loadListNhanVien(DataTable dt)
        {
            lstDSNV.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(new[] { dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), DateTime.Parse(dt.Rows[i][2].ToString()).ToString("dd/MM/yyyy"), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString(), dt.Rows[i][7].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][9].ToString() });
                lstDSNV.Items.Add(item);
            }
        }

        private void btn_TaoMaNV_Click(object sender, EventArgs e)
        {
            txtMaNV_AD.Text = qlAD.taoMaNV();
            txtTenNV_AD.Text = "";
            radioButtonNam_AD.Checked = false;
            radioButtonNu_AD.Checked = false;
            txtDiaChi_AD.Text = "";
            txtUserName_AD.Text = "";
            txtPassWord_AD.Text = "";
            txt_SoDienThoai.Text = "";
        }

        private void combo_Quyen_AD_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> Account = new List<string>(new[] { "Lễ tân", "Tạp vụ", "Phục vụ", "Bảo vệ", "Kho" });
            List<string> Admin = new List<string>(new[] { "Quản lí" });
            if (combo_Quyen_AD.SelectedItem.ToString() == "Admin")
                combo_ChucVu.DataSource = Admin;
            else
                combo_ChucVu.DataSource = Account;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Form_BaoCao frm = new Form_BaoCao();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
    }
}
