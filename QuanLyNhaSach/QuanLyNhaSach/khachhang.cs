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
    public partial class khachhang : Form
    {
        public khachhang()
        {
            InitializeComponent();
        }
        string chuoiketnoi = @"Data Source=HIEN-TT\THUHIEN;Initial Catalog=QUAN_LY_NHA_SACH;User ID=hientt;Password=123456";

        string sql;
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;
        private void khachhang_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            hienthi();
        }
        private void hienthi()
        {
            listView1.Items.Clear();
            ketnoi.Open();
            sql = "select * from NGUOI_MUA_HANG";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                listView1.Items.Add(docdulieu[0].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[1].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[2].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[3].ToString());
                i++;
            }
            ketnoi.Close();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = listView1.SelectedItems[0].SubItems[0].Text;
            txtMathue.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtTenKH.Text = listView1.SelectedItems[0].SubItems[2].Text;
            txtDiachi.Text = listView1.SelectedItems[0].SubItems[3].Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult f = MessageBox.Show("Bạn có chắc chắn thêm không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (f == DialogResult.Yes)
            {
                listView1.Items.Clear();
                ketnoi.Open();
                sql = @"insert into NGUOI_MUA_HANG (MaKH, MaThueNMH, TenNMH, Diachi) values(N'" + txtMaKH.Text + @"', N'" + txtMathue.Text + @"', N'" + txtTenKH.Text + @"', N'" + txtDiachi.Text + @"')";

                if (txtMaKH.Text == "" || txtMathue.Text == "" || txtTenKH.Text == "" || txtDiachi.Text == "")
                {
                    MessageBox.Show("Vui lòng điền thông tin đầy đủ!", "Thông báo");
                }
                else
                {
                    try
                    {
                        thuchien = new SqlCommand(sql, ketnoi);
                        thuchien.ExecuteNonQuery();
                        ketnoi.Close();
                        //MessageBox.Show("Thêm thành công!", "Thông báo");
                }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xảy ra lỗi trong quá trình thêm!", "Thông báo");
                    }
                    hienthi();                    
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            txtMaKH.ResetText();
            txtMathue.ResetText();
            txtTenKH.ResetText();
            txtDiachi.ResetText();
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
                    sql = @"Update NGUOI_MUA_HANG 
                            set MaKH = N'" + txtMaKH.Text + @"', MaThueNMH = N'" + txtMathue.Text + @"', TenNMH = N'" + txtTenKH.Text + @"', Diachi = N'" + txtDiachi.Text + @"'
                            where (MaKH = N'" + txtMaKH.Text + @"')";
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
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult f = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (f == DialogResult.Yes)
            {
                try
                {
                    listView1.Items.Clear();
                    ketnoi.Open();
                    sql = @"Delete from NGUOI_MUA_HANG where (MaKH = N'" + txtMaKH.Text + @"')";
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
    }
}
