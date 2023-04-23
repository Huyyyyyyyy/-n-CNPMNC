using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.mapVeChuyenBay
{
    public class mapVeChuyenBay
    {
        public VeChuyenBay TimVeChuyenBay (String MaDatVe)
        {
            cnpmNCEntities db = new cnpmNCEntities ();
            return db.VeChuyenBays.SingleOrDefault(m => m.MaDatVe == MaDatVe);
        }
    }
}