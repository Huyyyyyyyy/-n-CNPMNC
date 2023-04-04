using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.mapDatVe
{
    public class mapDatVe
    {
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