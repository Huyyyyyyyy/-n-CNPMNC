using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.taoMa
{
    public class taoMaChiTietChuyenBay
    {
        //Lấy mã chi tiết chuyến bay theo mã Chuyến Bay
        public List<string> LayMaCTCB(string MaChuyenBay)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from ChiTietBay in db.ChiTietBays
                        where ChiTietBay.MaChuyenBay == MaChuyenBay
                        orderby ChiTietBay.MaChuyenBay ascending
                        select ChiTietBay.MaChuyenBay).ToList();
            return data;
        }


        public List<string> LayMaCTChuyenBay()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from ChiTietBay in db.ChiTietBays
                        orderby ChiTietBay.MaChuyenBay ascending
                        select ChiTietBay.MaChuyenBay).ToList();
            return data;
        }

        public string TaoMaCTChuyenBay()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            string macuoi = "";
            foreach (var item in new taoMaChiTietChuyenBay().LayMaCTChuyenBay())
            {
                macuoi = item.Substring(2, 3);
            }

            string ma1 = "CT";
            string s = "";

            if (db.ChiTietBays.Count() <= 0)
            {
                s = Convert.ToString((ma1 + "001"));
                return s;
            }
            else
            {
                int k;
                s = ma1;
                k = Convert.ToInt32(macuoi);
                k = k + 1;
                if (k < 10)
                { s = s + "00"; }
                else if (k < 100)
                { s = s + "0"; }

                s = s + k.ToString();

                return s;
            }
        }

    }
}