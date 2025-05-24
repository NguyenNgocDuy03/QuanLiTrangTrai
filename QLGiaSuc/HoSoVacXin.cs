using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace QLGiaSuc
{
    public partial class HoSoVacXin : Form
    {
        public HoSoVacXin()
        {
            InitializeComponent();
        }

        SqlConnection con;

        private void HoSoVacXin_Load(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["QLGiaSuc"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();
            HienThi();
        }

        public void HienThi()
        {
            string sqlSELECT = "SELECT * FROM ho_so_vac_xin";
            SqlCommand cmd = new SqlCommand(sqlSELECT, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            danhsachHoSoVacXin.DataSource = dt;
        }

        private void HoSoVacXin_Load_1(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["QLGiaSuc"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();
            HienThi();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sqlINSERT = "INSERT INTO ho_so_vac_xin (id_dong_vat, ngay_vac_xin, loai_vac_xin, lieu_luong, bac_si) VALUES (@id_dong_vat, @ngay_vac_xin, @loai_vac_xin, @lieu_luong, @bac_si)";

            using (SqlCommand cmd = new SqlCommand(sqlINSERT, con))
            {

                cmd.Parameters.AddWithValue("@id_dong_vat", txtIdDV.Text);
                cmd.Parameters.AddWithValue("@ngay_vac_xin", txtNgayTiem.Text);
                cmd.Parameters.AddWithValue("@loai_vac_xin", txtLoaiTiem.Text);
                cmd.Parameters.AddWithValue("@lieu_luong", txtLieuLuong.Text);
                cmd.Parameters.AddWithValue("@bac_si", txtBacSi.Text);

                try
                {
                    cmd.ExecuteNonQuery();
                    HienThi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sqlEdit = "UPDATE ho_so_vac_xin SET id_dong_vat = @id_dong_vat, ngay_vac_xin = @ngay_vac_xin, loai_vac_xin = @loai_vac_xin, lieu_luong = @lieu_luong, bac_si= @bac_si  WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sqlEdit, con);
            cmd.Parameters.AddWithValue("id", txtId.Text);
            cmd.Parameters.AddWithValue("@id_dong_vat", txtIdDV.Text);
            cmd.Parameters.AddWithValue("@ngay_vac_xin", txtNgayTiem.Text);
            cmd.Parameters.AddWithValue("@loai_vac_xin", txtLoaiTiem.Text);
            cmd.Parameters.AddWithValue("@lieu_luong", txtLieuLuong.Text);
            cmd.Parameters.AddWithValue("@bac_si", txtBacSi.Text);
            try
            {
                cmd.ExecuteNonQuery();
                HienThi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sqlDelete = "DELETE FROM ho_so_vac_xin WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("id", txtId.Text);
            cmd.Parameters.AddWithValue("@id_dong_vat", txtIdDV.Text);
            cmd.Parameters.AddWithValue("@ngay_vac_xin", txtNgayTiem.Text);
            cmd.Parameters.AddWithValue("@loai_vac_xin", txtLoaiTiem.Text);
            cmd.Parameters.AddWithValue("@lieu_luong", txtLieuLuong.Text);
            cmd.Parameters.AddWithValue("@bac_si", txtBacSi.Text);
            cmd.ExecuteNonQuery();
            HienThi();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sqlTimKiem = "SELECT * FROM ho_so_vac_xin WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sqlTimKiem, con);

            cmd.Parameters.AddWithValue("id", txtTimKiem.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            danhsachHoSoVacXin.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TrangChu f = new TrangChu();
            f.Show();
            this.Hide();
        }

        private void danhsachHoSoVacXin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = danhsachHoSoVacXin.Rows[e.RowIndex];
                    txtId.Text = row.Cells["id"].Value.ToString();
                    txtIdDV.Text = row.Cells["id_dong_vat"].Value.ToString();
                    txtNgayTiem.Text = row.Cells["ngay_vac_xin"].Value.ToString();
                    txtLoaiTiem.Text = row.Cells["loai_vac_xin"].Value.ToString();
                    txtLieuLuong.Text = row.Cells["lieu_luong"].Value.ToString();
                    txtBacSi.Text = row.Cells["bac_si"].Value.ToString();
                }
            

        }
    }
}
