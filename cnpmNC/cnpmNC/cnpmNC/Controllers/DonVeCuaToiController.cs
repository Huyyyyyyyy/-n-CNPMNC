using cnpmNC.Models.mapDatVe;
using cnpmNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cnpmNC.Controllers
{
    public class DonVeCuaToiController : Controller
    {
        // GET: DonVeCuaToi
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DonVeCuaToi()
        {
            return View();
        }

        //xử lý yêu cầu hủy vé của khách hàng
        [HttpPost]
        public ActionResult YeuCauHuyVe(String MaDatVe, String LyDo)
        {
            cnpmNCEntities db = new cnpmNCEntities();


            HuyVe huyVe = new HuyVe();
            huyVe.MaDatVe = MaDatVe;
            huyVe.LyDo = LyDo;
            huyVe.TinhTrang = "Chờ xét duyệt";

            DatVe datVe = db.DatVes.SingleOrDefault(d => d.MaDatVe == MaDatVe);
            datVe.TrangThai = "Yêu cầu hủy đơn";

            mapDatVe map = new mapDatVe();
            map.YeuCauHuyVe(huyVe);
            db.SaveChanges();
            
                // tạo một alert message
                string message = "Yeu cau huy don ve  " + MaDatVe + " cua ban da duoc gui";

                // đặt kiểu và thông điệp của alert message vào TempData
                TempData["AlertMessage"] = message;
                return RedirectToAction("DonVeCuaToi", "DonVeCuaToi");
        }
    }
}