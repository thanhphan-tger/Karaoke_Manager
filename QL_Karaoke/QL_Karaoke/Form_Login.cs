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
    public partial class Form_Login : Form
    {
        public Form_Login()
        {
            InitializeComponent();

            txtTenDangNhap_DN.TextChanged += txt_TextChanged;
            txtMatKhau_DN.TextChanged += txt_TextChanged;
        }

        QL_Karaoke qlLG = new QL_Karaoke();

        public static string UserName = "";
        public static string roles = "";

        private void btnDangNhap_DN_Click(object sender, EventArgs e)
        {
            string userName = txtTenDangNhap_DN.Text, passWord = txtMatKhau_DN.Text;

            if (qlLG.checkUser_Pwd(userName, passWord) == true)
            {
                UserName = userName;
                roles = qlLG.checkRoles(userName);

                Form_QuanLyKaraoke frm_QLKra = new Form_QuanLyKaraoke();
                this.Hide();

                txtTenDangNhap_DN.Clear();
                txtMatKhau_DN.Clear();

                frm_QLKra.ShowDialog();
                this.Show();
                txtTenDangNhap_DN.Focus();
            }
            else
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtTenDangNhap_DN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            bool chkUserName = false;
            if (txtTenDangNhap_DN.Text.Length == 10)
                chkUserName = true;

            bool checkPassWord = false;
            if (txtMatKhau_DN.Text.Length >= 4)
                checkPassWord = true;

            if (chkUserName && checkPassWord)
            {
                btnDangNhap_DN.Enabled = true;
            }
            else
            {
                btnDangNhap_DN.Enabled = false;
            }
        }

        private void txtMatKhau_DN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)&&!char.IsLetter(e.KeyChar)&&!char.IsControl(e.KeyChar))
                e.Handled = true;
        }

    }
}
