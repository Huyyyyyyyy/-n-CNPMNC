using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.mapHoaDon
{
    public class mapHoaDon
    {
        //Thêm mới hóa đơn

        public bool ThemMoiHD(HoaDon model)
        {
            try
            {
                cnpmNCEntities db = new cnpmNCEntities();
                db.HoaDons.Add(model);

                //đổi trạng thái đơn đặt đã được thanh toán
                DatVe datVe = db.DatVes.SingleOrDefault(k => k.MaDatVe == model.MaDatVe);

                datVe.TrangThai = "Đã thanh toán";
                db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //tìm hóa đơn từ mã đặt vé
        public HoaDon TimHoaDon(String MaDatVe)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            return db.HoaDons.SingleOrDefault(k => k.MaDatVe == MaDatVe);
        }
    }
}