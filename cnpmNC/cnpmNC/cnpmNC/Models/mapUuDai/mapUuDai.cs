﻿using System;
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
        //thêm mới ưu đãi
        public bool ThemMoiUD(UuDai model)
        {
            try
            {
                cnpmNCEntities db = new cnpmNCEntities();
                db.UuDais.Add(model);
                db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //cập nhật ưu đãi
        public bool updateUD(UuDai model)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var updateModel = db.UuDais.Find(model.MaUD);

            if (updateModel == null) 
            {
                return false;
            }

            //cập nhật giá trị
            updateModel.MucUD = model.MucUD;
            updateModel.ThoiGianUD = model.ThoiGianUD;

            db.SaveChanges();
            return true;    
        }

        //xóa ưu đãi
        public bool XoaUD(String MaUD)
        {
            cnpmNCEntities db = new cnpmNCEntities();
            var model = db.UuDais.Find(MaUD);   

            if (model != null)
            {
                db.UuDais.Remove(model);
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