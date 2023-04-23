using cnpmNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cnpmNC.Models.taoMa;
using cnpmNC.Models.mapUuDai;

namespace cnpmNC.Controllers
{
    public class QuanLyUuDaiController : Controller
    {
        // GET: QuanLyUuDai
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DanhSachUuDai()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var dsud = db.UuDais.ToList();  
            return View(dsud);  
        }
        // xử lý thêm ưu đãi
        public ActionResult ThemUuDai() 
        { 
            return View();  
        }
        [HttpPost]
        public ActionResult ThemUuDai(UuDai model)
        {
            mapUuDai map = new mapUuDai();
            if (map.ThemMoiUD(model) == true)
            {
                return RedirectToAction("DanhSachUuDai");
            }
            else
            {
                return View(model);
            }
        }
        //xử lý cập nhật ưu đãi
        public ActionResult CapNhatUuDai(String MaUD)
        {
            var map = new mapUuDai();
            var uuDaiEdit = map.LayThongTinUuDai(MaUD);
            return View(uuDaiEdit);
        }

        [HttpPost]
        public ActionResult CapNhatUuDai(UuDai model)
        {
            mapUuDai map = new mapUuDai();
            if (map.updateUD(model) == true) 
            {
                return RedirectToAction("DanhSachUuDai");
            }
            else 
            { 
                return View(model);
            }
        }
        //xử lý xóa ưu đãi
        public ActionResult XoaUuDai(String MaUD)
        {
            mapUuDai map = new mapUuDai();
            map.XoaUD(MaUD);
            return RedirectToAction("DanhSachUuDai");
        }
    }
}