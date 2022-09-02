using Nhom5_ShopBanDoTrangSuc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Nhom5_ShopBanDoTrangSuc.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        BanTrangSucClasses1DataContext db = new BanTrangSucClasses1DataContext();
        public ActionResult Index()
        {
            TAIKHOAN tk = Session["ss_user"] as TAIKHOAN;
            if (tk == null || tk.PHAN_QUYEN != 1)
                return RedirectToAction("Index", "Home");
            return View();
        }

        public ActionResult sp()
        {
            List<TRANGSUC> lstts = db.TRANGSUCs.ToList();
            return View(lstts);
        }

        public ActionResult Chitiet(string masp)
        {
            TRANGSUC sp = db.TRANGSUCs.FirstOrDefault(t => t.MATRANGSUC == int.Parse(masp));
            return View(sp);
        }

        public ActionResult SanPham(int? page)
        {
            List<TRANGSUC> lstsp = db.TRANGSUCs.ToList();
            //return View(lstsp);
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var links = (from l in db.TRANGSUCs
                         select l).OrderBy(x => x.MATRANGSUC);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 5;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(links.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult themSP()
        {
            List<THELOAI> lstLoai = db.THELOAIs.ToList();
            return View(lstLoai);
        }

        

        public ActionResult xLyThemSP(FormCollection fc, TRANGSUC sp)
        {
            
            
            //sp.MALOAI = Int32.Parse(fc["maloai"]);
            sp.TENTRANGSUC = fc["tensp"];
            sp.GIA = int.Parse(fc["gia"]);
            sp.HINHANH = fc["hinhanh"];
            sp.NGAYDANG = DateTime.Parse(fc["ngaydang"]);
            sp.MOTA = fc["mota"];

            db.TRANGSUCs.InsertOnSubmit(sp);
            db.SubmitChanges();
            return RedirectToAction("SanPham");
        }

        public ActionResult suaSP(int masp)
        {
            TRANGSUC sp = db.TRANGSUCs.FirstOrDefault(t => t.MATRANGSUC == masp);
            
            return View(sp);
        }

        public ActionResult XlySuaSP(FormCollection fc, int masp)
        {
            TRANGSUC sp = db.TRANGSUCs.FirstOrDefault(t => t.MATRANGSUC == masp);
            //sp.MALOAI = int.Parse(fc["maloai"]);
            sp.TENTRANGSUC = fc["tensp"];
            sp.GIA = int.Parse(fc["gia"]);
            sp.HINHANH = fc["hinhanh"];
            sp.NGAYDANG = DateTime.Parse(fc["ngaydang"]);
            sp.MOTA = fc["mota"];
            db.SubmitChanges();
            return RedirectToAction("SanPham");

        }

        public ActionResult xoaSP(int masp)
        {
            TRANGSUC sp = db.TRANGSUCs.FirstOrDefault(t => t.MATRANGSUC == masp);
            if (sp != null)
            {
                db.TRANGSUCs.DeleteOnSubmit(sp);
                db.SubmitChanges();
                return RedirectToAction("SanPham");
            }
            return RedirectToAction("SanPham");
        }

        public ActionResult TaiKhoan()
        {
            TAIKHOAN tk = Session["ss_user"] as TAIKHOAN;
            if (tk == null || tk.PHAN_QUYEN != 1)
                return RedirectToAction("Index", "Home");
            List<TAIKHOAN> lsttk = db.TAIKHOANs.Where(t => t.PHAN_QUYEN == 1).ToList();
            return View(lsttk);
        }

        public ActionResult xoaTaiKhoan(string matk)
        {
            TAIKHOAN tk = db.TAIKHOANs.FirstOrDefault(t => t.UNAME == matk);
            if (tk != null)
            {
                db.TAIKHOANs.DeleteOnSubmit(tk);
                db.SubmitChanges();
                return RedirectToAction("TaiKhoan");
            }
            return RedirectToAction("TaiKhoan");
        }

        public ActionResult themTK()
        {
            return View();
        }

        public ActionResult XlyThemTK(FormCollection fc, TAIKHOAN tk)
        {
            tk.UNAME = fc["username"];
            tk.PASS = fc["pass"];
            tk.FULL_NAME = fc["fullname"];
            tk.EMAIL_ADDRESS = fc["email"];
            tk.PHAN_QUYEN = byte.Parse(fc["phanquyen"]);

            db.TAIKHOANs.InsertOnSubmit(tk);
            db.SubmitChanges();
            return RedirectToAction("TaiKhoan");
        }

        public ActionResult suaTK(string matk)
        {
            TAIKHOAN tk = db.TAIKHOANs.FirstOrDefault(t => t.UNAME == matk);
            return View(tk);
        }

        public ActionResult xLySuaTK(FormCollection fc, string matk)
        {
            TAIKHOAN tk = db.TAIKHOANs.FirstOrDefault(t => t.UNAME == matk);

            //tk.UNAME = fc["username"];
            tk.PASS = fc["pass"];
            tk.FULL_NAME = fc["fullname"];
            tk.EMAIL_ADDRESS = fc["email"];
            tk.PHAN_QUYEN = byte.Parse(fc["phanquyen"]);

            db.SubmitChanges();
            return RedirectToAction("TaiKhoan");
        }

        public ActionResult Loai()
        {
            List<THELOAI> lstLoai = db.THELOAIs.ToList();
            return View(lstLoai);
        }

        public ActionResult themLoai()
        {
            return View();
        }

        public ActionResult xLyThemLoai(FormCollection fc, THELOAI tl)
        {
            tl.TENLOAI = fc["tenloai"];
            tl.ICON_THELOAI = fc["icon"];

            db.THELOAIs.InsertOnSubmit(tl);
            db.SubmitChanges();
            return RedirectToAction("Loai");
        }

        public ActionResult xoaLoai(int maloai)
        {
            THELOAI l = db.THELOAIs.FirstOrDefault(t => t.MALOAI == maloai);
            if (l != null)
            {
                db.THELOAIs.DeleteOnSubmit(l);
                db.SubmitChanges();
                return RedirectToAction("Loai");
            }
            return RedirectToAction("Loai");
        }

        public ActionResult suaLoai(int maloai)
        {
            THELOAI l = db.THELOAIs.FirstOrDefault(t => t.MALOAI == maloai);
            return View(l);
        }

        public ActionResult XlySuaLoai(FormCollection fc, int maloai)
        {
            THELOAI l = db.THELOAIs.FirstOrDefault(t => t.MALOAI == maloai);
            l.TENLOAI = fc["tenloai"];
            l.ICON_THELOAI = fc["icon"];
            
            db.SubmitChanges();
            return RedirectToAction("Loai");

        }

        public ActionResult HoaDon()
        {
            List<HOADON> lsthd = db.HOADONs.ToList();
            return View(lsthd);
        }

        //public ActionResult xoaHD(int mahd)
        //{
        //    HOADON hd = db.HOADONs.FirstOrDefault(t => t.MAHD == mahd);
        //    if (hd != null)
        //    {
        //        List<CHITIETHD> cthd = db.CHITIETHDs.Where(ct => ct.MAHD == mahd).ToList();
        //        db.CHITIETHDs.DeleteAllOnSubmit(cthd);
        //        db.HOADONs.DeleteOnSubmit(hd);
        //        db.SubmitChanges();
        //        return RedirectToAction("HoaDon");
        //    }
        //    return RedirectToAction("HoaDon");
        //}

        public ActionResult suaHD(int mahd)
        {
            HOADON hd = db.HOADONs.FirstOrDefault(t => (t.MAHD == mahd));
            return View(hd);
        }

        public ActionResult xLySuaHD(FormCollection fc, int mahd)
        {
            HOADON hd = db.HOADONs.FirstOrDefault(t => (t.MAHD == mahd));


            hd.TINHTRANG = fc["tinhtrang"];
            hd.NGAYGIAO = DateTime.Parse(fc["ngaygiao"]);
            db.SubmitChanges();
            return RedirectToAction("HoaDon");
        }

        public ActionResult chitietHD(int mahd)
        {
            List<CHITIETHD> cthd = db.CHITIETHDs.Where(t => t.MAHD == mahd).ToList();
            return View(cthd);
        }

        //public ActionResult xoaCTHD(int mahd)
        //{
        //    CHITIETHD cthd = db.CHITIETHDs.FirstOrDefault(ct => ct.MAHD == mahd);
        //    if (cthd != null)
        //    {
                
        //        db.CHITIETHDs.DeleteOnSubmit(cthd);
        //        return RedirectToAction("chitietHD");
        //    }
        //    return RedirectToAction("chitietHD");
        //}

        public ActionResult KhachHang()
        {
            List<TAIKHOAN> lsttk = db.TAIKHOANs.Where(t => t.PHAN_QUYEN == 0).ToList();
            return View(lsttk);
        }

        public ActionResult suaTKKH(string matk)
        {
            TAIKHOAN tk = db.TAIKHOANs.FirstOrDefault(t => t.UNAME == matk);
            return View(tk);
        }

        public ActionResult xLySuaTKKH(FormCollection fc, string matk)
        {
            TAIKHOAN tk = db.TAIKHOANs.FirstOrDefault(t => t.UNAME == matk);

            //tk.UNAME = fc["username"];
            tk.PASS = fc["pass"];
            tk.FULL_NAME = fc["fullname"];
            tk.EMAIL_ADDRESS = fc["email"];
            tk.PHAN_QUYEN = byte.Parse(fc["phanquyen"]);

            db.SubmitChanges();
            return RedirectToAction("TaiKhoan");
        }
    }
}
