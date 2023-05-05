
using cnpmNC.Models;
using cnpmNC.Models.mapDatVe;
using cnpmNC.Models.mapHanhKhach;
using cnpmNC.Models.mapHoaDon;
using cnpmNC.Models.mapUuDai;
using Microsoft.Ajax.Utilities;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cnpmNC.Controllers
{
    public class DatVeController : Controller
    {
        // GET: DatVe
        public ActionResult Index()
        {
            return View();
        }
        //Xử lý chọn chuyến bay
        public ActionResult ChonChuyenBay(String MaSanBayDi, String MaSanBayDen, Nullable<DateTime> NgayKhoiHanh, int SoLuong)
        {
            if(MaSanBayDi == null || MaSanBayDen == null || NgayKhoiHanh == null) {
                return RedirectToAction("TrangChu","Home");
            }
            ViewBag.MaSanBayDi = MaSanBayDi;
            ViewBag.MaSanBayDen = MaSanBayDen;
            ViewBag.NgayKhoiHanh = NgayKhoiHanh;
            ViewBag.SoLuong = SoLuong;
            mapChuyenBay.mapChuyenBay mapChuyenBay = new mapChuyenBay.mapChuyenBay();
            return View(mapChuyenBay.DanhSachChuyenBay(MaSanBayDi, MaSanBayDen, NgayKhoiHanh));
        }

        public ActionResult ThongTinHanhKhach( int SoLuong, String MaChuyenBay)
        {
            ViewBag.SoLuong = SoLuong;
            ViewBag.MaChuyenBay = MaChuyenBay;
            return View();
        }

        [HttpPost]
        //xử lý lưu tạm thông tin hành khách và tạo đơn đặt vé 

        public ActionResult LuuTamHanhKhach(HanhKhachTam[] hanhKhachTams, int SoLuong,String MaChuyenBay)
        {

            mapDatVe mapDatVe = new mapDatVe();

            //tạo Session và  gán mảng hành khách tạm 
            Session["hanhKhachTam"] = hanhKhachTams;

            //gọi ra mảng hành khách tạm vừa lấy từ form
            HanhKhachTam[] hanhKhach = (HanhKhachTam[])Session["hanhKhachTam"];

            //tạo biến lưu tên tài khoản đặt vé
            string TaiKhoanDat = "";
            foreach(var item in hanhKhach)
            {
                TaiKhoanDat = item.TenTK;
                break;
            }

            //tạo biến lưu mã đặt vé
            string MaDatVe = new Models.taoMa.taoMaDonDatVe().TaoMaDonVe();

            //gọi ra chuyến bay đặt
            var ChuyenBayDaChon = new mapChuyenBay.mapChuyenBay().lichcb(MaChuyenBay);

            //xừ lý thêm đơn đặt vé 
            DatVe datVe = new DatVe();

            datVe.MaDatVe = MaDatVe;
            datVe.MaChuyenBay = MaChuyenBay;
            datVe.SoLuongVe = SoLuong;
            datVe.TaiKhoanDat = TaiKhoanDat;
            datVe.TrangThai = "Chưa thanh toán";
            datVe.TongTien = SoLuong * ChuyenBayDaChon.GiaVe;

            //thêm mới đơn đặt vé
            mapDatVe.ThemMoiDV(datVe);


            // thêm mới hành khách theo đơn đặt vé 

            mapHanhKhach mapHanhKhach = new mapHanhKhach();

            HanhKhach hanhKhachChinhThuc = new HanhKhach();
            foreach (var item in hanhKhach)
            {
                hanhKhachChinhThuc.MaHK = new Models.taoMa.taoMaHanhKhach().TaoMaHanhKhach();
                hanhKhachChinhThuc.MaChuyenBay = MaChuyenBay;
                hanhKhachChinhThuc.MaDatVe = MaDatVe;
                hanhKhachChinhThuc.TenHK = item.TenHK;
                hanhKhachChinhThuc.NgaySinh = item.NgaySinh;
                hanhKhachChinhThuc.GioiTinh = item.GioiTinh;
                hanhKhachChinhThuc.QuocTich = item.QuocTich;
                hanhKhachChinhThuc.CCCD = item.CCCD;

                mapHanhKhach.ThemMoiHanhKhach(hanhKhachChinhThuc);
            }
            return RedirectToAction("DatVeHoanTat", new { SoLuong = SoLuong, MaChuyenBay = MaChuyenBay, MaDatVe = MaDatVe });
         }


        // show thông báo hoàn tất đặt vé
        public ActionResult DatVeHoanTat(int SoLuong, String MaChuyenBay, String MaDatVe)
        {

            ViewBag.SoLuong = SoLuong;
            ViewBag.MaChuyenBay = MaChuyenBay;
            ViewBag.MaDatVe = MaDatVe;
            return View();
        }






        //xử lý giao diện thanh toán
        public ActionResult ThanhToan(String MaUuDai, int SoLuong, String MaChuyenBay, String MaDatVe)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            ViewBag.MaUuDai = MaUuDai;
            ViewBag.SoLuong = SoLuong;
            ViewBag.MaChuyenBay = MaChuyenBay;
            ViewBag.MaDatVe = MaDatVe;
            // tìm đơn vé dưới database
            var donDatVe = db.DatVes.SingleOrDefault(d => d.MaDatVe == MaDatVe);

            // lưu đơn vé vào session
            Session["donDatVe"] = donDatVe;

            //tìm đơn vé
            DatVe order = db.DatVes.Find(donDatVe.MaDatVe);


            //tìm chuyến bay 
            ChuyenBay modelChuyenBay = db.ChuyenBays.Find(order.MaChuyenBay);

            // tạo một model hóa đơn và thêm đổ dữ liệu vào 
            HoaDon modelHoaDon = new HoaDon();

            modelHoaDon.MaHD = new cnpmNC.Models.taoMa.taoMaHoaDon().TaoMaHoaDon();
            modelHoaDon.MaDatVe = order.MaDatVe;
            modelHoaDon.Ngay = DateTime.Now;
            modelHoaDon.TaiKhoanDat = order.TaiKhoanDat;
            modelHoaDon.SoLuongVe = order.SoLuongVe;
            modelHoaDon.GiaVe = modelChuyenBay.GiaVe;


            //tạo biến lưu mã ưu đãi đã chọn
            if (String.IsNullOrEmpty(MaUuDai) == true)
            {
                MaUuDai = "UD0001";
                modelHoaDon.MaUuDai = MaUuDai;
            }
            else
            {
                modelHoaDon.MaUuDai = MaUuDai;
            }

            //Lưu tiền

            decimal ThanhTien = 0;

            if (MaUuDai == "UD0001")
            {
                ThanhTien = modelChuyenBay.GiaVe * order.SoLuongVe;
            }
            else
            {
                // lấy ra thông tin Uu đãi chọn
                cnpmNC.Models.UuDai ThongTinUuDai = new cnpmNC.Models.mapUuDai.mapUuDai().LayThongTinUuDai(MaUuDai);

                //lấy ra mức ưu đãi

                decimal MucUD = Convert.ToDecimal(ThongTinUuDai.MucUD);


                ThanhTien = modelChuyenBay.GiaVe * order.SoLuongVe - (modelChuyenBay.GiaVe * order.SoLuongVe * (MucUD / 100));
            }

            modelHoaDon.ThanhTien = ThanhTien;


            //lưu model hóa đơn tạm thời vào session
            Session["hoaDonTemp"] = modelHoaDon;


            return View();
        }


        public ActionResult ThanhToanVoiCredit()
        {
            mapHoaDon map = new mapHoaDon();
            cnpmNCEntities db = new cnpmNCEntities();

            HoaDon hoaDon = Session["hoaDonTemp"] as HoaDon;

            //lấy thông tin đơn vé 
            var donVeDat = new mapDatVe().ThongTinDonDat(hoaDon.MaDatVe);
            // lấy thông tin chuyến bay đã đặt
            var chuyenBayDat = new mapChuyenBay.mapChuyenBay().lichcb(donVeDat.MaChuyenBay);
            if (map.ThemMoiHD(hoaDon) == true)
            {

                // trừ ghế trong chuyến bay khi thanh toán hoàn tất
                ChuyenBay chuyenBay = db.ChuyenBays.SingleOrDefault(m => m.MaChuyenBay == chuyenBayDat.MaChuyenBay);

                chuyenBay.SLGheTrong = chuyenBay.SLGheTrong - hoaDon.SoLuongVe;

                db.SaveChanges();

                Session.Remove("donDatVe");
                Session.Remove("MaUuDai");
                Session.Remove("hoaDonTemp");
                return RedirectToAction("HoanTatThanhToan");
            }
            else
            {
                return RedirectToAction("ThanhToan","DatVe", new {MaUuDai = hoaDon.MaUuDai, SoLuong = hoaDon.SoLuongVe, MaChuyenBay = chuyenBayDat.MaChuyenBay, MaDatVe = hoaDon.MaDatVe});
            }
        }


        //action xử lý chọn phương thức thanh toán
        public ActionResult PhuongThucThanhToan(String PhuongThuc)
        {

            //add order 
            DatVe modelDonVe = Session["donDatVe"] as DatVe;
            string MaUuDai = Session["MaUuDai"] as string;
            HoaDon hoaDon = Session["hoaDonTemp"] as HoaDon;

            if(PhuongThuc == "paypal")
            {
                return RedirectToAction("PaymentWithPaypal", "PayPal");
            }
            else if (PhuongThuc == "international")
            {
                return RedirectToAction("ThanhToanVoiCredit","DatVe");
            }
            else if (PhuongThuc == "momo")
            {
                return RedirectToAction("PaymentWithMomo", "Momo");
            }

            Session.Remove("donDatVe");
            Session.Remove("MaUuDai");
            Session.Remove("hoaDonTemp");
            return RedirectToAction("TrangChu","Home");
        }


        // thông báo thanh toán hoàn tất
        public ActionResult HoanTatThanhToan()
        {

            return View();
        }


    }
}