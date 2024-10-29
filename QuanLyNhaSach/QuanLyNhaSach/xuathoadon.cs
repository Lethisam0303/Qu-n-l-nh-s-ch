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
    public partial class xuathoadon : Form
    {
        public xuathoadon()
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

        private void xuathoadon_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            hienthi();
        }
        private void hienthi()
        {
            listView1.Items.Clear();
            ketnoi.Open();
            sql = "select XUAT.MaDBH, MaKH, MaHH, NgayBan, SL, HTTT, VAT, ThanhTien, TongCong from XUAT left outer join XUAT_CHI_TIET on XUAT_CHI_TIET.MaDBH = XUAT.MaDBH";
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
                i++;
            }
            ketnoi.Close();
        }
        private void listView1_Click(object sender, EventArgs e)
        {
            txtMaDBH.Text = listView1.SelectedItems[0].SubItems[0].Text;
            txtMaKH.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtMaHHX.Text = listView1.SelectedItems[0].SubItems[2].Text;
            txtNgayBan.Text = listView1.SelectedItems[0].SubItems[3].Text;
            txtSLX.Text = listView1.SelectedItems[0].SubItems[4].Text;
            txtHTTTX.Text = listView1.SelectedItems[0].SubItems[5].Text;
            txtVATX.Text = listView1.SelectedItems[0].SubItems[6].Text;
            txtThanhTienX.Text = listView1.SelectedItems[0].SubItems[7].Text;
            txtTongCongX.Text = listView1.SelectedItems[0].SubItems[8].Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtMaDBH.ResetText();
            txtMaKH.ResetText();
            txtMaHHX.ResetText();
            txtNgayBan.ResetText();
            txtSLX.ResetText();
            txtHTTTX.ResetText();
            txtVATX.ResetText();
            txtThanhTienX.ResetText();
            txtTongCongX.ResetText();
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
                    sql = @"insert into XUAT(MaDBH,MaKH,NgayBan,VAT) values(N'" + txtMaDBH.Text + @"', N'" + txtMaKH.Text + @"',N'" + txtNgayBan.Text + @"', N'" + txtVATX.Text + @"')";
                    thuchien = new SqlCommand(sql, ketnoi);
                    thuchien.ExecuteNonQuery();

                    sql1 = @"insert into XUAT_CHI_TIET(MaDBH,MaHH,HTTT,SL)  values(N'" + txtMaDBH.Text + @"', N'" + txtMaHHX.Text + @"',N'" + txtHTTTX.Text + @"', N'" + txtSLX.Text + @"')";
                    thuchien = new SqlCommand(sql1, ketnoi);
                    thuchien.ExecuteNonQuery();

                    sql2 = @"UPDATE XUAT SET TongCong = HANG_HOA.DonGiaBan * XUAT_CHI_TIET.SL * XUAT.VAT FROM XUAT_CHI_TIET inner join HANG_HOA ON HANG_HOA.MaHH = XUAT_CHI_TIET.MaHH inner join XUAT ON XUAT_CHI_TIET.MaDBH=XUAT.MaDBH";
                    thuchien = new SqlCommand(sql2, ketnoi);
                    thuchien.ExecuteNonQuery();

                    sql3 = @"UPDATE XUAT_CHI_TIET SET ThanhTien = HANG_HOA.DonGiaBan * XUAT_CHI_TIET.SL FROM XUAT_CHI_TIET inner join HANG_HOA ON HANG_HOA.MaHH = XUAT_CHI_TIET.MaHH";
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

                    sql = @"Update XUAT
                            set MaDBH = N'" + txtMaDBH.Text + @"', MaKH =  N'" + txtMaKH.Text + @"', NgayBan = N'" + txtNgayBan.Text + @"', VAT =  N'" + txtVATX.Text + @"'
                            where (MaDBH = N'" + txtMaDBH.Text + @"')";
                    thuchien = new SqlCommand(sql, ketnoi);
                    thuchien.ExecuteNonQuery();

                    sql1 = @"Update XUAT_CHI_TIET
                             set MaDBH = N'" + txtMaDBH.Text + @"', MaHH = N'" + txtMaHHX.Text + @"', HTTT = '" + txtHTTTX.Text + @"', SL = '" + txtSLX.Text + @"'
                             where (MaDBH = N'" + txtMaDBH.Text + @"')";
                    thuchien = new SqlCommand(sql1, ketnoi);
                    thuchien.ExecuteNonQuery();

                    sql2 = @"UPDATE XUAT SET TongCong = HANG_HOA.DonGiaBan * XUAT_CHI_TIET.SL * XUAT.VAT FROM XUAT_CHI_TIET inner join HANG_HOA ON HANG_HOA.MaHH = XUAT_CHI_TIET.MaHH inner join XUAT ON XUAT_CHI_TIET.MaDBH=XUAT.MaDBH";
                    thuchien = new SqlCommand(sql2, ketnoi);
                    thuchien.ExecuteNonQuery();

                    sql3 = @"UPDATE XUAT_CHI_TIET SET ThanhTien = HANG_HOA.DonGiaBan * XUAT_CHI_TIET.SL FROM XUAT_CHI_TIET inner join HANG_HOA ON HANG_HOA.MaHH = XUAT_CHI_TIET.MaHH";
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
                    sql = @"delete from XUAT_CHI_TIET where(MaDBH = N'" + txtMaDBH.Text + @"')";
                    thuchien = new SqlCommand(sql, ketnoi);
                    thuchien.ExecuteNonQuery();

                    //sql1 = @"delete from NHAP where(MaDNH = '" + txtMaDNH.Text + "')";
                    sql1 = @"delete from XUAT where MaKH = N'" + txtMaKH.Text + @"' and NgayBan = N'" + txtNgayBan.Text + @"' and VAT = N'" + txtVATX.Text + @"'";
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
