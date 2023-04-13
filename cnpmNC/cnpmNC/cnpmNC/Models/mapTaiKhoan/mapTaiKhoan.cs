using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace cnpmNC.Models.mapTaiKhoan
{
    public class mapTaiKhoan
    {
        cnpmNCEntities db = new cnpmNCEntities();
        public TaiKhoan TimTaiKhoan(String TenTK)
        {
            try
            {
                cnpmNCEntities db = new cnpmNCEntities();
                var model = db.TaiKhoans.SingleOrDefault(m => m.TenTK == TenTK.ToLower());
                return model;
            }
            catch
            {
                return null;
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



        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}