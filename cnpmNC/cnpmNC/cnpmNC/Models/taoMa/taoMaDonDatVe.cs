using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.taoMa
{
    public class taoMaDonDatVe
    {
        public List<string> maDonKoGiaTri()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from DatVe in db.DatVes
                        orderby DatVe.MaDatVe ascending
                        select DatVe.MaDatVe).ToList();
            return data;
        }
        public string TaoMaDonVe()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            string macuoi = "";
            foreach (var item in new taoMa.taoMaDonDatVe().maDonKoGiaTri())
            {
                macuoi = item.Substring(3, 7);
            }

            string ma1 = "DDV";
            string s = "";

            if (db.DatVes.Count() <= 0)
            {
                s = Convert.ToString((ma1 + "0000001"));
                return s;
            }
            else
            {
                int k;
                s = ma1;
                k = Convert.ToInt32(macuoi);
                k = k + 1;
                if (k < 10)
                { s = s + "000000"; }
                else if (k < 100)
                { s = s + "00000"; }
                else if (k < 1000)
                { s = s + "0000"; }
                else if (k < 10000)
                { s = s + "000"; }
                else if (k < 100000)
                { s = s + "00"; }
                else if (k < 1000000)
                { s = s + "0"; }

                s = s + k.ToString();

                return s;
            }
        }
    }
}