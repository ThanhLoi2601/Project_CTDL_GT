using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsGiaoDich
{
    static class Hash
    {   
        public static string Hash1(GiaoDich x)
        {
            string s = String.Concat(x.MaGD, x.TenKhach);
            char[] s1 = s.ToCharArray();
	        for (int i = 0; i < s1.Length; i++)
	        {
                long t = (26 * s1[i] + x.SoTien) % 96 + 32;
                s1[i] = (char)t ;
	    	    if (s1[i] == '_') // '_' để ngăn cắt các hash khác nhau trong ID
	    		    s1[i]++;
	        }
            string l= s1[0].ToString();
            for(int i=1; i<s1.Length; i++)
            {
                l= l + s1[i].ToString();
            }
            return l;
        }

        public static string Hash2(string s1, string s2)
        {
            int n = Math.Max(s1.Length, s2.Length);
            int i = s1.Length;
            while (i < n)
            {
                s1=s1+((char)(('0' + i) % 96 + 32));
                i++;
            }
            char[] kq1 = new char[100];
            kq1 = s1.ToCharArray();
            i = s2.Length;
            while (i < n)
            {
                s2 = s2 + ((char)(('0' + i) % 96 + 32));
                i++;
            }
            char[] kq2 = s2.ToCharArray();
            
            char[] kq = kq1;
            char t= (char)((kq1[kq1.Length - 1] + kq2[kq2.Length - 1]) % 96 + 32);
            for (i = 0; i < n; i++)
            {
                kq[i] = (char)((28 * (kq[i] + kq2[i]) + 97) % 96 + 32);
                if (kq[i] == '_')
                    kq[i]++;
            }

            string l = kq[0].ToString();
            for (int j = 1; j < kq.Length; j++)
            {
                l += kq[j].ToString();
            }
            return l + t.ToString();
        }

    }
}
