using cnpmNC.Models;
using cnpmNC.Models.mapHoaDon;
using cnpmNC.Models.mapPayPal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cnpmNC.Controllers
{
    public class MomoController : Controller
    {
        // GET: Momo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaymentWithMomo()
        {
            HoaDon cart = Session["hoaDonTemp"] as HoaDon;
            string endpoint = ConfigurationManager.AppSettings["endpoint"].ToString();
            string partnerCode = ConfigurationManager.AppSettings["partnerCode"].ToString();
            string accessKey = ConfigurationManager.AppSettings["accessKey"].ToString();
            string serectKey = ConfigurationManager.AppSettings["serectKey"].ToString();
            string orderInfo = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string returnUrl = ConfigurationManager.AppSettings["returnUrl"].ToString();
            string notifyUrl = ConfigurationManager.AppSettings["notifyUrl"].ToString();

            string amount = Convert.ToInt32(cart.ThanhTien).ToString();
            string orderid = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            string extraData = "";

            string rawHash = "partnerCode=" + partnerCode
                + "&accessKey=" + accessKey
                + "&requestId=" + requestId
                + "&amount=" + amount
                + "&orderId=" + orderid
                + "&orderInfo=" + orderInfo
                + "&returnUrl=" + returnUrl
                + "&notifyUrl=" + notifyUrl
                + "&extraData=" + extraData;
            cnpmNC.Models.mapMoMo.MoMoSecurity crypto = new Models.mapMoMo.MoMoSecurity();
            string signature = crypto.signSHA256(rawHash, serectKey);
            JObject message = new JObject
            {
                {"partnerCode",partnerCode },
                {"accessKey",accessKey },
                {"requestId",requestId },
                {"amount",amount },
                {"orderId",orderid },
                {"orderInfo",orderInfo },
                {"returnUrl",returnUrl },
                {"notifyUrl",notifyUrl },
                {"requestType","captureMoMoWallet" },
                {"signature",signature }
            };
            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
            JObject jmessage = JObject.Parse(responseFromMomo);
            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        public ActionResult ReturnUrl()
        {
            string param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
            param = Server.UrlDecode(param);
            cnpmNC.Models.mapMoMo.MoMoSecurity crypto = new Models.mapMoMo.MoMoSecurity();
            string serectKey = ConfigurationManager.AppSettings["serectKey"].ToString();
            string signature = crypto.signSHA256(param, serectKey);
            if (signature != Request["signature"].ToString())
            {
                ViewBag.message = "Thông tin request không hợp lệ";
            }
            else if (!Request.QueryString["errorCode"].Equals("0"))
            {
                ViewBag.message = "Thanh toán thất bại";
            }
            else
            {
                cnpmNCEntities db = new cnpmNCEntities();
                //lấy ra đơn đặt vé từ session
                DatVe donDatVe = Session["donDatVe"] as DatVe;

                //lấy ra hóa đơn đã tạo tạm thời từ session
                HoaDon hoaDonTemp = Session["hoaDonTemp"] as HoaDon;



                //thực hiện thêm mới hóa đơn vào database
                mapHoaDon map = new mapHoaDon();
                map.ThemMoiHD(hoaDonTemp);

                //tìm chuyến bay từ database
                ChuyenBay modelChuyenBay = db.ChuyenBays.SingleOrDefault(c => c.MaChuyenBay == donDatVe.MaChuyenBay);

                // thực hiện trừ chỗ trống khi hoàn thành thêm hóa đơn
                modelChuyenBay.SLGheTrong = modelChuyenBay.SLGheTrong - donDatVe.SoLuongVe;

                // lưu 
                db.SaveChanges();

                //gửi mail thông báo thanh toán thành công 
                new cnpmNC.Models.mapContactEmail.mapContactEmail().SendEmail(donDatVe.TaiKhoanDat, "Thanh toán thành công", "<p style=\"font-size:20px\">Cảm ơn bạn đã mua vé máy bay của chúng tôi <br/>Mã đơn vé của bạn là: " + donDatVe.MaDatVe);


                //xóa đơn và hóa đơn trong session sau khi hoàn thành 
                Session.Remove("donDatVe");
                Session.Remove("hoaDonTemp");

            }
            return RedirectToAction("HoanTatThanhToan", "DatVe");
        }

        public JsonResult NotifyUrl()
        {
            string param = "";
            param = "partner_code=" + Request["partner_code"]
                + "&access_key=" + Request["access_key"]
                + "&amount=" + Request["amount"]
                + "&order_id=" + Request["order_id"]
                + "&order_info=" + Request["orderInfo"]
                + "&order_type=" + Request["order_type"]
                + "&transaction_id=" + Request["transaction_id"]
                + "&message=" + Request["message"]
                + "&response_time=" + Request["response_time"]
                + "&status_code=" + Request["status_code"];
            param = Server.UrlDecode(param);
            cnpmNC.Models.mapMoMo.MoMoSecurity crypto = new Models.mapMoMo.MoMoSecurity();
            string serectKey = ConfigurationManager.AppSettings["serectKey"].ToString();
            //Thành công = 1
            //Thất bại = 0
            //Chờ thanh toán = -1
            string signature = crypto.signSHA256(param, serectKey);
            if (signature != Request["signature"].ToString())
            {
                //Fail
            }
            string status_code = Request["status_code"].ToString();
            if (status_code != "0")
            {
                //Fail
            }
            else
            {
                //Success
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}