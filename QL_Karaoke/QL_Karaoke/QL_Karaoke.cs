using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Globalization;

namespace QL_Karaoke
{
    class QL_Karaoke
    {
        SqlConnection connect = new SqlConnection("Data Source=Tger_Phreynitial Catalog = QL_Karaoke; Integrated Security = True");

        public QL_Karaoke() { }

        /*--------------------------------------------------------------------*/
        /*|                             FORM_LOGIN                           |*/
        /*--------------------------------------------------------------------*/

        public string checkRoles(string userName)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string checkString = "select QUYENDN from NHANVIEN where CCCD = '" + userName + "'";
                SqlCommand cmd = new SqlCommand(checkString, connect);

                string roles = (string)cmd.ExecuteScalar();

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return roles;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool checkUser_Pwd(string userName, string passWord)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string checkString = "select count(*) from NHANVIEN where CCCD = '" + userName + "' and PASSWORD = '" + passWord + "'";
                SqlCommand cmd = new SqlCommand(checkString, connect);

                int count = (int)cmd.ExecuteScalar();

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                if (count > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /*--------------------------------------------------------------------*/
        /*|                           FORM_QLKARAOKE                         |*/
        /*--------------------------------------------------------------------*/

        public SqlDataAdapter selectAll(string tableName)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select * from " + tableName;
                SqlDataAdapter da = new SqlDataAdapter(selectString, connect);

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SqlDataAdapter selectTableByKey(string tableName, string pKey, string value)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select * from " + tableName + " where " + pKey + " = '" + value + "'";
                SqlDataAdapter da = new SqlDataAdapter(selectString, connect);

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SqlDataAdapter selectRoomByMaPH(string maPH)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select MAPHONG, TENLOAI, GIA, TTPHONG, SUCCHUA from PHONG, LOAIPHONG " +
                                      "where PHONG.MALOAI = LOAIPHONG.MALOAI and MAPHONG = '" + maPH + "'";

                SqlDataAdapter da = new SqlDataAdapter(selectString, connect);

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string findMaHDNoPay(string maPhong)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select MAHD from HOADON, PHIEUDATPHONG " +
                                      "where TINHTRANG = 0 and HOADON.MAPHIEU = PHIEUDATPHONG.MAPHIEU " +
                                      "and PHIEUDATPHONG.MAPHONG = @maPH";

                SqlCommand cmd = new SqlCommand(selectString, connect);

                cmd.Parameters.AddWithValue("@maPH", maPhong);

                string maHD = (string)cmd.ExecuteScalar();

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return maHD;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void insertCTMatHang(string MaMH, string MaHD, int soLuong)
        {
            string insertString = null;
            if (!checkMaMH(MaMH, MaHD)) 
            {
                insertString = "insert into CHITIETHANG(MAMH, MAHD, SOLUONG) values (@mamh, @mahd, @soluong)";

                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                SqlCommand cmd = new SqlCommand(insertString, connect);

                cmd.Parameters.AddWithValue("@mamh", MaMH);
                cmd.Parameters.AddWithValue("@mahd", MaHD);
                cmd.Parameters.AddWithValue("@soluong", soLuong);

                cmd.ExecuteNonQuery();

                if (connect.State == ConnectionState.Open)
                    connect.Close();
            }
            else
            {
                insertString = "update CHITIETHANG set SOLUONG = SOLUONG + @soluong where MAHD = @mahd and MAMH = @mamh";

                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                SqlCommand cmd = new SqlCommand(insertString, connect);

                cmd.Parameters.AddWithValue("@mamh", MaMH);
                cmd.Parameters.AddWithValue("@mahd", MaHD);
                cmd.Parameters.AddWithValue("@soluong", soLuong);

                cmd.ExecuteNonQuery();

                if (connect.State == ConnectionState.Open)
                    connect.Close();
            }
        }

        public ListView selectCTMatHang(string maHD)
        {
            try
            {
                ListView listView = new ListView();

                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select TENMH, SOLUONG from MATHANG, CHITIETHANG " +
                                      "where MAHD = @maHD and CHITIETHANG.MAMH = MATHANG.MAMH";

                SqlCommand cmd = new SqlCommand(selectString, connect);
                cmd.Parameters.AddWithValue("@maHD", maHD);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = reader.GetString(0);

                    item.SubItems.Add(reader.GetInt32(1).ToString());

                    listView.Items.Add(item);
                }

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return listView;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool checkMaMH(string maMH, string maHD)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string checkString = "select count(*) from CHITIETHANG where MAHD = @mahd and MAMH = @mamh";
                SqlCommand cmd = new SqlCommand(checkString, connect);

                cmd.Parameters.AddWithValue("@mahd", maHD);
                cmd.Parameters.AddWithValue("@mamh", maMH);

                int count = (int)cmd.ExecuteScalar();

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                if (count != 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /*--------------------------------------------------------------------*/
        /*|                          FORM_QLDATPHONG                         |*/
        /*--------------------------------------------------------------------*/
        public ListView findKHByCCCD(string CCCD)
        {
            ListView listView = new ListView();
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select * from KHACHHANG where CMND = '" + CCCD + "'";

                SqlCommand cmd = new SqlCommand(selectString, connect);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = reader.GetString(0);

                    item.SubItems.Add(reader.GetString(1));
                    item.SubItems.Add(reader.GetString(2));
                    item.SubItems.Add(reader.GetDateTime(3).ToString());
                    item.SubItems.Add(reader.GetString(4));
                    item.SubItems.Add(reader.GetString(5));
                    item.SubItems.Add(reader.GetString(6));
                    item.SubItems.Add(reader.GetInt32(7).ToString());

                    listView.Items.Add(item);
                }

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return listView;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SqlDataAdapter selectEmptyRoomByKey(string key)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select * from PHONG where TTPhong = N'Trống' and MALOAI = '" + key + "'";
                SqlDataAdapter da = new SqlDataAdapter(selectString, connect);

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string getRoomStatus(string key)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select TTPHONG from PHONG where MAPHONG = '" + key + "'";

                SqlCommand cmd = new SqlCommand(selectString, connect);

                string ttP = (string)cmd.ExecuteScalar();

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return ttP;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public int countRowsInTable(string tableName)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select count(*) from " + tableName;

                SqlCommand cmd = new SqlCommand(selectString, connect);

                int count = (int)cmd.ExecuteScalar();

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return count;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void insertCustomer(string MaKH, string CCCD, string tenKH, string ngaySinh, 
            string sdt, string loaiKH, string dChi, int diemTL)
        {
            string insertString = null;

            if (diemTL > 50)
                loaiKH = "Khách VIP";
            else
                loaiKH = "Khách thường";

            if (!checkMaKH(MaKH))
            {
                insertString = "set dateformat dmy insert into KHACHHANG " +
                                "values (@makh, @cmnd, @tenkh, @nsinh, @sdt, " +
                                "@loaikh, @dchi, @diemtl)";

                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                SqlCommand cmd = new SqlCommand(insertString, connect);

                cmd.Parameters.AddWithValue("@makh", MaKH);
                cmd.Parameters.AddWithValue("@cmnd", CCCD);
                cmd.Parameters.AddWithValue("@tenkh", tenKH);
                cmd.Parameters.AddWithValue("@nsinh", ngaySinh);
                cmd.Parameters.AddWithValue("@sdt", sdt);
                cmd.Parameters.AddWithValue("@loaikh", loaiKH);
                cmd.Parameters.AddWithValue("@dchi", dChi);
                cmd.Parameters.AddWithValue("@diemtl", diemTL + 5);

                cmd.ExecuteNonQuery();

                if (connect.State == ConnectionState.Open)
                    connect.Close();
            }
            else
            {
                insertString = "set dateformat dmy update KHACHHANG set LOAIKH = @loaiKH, DIEMTICHLUY = @diemtl " +
                    "where MAKH = @makh";

                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                SqlCommand cmd = new SqlCommand(insertString, connect);

                cmd.Parameters.AddWithValue("@loaiKH", loaiKH);
                cmd.Parameters.AddWithValue("@diemtl", diemTL + 5);
                cmd.Parameters.AddWithValue("@makh", MaKH);

                cmd.ExecuteNonQuery();

                if (connect.State == ConnectionState.Open)
                    connect.Close();
            }
        }

        public bool checkMaKH(string maKH)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string checkString = "select count(*) from KHACHHANG where MAKH = '" + maKH + "'";
                SqlCommand cmd = new SqlCommand(checkString, connect);

                int count = (int)cmd.ExecuteScalar();

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                if (count != 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void updateRoomStatus(string roomKey, string roomStatus)
        {
            if (connect.State == ConnectionState.Closed)
                connect.Open();

            string updateString = "update PHONG set TTPhong = @rStatus where MAPHONG = @rKey";

            SqlCommand cmd = new SqlCommand(updateString, connect);
            cmd.Parameters.AddWithValue("@rStatus", roomStatus);
            cmd.Parameters.AddWithValue("@rKey", roomKey);

            cmd.ExecuteNonQuery();

            if (connect.State == ConnectionState.Open)
                connect.Close();
        }

        public void insertPhieuDP(string maPhieu, string maKH, string maPH)
        {
            if (connect.State == ConnectionState.Closed)
                connect.Open();

            string insertString = "set dateformat dmy insert into PHIEUDATPHONG " +
                                  "values(@maPhieu, @maKH, @maPh, @gioVao)";

            SqlCommand cmd = new SqlCommand(insertString, connect);
            cmd.Parameters.AddWithValue("@maPhieu", maPhieu);
            cmd.Parameters.AddWithValue("@maKH", maKH);
            cmd.Parameters.AddWithValue("@maPh", maPH);
            cmd.Parameters.AddWithValue("@gioVao", DateTime.Now);

            cmd.ExecuteNonQuery();

            if (connect.State == ConnectionState.Open)
                connect.Close();
        }

        public string createMaPhieuDP()
        {
            int count = countRowsInTable("PHIEUDATPHONG");
            count++;

            if (count < 10)
                return "PH000" + count;

            if (count < 100)
                return "PH00" + count;

            if (count < 1000)
                return "PH0" + count;

            return "PH" + count;
        }

        /*--------------------------------------------------------------------*/
        /*|                          FORM_QLTHANHTOAN                        |*/
        /*--------------------------------------------------------------------*/

        public string createMaHD()
        {
            int count = countRowsInTable("HOADON");
            count++;

            if (count < 10)
                return "HD000" + count;

            if (count < 100)
                return "HD00" + count;

            if (count < 1000)
                return "HD0" + count;

            return "HD" + count;
        }

        public void insertHD(string maHD, string maPhieu, string maNV, double giamGia)
        {
            if (connect.State == ConnectionState.Closed)
                connect.Open();

            string insertString = "set dateformat dmy insert into HOADON(MAHD, MAPHIEU, MANV, NGAYLAP, GIAMGIA) " +
                                  "values(@maHD, @maPhieu, @maNV, @ngayLap, @giamGia)";

            SqlCommand cmd = new SqlCommand(insertString, connect);
            cmd.Parameters.AddWithValue("@maHD", maHD);
            cmd.Parameters.AddWithValue("@maPhieu", maPhieu);
            cmd.Parameters.AddWithValue("@maNV", maNV);
            cmd.Parameters.AddWithValue("@ngayLap", DateTime.Now);
            cmd.Parameters.AddWithValue("@giamGia", giamGia);

            cmd.ExecuteNonQuery();

            if (connect.State == ConnectionState.Open)
                connect.Close();
        }

        public string findMaNV(string CCCD)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string checkString = "select MANV from NHANVIEN where CCCD = '" + CCCD + "'";
                SqlCommand cmd = new SqlCommand(checkString, connect);

                string maNV = (string)cmd.ExecuteScalar();

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return maNV;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ListView selectCTMatHangForHD(string maHD)
        {
            try
            {
                ListView listView = new ListView();

                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select TENMH, DONGIA, SOLUONG, TENDVT, THANHTIEN " +
                                      "from MATHANG mh, CHITIETHANG ct, DOVITINH dv " +
                                      "where mh.MADVT = dv.MADVT and ct.MAMH = mh.MAMH and MAHD = @maHD";

                SqlCommand cmd = new SqlCommand(selectString, connect);
                cmd.Parameters.AddWithValue("@maHD", maHD);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = reader.GetString(0);
                    
                    item.SubItems.Add(reader.GetDouble(1).ToString());
                    item.SubItems.Add(reader.GetInt32(2).ToString());
                    item.SubItems.Add(reader.GetString(3));
                    item.SubItems.Add(reader.GetDouble(4).ToString());

                    listView.Items.Add(item);
                }

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return listView;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ListView selectCTPhongForHD(string maHD)
        {
            try
            {
                ListView listView = new ListView();

                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select p.MAPHONG, TENLOAI, GIA, GIOVAO, GIORA, GIAMGIA, TONGTIEN " +
                                      "from PHONG p , LOAIPHONG l, PHIEUDATPHONG pd, HOADON h " +
                                      "where p.MALOAI = l.MALOAI and h.MAPHIEU = pd.MAPHIEU " +
                                      "and pd.MAPHONG = p.MAPHONG and MAHD = @maHD";

                SqlCommand cmd = new SqlCommand(selectString, connect);
                cmd.Parameters.AddWithValue("@maHD", maHD);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = reader.GetString(0);

                    item.SubItems.Add(reader.GetString(1));
                    item.SubItems.Add(reader.GetDouble(2).ToString());
                    item.SubItems.Add(reader.GetDateTime(3).ToString("t"));
                    item.SubItems.Add(reader.GetDateTime(4).ToString("t"));
                    item.SubItems.Add(reader.GetDouble(5).ToString());
                    item.SubItems.Add(reader.GetDouble(6).ToString());

                    listView.Items.Add(item);
                }

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return listView;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void updateGioRaForHD(string maHD)
        {
            if (connect.State == ConnectionState.Closed)
                connect.Open();

            string insertString = "update HOADON " +
                                  "set GIORA = @gioRa, TINHTRANG = 1 where MAHD = @maHD";

            SqlCommand cmd = new SqlCommand(insertString, connect);
            cmd.Parameters.AddWithValue("@maHD", maHD);
            cmd.Parameters.AddWithValue("@gioRa", DateTime.Now);

            cmd.ExecuteNonQuery();

            if (connect.State == ConnectionState.Open)
                connect.Close();
        }

        public double sumMoney(string maHD)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "SELECT TONGTIEN FROM HOADON WHERE MAHD=@maHD";
                SqlCommand cmd = new SqlCommand(selectString, connect);
                cmd.Parameters.AddWithValue("@maHD", maHD);

                double sum = (double)cmd.ExecuteScalar();

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return sum;
            }
            catch (Exception ex)
            {
                return 0; ;
            }
        }

        /*--------------------------------------------------------------------*/
        /*|                            FORM_ACCOUNT                          |*/
        /*--------------------------------------------------------------------*/

        public ListView selectInfoUser(string userName)
        {
            try
            {
                ListView listView = new ListView();

                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select TENNV, GIOITINH, NGSINH, CCCD, SDTH, DIACHI from NHANVIEN where CCCD = @userName";

                SqlCommand cmd = new SqlCommand(selectString, connect);
                cmd.Parameters.AddWithValue("@userName", userName);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = reader.GetString(0);

                    item.SubItems.Add(reader.GetString(1));
                    item.SubItems.Add(reader.GetDateTime(2).ToString("d"));
                    item.SubItems.Add(reader.GetString(3));
                    item.SubItems.Add(reader.GetString(4));
                    item.SubItems.Add(reader.GetString(5));

                    listView.Items.Add(item);
                }

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return listView;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /*--------------------------------------------------------------------*/
        /*|                              FORM_ADMIN                          |*/
        /*--------------------------------------------------------------------*/

        public string CheckStatus(int a)
        {
            if (a == 0)
                return "Chưa thanh toán";
            else
                return "Đã thanh toán";
        }

        public SqlDataAdapter TimMatHang(string MaMH)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select * from MATHANG where MAMH ='" + MaMH + "'";
                SqlDataAdapter da = new SqlDataAdapter(selectString, connect);

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SqlDataAdapter TimHoaDon(string value)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select HOADON.MAHD,NGAYLAP, GIOVAO, HOADON.GIORA,GIAMGIA,TINHTRANG,TONGTIEN from HOADON , PHIEUDATPHONG where HOADON.MAHD = '" + value + "' and PHIEUDATPHONG.MAPHIEU = HOADON.MAPHIEU GROUP BY HOADON.MAHD,NGAYLAP, GIOVAO, HOADON.GIORA,GIAMGIA,TINHTRANG,TONGTIEN";
                SqlDataAdapter da = new SqlDataAdapter(selectString, connect);

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SqlDataAdapter TinhTienDV(string value)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select SUM(SOLUONG*DONGIA) from CHITIETHANG,MATHANG where CHITIETHANG.MAHD='" + value + "' AND CHITIETHANG.MAMH=MATHANG.MAMH";
                SqlDataAdapter da = new SqlDataAdapter(selectString, connect);

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SqlDataAdapter SearchHoaDonTheoNgay(string Ngay)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = " select * from HOADON where CONVERT(DATE,NGAYLAP)='" + Ngay + "'AND TINHTRANG=1 ";
                SqlDataAdapter da = new SqlDataAdapter(selectString, connect);

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SqlDataAdapter SearchHoaDonTheoThang(string Thang)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = " select * from HOADON where CONVERT(CHAR(20),FORMAT(NGAYLAP,'MM/yyyy'))='" + Thang + "' AND TINHTRANG=1 ";
                SqlDataAdapter da = new SqlDataAdapter(selectString, connect);

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Update(string column, string value, string Ma, string table)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }

                SqlCommand NewCmd = connect.CreateCommand();
                NewCmd.Connection = connect;
                NewCmd.CommandType = CommandType.Text;
                NewCmd.CommandText = "Update " + table + " SET " + column + "='" + value + "' WHERE MAHD='" + Ma + "'";
                int a = NewCmd.ExecuteNonQuery();
                connect.Close();
                if (a == 0)
                    return 0;
                else
                    return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdatePHIEUDATPHONG(string column, string value, string Ma, string table)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }

                SqlCommand NewCmd = connect.CreateCommand();
                NewCmd.Connection = connect;
                NewCmd.CommandType = CommandType.Text;
                NewCmd.CommandText = "Update " + table + " SET " + column + "='" + value + "'FROM HOADON WHERE HOADON.MAPHIEU=PHIEUDATPHONG.MAPHIEU AND MAHD='" + Ma + "'";
                int a = NewCmd.ExecuteNonQuery();
                connect.Close();
                if (a == 0)
                    return 0;
                else
                    return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public SqlDataAdapter selectAllMatHang()
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "SELECT MAMH,TENMH,DONGIA,TENDM,TENDVT FROM MATHANG, DOVITINH, DANHMUCHANG WHERE MATHANG.MADVT=DOVITINH.MADVT AND MATHANG.MADM=DANHMUCHANG.MADM ORDER BY MAMH";
                SqlDataAdapter da = new SqlDataAdapter(selectString, connect);

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string TaoMaMH()
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select * from MATHANG ";
                SqlDataAdapter da = new SqlDataAdapter(selectString, connect);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int max = 1;
                if (dt.Rows.Count != 0)
                {
                    max = Int16.Parse(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 3));
                    return "MH" + string.Format("{0:D3}", max += 1);
                }
                else
                    return "MH" + string.Format("{0:D3}", max);

                if (connect.State == ConnectionState.Open)
                    connect.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void insertMH(string maMH, string maDM, string maDVT, string tenMH, double dongia)
        {
            if (connect.State == ConnectionState.Closed)
                connect.Open();

            string insertString = "set dateformat dmy insert into MATHANG(MAMH, MADM, MADVT, TENMH, DONGIA) " +
                                  "values(@maMH, @maDM, @maDVT, @tenMH,@dongia)";

            SqlCommand cmd = new SqlCommand(insertString, connect);
            cmd.Parameters.AddWithValue("@maMH", maMH);
            cmd.Parameters.AddWithValue("@maDM", maDM);
            cmd.Parameters.AddWithValue("@maDVT", maDVT);
            cmd.Parameters.AddWithValue("@tenMH", tenMH);
            cmd.Parameters.AddWithValue("@dongia", dongia);
            cmd.ExecuteNonQuery();

            if (connect.State == ConnectionState.Open)
                connect.Close();
        }

        public void deleteMH(string maMH)
        {
            if (connect.State == ConnectionState.Closed)
                connect.Open();
            string delete = "DELETE FROM MATHANG WHERE MAMH='" + maMH + "'";
            string deleteCHITIETHD = "DELETE FROM CHITIETHANG WHERE MAMH='" + maMH + "'";
            SqlCommand cmd = new SqlCommand(deleteCHITIETHD, connect);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand(delete, connect);
            cmd.ExecuteNonQuery();
            if (connect.State == ConnectionState.Open)
                connect.Close();
        }

        public void updateMH(string maMH, string maDM, string maDVT, string tenMH, double dongia)
        {
            if (connect.State == ConnectionState.Closed)
                connect.Open();
            string delete = "UPDATE MATHANG SET MADM='" + maDM + "', MADVT='" + maDVT + "', TENMH=N'" + tenMH + "', DONGIA=" + dongia + " WHERE MAMH='" + maMH + "'";
            SqlCommand cmd = new SqlCommand(delete, connect);
            cmd.ExecuteNonQuery();
            if (connect.State == ConnectionState.Open)
                connect.Close();
        }

        public SqlDataAdapter SearchMH(string search)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "SELECT MAMH,TENMH,DONGIA,TENDM,TENDVT FROM MATHANG, DOVITINH, DANHMUCHANG" +
                                      " WHERE MATHANG.MADVT = DOVITINH.MADVT" +
                                      " AND MATHANG.MADM = DANHMUCHANG.MADM" +
                                      " AND(MATHANG.MAMH='" + search.Trim() + "'" +
                                      " OR TENDVT=N'" + search.Trim() + "'" +
                                      " OR TENDM=N'" + search.Trim() + "'" +
                                      " OR TENMH=N'" + search.Trim() + "')" +
                                      " ORDER BY MAMH";
                SqlDataAdapter da = new SqlDataAdapter(selectString, connect);

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SqlDataAdapter timNV(string maMV)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select * from NHANVIEN where MANV= '" + maMV + "'";
                SqlDataAdapter da = new SqlDataAdapter(selectString, connect);

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string taoMaNV()
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string selectString = "select * from NHANVIEN ";
                SqlDataAdapter da = new SqlDataAdapter(selectString, connect);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int max = 1;
                if (dt.Rows.Count != 0)
                {
                    max = Int16.Parse(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 4));
                    return "NV" + string.Format("{0:D4}", max += 1);
                }
                else
                    return "NV" + string.Format("{0:D4}", max);

                if (connect.State == ConnectionState.Open)
                    connect.Close();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void insertNV(string maNV, string tenNV, string ngaysinh, string gioitinh, string diachi, string username, string password, string chucvu, string quyen, string sdt)
        {
            if (connect.State == ConnectionState.Closed)
                connect.Open();

            string insertString = "set dateformat dmy insert into NHANVIEN " +
                                  "values('" + maNV + "', N'" + tenNV + "', '" + DateTime.Parse(ngaysinh).ToString("dd/MM/yyyy") + "', N'" + gioitinh + "', N'" + diachi + "', N'" + username + "', '" + password + "', N'" + chucvu + "', N'" + quyen + "', '" + sdt + "')";

            SqlCommand cmd = new SqlCommand(insertString, connect);
            cmd.ExecuteNonQuery();

            if (connect.State == ConnectionState.Open)
                connect.Close();
        }

        public SqlDataAdapter SearchNV(string search)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();
                string selectString = "";
                DateTime dt = new DateTime();
                if (DateTime.TryParseExact(search, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) == true)
                {
                    selectString = "SET DATEFORMAT DMY SELECT * FROM NHANVIEN" +
                                      " WHERE " +
                                      " NGSINH='" + search.Trim() + "'" +
                                      " ORDER BY MANV";
                }
                else
                {
                    selectString = "SET DATEFORMAT DMY SELECT * FROM NHANVIEN" +
                                      " WHERE " +
                                      " (MANV='" + search.Trim() + "'" +
                                      " OR TENNV=N'" + search.Trim() + "'" +
                                      " OR GIOITINH=N'" + search.Trim() + "'" +
                                      " OR DIACHI=N'" + search.Trim() + "'" +
                                      " OR CCCD=N'" + search.Trim() + "'" +
                                      " OR SDTH='" + search.Trim() + "')" +
                                      " ORDER BY MANV";
                }

                SqlDataAdapter da = new SqlDataAdapter(selectString, connect);

                if (connect.State == ConnectionState.Open)
                    connect.Close();

                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void deleteNV(string maNV)
        {
            if (connect.State == ConnectionState.Closed)
                connect.Open();
            string delete = "DELETE FROM NHANVIEN WHERE MANV='" + maNV + "'";
            string changeNV_HoaDon = "UPDATE HOADON SET MANV='NV0001' WHERE MANV='" + maNV + "'";
            SqlCommand cmd = new SqlCommand(changeNV_HoaDon, connect);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand(delete, connect);
            cmd.ExecuteNonQuery();
            if (connect.State == ConnectionState.Open)
                connect.Close();
        }

        public void updateNV(string maNV, string tenNV, string ngaysinh, string gioitinh, string diachi, string username, string password, string chucvu, string quyen, string sdt)
        {
            if (connect.State == ConnectionState.Closed)
                connect.Open();
            string delete = " SET DATEFORMAT DMY UPDATE NHANVIEN SET MANV='" + maNV + "', TENNV=N'" + tenNV + "', NGSINH='" + DateTime.Parse(ngaysinh).ToString("dd/MM/yyyy") + "', GIOITINH=N'" + gioitinh + "', DIACHI=N'" + diachi + "', CCCD='" + username + "', PASSWORD='" + password + "', QUYENDN=N'" + quyen + "',CHUCVU=N'" + chucvu + "',SDTH='" + sdt + "' WHERE MANV='" + maNV + "'";
            SqlCommand cmd = new SqlCommand(delete, connect);
            cmd.ExecuteNonQuery();
            if (connect.State == ConnectionState.Open)
                connect.Close();
        }
    }
}
