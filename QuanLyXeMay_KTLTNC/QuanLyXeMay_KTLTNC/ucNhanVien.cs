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
    public partial class ucNhanVien : UserControl
    {
        public ucNhanVien()
        {
            InitializeComponent();
        }

        private void ucNhanVien_Load(object sender, EventArgs e)
        {
            txtMa.Enabled = false;
            enable(false);
            deleteText();
            rdbNam.Checked = true;
            dtgv.DataSource = KetNoi.table("select * from NhanVien");
            dtgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void enable(bool k)
        {
            txtDiaChi.Enabled = txtSDT.Enabled = txtSoCMND.Enabled = txtTen.Enabled = rdbNam.Enabled = rdbNu.Enabled = dateNgaySinh.Enabled = btnLuu.Enabled = btnHuy.Enabled = txtChucVu.Enabled = txtLuong.Enabled = k;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = !k;
        }

        private void deleteText()
        {
            txtDiaChi.Text = txtMa.Text = txtSDT.Text = txtSoCMND.Text = txtTen.Text = txtLuong.Text = txtChucVu.Text = string.Empty;
        }

        string ma, ten, ngaySinh, diaChi, soDT, soCMND, gioiTinh, chucVu;
        private void ganGiaTri()
        {
            ma = txtMa.Text;
            ten = txtTen.Text;
            ngaySinh = dateNgaySinh.Text;
            soDT = txtSDT.Text;
            soCMND = txtSoCMND.Text;
            gioiTinh = rdbNam.Checked ? "Nam" : "Nữ";
            diaChi = txtDiaChi.Text;
            chucVu = txtChucVu.Text;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ucNhanVien_Load(sender, e);
        }

        bool flag = true;
        private void btnThem_Click(object sender, EventArgs e)
        {
            deleteText();
            txtMa.Text = KetNoi.MaTuTang("NV", KetNoi.table("select * from NhanVien"));
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
            dateNgaySinh.Text = dtgv.Rows[hangDangChon].Cells[2].Value.ToString();
            txtSoCMND.Text = dtgv.Rows[hangDangChon].Cells[4].Value.ToString();
            txtLuong.Text = dtgv.Rows[hangDangChon].Cells[5].Value.ToString();
            txtChucVu.Text = dtgv.Rows[hangDangChon].Cells[6].Value.ToString();
            txtDiaChi.Text = dtgv.Rows[hangDangChon].Cells[7].Value.ToString();
            txtSDT.Text = dtgv.Rows[hangDangChon].Cells[8].Value.ToString();
            string s = dtgv.Rows[hangDangChon].Cells[3].Value.ToString();
            rdbNam.Checked = s == "Nam" ? true : false;
            rdbNu.Checked = s == "Nữ" ? true : false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int hangDangChon = dtgv.CurrentRow.Index;
                if (MessageBox.Show("Bạn có muốn xóa dữ liệu đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string ma = dtgv.Rows[hangDangChon].Cells[0].Value.ToString();
                    string xoa = string.Format("DELETE FROM NhanVien WHERE (MaNhanVien = '{0}')", ma);
                    KetNoi.CapNhat(xoa);
                    MessageBox.Show("Xóa thành công");
                    ucNhanVien_Load(sender, e);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn chưa chọn dữ liệu cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        int luong;
        private void btnLuu_Click(object sender, EventArgs e)
        {
            bool kt = false;
            try
            {
                kt = true;
                luong = Convert.ToInt32(txtLuong.Text);
            }
            catch (Exception)
            {
                kt = false;
                MessageBox.Show("Lương nhập không đúng định dạng\nVui lòng nhập lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ganGiaTri();
            if (txtDiaChi.Text != "" && txtSDT.Text != "" && txtSoCMND.Text != "" && txtTen.Text != "" && txtLuong.Text != "" && txtChucVu.Text != "" && kt)
            {
                if (flag)
                {
                    try
                    {
                        string them = string.Format("INSERT INTO NhanVien (MaNhanVien, TenNhanVien, NgaySinh, GioiTinh, SoCMND, Luong, ChucVu, DiaChi, SoDienThoai) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}', '{7}', '{8}')", ma, ten, ngaySinh, gioiTinh, soCMND, luong, chucVu, diaChi, soDT);
                        KetNoi.CapNhat(them);
                        MessageBox.Show("Thêm thành công");
                        ucNhanVien_Load(sender, e);
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
                        string sua = string.Format("UPDATE NhanVien SET MaNhanVien = '{0}', TenNhanVien = '{1}', NgaySinh = '{2}', GioiTinh = '{3}', SoCMND = '{4}', Luong = {5}, ChucVu = '{6}', DiaChi = {7}, SoDienThoai = {8} WHERE (MaNhanVien = '{0}')", ma, ten, ngaySinh, gioiTinh, soCMND, luong, chucVu, diaChi, soDT);
                        KetNoi.CapNhat(sua);
                        MessageBox.Show("Sửa thành công");
                        ucNhanVien_Load(sender, e);
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
