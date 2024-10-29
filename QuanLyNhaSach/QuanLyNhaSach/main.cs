using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            khachhang kh = new khachhang();
            kh.Show();
            this.Hide();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            NhapDL nhap = new NhapDL();
            nhap.Show();
            this.Hide();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            hanghoa hh = new hanghoa();
            hh.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            xuathoadon xhd = new xuathoadon();
            xhd.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Thongke tk = new Thongke();
            tk.Show();
            this.Hide();
        }
    }
}
