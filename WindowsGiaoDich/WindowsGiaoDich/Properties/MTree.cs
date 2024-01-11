using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WindowsGiaoDich
{
    class MTree
    {
        public string hash;
        public MTree left;
        public MTree right;

        public MTree()
        {
            this.left = null;
            this.right = null;
        }
        public MTree(string hash1, MTree left, MTree right)
        {
            this.hash = hash1;
            this.left = left;
            this.right = right;
        }
        ~MTree()
        {
        }

        //Tạo leaf
        //public void TaoLeaf(GiaoDich x)
        //{
        //    string h = Hash.Hash1(x);
        //    this.left = null;
        //    this.right = null;
        //    this.hash = h;
           
        //}
        // Tạo Branch
        public static MTree TaoBranch(MTree x, MTree y)
        {
            MTree n = new MTree();
            n.left = x;
            n.right = y;
            n.hash=Hash.Hash2(x.hash, y.hash);
            return n;
        }

        //Tạo Root
        public static MTree TaoRoot(MTree[] LLeaf, int n)
        {
            if (n == 1) return LLeaf[0];
            else
            {
                
                if (n % 2 == 1)
                {
                    LLeaf[n] = new MTree();
                    LLeaf[n].hash = LLeaf[n - 1].hash;
                    LLeaf[n].left = null;
                    LLeaf[n].right = null;
                    n++;
                }
                int m = 0;
                MTree[] LBranch = new MTree[10];
                for (int i = 0; i < n; i = i + 2)
                {
                    LBranch[m++] = MTree.TaoBranch(LLeaf[i], LLeaf[i + 1]);
                }
                return TaoRoot(LBranch, m);
            }
        }

        //Duyệt
        public static void xuatMTree(MTree T)
        {
            if (T == null) return;
            else
            {
                Console.WriteLine(T.hash);
                xuatMTree(T.left);
                xuatMTree(T.right);
            }
        }
    }
}
