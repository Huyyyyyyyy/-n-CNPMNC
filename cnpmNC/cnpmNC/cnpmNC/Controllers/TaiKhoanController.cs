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
        cnpmNCEntities db = new cnpmNCEntities();
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
            mapTaiKhoan map = new mapTaiKhoan();
            
            if (map.ThemMoiTK(model) == true)
            {
                return RedirectToAction("DanhSachTaiKhoan");
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult CapNhatTaiKhoan(String TenTK)
        {
            var map = new mapTaiKhoan();
            var TKEdit = map.lichcb(TenTK);
            return View(TKEdit);
        }

        [HttpPost]
        public ActionResult CapNhatTaiKhoan(TaiKhoan model)
        {
            mapTaiKhoan map = new mapTaiKhoan();
            if (map.updateTK(model) == true)
            {
                return RedirectToAction("DanhSachTaiKhoan", new { TenTK = model.TenTK });
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult XoaTaiKhoan(String TenTK)
        {          
            mapTaiKhoan map = new mapTaiKhoan();
            map.XoaCB(TenTK);

            return RedirectToAction("DanhSachTaiKhoan");
        }
    }
}