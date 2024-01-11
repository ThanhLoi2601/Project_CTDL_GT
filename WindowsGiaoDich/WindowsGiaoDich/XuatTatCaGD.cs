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
    public partial class XuatTatCaGD : Form
    {
        public XuatTatCaGD()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if(textBox1.Text!="12345") //Mật khẩu là 12345;
            {
                listBox1.Items.Add("Nhập mật khẩu sai, vui lòng nhập lại !!");
            }else
            {
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
                for(int i=0; i<l.n; i++)
                {
                    
                    string m = "Mã Giao dịch: " + l.A[i].MaGD ;
                    listBox1.Items.Add(m);
                    m = "Tên khách: " + l.A[i].TenKhach ;
                    listBox1.Items.Add(m);
                    m = "Số tiền: " + l.A[i].SoTien;
                    listBox1.Items.Add(m);
                    m = "ID: ";
                    for (int j=0; j < l.A[i].HashXD.n; j++)
                    {
                        m += l.A[i].HashXD.lHash[j]+ "_";
                    }
                    listBox1.Items.Add(m);
                    listBox1.Items.Add("");
                }
                
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
