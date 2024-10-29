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
    public partial class NhapDL : Form
    {
        public NhapDL()
        {
            InitializeComponent();
        }

        string chuoiketnoi = @"Data Source=HIEN-TT\THUHIEN;Initial Catalog=QUAN_LY_NHA_SACH;User ID=hientt;Password=123456";

        string sql;
        string sql1;
        string sql2;
        string sql3;
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;
        private void button5_Click(object sender, EventArgs e)
        {
            txtMaDNH.ResetText();
            txtMaNCC.ResetText();
            txtMaNNH.ResetText();
            txtMaHH.ResetText();
            txtNgayNhap.ResetText();
            txtVAT.ResetText();
            txtHTTT.ResetText();
            txtSL.ResetText();
            txtThanhTien.ResetText();
            txtTongCong.ResetText();
        }

        private void NhapDL_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            hienthi();
        }
        private void hienthi()
        {
            listView1.Items.Clear();
            ketnoi.Open();
            sql = "select NHAP_CHI_TIET.MaDNH, MaNCC, MaNNH, MaHH, NgayNhap, VAT, HTTT, SL, ThanhTien, TongCong from NHAP left outer join NHAP_CHI_TIET on NHAP_CHI_TIET.MaDNH = NHAP.MaDNH";
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
                listView1.Items[i].SubItems.Add(docdulieu[5].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[6].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[7].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[8].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[9].ToString());
                i++;
            }
            ketnoi.Close();
        }
        private void listView1_Click(object sender, EventArgs e)
        {
            txtMaDNH.Text = listView1.SelectedItems[0].SubItems[0].Text;
            txtMaNCC.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtMaNNH.Text = listView1.SelectedItems[0].SubItems[2].Text;
            txtMaHH.Text = listView1.SelectedItems[0].SubItems[3].Text;
            txtNgayNhap.Text = listView1.SelectedItems[0].SubItems[4].Text;
            txtVAT.Text = listView1.SelectedItems[0].SubItems[5].Text;
            txtHTTT.Text = listView1.SelectedItems[0].SubItems[6].Text;
            txtSL.Text = listView1.SelectedItems[0].SubItems[7].Text;
            txtThanhTien.Text = listView1.SelectedItems[0].SubItems[8].Text;
            txtTongCong.Text = listView1.SelectedItems[0].SubItems[9].Text;
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
                    sql = @"insert into NHAP (MaDNH, MaNNH, NgayNhap, VAT, MaNCC) values(N'" + txtMaDNH.Text + @"', N'" + txtMaNNH.Text + @"',N'" + txtNgayNhap.Text + @"', N'" + txtVAT.Text + @"', N'" + txtMaNCC.Text + @"')";
                    thuchien = new SqlCommand(sql, ketnoi);
                    thuchien.ExecuteNonQuery();

                    sql1 = @"insert into NHAP_CHI_TIET (MaDNH, MaHH, HTTT, SL) values(N'" + txtMaDNH.Text + @"', N'" + txtMaHH.Text + @"',N'" + txtHTTT.Text + @"', N'" + txtSL.Text + @"')";
                    thuchien = new SqlCommand(sql1, ketnoi);
                    thuchien.ExecuteNonQuery();

                    sql2 = @"UPDATE NHAP SET TongCong = HANG_HOA.DonGiaNhap * NHAP_CHI_TIET.SL * NHAP.VAT FROM NHAP INNER JOIN NHAP_CHI_TIET ON NHAP_CHI_TIET.MaDNH = NHAP.MaDNH INNER JOIN HANG_HOA ON HANG_HOA.MaHH = NHAP_CHI_TIET.MaHH";
                    thuchien = new SqlCommand(sql2, ketnoi);
                    thuchien.ExecuteNonQuery();

                    sql3 = @"UPDATE NHAP_CHI_TIET SET ThanhTien = HANG_HOA.DonGiaNhap * NHAP_CHI_TIET.SL FROM NHAP_CHI_TIET INNER JOIN HANG_HOA ON HANG_HOA.MaHH = NHAP_CHI_TIET.MaHH";
                    thuchien = new SqlCommand(sql3, ketnoi);
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

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult f = MessageBox.Show("Bạn có chắc chắn muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (f == DialogResult.Yes)
            {
                try
                {
                    listView1.Items.Clear();
                    ketnoi.Open();

                    sql = @"Update NHAP
                            set MaDNH = N'" + txtMaDNH.Text + @"', MaNNH =  N'" + txtMaNNH.Text + @"', NgayNhap = N'" + txtNgayNhap.Text + @"', VAT =  N'" + txtVAT.Text + @"', MaNCC = N'" + txtMaNCC.Text + @"'
                            where (MaDNH = N'" + txtMaDNH.Text + @"')";
                    thuchien = new SqlCommand(sql, ketnoi);
                    thuchien.ExecuteNonQuery();

                    sql1 = @"Update NHAP_CHI_TIET
                            set MaDNH = N'" + txtMaDNH.Text + @"', MaHH = N'" + txtMaHH.Text + @"', HTTT = '" + txtHTTT.Text + @"', SL = '" + txtSL.Text + @"'
                            where (MaDNH = N'" + txtMaDNH.Text + @"')";
                    thuchien = new SqlCommand(sql1, ketnoi);
                    thuchien.ExecuteNonQuery();

                    sql2 = @"UPDATE NHAP SET TongCong = HANG_HOA.DonGiaNhap * NHAP_CHI_TIET.SL * NHAP.VAT FROM NHAP INNER JOIN NHAP_CHI_TIET ON NHAP_CHI_TIET.MaDNH = NHAP.MaDNH INNER JOIN HANG_HOA ON HANG_HOA.MaHH = NHAP_CHI_TIET.MaHH";
                    thuchien = new SqlCommand(sql2, ketnoi);
                    thuchien.ExecuteNonQuery();

                    sql3 = @"UPDATE NHAP_CHI_TIET SET ThanhTien = HANG_HOA.DonGiaNhap * NHAP_CHI_TIET.SL FROM NHAP_CHI_TIET INNER JOIN HANG_HOA ON HANG_HOA.MaHH = NHAP_CHI_TIET.MaHH";
                    thuchien = new SqlCommand(sql3, ketnoi);
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

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult f = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (f == DialogResult.Yes)
            {
                try
                {
                    listView1.Items.Clear();
                    ketnoi.Open();
                    sql = @"delete from NHAP_CHI_TIET where(MaDNH = '" + txtMaDNH.Text + "')";
                    thuchien = new SqlCommand(sql, ketnoi);
                    thuchien.ExecuteNonQuery();

                    //sql1 = @"delete from NHAP where(MaDNH = '" + txtMaDNH.Text + "')";
                    sql1 = @"delete from NHAP where MaNCC = N'" + txtMaNCC.Text + @"' and MaNNH = N'" + txtMaNNH.Text + @"' and NgayNhap = N'" + txtNgayNhap.Text + @"'";
                    thuchien = new SqlCommand(sql1, ketnoi);
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

        private void button6_Click(object sender, EventArgs e)
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
