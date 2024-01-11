#include<iostream>
#include<string>
#include<math.h>
using namespace std;

struct Node {
	string Hash;
	Node* left;
	Node* right;
};

typedef Node* MTree;

struct ID {			//Luu cac Hash cần để mã hóa 1 giao dịch
	string lHash[50];
	int n;
};

struct GiaoDich {
	string MaGD;
	string TenKhach;
	//	string NhanVienPV[50];
	//	string NoiDung[100];
	long SoTien;
	ID HashXD;
};

struct ListGD {
	GiaoDich A[100];
	int n;
};

Node* TaoBranch(Node* x, Node* y);
Node* TaoLeaf(GiaoDich x);
void Hash1(string& s, GiaoDich x);
void Hash2(string s1, string s2, string& kq);
void TaoDuLieu(ListGD& l);
Node* TaoRoot(Node* LLeaf[], int n);
void xuatMTree(MTree T);
void TaoID(ListGD& l, int start, int end, MTree T);
void sapxepID(ListGD& l);
void sapxep(ID& l);
void hoanvi(string& a, string& b);
void NhapGD(GiaoDich& x);
void XuatGD(GiaoDich x);
bool XacThuc(GiaoDich x, string HashRoot);
int strMaGDToIn(string MaGD);
ID TimKiemID(ListGD l, int start, int end, int MaGD);
void Menu(ListGD l, MTree T);

int main()
{
	ListGD l;
	TaoDuLieu(l); // có thể sử dụng đọc file thay cho hàm này
	//Chuyển hóa dữ liệu thành các Hash và lưu trữ trên Merkle Tree
	Node* LLeaf[100];
	for (int i = 0; i < l.n; i++)
		LLeaf[i] = TaoLeaf(l.A[i]);
	MTree T = TaoRoot(LLeaf, l.n);
	//Xuất cây MTree
	//xuatMTree(T);
	//Tạo ID cho các dịch giao dịch
	TaoID(l, 0,l.n-1,T);
	sapxepID(l);
	Menu(l, T);


	return 0;
}

//Menu Chon va xuat ket qua
void Menu(ListGD l, MTree T)
{
	int bc = 0; //trở về bảng chon bc=0, không thì bc=1;
	int c;
	while (bc == 0)
	{
		cout << "Chon chuc nang: \n";
		cout << "1.Xac thuc giao dich\n";
		cout << "2.Tim kiem ID\n";
		cout << "3.Xuat tat ca giao dich da thuc hien\n";
		cout << "=>Chon: ";
		cin >> c;
		bool t = true;
		if (c == 3)
		{
			//Xuất ra tất cả giao dịch đã thực hiện
			for (int i = 0; i < l.n; i++)
				XuatGD(l.A[i]);
		}
		else if (c == 1)
		{
			//Xác thực giao dịch đã được nhập vào
			GiaoDich x;
			do
			{
				NhapGD(x);
				/*cout << x.MaGD << " " << x.TenKhach << " " << x.SoTien << endl;
				for (int i = 0; i < x.HashXD.n; i++)
				{
					cout << x.HashXD.lHash[i] << endl;
				}*/

				if (XacThuc(x, T->Hash) == true)
					cout << "Giao dich da duoc thuc hien !!\n";
				else
				{
					cout << "Giao dich chua duoc thuc hien! Hay kiem tra lai thuong tin!\n";
					int e;
					cout << "1. Nhap lai.\n";
					cout << "2.Thoat xac thuc.\n";
					cout << "=>Chon: ";
					cin >> e;
					if (e == 1)
						t = false;
					else
						t = true;
				}
			} while (t == false);
		}
		else if (c == 2)
		{
			//Tìm kiếm ID 
			do
			{
				string sMaGD;
				cout << "\nNhap Ma giao dich: ";
				cin >> sMaGD;
				int iMaGD = strMaGDToIn(sMaGD);
				ID x = TimKiemID(l, 0, l.n - 1, iMaGD);
				if (x.n > 0)
				{
					cout << "ID: ";
					for (int j = 0; j < x.n; j++)
					{
						cout << x.lHash[j] << "_";
					}
					cout << endl;
				}
				else
				{
					cout << "\nNhap Ma giao dich sai!\n";
					int e;
					cout << "1. Nhap lai.\n";
					cout << "2.Thoat Tim kiem ID.\n";
					cout << "=>Chon: ";
					cin >> e;
					if (e == 1)
						t = false;
					else
						t = true;
				}
			} while (t == false);
		}
		else
			cout << "LOI!!!" << endl << endl;
		cout << "\n0.Tro ve bang chon.\n";
		cout << "1.Thoat toan bo.\n";
		cout << "=>Chon: ";
		cin >> bc;
		cout << endl;
		if (bc != 1 && bc != 0)
		{
			cout << "LOI!\n";
			break;
		}
	}
}

