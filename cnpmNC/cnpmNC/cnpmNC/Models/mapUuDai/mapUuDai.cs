using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.mapUuDai
{
    public class mapUuDai
    {
        //lấy thông tin ưu đãi từ mã ưu đãi
        public UuDai LayThongTinUuDai(String MaUuDai)
        {
            cnpmNCEntities db= new cnpmNCEntities();
            return db.UuDais.SingleOrDefault(s => s.MaUD == MaUuDai);
        }

        //danh sách các ưu đãi
        public List<UuDai> DanhSachUuDai()
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var data = (from UuDai in db.UuDais
                        select UuDai).ToList();

            return data;
        }
    }
}