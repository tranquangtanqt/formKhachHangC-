using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyXeMay_KTLTNC
{
    public partial class ucKhachHang : UserControl
    {
        public ucKhachHang()
        {
            InitializeComponent();
        }

        private void ucKhachHang_Load(object sender, EventArgs e)
        {
            enable(false); txtMaKH.Enabled = false;
            deleteText();
            rdbNam.Checked = true;
            dtgvKhachHang.DataSource = KetNoi.table("select * from KhachHang");
            dtgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void enable(bool k)
        {
            txtDiaChi.Enabled = txtSDT.Enabled = txtSoCMND.Enabled = 
                txtTenKH.Enabled = rdbNam.Enabled = rdbNu.Enabled = dateNgaySinh.Enabled = 
                btnLuu.Enabled = btnHuy.Enabled = k;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = !k;
        }

        private void deleteText()
        {
            txtDiaChi.Text = txtMaKH.Text = txtSDT.Text = txtSoCMND.Text = txtTenKH.Text = string.Empty;
        }

        bool flag = false;
        private void btnThem_Click(object sender, EventArgs e)
        {
            deleteText();
            txtMaKH.Text = KetNoi.MaTuTang("KH", KetNoi.table("select * from KhachHang"));
            flag = true;
            enable(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int hangDangChon = dtgvKhachHang.CurrentRow.Index;
            if (hangDangChon >= 0)
            {
                flag = false;
                enable(true);
                dtgvKhachHang_Click(sender, e);
            }
            else MessageBox.Show("Bạn chưa chọn dữ liệu cần chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                  int hangDangChon = dtgvKhachHang.CurrentRow.Index;
                  if (MessageBox.Show("Bạn có muốn xóa dữ liệu đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                      == DialogResult.Yes)
                  {
                      string ma = dtgvKhachHang.Rows[hangDangChon].Cells[0].Value.ToString();
                      string xoa = string.Format("DELETE FROM KhachHang WHERE (MaKhachHang = '{0}')", ma);
                      KetNoi.CapNhat(xoa);
                      MessageBox.Show("Xóa thành công");
                      ucKhachHang_Load(sender, e);
                  }
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn chưa chọn dữ liệu cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            ganGiaTri();
            if (txtDiaChi.Text != "" && txtSDT.Text != "" && txtSoCMND.Text != "" && txtTenKH.Text != string.Empty)
            {
                if (flag)
                {
                    try
                    {
                         string s = "INSERT INTO KhachHang (MaKhachHang, TenKhachHang, NgaySinh, GioiTinh, SoCMND, DiaChi, SoDienThoai) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')";
                         string them = string.Format(s, maKH,tenKH,ngaySinh,gioiTinh,soCMND,diaChi,soDT);
                         KetNoi.CapNhat(them);
                         MessageBox.Show("Thêm thành công");
                         ucKhachHang_Load(sender, e);
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
                        string s = "UPDATE KhachHang SET MaKhachHang = '{0}', TenKhachHang = '{1}', NgaySinh = '{2}', GioiTinh = '{3}', SoCMND = '{4}', DiaChi = '{5}', SoDienThoai = '{6}' WHERE (MaKhachHang = '{0}')";
                        string sua = string.Format(s, maKH, tenKH, ngaySinh, gioiTinh, soCMND, diaChi, soDT);
                        KetNoi.CapNhat(sua);
                        MessageBox.Show("Sửa thành công");
                        ucKhachHang_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        KetNoi.DongKetNoi();
                    }
            }
            else MessageBox.Show("Bạn chưa điền đầy đủ thông tin", "Lỗi", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);
        }

        string maKH, tenKH, ngaySinh, diaChi, soDT, soCMND, gioiTinh;
        private void ganGiaTri()
        {
           maKH = txtMaKH.Text;
           tenKH = txtTenKH.Text;
           ngaySinh = dateNgaySinh.Text;
           soDT = txtSDT.Text;
           soCMND = txtSoCMND.Text;
           gioiTinh = rdbNam.Checked ? "Nam" : "Nữ";
           diaChi = txtDiaChi.Text;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ucKhachHang_Load(sender, e);
        }

        private void dtgvKhachHang_Click(object sender, EventArgs e)
        {
            int hangDangChon = dtgvKhachHang.CurrentRow.Index;
            txtMaKH.Text = dtgvKhachHang.Rows[hangDangChon].Cells[0].Value.ToString();
            txtTenKH.Text = dtgvKhachHang.Rows[hangDangChon].Cells[1].Value.ToString();
            dateNgaySinh.Text = dtgvKhachHang.Rows[hangDangChon].Cells[2].Value.ToString();
            txtSoCMND.Text = dtgvKhachHang.Rows[hangDangChon].Cells[4].Value.ToString();
            txtDiaChi.Text = dtgvKhachHang.Rows[hangDangChon].Cells[5].Value.ToString();
            txtSDT.Text = dtgvKhachHang.Rows[hangDangChon].Cells[6].Value.ToString();
            string s = dtgvKhachHang.Rows[hangDangChon].Cells[3].Value.ToString();
            rdbNam.Checked = s == "Nam" ? true : false;
            rdbNu.Checked = s == "Nữ" ? true : false;
        }
    }
}