//Tìm kiếm ID trong ListGD
// Không thể tìm bằng họ tên vì khóa chính là MaGD
//Mã giao dịch được tạo theo thứ tự tăng dần nên tìm theo thuật toán tìm kiếm nhị phân
ID TimKiemID(ListGD l, int start, int end, int iMaGD)  
{
	if (start > end)
	{
		ID noExist;
		noExist.n = -1;
		return noExist;
	}
	int i = start+(end-start) / 2;
	int y = strMaGDToIn(l.A[i].MaGD);
	if (iMaGD == y)
		return l.A[i].HashXD;
	else if (iMaGD < y)
		return TimKiemID(l, start, i - 1, iMaGD);
	else 
		return TimKiemID(l, i+1, end, iMaGD);
}

int strMaGDToIn(string MaGD)
{
	int tong = 0;
	int n = MaGD.length();
	for (int i = 0; i < n; i++)
	{
		int x = (MaGD[i] - '0');
		if (x >= 10 || x < 0)
			return -1;
		tong += x * pow(10, n - 1 - i);
	}
	return tong;
}
//Xác thực giao dịch
bool XacThuc(GiaoDich x, string HashRoot)
{
	string kq;
	Hash1(kq, x);
	for (int i = 0; i < x.HashXD.n; i++)
		Hash2(kq, x.HashXD.lHash[i], kq);
	//cout << kq;
	return kq == HashRoot;
}

//Xuất giao dịch
void XuatGD(GiaoDich x)
{
	cout << endl;
	cout << "Ma giao dich: " << x.MaGD << endl;
	cout << "Ten khach hang: " << x.TenKhach << endl;
	cout << "So tien giao dich: " << x.SoTien << " VND" << endl;
	cout << "ID: ";
	for (int j = 0; j < x.HashXD.n; j++)
	{
		cout << x.HashXD.lHash[j] << "_";
	}
	cout << endl;
}

//Nhập giao dịch 
void NhapGD(GiaoDich& x)
{
	cout << "Nhap Ma giao dich: ";
	cin >> x.MaGD;
	cout << "Nhap Ten khach: ";
	cin.ignore();
	getline(std::cin, x.TenKhach);
	cout << "Nhap So tien giao dich: ";
	cin >> x.SoTien;
	cin.ignore();
	string lHash;
	cout << "Nhap Hash XD: ";
	getline(std::cin, lHash);
//	cout << lHash;
	int vt1 = 0;
	x.HashXD.n = 0;
	for (int vt2 = 0; vt2 < lHash.length(); vt2++)
	{
		if (lHash[vt2] == '_')
		{
			int k = x.HashXD.n;
			x.HashXD.lHash[k].append(lHash.substr(vt1, vt2-vt1));
			x.HashXD.n++;
			vt1 = vt2 + 1;
		}
	}
}

//sắp xếp các Hash theo thứ tự sử dụng
void sapxepID(ListGD& l)
{
	for (int i = 0; i <= l.n; i++)
		sapxep(l.A[i].HashXD);
}

void sapxep(ID& l) //sắp xếp theo chiều dài tăng dần //sử dụng thuật toán sắp xếp nổi bọt
{
	for (int i =0; i<l.n-1; i++)
	{
		for (int j = l.n-1; j > i; j--)
			if (l.lHash[j].length() < l.lHash[j - 1].length())
				hoanvi(l.lHash[j], l.lHash[j - 1]);
	}
}

void hoanvi(string& a, string& b)
{
	string t = a;
	a = b; 
	b = t;
}

