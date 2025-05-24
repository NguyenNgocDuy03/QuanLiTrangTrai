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
    public partial class HoSoSucKhoe : Form
    {
        public HoSoSucKhoe()
        {
            InitializeComponent();
        }

        SqlConnection con;

        private void HoSoSucKhoe_Load(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["QLGiaSuc"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();
            HienThi();
        }

        public void HienThi()
        {
            string sqlSELECT = "SELECT * FROM ho_so_suc_khoe";
            SqlCommand cmd = new SqlCommand(sqlSELECT, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dsHoSoSucKHoe.DataSource = dt;
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdDongVat.Text) ||
                string.IsNullOrWhiteSpace(txtNhipTim.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(txtCanNang.Text) ||
                string.IsNullOrWhiteSpace(txtNgayKiemTra.Text) ||
                string.IsNullOrWhiteSpace(txtChuanDoan.Text) ||
                string.IsNullOrWhiteSpace(txtKhuyenCaoDieuTri.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            DateTime ngayKiemTra;
            if (!DateTime.TryParse(txtNgayKiemTra.Text, out ngayKiemTra))
            {
                MessageBox.Show("Ngày kiểm tra không hợp lệ. Vui lòng nhập đúng định dạng.");
                return;
            }

            string sqlINSERT = "INSERT INTO ho_so_suc_khoe (id_dong_vat, nhip_tim, nhiet_do_co_the, can_nang, ngay_kiem_tra, chan_doan, khuyen_cao_dieu_tri) VALUES (@id_dong_vat, @nhip_tim, @nhiet_do_co_the, @can_nang, @ngay_kiem_tra, @chan_doan, @khuyen_cao_dieu_tri)";

            using (SqlCommand cmd = new SqlCommand(sqlINSERT, con))
            {
                cmd.Parameters.AddWithValue("@id_dong_vat", txtIdDongVat.Text);
                cmd.Parameters.AddWithValue("@nhip_tim", txtNhipTim.Text);
                cmd.Parameters.AddWithValue("@nhiet_do_co_the", textBox5.Text);
                cmd.Parameters.AddWithValue("@can_nang", txtCanNang.Text);
                cmd.Parameters.AddWithValue("@ngay_kiem_tra", ngayKiemTra.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@chan_doan", txtChuanDoan.Text);
                cmd.Parameters.AddWithValue("@khuyen_cao_dieu_tri", txtKhuyenCaoDieuTri.Text);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm hồ sơ sức khỏe thành công.");
                    HienThi(); // Gọi hàm để làm mới danh sách
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
                    string sqlEdit = "UPDATE ho_so_suc_khoe SET id_dong_vat = @id_dong_vat, nhip_tim = @nhip_tim, nhiet_do_co_the = @nhiet_do_co_the, can_nang = @can_nang, ngay_kiem_tra= @ngay_kiem_tra, chan_doan = @chan_doan, khuyen_cao_dieu_tri =@khuyen_cao_dieu_tri WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(sqlEdit, con);

                    cmd.Parameters.AddWithValue("id", int.Parse(txtID.Text.Trim()));
                    cmd.Parameters.AddWithValue("id_dong_vat", txtIdDongVat.Text);
                    cmd.Parameters.AddWithValue("nhip_tim", txtNhipTim.Text);
                    cmd.Parameters.AddWithValue("nhiet_do_co_the", textBox5.Text);
                    cmd.Parameters.AddWithValue("can_nang", txtCanNang.Text);
                    cmd.Parameters.AddWithValue("ngay_kiem_tra", Convert.ToDateTime(txtNgayKiemTra.Text).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("chan_doan", txtChuanDoan.Text);
                    cmd.Parameters.AddWithValue("khuyen_cao_dieu_tri", txtKhuyenCaoDieuTri.Text);

                    cmd.ExecuteNonQuery();
                    HienThi();
        }


        


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sqlTimKiem = "SELECT * FROM ho_so_suc_khoe WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sqlTimKiem, con);
            cmd.Parameters.AddWithValue("@id", txtTimKiem.Text);
            cmd.Parameters.AddWithValue("@id_dong_vat", txtIdDongVat.Text);
            cmd.Parameters.AddWithValue("@nhip_tim", txtNhipTim.Text);
            cmd.Parameters.AddWithValue("@nhiet_do_co_the", textBox5.Text);
            cmd.Parameters.AddWithValue("@can_nang", txtCanNang.Text);
            cmd.Parameters.AddWithValue("@chan_doan", txtChuanDoan.Text);
            cmd.Parameters.AddWithValue("@khuyen_cao_dieu_tri", txtKhuyenCaoDieuTri.Text);

            // Thực hiện truy vấn
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dsHoSoSucKHoe.DataSource = dt;
        }


        private void dsHoSoSucKHoe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void HoSoSucKhoe_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TrangChu  f = new TrangChu();
            f.Show();
            this.Hide();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu không có ID hoặc ID không hợp lệ
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn bản ghi để xóa.");
                return;
            }

            // Xác nhận xóa
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?",
                                                "Xác nhận xóa",
                                                MessageBoxButtons.YesNo);
            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

            // Câu lệnh xóa
            string sqlDelete = "DELETE FROM ho_so_suc_khoe WHERE id = @id";

            using (SqlCommand cmd = new SqlCommand(sqlDelete, con))
            {
                // Thêm tham số cho câu lệnh SQL
                cmd.Parameters.AddWithValue("@id", textBox1.Text);

                // Mở kết nối nếu cần
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                try
                {
                    // Thực thi câu lệnh xóa
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa thành công!");

                    // Làm mới giao diện sau khi xóa
                    HienThi();
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu có
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void dsHoSoSucKHoe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked row is valid
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow row = dsHoSoSucKHoe.Rows[e.RowIndex];

                // Populate the TextBox controls with the data from the selected row
                txtID.Text = row.Cells["id"].Value.ToString();
                txtIdDongVat.Text = row.Cells["id_dong_vat"].Value.ToString();
                txtNhipTim.Text = row.Cells["nhip_tim"].Value.ToString();
                textBox5.Text = row.Cells["nhiet_do_co_the"].Value.ToString();
                txtCanNang.Text = row.Cells["can_nang"].Value.ToString();
                txtNgayKiemTra.Text = row.Cells["ngay_kiem_tra"].Value.ToString();
                txtChuanDoan.Text = row.Cells["chan_doan"].Value.ToString();
                txtKhuyenCaoDieuTri.Text = row.Cells["khuyen_cao_dieu_tri"].Value.ToString();
            }
        }
    }
}
