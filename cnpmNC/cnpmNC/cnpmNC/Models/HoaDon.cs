//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cnpmNC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HoaDon
    {
        public string MaHD { get; set; }
        public string MaDatVe { get; set; }
        public System.DateTime Ngay { get; set; }
        public string TaiKhoanDat { get; set; }
        public int SoLuongVe { get; set; }
        public decimal GiaVe { get; set; }
        public string MaUuDai { get; set; }
        public decimal ThanhTien { get; set; }
    
        public virtual DatVe DatVe { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual UuDai UuDai { get; set; }
    }
}