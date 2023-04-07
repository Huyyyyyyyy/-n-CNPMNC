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
    }
}