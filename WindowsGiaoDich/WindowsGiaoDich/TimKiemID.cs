using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsGiaoDich
{
    public partial class TimKiemID : Form
    {
        public TimKiemID()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void TimKiemID_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n;
            if (int.TryParse(textBox1.Text, out n) == false)
            {
                textBox2.Clear();
                textBox2.Text="Không tìm thấy, vui lòng nhập lại";
                return;
            }
            MTree T = new MTree();
            ListGD l = new ListGD(0);
            XuLyGD.TaoDuLieu(ref l);
            MTree[] LLeaf = new MTree[100];
            for (int i = 0; i < l.n; i++)
            {
                string h = Hash.Hash1(l.A[i]);
                LLeaf[i] = new MTree(h, null, null);
            }
            T = MTree.TaoRoot(LLeaf, l.n);

            XuLyGD.TaoID(ref l, 0, l.n - 1, T);
            XuLyGD.sapxepID(ref l);
            ID x = XuLyGD.TimKiemID(l, 0, l.n - 1, n);
            if (x.n < 0)
            {
                textBox2.Clear();
                textBox2.Text="Không tìm thấy, vui lòng nhập lại";
            }
            else
            {
                string t = x.lHash[0] + "_";
                for (int i = 1; i < x.n; i++)
                    t += x.lHash[i] + "_";
                textBox2.Clear();
                textBox2.Text = t;
            }
        }

        private void textBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
        }
    }
}
