﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class cnpmNCEntities : DbContext
    {
        public cnpmNCEntities()
            : base("name=cnpmNCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ChiTietBay> ChiTietBays { get; set; }
        public virtual DbSet<ChuyenBay> ChuyenBays { get; set; }
        public virtual DbSet<DatVe> DatVes { get; set; }
        public virtual DbSet<DoanhThu> DoanhThus { get; set; }
        public virtual DbSet<HanhKhach> HanhKhaches { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<SanBay> SanBays { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<UuDai> UuDais { get; set; }
        public virtual DbSet<VeChuyenBay> VeChuyenBays { get; set; }
    }
}