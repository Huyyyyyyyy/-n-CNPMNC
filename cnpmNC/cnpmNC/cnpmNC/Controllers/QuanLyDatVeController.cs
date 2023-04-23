using cnpmNC.Models;
using cnpmNC.Models.mapDatVe;
using cnpmNC.Models.mapHanhKhach;
using cnpmNC.Models.mapHoaDon;
using System.Linq;
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
            vedat.TrangThai = "Đã xác nhận";
            DatVe model = db.DatVes.FirstOrDefault(ma => ma.MaDatVe == MaDatVe);
            model.TrangThai = vedat.TrangThai;

            //lấy ra hành khách từ đơn vé 
            var hanhKhach = new mapHanhKhach().TimHanhKhach(model.MaDatVe);

            //lấy ra hóa đơn của người dùng
            var hoaDon = new mapHoaDon().TimHoaDon(model.MaDatVe);

            // lấy thông tin chuyến bay đã đặt
            var chuyenBayDat = new mapChuyenBay.mapChuyenBay().lichcb(model.MaChuyenBay);

            // tiến hành tạo vé chuyến bay cho từng hành khách có trong đơn vé đã đặt 
            foreach (var item in hanhKhach)
            {
                VeChuyenBay veChuyenBay = new VeChuyenBay();
                veChuyenBay.MaVe = new cnpmNC.Models.taoMa.taoMaVe().TaoMaVe();
                veChuyenBay.MaDatVe = model.MaDatVe;
                veChuyenBay.MaHD = hoaDon.MaHD;
                veChuyenBay.MaHK = item.MaHK;
                veChuyenBay.TenHanhKhach = item.TenHK;
                veChuyenBay.MaChuyenBay = model.MaChuyenBay;
                veChuyenBay.NgayKhoiHanh = chuyenBayDat.NgayKhoiHanh;
                veChuyenBay.GioKhoiHanh = chuyenBayDat.GioKhoiHanh;
                veChuyenBay.MaSanBayDi = chuyenBayDat.MaSanBayDi;
                veChuyenBay.MaSanBayDen = chuyenBayDat.MaSanBayDen;

                db.VeChuyenBays.Add(veChuyenBay);
                db.SaveChanges();
            }

            db.SaveChanges();
            return RedirectToAction("DanhSachVeDat");


        }
    }
}
