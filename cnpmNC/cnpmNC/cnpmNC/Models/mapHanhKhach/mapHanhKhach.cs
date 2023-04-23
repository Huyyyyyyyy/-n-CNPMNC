using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.mapHanhKhach
{
    public class mapHanhKhach
    {
        //Thêm mới hành khách

        public bool ThemMoiHanhKhach(HanhKhach model)
        {
            try
            { 
                cnpmNCEntities db = new cnpmNCEntities();
                db.HanhKhaches.Add(model);
                db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

       // tìm hành khách theo đơn đặt vé
        public List<HanhKhach> TimHanhKhach(String MaDatVe)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from HanhKhach in db.HanhKhaches
                        where HanhKhach.MaDatVe == MaDatVe
                        select HanhKhach).ToList();
            return data;
        }

    }
}