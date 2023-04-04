using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.taoMa
{
    public class taoMaHanhKhach
    {
        //Lấy mã với số đuôi lớn nhất 
        public List<string> maKoGiaTri()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from HanhKhach in db.HanhKhaches
                        orderby HanhKhach.MaHK ascending
                        select HanhKhach.MaHK).ToList();
            return data;
        }
        //Tạo mã
        public string TaoMaHanhKhach()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            string macuoi = "";
            foreach (var item in new taoMaHanhKhach().maKoGiaTri())
            {
                macuoi = item.Substring(2, 3);
            }

            string ma1 = "HK";
            string s = "";

            if (db.HanhKhaches.Count() <= 0)
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