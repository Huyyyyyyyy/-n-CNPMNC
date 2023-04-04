
using cnpmNC.Models;
using cnpmNC.Models.taoMa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cnpmNC.Controllers
{
    public class QuanLyChuyenBayController : Controller
    {

        // GET: QuanLyChuyenBay
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DanhSachChuyenBay()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var dscb = db.ChuyenBays.ToList();
            return View(dscb);
        }

        public ActionResult ThemChuyenBay()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemChuyenBay(ChuyenBay model)
        {
            mapChuyenBay.mapChuyenBay map = new mapChuyenBay.mapChuyenBay();
            if (map.ThemMoiCB(model) == true)
            {
                return RedirectToAction("ThemChiTietChuyenBay");
            }
            else
            {
                return View(model);
            }
        }


        public ActionResult ThemChiTietChuyenBay()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemChiTietChuyenBay(ChiTietBay model)
        {
            if(model == null)
            {
                return RedirectToAction("ThemChuyenBay");
            }
            else
            {
                mapChuyenBay.mapChuyenBay map = new mapChuyenBay.mapChuyenBay();
                if (map.ThemMoiChiTietChuyenBay(model) == true)
                {
                    return RedirectToAction("DanhSachChuyenBay");
                }
                else
                {
                    return View(model);
                }
            }
        }

        //Xử lý sự kiện onlick (Đóng mở Đặt Vé)
        public ActionResult DongMoVe(string MaChuyenBay)
        {
            cnpmNCEntities db= new cnpmNCEntities();
            var map = new mapChuyenBay.mapChuyenBay();
            var lichCBEdit = map.lichcb(MaChuyenBay);
            if(lichCBEdit.TinhTrang == "Closed")
            {
                lichCBEdit.TinhTrang = "Opening";
            }
            else
            {
                lichCBEdit.TinhTrang = "Closed";
            }

            ChuyenBay model = db.ChuyenBays.FirstOrDefault(s => s.MaChuyenBay == MaChuyenBay);

            model.TinhTrang = lichCBEdit.TinhTrang;
            db.SaveChanges();
            return RedirectToAction("DanhSachChuyenBay");
        }

        // Xử lý cập nhật chuyến bay
        public ActionResult CapNhatChuyenBay(String MaChuyenBay)
        {
            var map = new mapChuyenBay.mapChuyenBay();
            var lichCBEdit = map.lichcb(MaChuyenBay);
            return View(lichCBEdit);
        }

        [HttpPost]
        public ActionResult CapNhatChuyenBay(ChuyenBay model)
        {
            mapChuyenBay.mapChuyenBay map = new mapChuyenBay.mapChuyenBay();
            if (map.updateCB(model) == true)
            {
                return RedirectToAction("CapNhatCTChuyenBay", new { MaChuyenBay = model.MaChuyenBay });
            }
            else
            {
                return View(model);
            }
        }


        //xử lý cập nhật Chi Tiết Chuyến Bay
        public ActionResult CapNhatCTChuyenBay(String MaChuyenBay)
        {
            var map = new mapChuyenBay.mapChuyenBay();
            var lichCBEdit = map.lichctcb(MaChuyenBay);
            return View(lichCBEdit);
        }

        [HttpPost]
        public ActionResult CapNhatCTChuyenBay(ChiTietBay model)
        {
            mapChuyenBay.mapChuyenBay map = new mapChuyenBay.mapChuyenBay();
            if (map.updateCTCB(model) == true)
            {
                return RedirectToAction("DanhSachChuyenBay");
            }
            else
            {
                return View(model);
            }
        }

        //xử lý xóa chuyến bay
        public ActionResult XoaChuyenBay(String MaChuyenBay)
        {
            String MaCTBay = "";
            mapChuyenBay.mapChuyenBay map = new mapChuyenBay.mapChuyenBay();
            MaCTBay = map.lichctcb(MaChuyenBay).MaCTBay;
            map.XoaCB(MaCTBay, MaChuyenBay);

            return RedirectToAction("DanhSachChuyenBay");
        }

    }
}