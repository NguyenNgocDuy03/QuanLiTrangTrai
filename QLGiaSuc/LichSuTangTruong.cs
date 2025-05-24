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
    public partial class LichSuTangTruong : Form
    {
        public LichSuTangTruong()
        {
            InitializeComponent();
        }

        SqlConnection con;


        private void LichSuTangTruong_Load(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["QLGiaSuc"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();
            HienThi();


        }

        public void HienThi()
        {
            string sqlSELECT = "SELECT * FROM lich_su_tang_truong";
            SqlCommand cmd = new SqlCommand(sqlSELECT, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sqlEdit = "UPDATE lich_su_tang_truong SET id_dong_vat = @id_dong_vat, ngay = @ngay, can_nang = @can_nang, ghi_chu = @ghi_chu WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sqlEdit, con);
            cmd.Parameters.AddWithValue("id", txtID.Text);
            cmd.Parameters.AddWithValue("@id_dong_vat", txtIDDV.Text);
            cmd.Parameters.AddWithValue("@ngay", txtNgay.Text);
            cmd.Parameters.AddWithValue("@can_nang", txtCanNang.Text);
            cmd.Parameters.AddWithValue("@ghi_chu", txtGhiChu.Text);
            cmd.ExecuteNonQuery();
            HienThi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sqlDelete = "DELETE FROM lich_su_tang_truong WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("id", txtID.Text);
            cmd.Parameters.AddWithValue("@id_dong_vat", txtIDDV.Text);
            cmd.Parameters.AddWithValue("@ngay", txtNgay.Text);
            cmd.Parameters.AddWithValue("@can_nang", txtCanNang.Text);
            cmd.Parameters.AddWithValue("@ghi_chu", txtGhiChu.Text);
            cmd.ExecuteNonQuery();
            HienThi();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sqlTimKiem = "SELECT * FROM lich_su_tang_truong WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sqlTimKiem, con);

            cmd.Parameters.AddWithValue("id", txtTimKiem.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            string sqlINSERT = "INSERT INTO lich_su_tang_truong (id_dong_vat, ngay, can_nang, ghi_chu) VALUES (@id_dong_vat, @ngay, @can_nang, @ghi_chu)";

            using (SqlCommand cmd = new SqlCommand(sqlINSERT, con))
            {

                cmd.Parameters.AddWithValue("@id_dong_vat", txtIDDV.Text);
                cmd.Parameters.AddWithValue("@ngay", txtNgay.Text);
                cmd.Parameters.AddWithValue("@can_nang", txtCanNang.Text);
                cmd.Parameters.AddWithValue("@ghi_chu", txtGhiChu.Text);

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

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TrangChu f = new TrangChu();
            f.Show();
            this.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
                // Kiểm tra nếu không phải là một ô dữ liệu
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Lấy hàng được chọn
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    // Gán giá trị từ hàng được chọn vào các TextBox
                    txtID.Text = row.Cells["id"].Value.ToString();
                    txtIDDV.Text = row.Cells["id_dong_vat"].Value.ToString();
                    txtNgay.Text = row.Cells["ngay"].Value.ToString();
                    txtCanNang.Text = row.Cells["can_nang"].Value.ToString();
                    txtGhiChu.Text = row.Cells["ghi_chu"].Value.ToString();
                }
            

        }
    }
}
