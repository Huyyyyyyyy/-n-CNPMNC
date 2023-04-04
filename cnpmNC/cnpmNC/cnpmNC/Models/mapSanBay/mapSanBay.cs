using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.mapSanBay
{
    public class mapSanBay
    {
        // lấy ra danh sách chuyến bay
        public List<SanBay> DanhSachThanhPho()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from SanBay in db.SanBays
                        select SanBay
                ).ToList();
            return data;
        }

        //lấy ra sân bay 
        public List<string> TimSanBayDi(String MaSanBayDi)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from SanBay in db.SanBays
                        where (SanBay.MaSanBay == MaSanBayDi)
                        select SanBay.DiaDiem).ToList();
            return data;
        }
        public List<string> TimSanBayDen(String MaSanBayDen)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from SanBay in db.SanBays
                        where (SanBay.MaSanBay == MaSanBayDen)
                        select SanBay.DiaDiem).ToList();
            return data;
        }
        //lấy ra sân bay 

        //tìm thành phố 
        public SanBay TimSanBay(String MaSanBay)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            return db.SanBays.SingleOrDefault(ma => ma.MaSanBay == MaSanBay);
        }
    }
}