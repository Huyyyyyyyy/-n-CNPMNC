using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.mapDoanhThu
{
    public class mapDoanhThu
    {

        public DoanhThu DoanhThuTheoNgay(Nullable<int> Ngay, int Thang, int Nam)
        {
            if (Ngay != null)
            {
                cnpmNCEntities db = new cnpmNCEntities();
                return db.DoanhThus.SingleOrDefault(m => m.Ngay == Ngay);
            }
            else
            {
                
                    cnpmNCEntities db = new cnpmNCEntities();
                    var dateDoanhThu = from DoanhThu in db.DoanhThus
                                       where DoanhThu.Thang == Thang &
                                       DoanhThu.Nam == Nam
                                       select DoanhThu;

                    var result = dateDoanhThu.GroupBy(X => X.Thang).Select(x => new {
                        Thang = x.Key,
                        TongSoVe = x.Sum(y => y.SoVeBan),
                        TongDoanhThu = x.Sum(y => y.TongDoanhThu),
                        TongLoiNhuan = x.Sum(y => y.LoiNhuan)
                    });

                    DoanhThu doanhThu = new DoanhThu();
                    doanhThu.Thang = Thang;
                    doanhThu.Nam = Nam;
                    doanhThu.SoVeBan = result.Sum(x => x.TongSoVe);
                    doanhThu.TongDoanhThu = result.Sum(x => x.TongDoanhThu);
                    doanhThu.LoiNhuan = result.Sum(x => x.TongLoiNhuan);

                    return doanhThu;
                }

        }

    }
}