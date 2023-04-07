using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cnpmNC.Models.mapTaiKhoan
{
    public class mapTaiKhoan
    {
        cnpmNCEntities db = new cnpmNCEntities();
        public TaiKhoan lichcb(String TenTK)
        {           
            return db.TaiKhoans.SingleOrDefault(ma => ma.TenTK == TenTK);
        }
        public bool ThemMoiTK(TaiKhoan model)
        {
            try
            {               
                db.TaiKhoans.Add(model);
                db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool updateTK(TaiKhoan model)
        {          
            var updateModel = db.TaiKhoans.Find(model.TenTK);

            //check null
            if (updateModel == null)
            {
                return false;
            }

            // cập nhật giá trị
            updateModel.TenTK = model.TenTK;
            updateModel.MatKhau = model.MatKhau;
            updateModel.QuyenTruyCap = model.QuyenTruyCap;
            updateModel.HoTen = model.HoTen;
            updateModel.SoDT = model.SoDT;
            updateModel.CCCD = model.CCCD;

            db.SaveChanges();
            return true;
        }
        public bool XoaTK(String TenTK)
        {
            
            var modelcb = db.TaiKhoans.Find(TenTK);
            if (modelcb != null)
            {
                db.TaiKhoans.Remove(modelcb);
                db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}