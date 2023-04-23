using cnpmNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cnpmNC.Controllers
{
    public class ChuyenBayCuaToiController : Controller
    {
        // GET: ChuyenBayCuaToi
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChuyenBayCuaToi(String MaDatVe)
        {
            ViewBag.MaDatVe = MaDatVe;
            return View();
        }
    }
}