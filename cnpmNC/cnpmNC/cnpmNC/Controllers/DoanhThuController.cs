using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cnpmNC.Controllers
{
    public class DoanhThuController : Controller
    {
        // GET: DoanhThu
        public ActionResult ThongKeDoanhThu()
        {
            return View();
        }



        public ActionResult DuLieuDoanhThu(string fromDate, string toDate)
        {
            return Json(true);
        }
    }
}