using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyXeMay_KTLTNC
{
    public partial class ucXeMay : UserControl
    {
        public ucXeMay()
        {
            InitializeComponent();
        }

        private void ucXeMay_Load(object sender, EventArgs e)
        {
            enable(false); txtMa.Enabled = false;
            deleteText();
            dtgv.DataSource = KetNoi.table("select * from XeMay");
            dtgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void enable(bool k)
        {
            txtTen.Enabled = txtHang.Enabled = txtMau.Enabled = txtSoMay.Enabled = txtSoKhung.Enabled = txtNamSX.Enabled = txtDonGia.Enabled = btnLuu.Enabled = btnHuy.Enabled = k;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = !k;
        }

        private void deleteText()
        {
            txtTen.Text = txtMa.Text = txtMau.Text = txtHang.Text = txtSoMay.Text = txtSoKhung.Text = txtDonGia.Text = txtNamSX.Text = string.Empty;
        }

        bool flag = true;
        private void btnThem_Click(object sender, EventArgs e)
        {
            deleteText();
            txtMa.Text = KetNoi.MaTuTang("X", KetNoi.table("select * from XeMay"));
            flag = true;
            enable(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int hangDangChon = dtgv.CurrentRow.Index;
            if (hangDangChon >= 0)
            {
                flag = false;
                enable(true);
                dtgv_Click(sender, e);
            }
            else MessageBox.Show("Bạn chưa chọn dữ liệu cần chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dtgv_Click(object sender, EventArgs e)
        {
            int hangDangChon = dtgv.CurrentRow.Index;
            txtMa.Text = dtgv.Rows[hangDangChon].Cells[0].Value.ToString();
            txtTen.Text = dtgv.Rows[hangDangChon].Cells[1].Value.ToString();
            txtHang.Text = dtgv.Rows[hangDangChon].Cells[2].Value.ToString();
            txtMau.Text = dtgv.Rows[hangDangChon].Cells[3].Value.ToString();
            txtSoKhung.Text = dtgv.Rows[hangDangChon].Cells[4].Value.ToString();
            txtSoMay.Text = dtgv.Rows[hangDangChon].Cells[5].Value.ToString();
            txtDonGia.Text = dtgv.Rows[hangDangChon].Cells[6].Value.ToString();
            txtNamSX.Text = dtgv.Rows[hangDangChon].Cells[7].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int hangDangChon = dtgv.CurrentRow.Index;
                if (MessageBox.Show("Bạn có muốn xóa dữ liệu đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string ma = dtgv.Rows[hangDangChon].Cells[0].Value.ToString();
                    string xoa = string.Format("DELETE FROM XeMay WHERE (MaXe = '{0}')", ma);
                    KetNoi.CapNhat(xoa);
                    MessageBox.Show("Xóa thành công");
                    ucXeMay_Load(sender, e);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn chưa chọn dữ liệu cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        string ma, ten, hang, mau, soKhung, soMay, donGia, nam;
        private void ganGiaTri()
        {
            ma= txtMa.Text;
            ten = txtTen.Text;
            hang = txtHang.Text;
            mau = txtMau.Text;
            soKhung = txtSoKhung.Text;
            soMay = txtSoMay.Text;
            donGia = txtDonGia.Text;
            nam = txtNamSX.Text;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            ganGiaTri();
            if (txtTen.Text != "" && txtHang.Text != "" && txtMau.Text != "" && txtSoMay.Text != "" && txtSoKhung.Text != "" && txtDonGia.Text != "" && txtNamSX.Text != "")
            {
                if (flag)
                {
                    try
                    {
                        string them = string.Format("INSERT INTO XeMay (MaXe, TenXe, HangXe, MauXe, SoKhung, SoMay, DonGiaBan, NamSanXuat) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7})", ma, ten, hang, mau, soKhung, soMay, donGia, nam);
                        KetNoi.CapNhat(them);
                        MessageBox.Show("Thêm thành công");
                        ucXeMay_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        KetNoi.DongKetNoi();
                    }
                }
                else
                    try
                    {
                        string sua = string.Format("UPDATE XeMay SET MaXe = '{0}', TenXe = '{1}', HangXe = '{2}', MauXe = '{3}', SoKhung = '{4}', SoMay = '{5}', DonGiaBan = {6}, NamSanXuat = {7} WHERE (MaXe = '{0}')", ma, ten, hang, mau, soKhung, soMay, donGia, nam);
                        KetNoi.CapNhat(sua);
                        MessageBox.Show("Sửa thành công");
                        ucXeMay_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        KetNoi.DongKetNoi();
                    }
            }
            else MessageBox.Show("Bạn chưa điền đầy đủ thông tin", "Lỗi", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ucXeMay_Load(sender, e);
        }


    }
}
