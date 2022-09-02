using Nhom5_ShopBanDoTrangSuc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Nhom5_ShopBanDoTrangSuc.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        BanTrangSucClasses1DataContext db = new BanTrangSucClasses1DataContext();
        public ActionResult Index()
        {
            TAIKHOAN tk = Session["ss_user"] as TAIKHOAN;
            if (tk != null && tk.PHAN_QUYEN == 1)
                return RedirectToAction("Index", "Admin");
            return View();
        }


        public ActionResult SanPham(int? page)
        {
        
            List<TRANGSUC> lstTS = db.TRANGSUCs.ToList();
            //return View(lstTS);
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var links = (from l in db.TRANGSUCs
                         select l).OrderBy(x => x.MATRANGSUC);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 10;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LoaiSP()
        {
            List<THELOAI> lstLoai = db.THELOAIs.ToList();
            return PartialView(lstLoai);
        }

        public ActionResult timSPtheoNHAN()
        {
            List<TRANGSUC> lstSP = db.TRANGSUCs.Where(t => t.MALOAI == 1).ToList();
            return View(lstSP);
        }

        public ActionResult timSPtheoBONGTAI()
        {
            List<TRANGSUC> lstSP = db.TRANGSUCs.Where(t => t.MALOAI == 2).ToList();
            return View(lstSP);
        }

        public ActionResult timSPtheoDAYCHUYEN()
        {
            List<TRANGSUC> lstSP = db.TRANGSUCs.Where(t => t.MALOAI == 3).ToList();
            return View(lstSP);
        }

        public ActionResult TimKiem(string searchString)
        {
            List<TRANGSUC> timkiemsp = db.TRANGSUCs.Where(s => s.TENTRANGSUC.Contains(searchString)).ToList();
            return View(timkiemsp);
        }


        public ActionResult Chitiet(string masp)
        {
            TRANGSUC sp = db.TRANGSUCs.FirstOrDefault(t => t.MATRANGSUC == int.Parse(masp));
            return View(sp);
        }

        public ActionResult TinhTrangXemHang()
        {
            TAIKHOAN tk = Session["ss_user"] as TAIKHOAN;
            List<HOADON> lstHD = db.HOADONs.Where(s => s.MATAIKHOAN == tk.UNAME).ToList();
            return View(lstHD);
        }

        public ActionResult xoaHD(int mahd)
        {
            HOADON hd = db.HOADONs.FirstOrDefault(t => (t.MAHD == mahd) && (t.TINHTRANG == "Chưa xử lý"));
            if (hd != null)
            {
                List<CHITIETHD> cthd = db.CHITIETHDs.Where(ct => ct.MAHD == mahd).ToList();
                db.CHITIETHDs.DeleteAllOnSubmit(cthd);
                db.HOADONs.DeleteOnSubmit(hd);
                db.SubmitChanges();
                return RedirectToAction("TinhTrangXemHang");
            }
            return RedirectToAction("TinhTrangXemHang");
        }

        public ActionResult chitietHD(int mahd)
        {
            List<CHITIETHD> cthd = db.CHITIETHDs.Where(t => t.MAHD == mahd).ToList();
            return View(cthd);
        }

        

    }
}
