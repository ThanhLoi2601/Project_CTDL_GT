using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsGiaoDich
{
    public partial class XacThucGD : Form
    {
        public XacThucGD()
        {
            InitializeComponent();
        }

        private void XacThucGD_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MTree T = new MTree();
            ListGD l = new ListGD(0);
            XuLyGD.TaoDuLieu(ref l);
            MTree[] LLeaf = new MTree[100];
            for (int i = 0; i < l.n; i++)
            {
                string h = Hash.Hash1(l.A[i]);
                LLeaf[i] = new MTree(h,null,null);
            }
            T = MTree.TaoRoot(LLeaf, l.n);

            XuLyGD.TaoID(ref l, 0, l.n - 1, T);
            XuLyGD.sapxepID(ref l);

            GiaoDich x = new GiaoDich();
            x.MaGD = textBox1.Text;
            x.TenKhach = textBox2.Text;
            x.SoTien = long.Parse(textBox3.Text);
            int vt1 = 0;
            x.HashXD.n = 0;
            char[] lH = textBox4.Text.ToCharArray();
            for (int vt2 = 0; vt2 < lH.Length; vt2++)
            {
                if (lH[vt2] == '_')
                {
                    int k = x.HashXD.n;
                    string str = textBox4.Text.Substring(vt1, vt2-vt1);
                    x.HashXD.lHash[k] = str;
                    x.HashXD.n++;
                    vt1 = vt2 + 1;
                }
            }

            if (XuLyGD.XacThuc(x, T.hash))
                MessageBox.Show("Giao dịch đã được thực hiện !!");
            else
                MessageBox.Show("Giao dich chưa được thực hiện! Hãy kiểm tra lại thông tin!");
        }

        private void XacThucGD_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}
