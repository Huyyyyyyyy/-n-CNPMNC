using cnpmNC.Models.mapDatVe;
using cnpmNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cnpmNC.Controllers
{
    public class QuanLyDatVeController : Controller
    {
        // GET: QuanLyDatVe
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhSachVeDat()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var dsvd = db.DatVes.ToList();
            return View(dsvd);
        }

        public ActionResult XacNhanDatVe(string MaDatVe)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var map = new mapDatVe();
            var vedat = map.DanhSachVe(MaDatVe);
            vedat.TrangThai = "Done";      
            DatVe model = db.DatVes.FirstOrDefault(ma => ma.MaDatVe == MaDatVe);
            model.TrangThai = vedat.TrangThai;
            db.SaveChanges();
            return RedirectToAction("DanhSachVeDat");
        }
    }
}