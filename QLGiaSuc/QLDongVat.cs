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
using System.IO;

namespace QLGiaSuc
{
    public partial class QLDongVat : Form
    {
        public QLDongVat()
        {
            InitializeComponent();
        }

        SqlConnection con;

        private void QLDongVat_Load(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["QLGiaSuc"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();
            HienThi();
        }

        public void HienThi()
        {
            string sqlSELECT = "SELECT * FROM dong_vat";
            SqlCommand cmd = new SqlCommand(sqlSELECT, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            danhsachQLDV.DataSource = dt;
        }

        private byte[] ImageToByteArray(Image img)
        {
            if (img == null)
            {
                return null; // Hoặc xử lý lỗi theo cách khác nếu hình ảnh không hợp lệ
            }

            using (MemoryStream m = new MemoryStream())
            {
                img.Save(m, System.Drawing.Imaging.ImageFormat.Png);
                return m.ToArray();
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text) ||
                string.IsNullOrWhiteSpace(txtTuoi.Text) ||
                string.IsNullOrWhiteSpace(txtGiong.Text) ||
                string.IsNullOrWhiteSpace(txtCanNang.Text) ||
                string.IsNullOrWhiteSpace(txtNguonGoc.Text) ||
                string.IsNullOrWhiteSpace(txtThoiGian.Text) ||
                pictureBox1.Image == null)
            {
                MessageBox.Show("Vui lòng điền tất cả các trường thông tin.");
                return; // Dừng lại nếu có trường nào còn trống
            }

            byte[] imageBytes = ImageToByteArray(pictureBox1.Image);
            string sqlINSERT = "INSERT INTO dong_vat (ten, tuoi, giong, can_nang, hinh_anh, nguon_goc, thoi_gian_tao) VALUES (@ten, @tuoi, @giong, @can_nang, @hinh_anh, @nguon_goc, @thoi_gian_tao)";

            using (SqlCommand cmd = new SqlCommand(sqlINSERT, con))
            {
                cmd.Parameters.AddWithValue("@ten", txtTen.Text);
                cmd.Parameters.AddWithValue("@tuoi", txtTuoi.Text);
                cmd.Parameters.AddWithValue("@giong", txtGiong.Text);
                cmd.Parameters.AddWithValue("@can_nang", txtCanNang.Text);
                cmd.Parameters.AddWithValue("@hinh_anh", (object)imageBytes ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nguon_goc", txtNguonGoc.Text);
                cmd.Parameters.AddWithValue("@thoi_gian_tao", Convert.ToDateTime(txtThoiGian.Text).ToString("yyyy-MM-dd"));

                try
                {
                    cmd.ExecuteNonQuery();

                    // Hiển thị thông báo thêm thành công
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại dữ liệu sau khi thêm
                    HienThi();
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu xảy ra vấn đề
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }



        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Vui lòng nhập ID để sửa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTen.Text) ||
                string.IsNullOrWhiteSpace(txtTuoi.Text) ||
                string.IsNullOrWhiteSpace(txtGiong.Text) ||
                string.IsNullOrWhiteSpace(txtCanNang.Text) ||
                string.IsNullOrWhiteSpace(txtNguonGoc.Text) ||
                string.IsNullOrWhiteSpace(txtThoiGian.Text) ||
                pictureBox1.Image == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin để sửa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            byte[] imageBytes = ImageToByteArray(pictureBox1.Image);
            string sqlEdit = "UPDATE dong_vat SET ten = @ten, tuoi = @tuoi, giong = @giong, can_nang = @can_nang, hinh_anh = @hinh_anh, nguon_goc = @nguon_goc, thoi_gian_tao = @thoi_gian_tao WHERE id = @id";

            using (SqlCommand cmd = new SqlCommand(sqlEdit, con))
            {
                cmd.Parameters.AddWithValue("@id", txtId.Text);
                cmd.Parameters.AddWithValue("@ten", txtTen.Text);
                cmd.Parameters.AddWithValue("@tuoi", txtTuoi.Text);
                cmd.Parameters.AddWithValue("@giong", txtGiong.Text);
                cmd.Parameters.AddWithValue("@can_nang", txtCanNang.Text);
                cmd.Parameters.AddWithValue("@hinh_anh", (object)imageBytes ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nguon_goc", txtNguonGoc.Text);
                cmd.Parameters.AddWithValue("@thoi_gian_tao", Convert.ToDateTime(txtThoiGian.Text).ToString("yyyy-MM-dd"));

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Hiển thị thông báo sửa thành công
                        MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật lại giao diện sau khi sửa
                        HienThi();
                    }
                    else
                    {
                        // Hiển thị thông báo khi không tìm thấy bản ghi để sửa
                        MessageBox.Show("Không tìm thấy bản ghi với ID đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu xảy ra vấn đề
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Vui lòng nhập ID để xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sqlDelete = "DELETE FROM dong_vat WHERE id = @id";
            using (SqlCommand cmd = new SqlCommand(sqlDelete, con))
            {
                cmd.Parameters.AddWithValue("@id", txtId.Text);

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Hiển thị thông báo thành công
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật lại giao diện sau khi xóa
                        HienThi();
                    }
                    else
                    {
                        // Hiển thị thông báo khi không tìm thấy bản ghi để xóa
                        MessageBox.Show("Không tìm thấy bản ghi với ID đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu xảy ra vấn đề
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sqlTimKiem = "SELECT * FROM dong_vat WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sqlTimKiem, con);

            cmd.Parameters.AddWithValue("id", txtTimKiem.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            danhsachQLDV.DataSource = dt;
        }


        private void QLDongVat_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(open.FileName);
                this.Text = open.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TrangChu f = new TrangChu();
            f.Show();
            this.Hide();
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void danhsachQLDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void danhsachQLDV_CellClick(object sender, DataGridViewCellEventArgs e)
{
    // Kiểm tra nếu người dùng click vào header hoặc ngoài phạm vi bảng
    if (e.RowIndex < 0 || e.ColumnIndex < 0)
        return;

    // Lấy hàng được chọn
    DataGridViewRow selectedRow = danhsachQLDV.Rows[e.RowIndex];

    // Hiển thị dữ liệu vào các TextBox
    txtId.Text = selectedRow.Cells["id"].Value?.ToString();
    txtTen.Text = selectedRow.Cells["ten"].Value?.ToString();
    txtTuoi.Text = selectedRow.Cells["tuoi"].Value?.ToString();
    txtGiong.Text = selectedRow.Cells["giong"].Value?.ToString();
    txtCanNang.Text = selectedRow.Cells["can_nang"].Value?.ToString();
    txtNguonGoc.Text = selectedRow.Cells["nguon_goc"].Value?.ToString();
    txtThoiGian.Text = selectedRow.Cells["thoi_gian_tao"].Value?.ToString();

    // Hiển thị ảnh nếu có
    if (selectedRow.Cells["hinh_anh"].Value != DBNull.Value)
    {
        byte[] imageBytes = (byte[])selectedRow.Cells["hinh_anh"].Value;
        using (MemoryStream ms = new MemoryStream(imageBytes))
        {
            pictureBox1.Image = Image.FromStream(ms);
        }
    }
    else
    {
        pictureBox1.Image = null; // Không có ảnh
    }
}

    }
}
