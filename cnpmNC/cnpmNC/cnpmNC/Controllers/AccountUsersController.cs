using cnpmNC.Models;
using cnpmNC.Models.mapAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace cnpmNC.Controllers
{
    public class AccountUsersController : Controller
    {

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

        // GET: TaiKhoan
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(String TenTK, String MatKhau)
        {
            MatKhau = GetMD5(MatKhau);
            // kiem tra ten dang nhap & mat khau null >> thong bao thieu thong tin 
            if (string.IsNullOrEmpty(TenTK) == true | string.IsNullOrEmpty(MatKhau) == true)
            {
                ViewBag.thongBao = "Vui lòng nhập đẩy đủ tài khoản & mật khẩu";
                return View();
            }

            // tim tai khoan theo ten dang nhap trong database
            var find = new mapAccount().chiTiet(TenTK);

            // kiem tra ton tai tai khoan  if null >> return form dang nhap 
            if (find == null)
            {
                ViewBag.thongBao = "Tài khoản hoặc mật khẩu không đúng!";
                ViewBag.tendangnhap = TenTK;
                return View();
            }
            // if exist >> kiem tra mat khau if flase >> tro ve trang dang nhap 
            if (find.MatKhau != MatKhau)
            {
                ViewBag.thongBao = "Tài khoản hoặc mật khẩu không đúng!";
                ViewBag.tendangnhap = TenTK;
                return View();
            }
            // kiem tra phan quyen admin hay user
            if (find.QuyenTruyCap == "admin")
            {
                // if true >> save section server 
                Session["user"] = find;
                // chuyen huong sang trang chu admin
                return Redirect("/QuanLyChuyenBay/DanhSachChuyenBay");
            }
            else
            {
                // if true >> save section server 
                Session["user"] = find;
                return Redirect("/Home/TrangChu");
            }
        }



        public ActionResult DangKy()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(TaiKhoan taiKhoan)
        {
            cnpmNCEntities db = new cnpmNCEntities();

            var check = db.TaiKhoans.FirstOrDefault(s => s.TenTK == taiKhoan.TenTK);
            if (check == null)
            {
                if (taiKhoan.A_MatKhau == taiKhoan.MatKhau)
                {
                    taiKhoan.MatKhau = GetMD5(taiKhoan.MatKhau);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();
                    var find = new mapAccount().chiTiet(taiKhoan.TenTK);
                    Session["user"] = find;
                    return Redirect("/Home/TrangChu");
                }
                else
                {
                    ViewBag.error = "Xác nhận mật khẩu không khớp";
                    return View();
                }

            }
            else
            {
                ViewBag.error = "Tài khoản đã tồn tại";
                return View();
            }
        }

        public ActionResult DangXuat()
        {
            Session.Clear();//remove session
            return RedirectToAction("TrangChu","Home");

        }
    }
}