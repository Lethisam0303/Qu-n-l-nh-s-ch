using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class hanghoa : Form
    {
        public hanghoa()
        {
            InitializeComponent();
        }
        string chuoiketnoi = @"Data Source=HIEN-TT\THUHIEN;Initial Catalog=QUAN_LY_NHA_SACH;User ID=hientt;Password=123456";

        string sql;
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;
        private void hanghoa_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            hienthi();
        }
        private void hienthi()
        {
            listView1.Items.Clear();
            ketnoi.Open();
            sql = "select * from HANG_HOA";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                listView1.Items.Add(docdulieu[0].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[1].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[2].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[3].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[4].ToString());
                i++;
            }
            ketnoi.Close();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            txtMaHH.Text = listView1.SelectedItems[0].SubItems[0].Text;
            txtTenHH.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtGN.Text = listView1.SelectedItems[0].SubItems[2].Text;
            txtGB.Text = listView1.SelectedItems[0].SubItems[3].Text;
            txtDVT.Text = listView1.SelectedItems[0].SubItems[4].Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtMaHH.ResetText();
            txtTenHH.ResetText();
            txtGN.ResetText();
            txtGB.ResetText();
            txtDVT.ResetText();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult f = MessageBox.Show("Bạn có chắc chắn thêm không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (f == DialogResult.Yes)
            {
                try
                {
                    listView1.Items.Clear();
                    ketnoi.Open();
                    sql = @"insert into HANG_HOA (MaHH, TenHH, DonGiaNhap, DonGiaBan, ĐVT) values(N'" + txtMaHH.Text + @"', N'" + txtTenHH.Text + @"', N'" + txtGN.Text + @"', N'" + txtGB.Text + @"', N'" + txtDVT.Text + @"')";
                    thuchien = new SqlCommand(sql, ketnoi);
                    thuchien.ExecuteNonQuery();
                    ketnoi.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình thêm!", "Thông báo");
                }
                hienthi();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult f = MessageBox.Show("Bạn có chắc chắn muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (f == DialogResult.Yes)
            {
                try
                {
                    listView1.Items.Clear();
                    ketnoi.Open();
                    sql = @"Update HANG_HOA 
                            set MaHH = N'" + txtMaHH.Text + @"', TenHH = N'" + txtTenHH.Text + @"', DonGiaNhap = N'" + txtGN.Text + @"', DonGiaBan = N'" + txtGB.Text + @"',  ĐVT = N'" + txtDVT.Text + @"'
                            where (MaHH = N'" + txtMaHH.Text + @"')";
                    thuchien = new SqlCommand(sql, ketnoi);
                    thuchien.ExecuteNonQuery();
                    ketnoi.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình sửa!", "Thông báo");
                }
                hienthi();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult f = MessageBox.Show("Bạn có chắc chắn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (f == DialogResult.Yes)
            {
                this.Close();
                main mn = new main();
                this.Hide();
                mn.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult f = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (f == DialogResult.Yes)
            {
                try
                {
                    listView1.Items.Clear();
                    ketnoi.Open();
                    sql = @"Delete from HANG_HOA where (MaHH = N'" + txtMaHH.Text + @"')";
                    thuchien = new SqlCommand(sql, ketnoi);
                    thuchien.ExecuteNonQuery();
                    ketnoi.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình xóa!", "Thông báo");
                }
                hienthi();
            }
        }
    }
}
