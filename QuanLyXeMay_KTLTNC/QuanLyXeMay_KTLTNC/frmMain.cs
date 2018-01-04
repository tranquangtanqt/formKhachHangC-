using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyXeMay_KTLTNC
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
        }

        private void btnDongTabPage_Click(object sender, EventArgs e)
        {
            int a = tabControl1.SelectedIndex;
            if(a == tabControl1.TabCount - 1 && a != 0)
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                tabControl1.SelectedIndex = a - 1;
            }
            else if(a != 0)
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                tabControl1.SelectedIndex = a;
            }
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            int tab = 0;
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                if (tabControl1.TabPages[i].Text == "Khách hàng") 
                {
                    tab = i; break;
                }
            }
            if (tab == 0)
            {
                TabPage tp = new TabPage();
                tp.Text = "Khách hàng";
                ucKhachHang uc = new ucKhachHang();
                uc.Dock = DockStyle.Fill;
                tp.Controls.Add(uc);
                tabControl1.TabPages.Add(tp);
                tabControl1.SelectedTab = tp;
            }
            else tabControl1.SelectedIndex = tab;
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            int tab = 0;
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                if (tabControl1.TabPages[i].Text == "Nhân viên")
                {
                    tab = i; break;
                }
            }
            if (tab == 0)
            {
                TabPage tp = new TabPage();
                tp.Text = "Nhân viên";
                ucNhanVien uc = new ucNhanVien();
                uc.Dock = DockStyle.Fill;
                tp.Controls.Add(uc);
                tabControl1.TabPages.Add(tp);
                tabControl1.SelectedTab = tp;
            }
            else tabControl1.SelectedIndex = tab;
        }

        private void btnXeMay_Click(object sender, EventArgs e)
        {
            int tab = 0;
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                if (tabControl1.TabPages[i].Text == "Xe máy")
                {
                    tab = i; break;
                }
            }
            if (tab == 0)
            {
                TabPage tp = new TabPage();
                tp.Text = "Xe máy";
                ucXeMay uc = new ucXeMay();
                uc.Dock = DockStyle.Fill;
                tp.Controls.Add(uc);
                tabControl1.TabPages.Add(tp);
                tabControl1.SelectedTab = tp;
            }
            else tabControl1.SelectedIndex = tab;
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            int tab = 0;
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                if (tabControl1.TabPages[i].Text == "Hóa đơn")
                {
                    tab = i; break;
                }
            }
            if (tab == 0)
            {
                TabPage tp = new TabPage();
                tp.Text = "Hóa đơn";
                ucHoaDon uc = new ucHoaDon();
                uc.Dock = DockStyle.Fill;
                tp.Controls.Add(uc);
                tabControl1.TabPages.Add(tp);
                tabControl1.SelectedTab = tp;
            }
            else tabControl1.SelectedIndex = tab;
        }


 

    }
}
