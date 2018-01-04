using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
namespace QuanLyXeMay_KTLTNC
{
    class KetNoi
    {
        public static OleDbCommand cmd;
        public static OleDbConnection con = new OleDbConnection();
        public static OleDbDataAdapter da;

        public static void MoKetNoi()
        {
            con.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=quanlybanXeMay.mdb";
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
        }

        public static void DongKetNoi()
        {
            con.Close();
        }

        public static DataTable table(string chuoiTruyVan)
        {
            MoKetNoi();
            DataTable dt = new DataTable();
            cmd = new OleDbCommand(chuoiTruyVan, con);
            da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            DongKetNoi();
            return dt;
        }

        public static void CapNhat(string chuoiTruyVan)
        {
            MoKetNoi();
            cmd = new OleDbCommand(chuoiTruyVan, con);
            cmd.ExecuteNonQuery();
            DongKetNoi();
        }

        public static string GiaTri(string chuoiTruyVan)
        {
            MoKetNoi();
            cmd = new OleDbCommand(chuoiTruyVan, con);
            var value = cmd.ExecuteScalar();
            DongKetNoi();
            return value.ToString().Trim();
        }

        public static string MaTuTang(string chuoiMaCoDinh, DataTable dt)
        {
            var hashset = new HashSet<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                hashset.Add(Convert.ToInt32(dt.Rows[i][0].ToString().Remove(0, chuoiMaCoDinh.Length)));
            }

            for (int i = 0; i < hashset.Count; i++)
            {
                if (!hashset.Contains(i + 1)) return i + 1 < 10 ? string.Format("{0}00{1}", chuoiMaCoDinh, ++i) : 
                    i + 1 < 100 ? string.Format("{0}0{1}", chuoiMaCoDinh, ++i) : chuoiMaCoDinh + ++i;
            }
            return hashset.Count + 1 < 10 ? string.Format("{0}00{1}", chuoiMaCoDinh, (hashset.Count + 1)) : 
                hashset.Count + 1 < 100 ? string.Format("{0}0{1}", chuoiMaCoDinh, (hashset.Count + 1)) : 
                chuoiMaCoDinh + (hashset.Count + 1);
        }

    }
}
