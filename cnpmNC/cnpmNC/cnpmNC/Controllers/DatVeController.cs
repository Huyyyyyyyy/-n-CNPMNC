
using cnpmNC.Models;
using cnpmNC.Models.mapDatVe;
using cnpmNC.Models.mapHanhKhach;
using cnpmNC.Models.mapHoaDon;
using cnpmNC.Models.mapUuDai;
using Microsoft.Ajax.Utilities;
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

        //xử lý thanh toán
        public ActionResult ThanhToan(String MaUuDai, int SoLuong, String MaChuyenBay, String MaDatVe)
        {
            ViewBag.MaUuDai = MaUuDai;
            ViewBag.SoLuong = SoLuong;
            ViewBag.MaChuyenBay = MaChuyenBay;
            ViewBag.MaDatVe = MaDatVe;
            return View();
        }

        [HttpPost]
        public ActionResult ThanhToan(HoaDon model)
        {
            mapHoaDon map = new mapHoaDon();
            if (map.ThemMoiHD(model) == true)
            {
                return RedirectToAction("HoanTatThanhToan");
            }
            else
            {
                return View(model);
            }
        }

        // thông báo thanh toán hoàn tất
        public ActionResult HoanTatThanhToan()
        {
            return View();
        }


    }
}