using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.mapChuyenBayCuaToi
{
    public class mapChuyenBayCuaToi
    {

        public List<DatVe> DanhSachDonVe(String TaiKhoanDat)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from DatVe in db.DatVes
                        where DatVe.TaiKhoanDat == TaiKhoanDat
                        select DatVe).ToList();
            return data;
        }
    }
}