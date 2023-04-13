using cnpmNC.Models;
using cnpmNC.Models.mapTaiKhoan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cnpmNC.Controllers
{
    public class TaiKhoanController : Controller
    {

        // GET: TaiKhoan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DanhSachTaiKhoan()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var dstk = db.TaiKhoans.ToList();
            return View(dstk);
        }
        public ActionResult ThemTaiKhoan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemTaiKhoan(TaiKhoan model)
        {
            cnpmNCEntities db = new cnpmNCEntities();

            var check = db.TaiKhoans.FirstOrDefault(s => s.TenTK == model.TenTK);
            if (check == null)
            {
                if (model.A_MatKhau == model.MatKhau)
                {
                    model.MatKhau = mapTaiKhoan.GetMD5(model.MatKhau);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.TaiKhoans.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("DanhSachTaiKhoan");
                }
                else
                {
                    ViewBag.error = "Xác nhận mật khẩu không khớp";
                    return View();
                }

            }
            else
            {
                ViewBag.error = "Tài khoản đã tồn tại";
                return View();
            }
        }
        public ActionResult CapNhatTaiKhoan(String TenTK)
        {
            var map = new mapTaiKhoan();
            var TKEdit = map.TimTaiKhoan(TenTK);
            return View(TKEdit);
        }

        [HttpPost]
        public ActionResult CapNhatTaiKhoan(TaiKhoan model)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var find = new mapTaiKhoan().TimTaiKhoan(model.TenTK);

            var check = db.TaiKhoans.FirstOrDefault(s => s.TenTK == model.TenTK);
            if (check != null)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                check.HoTen = model.HoTen;
                check.CCCD = model.CCCD;
                check.SoDT = model.SoDT;
                db.SaveChanges();
                return RedirectToAction("DanhSachTaiKhoan");

            }
            else
            {
                return View(model);
            }
        }
        public ActionResult XoaTaiKhoan(String TenTK)
        {          
            mapTaiKhoan map = new mapTaiKhoan();
            map.XoaTK(TenTK);

            return RedirectToAction("DanhSachTaiKhoan");
        }
    }
}