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

namespace QLGiaSuc
{
    public partial class Form1 : Form
    {
        SqlConnection conn = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (conn == null)
            {
                conn = new SqlConnection(@"Data Source=LAPTOP-1C92RKOB\NGUYENNGOCDUYM;Initial Catalog=QLGiaSuc;Integrated Security=True");
            }

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string tk = txtTaiKhoan.Text.Trim();
            string mk = txtMatKhau.Text.Trim();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM NguoiDung WHERE TaiKhoan = @TaiKhoan AND MatKhau = @MatKhau";
            cmd.Connection = conn;

            // Sử dụng tham số để tránh SQL Injection
            cmd.Parameters.AddWithValue("@TaiKhoan", tk);
            cmd.Parameters.AddWithValue("@MatKhau", mk);

            SqlDataReader data = cmd.ExecuteReader();

            if (data.Read() == true)
            {
                // Hiển thị thông báo đăng nhập thành công
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở form TrangChu
                TrangChu f = new TrangChu();
                f.Show();

                // Ẩn form hiện tại
                this.Hide();
            }
            else
            {
                // Hiển thị thông báo đăng nhập thất bại
                MessageBox.Show("Đăng nhập thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            data.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            Dangki f = new Dangki();
            f.ShowDialog();
            this.Hide();
        }
    }
}
