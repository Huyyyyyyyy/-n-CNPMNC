using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.taoMa
{
    public class taoMaUuDai
    {
        public List<string> maKoGiaTri()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from UuDai in db.UuDais
                        orderby UuDai.MaUD ascending
                        select UuDai.MaUD).ToList();
            return data;
        }
        //Tạo mã
        public string TaoMaUuDai()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            string macuoi = "";
            foreach (var item in new taoMaUuDai().maKoGiaTri())
            {
                macuoi = item.Substring(2, 4);
            }

            string ma1 = "UD";
            string s = "";

            if (db.UuDais.Count() <= 0)
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