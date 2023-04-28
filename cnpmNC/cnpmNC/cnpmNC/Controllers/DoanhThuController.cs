using cnpmNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cnpmNC.Controllers
{
    public class DoanhThuController : Controller
    {
        // GET: DoanhThu
        public ActionResult ThongKeDoanhThu(Nullable<int> Ngay, Nullable<int> Thang, Nullable<int> Nam)
        {
            ViewBag.Ngay = Ngay; ViewBag.Thang = Thang; ViewBag.Nam = Nam;
            if (Ngay.HasValue || Thang.HasValue || Nam.HasValue) {
                List<HoaDon> hoaDons = new List<HoaDon>();
                hoaDons = new cnpmNC.Models.mapHoaDon.mapHoaDon().TimHoaDonTheoDate(Ngay, Thang, Nam);
                ViewBag.hoaDon = hoaDons;
                return View();
            }
            else
            {
                return View();

            }
        }



        public ActionResult DuLieuDoanhThu()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var query = (from DoanhThu in db.DoanhThus
                         select DoanhThu);

            var result = query.GroupBy(X => X.Thang).Select(x => new {
                Thang = x.Key,
                TongSoVe = x.Sum(y=>y.SoVeBan),
                TongDoanhThu = x.Sum(y=>y.TongDoanhThu),
                TongLoiNhuan = x.Sum(y=>y.LoiNhuan)
            });
                        
            return Json(new {Data = result}, JsonRequestBehavior.AllowGet);
        }
    }
}