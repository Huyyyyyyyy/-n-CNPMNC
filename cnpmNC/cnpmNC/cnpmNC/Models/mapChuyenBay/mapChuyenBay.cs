using cnpmNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.mapChuyenBay
{
    public class mapChuyenBay
    {
        //Tìm chuyến bay theo mã chuyến bay
        public ChuyenBay lichcb(String MaChuyenBay)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            return db.ChuyenBays.SingleOrDefault(ma => ma.MaChuyenBay == MaChuyenBay);
        }

        // tìm chuyến bay dành cho admin trong danh sách chuyến bay 

        public List<ChuyenBay> searchChuyenBay(String tuKhoa)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var ketQua = db.ChuyenBays.Where(p => p.MaChuyenBay.Contains(tuKhoa)
                                                || p.MaSanBayDi.Contains(tuKhoa)
                                                || p.MaSanBayDen.Contains(tuKhoa));
            return ketQua.ToList();
        }

        //Xử lý tra cứu chuyến bay
        public List<ChuyenBay> DanhSachChuyenBay(String MaSanBayDi, String MaSanBayDen, Nullable<DateTime> NgayKhoiHanh)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from ChuyenBay in db.ChuyenBays
                        where (ChuyenBay.MaSanBayDi == MaSanBayDi) &
                        (ChuyenBay.MaSanBayDen == MaSanBayDen) &
                        (ChuyenBay.NgayKhoiHanh == NgayKhoiHanh)
                        select ChuyenBay).ToList();
            return data;
        }

        // Lấy toàn bộ chuyến bay
        public List<ChuyenBay> DSCB()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from ChuyenBay in db.ChuyenBays
                        select ChuyenBay).ToList();
            return data;
        }

        // Tìm chi tiết chuyến bay theo mã chuyến bay
        public ChiTietBay lichctcb(String MaChuyenBay)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            return db.ChiTietBays.SingleOrDefault(ma => ma.MaChuyenBay == MaChuyenBay);
        }

        //Lấy thông tin chuyến bay
        public List<string> LayChuyenBay(String macb)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from ChuyenBay in db.ChuyenBays
                        where ChuyenBay.MaChuyenBay == macb
                        orderby ChuyenBay.MaChuyenBay ascending
                        select ChuyenBay.MaChuyenBay).ToList();
            return data;
        }

        //Lấy thông tin chi tiết chuyến bay 
        public List<ChiTietBay> LayChiTietChuyenBay(String MaChuyenBay)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from ChiTietChuyenBay in db.ChiTietBays
                        where ChiTietChuyenBay.MaChuyenBay == MaChuyenBay
                        select ChiTietChuyenBay).ToList();
            return data;

        }

        //Thêm mới chuyến bay

        public bool ThemMoiCB(ChuyenBay model)
        {
            try
            {
                cnpmNCEntities db = new cnpmNCEntities();
                db.ChuyenBays.Add(model);
                db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Thêm mới chi tiết chuyến bay

        public bool ThemMoiChiTietChuyenBay(ChiTietBay model)
        {
            try
            {
                cnpmNCEntities db = new cnpmNCEntities();
                db.ChiTietBays.Add(model);
                db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //cập nhật chuyến bay

        public bool updateCB(ChuyenBay model)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var updateModel = db.ChuyenBays.Find(model.MaChuyenBay);

            //check null
            if (updateModel == null)
            {
                return false;
            }

            // cập nhật giá trị
            updateModel.MaSanBayDi = model.MaSanBayDi;
            updateModel.MaSanBayDen = model.MaSanBayDen;
            updateModel.NgayKhoiHanh = model.NgayKhoiHanh;
            updateModel.GioKhoiHanh = model.GioKhoiHanh;
            updateModel.NgayHaCanh = model.NgayHaCanh;
            updateModel.GioHaCanh = model.GioHaCanh;
            updateModel.GioBay = model.GioBay;
            updateModel.PhutBay = model.PhutBay;
            updateModel.SLGheTrong = model.SLGheTrong;
            updateModel.GiaVe = model.GiaVe;

            db.SaveChanges();
            return true;
        }

        // cập nhật chi tiết chuyến bay
        public bool updateCTCB(ChiTietBay model)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var updateModel = db.ChiTietBays.Find(model.MaCTBay);

            //check null
            if (updateModel == null)
            {
                return false;
            }

            // cập nhật giá trị
            updateModel.MaChuyenBay = model.MaChuyenBay;
            updateModel.MaSanBayTG = model.MaSanBayTG;
            updateModel.GioDung = model.GioDung;
            updateModel.PhutDung = model.PhutDung;
            updateModel.GhiChu = model.GhiChu;

            db.SaveChanges();
            return true;
        }

        //Xóa chuyến bay
        public bool XoaCB(String MaCTBay, String MaChuyenBay)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var model = db.ChiTietBays.Find(MaCTBay);
            var modelcb = db.ChuyenBays.Find(MaChuyenBay);
            if (model != null && modelcb != null)
            {
                db.ChiTietBays.Remove(model);
                db.ChuyenBays.Remove(modelcb);
                db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}