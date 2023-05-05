using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.mapDatVe
{
    public class mapDatVe
    {

        //lấy thong tin từ mã đơn vé
        public DatVe ThongTinDonDat(String MaDatVe)
        {
            cnpmNCEntities db = new cnpmNCEntities();

            return db.DatVes.SingleOrDefault(m => m.MaDatVe == MaDatVe); 
        }
        //Thêm mới đơn vé

        public bool ThemMoiDV(DatVe model)
        {
            try
            {
                cnpmNCEntities db = new cnpmNCEntities();
                db.DatVes.Add(model);
                db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DatVe DanhSachVe(String MaDatVe)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            return db.DatVes.SingleOrDefault(ma => ma.MaDatVe == MaDatVe);
        }

        //xử lý thêm yêu cầu hủy vé
        public bool YeuCauHuyVe(HuyVe model)
        {
            try
            {
                cnpmNCEntities db = new cnpmNCEntities();
                db.HuyVes.Add(model);
                db.SaveChangesAsync();
                    return true;
            }
            catch
            {
                return false;
            }
        }

        //lấy danh sách đơn đặt vé mới (tình trang yeu cau huy don, da thanh toan, chua thanh toan)
        public int DanhSachDonMoi()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            int dsvm = (from ds in db.DatVes
                        where ds.TrangThai == "Yêu cầu hủy đơn" ||
                        ds.TrangThai == "Đã thanh toán" ||
                        ds.TrangThai == "Chưa thanh toán"
                        select ds).Count();
            return dsvm;
        }

        //lấy thông tin yêu cầu hủy đơn

        public HuyVe thongTinHuyVe(String MaDatVe)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            return db.HuyVes.SingleOrDefault(h => h.MaDatVe == MaDatVe);
        }
                  
    }
}