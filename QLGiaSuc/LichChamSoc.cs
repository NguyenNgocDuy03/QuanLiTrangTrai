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
    public partial class LichChamSoc : Form
    {
        public LichChamSoc()
        {
            InitializeComponent();
        }

        SqlConnection con;

        private void LichChamSoc_Load(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["QLGiaSuc"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();
            HienThi();
        }

        public void HienThi()
        {
            string sqlSELECT = "SELECT * FROM lich_cham_soc";
            SqlCommand cmd = new SqlCommand(sqlSELECT, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataLichChamSoc.DataSource = dt;
        }




        private void btnSua_Click(object sender, EventArgs e)
        {
            string sqlEdit = "UPDATE lich_cham_soc SET id_dong_vat = @id_dong_vat, nhiem_vu = @nhiem_vu, ngay_len_ke_hoach = @ngay_len_ke_hoach, tan_suat = @tan_suat, thong_bao_gui= @thong_bao_gui  WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sqlEdit, con);
            cmd.Parameters.AddWithValue("id", txtID.Text);
            cmd.Parameters.AddWithValue("@id_dong_vat", txtIDongVat.Text);
            cmd.Parameters.AddWithValue("@nhiem_vu", txtNhiemVu.Text);
            cmd.Parameters.AddWithValue("@ngay_len_ke_hoach", txtNgayLenKeHoach.Text);
            cmd.Parameters.AddWithValue("@tan_suat", txtTanSuat.Text);
            cmd.Parameters.AddWithValue("@thong_bao_gui", txtThongBaoGui.Text);
            try
            {
                cmd.ExecuteNonQuery();
                HienThi();
                MessageBox.Show("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sqlDelete = "DELETE FROM lich_cham_soc WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("id", txtID.Text);
            cmd.Parameters.AddWithValue("@id_dong_vat", txtIDongVat.Text);
            cmd.Parameters.AddWithValue("@nhiem_vu", txtNhiemVu.Text);
            cmd.Parameters.AddWithValue("@ngay_len_ke_hoach", txtNgayLenKeHoach.Text);
            cmd.Parameters.AddWithValue("@tan_suat", txtTanSuat.Text);
            cmd.Parameters.AddWithValue("@thong_bao_gui", txtThongBaoGui.Text);
            cmd.ExecuteNonQuery();
            HienThi();

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sqlTimKiem = "SELECT * FROM lich_cham_soc WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sqlTimKiem, con);

            cmd.Parameters.AddWithValue("id", txtTimKiem.Text);
            cmd.Parameters.AddWithValue("@id_dong_vat", txtIDongVat.Text);
            cmd.Parameters.AddWithValue("@nhiem_vu", txtNhiemVu.Text);
            cmd.Parameters.AddWithValue("@ngay_len_ke_hoach", txtNgayLenKeHoach.Text);
            cmd.Parameters.AddWithValue("@tan_suat", txtTanSuat.Text);
            cmd.Parameters.AddWithValue("@thong_bao_gui", txtThongBaoGui.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataLichChamSoc.DataSource = dt;
        }

        private void dataLichChamSoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TrangChu f = new TrangChu();
            f.Show();
            this.Hide();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường nhập liệu nếu trống
            if (string.IsNullOrWhiteSpace(txtIDongVat.Text) ||
                string.IsNullOrWhiteSpace(txtNhiemVu.Text) ||
                string.IsNullOrWhiteSpace(txtNgayLenKeHoach.Text) ||
                string.IsNullOrWhiteSpace(txtTanSuat.Text) ||
                string.IsNullOrWhiteSpace(txtThongBaoGui.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            // Câu lệnh INSERT
            string sqlINSERT = "INSERT INTO lich_cham_soc (id_dong_vat, nhiem_vu, ngay_len_ke_hoach, tan_suat, thong_bao_gui) VALUES (@id_dong_vat, @nhiem_vu, @ngay_len_ke_hoach, @tan_suat, @thong_bao_gui)";

            using (SqlCommand cmd = new SqlCommand(sqlINSERT, con))
            {
                // Thêm tham số vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@id_dong_vat", txtIDongVat.Text);
                cmd.Parameters.AddWithValue("@nhiem_vu", txtNhiemVu.Text);
                cmd.Parameters.AddWithValue("@ngay_len_ke_hoach", txtNgayLenKeHoach.Text);
                cmd.Parameters.AddWithValue("@tan_suat", txtTanSuat.Text);
                cmd.Parameters.AddWithValue("@thong_bao_gui", txtThongBaoGui.Text);

                try
                {
                    // Thực thi câu lệnh INSERT
                    cmd.ExecuteNonQuery();

                    // Cập nhật lại giao diện sau khi thêm
                    HienThi();

                    // Thông báo thành công
                    MessageBox.Show("Thêm thành công!");
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu có
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void dataLichChamSoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataLichChamSoc.Rows[e.RowIndex];
                txtID.Text = row.Cells["id"].Value.ToString();
                txtIDongVat.Text = row.Cells["id_dong_vat"].Value.ToString();
                txtNhiemVu.Text = row.Cells["nhiem_vu"].Value.ToString();
                txtNgayLenKeHoach.Text = row.Cells["ngay_len_ke_hoach"].Value.ToString();
                txtTanSuat.Text = row.Cells["tan_suat"].Value.ToString();
                txtThongBaoGui.Text = row.Cells["thong_bao_gui"].Value.ToString();
            }
        }
    }
}
    

