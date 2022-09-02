using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nhom5_ShopBanDoTrangSuc.Models;

namespace Nhom5_ShopBanDoTrangSuc.Models
{
    public class Item
    {
        BanTrangSucClasses1DataContext db = new BanTrangSucClasses1DataContext();
        public int masp { get; set; }
        public string tensp { get; set; }
        public int gia { get; set; }
        public string hinhanh { get; set; }
        public DateTime ngaydang { get; set; }
        public string mota { get; set; }
        public int soluong { get; set; }
        public int thanhtien { get { return gia * soluong; } }

        public Item(int matrangsuc)
        {
            masp = matrangsuc;
            TRANGSUC sp = db.TRANGSUCs.FirstOrDefault(t => t.MATRANGSUC == masp);
            tensp = sp.TENTRANGSUC;
            gia = (int)sp.GIA;
            hinhanh = sp.HINHANH;
            ngaydang = (DateTime)sp.NGAYDANG;
            mota = sp.MOTA;
            soluong = 1;
        }
    }
}