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
    public partial class ucHoaDon : UserControl
    {
        public ucHoaDon()
        {
            InitializeComponent();
        }

        private void ucHoaDon_Load(object sender, EventArgs e)
        {
            cboMaKH.DataSource = KetNoi.table("select MaKhachHang from KhachHang");
            cboMaKH.DisplayMember = "MaKhachHang";

            cboMaXe.DataSource = KetNoi.table("select MaXe from XeMay where MaXe not in (select MaXe from HoaDonBanXeMay)");
            cboMaXe.DisplayMember = "MaXe";

            enable(false); txtMaHD.Enabled = false; txtSoLuong.Enabled = false;
            deleteText();
            dtgvHoaDon.DataSource = KetNoi.table("select * from HoaDonBanXeMay");
            dtgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dtgvKH.DataSource = KetNoi.table("select MaKhachHang, TenKhachHang, DiaChi from KhachHang");
            dtgvKH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvXeMay.DataSource = KetNoi.table("select MaXe, TenXe, HangXe, DonGiaBan from XeMay where MaXe not in (select MaXe from HoaDonBanXeMay)");
            dtgvXeMay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void enable(bool k)
        {
            cboMaKH.Enabled = cboMaXe.Enabled = txtTenKH.Enabled = txtTenXe.Enabled = txtMaNV.Enabled = txtThue.Enabled = txtDonGia.Enabled = dateTimePicker1.Enabled = btnLuu.Enabled = btnHuy.Enabled = k;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = !k;
        }

        private void deleteText()
        {
            txtMaHD.Text = cboMaKH.Text = cboMaXe.Text = txtDonGia.Text = txtSoLuong.Text = txtThue.Text = txtTenKH.Text = txtTenXe.Text = string.Empty;
        }

        private void dtgvKH_Click(object sender, EventArgs e)
        {
            int hang = dtgvKH.CurrentRow.Index;
            cboMaKH.Text = dtgvKH.Rows[hang].Cells[0].Value.ToString();
            txtTenKH.Text = dtgvKH.Rows[hang].Cells[1].Value.ToString();
        }

        private void dtgvXeMay_Click(object sender, EventArgs e)
        {
            int hang = dtgvXeMay.CurrentRow.Index;
            cboMaXe.Text = dtgvXeMay.Rows[hang].Cells[0].Value.ToString();
            txtTenXe.Text = dtgvXeMay.Rows[hang].Cells[1].Value.ToString();
            txtDonGia.Text = dtgvXeMay.Rows[hang].Cells[3].Value.ToString();
        }

        private void cboMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaKH.Text != "System.Data.DataRowView") txtTenKH.Text = KetNoi.GiaTri(string.Format("SELECT TenKhachHang FROM KhachHang WHERE (MaKhachHang = '{0}')", cboMaKH.Text.Trim()));
        }

        private void cboMaXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaXe.Text != "System.Data.DataRowView")
            {
                txtTenXe.Text = KetNoi.GiaTri(string.Format("SELECT TenXe FROM XeMay WHERE (MaXe = '{0}')", cboMaXe.Text.Trim()));
                txtDonGia.Text = KetNoi.GiaTri(string.Format("SELECT DonGiaBan FROM XeMay WHERE (MaXe = '{0}')", cboMaXe.Text.Trim()));
            }
        }

        bool flag = false;
        private void btnThem_Click(object sender, EventArgs e)
        {
            deleteText();
            txtMaHD.Text = KetNoi.MaTuTang("HD", KetNoi.table("select * from HoaDonBanXeMay"));
            txtSoLuong.Text = "1";
            flag = true;
            enable(true);
        }

        private void dtgvHoaDon_Click(object sender, EventArgs e)
        {
            int hangDangChon = dtgvHoaDon.CurrentRow.Index;
            txtMaHD.Text = dtgvHoaDon.Rows[hangDangChon].Cells[0].Value.ToString();
            cboMaXe.Text = dtgvHoaDon.Rows[hangDangChon].Cells[1].Value.ToString();
            txtMaNV.Text = dtgvHoaDon.Rows[hangDangChon].Cells[2].Value.ToString();
            cboMaKH.Text = dtgvHoaDon.Rows[hangDangChon].Cells[3].Value.ToString();
            dateTimePicker1.Text = dtgvHoaDon.Rows[hangDangChon].Cells[4].Value.ToString();
            txtSoLuong.Text = dtgvHoaDon.Rows[hangDangChon].Cells[5].Value.ToString();
            txtThue.Text = dtgvHoaDon.Rows[hangDangChon].Cells[6].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int hangDangChon = dtgvHoaDon.CurrentRow.Index;
            if (hangDangChon >= 0)
            {
                flag = false;
                enable(true);
                dtgvHoaDon_Click(sender, e);
            }
            else MessageBox.Show("Bạn chưa chọn dữ liệu cần chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int hangDangChon = dtgvHoaDon.CurrentRow.Index;
                if (MessageBox.Show("Bạn có muốn xóa dữ liệu đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string ma = dtgvHoaDon.Rows[hangDangChon].Cells[0].Value.ToString();
                    string xoa = string.Format("DELETE FROM HoaDonBanXeMay WHERE (MaHoaDon = '{0}')", ma);
                    KetNoi.CapNhat(xoa);
                    MessageBox.Show("Xóa thành công");
                    ucHoaDon_Load(sender, e);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn chưa chọn dữ liệu cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ucHoaDon_Load(sender, e);
        }

        string maHD, maKH, maNV, ngayBan, maXe;
        int soLuong, thue;
        private void ganGiaTri()
        {
            maKH = cboMaKH.Text;
            maNV = txtMaNV.Text;
            maHD = txtMaHD.Text;
            maXe = cboMaXe.Text;
            soLuong = Convert.ToInt32(txtSoLuong.Text.Trim());
            ngayBan = dateTimePicker1.Text;
            thue = Convert.ToInt32(txtThue.Text.Trim());
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            ganGiaTri();
            if (cboMaKH.Text != "" && cboMaXe.Text != "" && txtMaNV.Text != "" && txtThue.Text != string.Empty)
            {
                if (flag)
                {
                    try
                    {
                        string them = string.Format("INSERT INTO HoaDonBanXeMay (MaHoaDon, MaXe, MaNhanVien, MaKhachHang, NgayBan, SoLuong, Thue) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6})", maHD, maXe, maNV, maKH, ngayBan, soLuong, thue);
                        KetNoi.CapNhat(them);
                        MessageBox.Show("Thêm thành công");
                        ucHoaDon_Load(sender, e);
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
                        string sua = string.Format("UPDATE HoaDonBanXeMay SET MaHoaDon = '{0}', MaXe = '{1}', MaNhanVien = '{2}', MaKhachHang = '{3}', NgayBan = '{4}', SoLuong = {5}, Thue = {6} WHERE (MaHoaDon = '{0}')", maHD, maXe, maNV, maKH, ngayBan, soLuong, thue);
                        KetNoi.CapNhat(sua);
                        MessageBox.Show("Sửa thành công");
                        ucHoaDon_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        KetNoi.DongKetNoi();
                    }
            }
            else MessageBox.Show("Bạn chưa điền đầy đủ thông tin", "Lỗi", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);
        }






    }
}