//Tạo ID
void TaoID(ListGD& l, int start, int end, MTree T)
{
	if (T->right->right == NULL && T->left->left != NULL)
	{
		for (int i = start; i <= end; i++)
		{
			int k = l.A[i].HashXD.n;
			l.A[i].HashXD.lHash[k].append(T->right->Hash);
			l.A[i].HashXD.n++;
		}
		TaoID(l, start, end, T->left);
	}
	else
	if (T->left->left== NULL && T->right->right == NULL) {
		l.A[start].HashXD.lHash[l.A[start].HashXD.n++].append(T->right->Hash);
		l.A[end].HashXD.lHash[l.A[end].HashXD.n++].append(T->left->Hash);
		return;
	}
	else
	{
		int m=(end-start+1)/2;
		if (m % 2 == 1)	m++;
		m += start;
		for (int i = start; i < m; i++)
		{
			int k = l.A[i].HashXD.n;
			l.A[i].HashXD.lHash[k].append(T->right->Hash);
			l.A[i].HashXD.n++;
		}
		TaoID(l, m, end, T->right);
		for (int i = m; i <= end; i++)
		{
			int k = l.A[i].HashXD.n;
			l.A[i].HashXD.lHash[k].append(T->left->Hash);
			l.A[i].HashXD.n++;
		}
		TaoID(l, start, m-1, T->left);
	}
}


//Tạo dữ liệu
void TaoDuLieu(ListGD& l)
{
	l.n = 6;
	string s1="0000";
	string s2 = "Nguyen Van A";
	long tien = 200000;
	for (int i = 0; i < l.n; i++)
	{
		s1[3]++;
		if (s1[3] > '9')
		{
			char t = s1[3]-'0';
			s1[3] = t % 10+'0';
			s1[2] += t / 10;
			if (s1[2] > '9')
			{
				char t1 = s1[2] - '0';
				s1[2] = t1 % 10 + '0';
				s1[1] += t1 / 10;
				if (s1[1] > '9')
				{
					char t2 = s1[1] - '0';
					s1[1] = t2 % 10 + '0';
					s1[0] += t2 / 10;
				}
			}
		}
		l.A[i].MaGD= s1;
		s2[s2.length() - 1]++;
		l.A[i].TenKhach= s2;
		l.A[i].SoTien = 50000 + tien * i;
		l.A[i].HashXD.n = 0;
	}

}

//Duyệt
void xuatMTree(MTree T)
{
	if (T == NULL) return;
	else
	{
		cout << T->Hash << endl;
		xuatMTree(T->left);
		xuatMTree(T->right);
	}
}

// Tạo Root
Node* TaoRoot(Node* LLeaf[], int n)
{
	if (n == 1) return LLeaf[0];
	else
	{
		Node* LBranch[100];
		int m = 0;
		if (n % 2 == 1)
		{
			LLeaf[n] = new Node();
			LLeaf[n]->Hash= LLeaf[n - 1]->Hash;
			LLeaf[n]->left = NULL;
			LLeaf[n]->right = NULL;
			n++;
		}
		for (int i = 0; i < n; i = i + 2)
		{
			LBranch[m++] = TaoBranch(LLeaf[i], LLeaf[i + 1]);
		}
		return TaoRoot(LBranch, m);
	}
}

// Tạo Branch
Node* TaoBranch(Node* x, Node* y)
{
	Node* n = new Node();
	n->left = x;
	n->right = y;
	Hash2(x->Hash, y->Hash, n->Hash);
	return n;
}

//Tạo leaf
Node* TaoLeaf(GiaoDich x)
{
	Node* n = new Node();
	Hash1(n->Hash, x);
	n->left = NULL;
	n->right = NULL;
	return n;
}

//Hash
void Hash1(string& s, GiaoDich x) // hóa đơn -> Hash(hóa đơn)
{
	s= x.MaGD + x.TenKhach;
	for (int i = 0; i < s.length(); i++)
	{
		s[i] = (26 * s[i] + x.SoTien) % 96 + 32;
		if (s[i] == '_') // '_' để ngăn cắt các hash khác nhau trong ID
			s[i]++;
	}
}

void Hash2(string s1, string s2, string& kq) // kq=Hash(s1,s2)
{
	//kq = s1 + s2;
	//for (int i = 0; i < kq.length(); i++)
	//{
	//	kq[i] = (28 * kq[i] + 97) % 96 + 32;
	//	if (kq[i] == '_')
	//		kq[i] ++;
	//}
	int n = max(s1.length(),s2.length());
	int i = s1.length();
	while (i < n)
	{
		s1.push_back( ('0' + i) % 96 + 32);
		i++;
	}

	i = s2.length();
	while (i < n)
	{
		s2.push_back(('0' + i) % 96 + 32);
		i++;
	}

	kq = s1;
	kq.push_back((s1[s1.length()-1] + s2[s2.length()-1]) % 96 + 32);
	for (int i = 0; i < n; i++)
	{
		kq[i] = (28 * (kq[i]+s2[i]) + 97) % 96 + 32;
		if (kq[i] == '_')
			kq[i] ++;
	}
}
