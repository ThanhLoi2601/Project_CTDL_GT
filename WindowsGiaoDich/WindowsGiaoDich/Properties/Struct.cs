namespace WindowsGiaoDich
{
    public class ID
    {           //Luu cac Hash cần để mã hóa 1 giao dịch
        public string[] lHash { get; set; }
        public int n { get; set; }
        public ID()
        {
            this.lHash=new string[100];
        }
    }

    public class  GiaoDich
    {
        public string MaGD { get; set; }
        public string TenKhach { get; set; }
        //	string NhanVienPV[50];
        //	string NoiDung[100];
        public long SoTien { get; set; }
        public ID HashXD { get; set; }

        public GiaoDich()
        {
            this.HashXD = new ID();
        }

        public void CapNhat(long t)
        {
            this.SoTien = t;
        }
    }

    public class ListGD
    {
        public GiaoDich[] A { get; set; }
        public int n { get; set; }
        public ListGD ()
        {
            this.A = new GiaoDich[100];
            for (int i = 0; i < 100; i++)
            {
                this.A[i] = new GiaoDich();
            }
            this.n = 0;
        }

        public ListGD(int n)
        {
            this.A = new GiaoDich[100];
            for (int i = 0; i < 100; i++) {
                this.A[i] = new GiaoDich();
            }
            this.n = n;
        }

    }
}
