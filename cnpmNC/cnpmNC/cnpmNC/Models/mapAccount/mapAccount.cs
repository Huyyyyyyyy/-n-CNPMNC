using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.mapAccount
{
    public class mapAccount
    {
        public List<TaiKhoan> DanhSach()
        {
            try
            {
                cnpmNCEntities db = new cnpmNCEntities();
                return db.TaiKhoans.ToList();
            }
            catch
            {
                return new List<TaiKhoan>();
            }
        }
        public TaiKhoan chiTiet(String taiKhoan)
        {
            try
            {
                cnpmNCEntities db = new cnpmNCEntities();
                var model = db.TaiKhoans.SingleOrDefault(m => m.TenTK == taiKhoan.ToLower());
                return model;
            }
            catch
            {
                return null;
            }
        }
    }
}