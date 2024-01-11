using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsGiaoDich
{
    class XuLyGD
    {
        //Tạo dữ liệu
        static public void TaoDuLieu(ref ListGD l)
        {
            l.n = 6;
            string s1 = "0000";
            char[] a = s1.ToCharArray();
            string s2 = "Nguyen Van A";
            char[] b = s2.ToCharArray();
            long tien = 200000;

            for (int i = 0; i < l.n; i++)
            {
                a[3]++;
                if (a[3] > '9')
                {
                    char t = (char)(a[3] - '0');
                    a[3] = (char)(t % 10 + '0');
                    a[2] += (char)(t / 10);
                    if (a[2] > '9')
                    {
                        char t1 = (char)(a[2] - '0');
                        a[2] = (char)(t1 % 10 + '0');
                        a[1] += (char)(t1 / 10);
                        if (a[1] > '9')
                        {
                            char t2 = (char)(s1[1] - '0');
                            a[1] = (char)(t2 % 10 + '0');
                            a[0] += (char)(t2 / 10);
                        }
                    }
                }
                for (int j = 0; j < a.Length; j++)
                    l.A[i].MaGD = l.A[i].MaGD + a[j];
                b[b.Length - 1]++;
                for (int j = 0; j < b.Length; j++)
                    l.A[i].TenKhach = l.A[i].TenKhach + b[j];

                l.A[i].CapNhat(50000 + (tien * i));
                l.A[i].HashXD.n = 0;
            }
            //l.A[0].MaGD = "0001"; l.A[0].TenKhach = "Nguyen Van B";
            //l.A[1].MaGD = "0002"; l.A[1].TenKhach = "Nguyen Van C";
            //l.A[2].MaGD = "0003"; l.A[2].TenKhach = "Nguyen Van D";
            //l.A[3].MaGD = "0004"; l.A[3].TenKhach = "Nguyen Van E";
            //l.A[4].MaGD = "0005"; l.A[4].TenKhach = "Nguyen Van F";
            //l.A[5].MaGD = "0006"; l.A[5].TenKhach = "Nguyen Van G";
        }

        //Tạo ID
        static public void TaoID(ref ListGD l, int start, int end, MTree T)
        {
            if (T.right.right == null && T.left.left != null)
            {
                for (int i = start; i <= end; i++)
                {
                    int k = l.A[i].HashXD.n;
                    l.A[i].HashXD.lHash[k]=T.right.hash;
                    l.A[i].HashXD.n++;
                }
                TaoID(ref l, start, end, T.left);
            }
            else
            if (T.left.left == null && T.right.right == null)
            {
                l.A[start].HashXD.lHash[l.A[start].HashXD.n++]=T.right.hash;
                l.A[end].HashXD.lHash[l.A[end].HashXD.n++]=T.left.hash;
                return;
            }
            else
            {
                int m = (end - start + 1) / 2;
                if (m % 2 == 1) m++;
                m += start;
                for (int i = start; i < m; i++)
                {
                    int k = l.A[i].HashXD.n;
                    l.A[i].HashXD.lHash[k]=T.right.hash;
                    l.A[i].HashXD.n++;
                }
                TaoID(ref l, m, end, T.right);
                for (int i = m; i <= end; i++)
                {
                    int k = l.A[i].HashXD.n;
                    l.A[i].HashXD.lHash[k]=T.left.hash;
                    l.A[i].HashXD.n++;
                }
                TaoID(ref l, start, m - 1, T.left);
            }
        }

        //sắp xếp các Hash theo thứ tự sử dụng
        static public void sapxepID(ref ListGD l)
        {
            for (int i = 0; i <= l.n; i++)
                for (int j = 0; j < l.A[i].HashXD.n- 1; j++) //sắp xếp theo chiều dài tăng dần //sử dụng thuật toán sắp xếp nổi bọt
                {
                    for (int k = l.A[i].HashXD.n - 1; k > j; k--)
                        if (l.A[i].HashXD.lHash[k].Length < l.A[i].HashXD.lHash[k - 1].Length)
                            XuLyGD.hoanvi(ref l.A[i].HashXD.lHash[k], ref l.A[i].HashXD.lHash[k- 1]);
                }
        }

        static public void hoanvi(ref string a, ref string b)
        {
            string t = a;
            a = b;
            b = t;
        }

        //Xác thực giao dịch
        static public bool XacThuc(GiaoDich x, string HashRoot)
        {
            string kq;
            kq=Hash.Hash1(x);
            for (int i = 0; i < x.HashXD.n; i++)
                kq=Hash.Hash2(kq, x.HashXD.lHash[i]);
            //cout << kq;
            return kq == HashRoot;
        }

        //Tìm kiếm ID trong ListGD
        // Không thể tìm bằng họ tên vì khóa chính là MaGD
        //Mã giao dịch được tạo theo thứ tự tăng dần nên tìm theo thuật toán tìm kiếm nhị phân
        static public ID TimKiemID(ListGD l, int start, int end, int iMaGD)
        {
            if (start > end)
            {
                ID noExist=new ID();
                noExist.n = -1;
                noExist.lHash = null;
                return noExist;
            }
            int i = start + (end - start) / 2;
            int y = int.Parse(l.A[i].MaGD);
            if (iMaGD == y)
                return l.A[i].HashXD;
            else if (iMaGD < y)
                return TimKiemID(l, start, i - 1, iMaGD);
            else
                return TimKiemID(l, i + 1, end, iMaGD);

        }
    }
}
