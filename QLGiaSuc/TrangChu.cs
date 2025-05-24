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
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
            this.Resize += new EventHandler(frmMain_Resize);

        }

        private void btnQuanLyDongVat_Click(object sender, EventArgs e)
        {
            QLDongVat f = new QLDongVat();
            f.Show();
            this.Hide();
        }

        private void btnHoSoSucKhoe_Click(object sender, EventArgs e)
        {
            HoSoSucKhoe f = new HoSoSucKhoe();
            f.Show();
            this.Hide();
        }

        private void btnHoSoVacXin_Click(object sender, EventArgs e)
        {
            HoSoVacXin f = new HoSoVacXin();
            f.Show();
            this.Hide();
        }

        private void btnLichChamSoc_Click(object sender, EventArgs e)
        {
            LichChamSoc f = new LichChamSoc();
            f.Show();
            this.Hide();
        }

        private void btnLichSuTangTruong_Click(object sender, EventArgs e)
        {
            LichSuTangTruong f = new LichSuTangTruong();
            f.Show();
            this.Hide();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void mnuQuanligiasuc_Click(object sender, EventArgs e)
        {
            QLDongVat f = new QLDongVat();
            f.ShowDialog();
            this.Hide();
        }

        private void Hososuckhoe_Click(object sender, EventArgs e)
        {
            HoSoSucKhoe f = new HoSoSucKhoe();
            f.ShowDialog();
            this.Hide();
        }

        private void Hosovc_Click(object sender, EventArgs e)
        {
            HoSoVacXin f = new HoSoVacXin();
            f.ShowDialog();
            this.Hide();
        }

        private void mnuTimhoadon_Click(object sender, EventArgs e)
        {
            LichChamSoc f = new LichChamSoc();
            f.ShowDialog();
            this.Hide();
        }

        private void Lichsutangtruong_Click(object sender, EventArgs e)
        {
            LichSuTangTruong f = new LichSuTangTruong();
            f.ShowDialog();
            this.Hide();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmMain_Resize(object sender, EventArgs e)
        {

            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
    }
}