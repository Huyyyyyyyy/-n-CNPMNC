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
    
    public partial class HuyVe
    {
        public string MaDatVe { get; set; }
        public string LyDo { get; set; }
        public string TinhTrang { get; set; }
    
        public virtual DatVe DatVe { get; set; }
    }
}