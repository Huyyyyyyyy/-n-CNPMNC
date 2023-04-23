using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.taoMa
{
    public class taoMaVe
    {
        public List<string> maKoGiaTri()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from VeChuyenBay in db.VeChuyenBays
                        orderby VeChuyenBay.MaVe ascending
                        select VeChuyenBay.MaVe).ToList();
            return data;
        }
        //Tạo mã
        public string TaoMaVe()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            string macuoi = "";
            foreach (var item in new taoMaVe().maKoGiaTri())
            {
                macuoi = item.Substring(2, 4);
            }

            string ma1 = "VE";
            string s = "";

            if (db.VeChuyenBays.Count() <= 0)
            {
                s = Convert.ToString((ma1 + "0001"));
                return s;
            }
            else
            {
                int k;
                s = ma1;
                k = Convert.ToInt32(macuoi);
                k = k + 1;
                if (k < 10)
                { s = s + "000"; }
                else if (k < 100)
                { s = s + "00"; }
                else if (k < 1000)
                { s = s + "0"; }

                s = s + k.ToString();

                return s;
            }
        }


    }
}