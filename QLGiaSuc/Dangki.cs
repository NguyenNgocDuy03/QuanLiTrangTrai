using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLGiaSuc
{
    public partial class Dangki : Form
    {
        public Dangki()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void btnDang_Click(object sender, EventArgs e)
        {

            TrangChu f = new TrangChu();
            f.ShowDialog();
            this.Hide();
            // Hiển thị hộp thoại thông báo
            MessageBox.Show("Tạo tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
          

        }
    }
}
