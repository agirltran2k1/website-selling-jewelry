using Nhom5_ShopBanDoTrangSuc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom5_ShopBanDoTrangSuc.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        BanTrangSucClasses1DataContext db = new BanTrangSucClasses1DataContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult XlyDangKy(FormCollection fc, TAIKHOAN tk)
        {
           
            tk.UNAME = fc["username"];
            tk.PASS = fc["pass"];
            tk.FULL_NAME = fc["fullname"];
            tk.EMAIL_ADDRESS = fc["email"];
            tk.PHAN_QUYEN = 0;
            var check = db.TAIKHOANs.FirstOrDefault(s => s.UNAME == tk.UNAME);
            if (check == null)
            {
                db.TAIKHOANs.InsertOnSubmit(tk);
                db.SubmitChanges();
                return RedirectToAction("DangNhap");
            }
            else
            {

                    ViewBag.error = "Tài khoản đã tồn tại";
                    return RedirectToAction("Index");
            }

        }

        public ActionResult DangNhap()
        {
            return View();
        }

        public ActionResult XlyDangNhap(FormCollection fc)
        {
            string tendangnhap = fc["taikhoan"];
            string matkhau = fc["matkhau"];
            TAIKHOAN tk = db.TAIKHOANs.FirstOrDefault(t => t.UNAME == tendangnhap && t.PASS == matkhau);
            if (tk == null)
                return RedirectToAction("DangNhap");
            else
            {
                Session["ss_user"] = tk;
                if (tk.PHAN_QUYEN == 1)
                    return RedirectToAction("Index", "Admin");
                else
                    return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult dangXuat()
        {
            Session["ss_user"] = null;
            return RedirectToAction("Index", "Home");
        }

    }
}
