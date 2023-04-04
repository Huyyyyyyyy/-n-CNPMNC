using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.taoMa
{
    public class taoMaChuyenBay
    {
        //Lấy mã với số đuôi lớn nhất 
        public List<string> maKoGiaTri()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from ChuyenBay in db.ChuyenBays
                        orderby ChuyenBay.MaChuyenBay ascending
                        select ChuyenBay.MaChuyenBay).ToList();
            return data;
        }
        //Tạo mã
        public string TaoMaChuyenBay()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            string macuoi = "";
            foreach (var item in new taoMaChuyenBay().maKoGiaTri())
            {
                macuoi = item.Substring(2, 3);
            }

            string ma1 = "CB";
            string s = "";

            if (db.ChuyenBays.Count() <= 0)
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