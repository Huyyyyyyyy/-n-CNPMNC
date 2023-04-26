create database cnpmNC
go

use cnpmNC
go

create table SanBay(
	MaSanBay nvarchar(10) primary key,
	TenSanBay nvarchar(150) not null,
	DiaDiem nvarchar(150) not null
)

create table TaiKhoan(
	TenTK nvarchar(50) primary key,
	MatKhau nvarchar(9) not null,
	QuyenTruyCap nvarchar(50) not null,
	HoTen nvarchar(150) not null,
	SoDT nvarchar(11) not null,
	CCCD nvarchar(15) not null
)

create table ChuyenBay(
	MaChuyenBay nvarchar(10) primary key,
	MaSanBayDi nvarchar(10) not null,
	MaSanBayDen nvarchar(10) not null,
	NgayKhoiHanh date not null,
	GioKhoiHanh time not null,
	NgayHaCanh date not null,
	GioHaCanh time not null,
	ThoiGianBay time not null,
	SLGheTrong int not null,
	GiaVe decimal(10,2) not null

	constraint FK_SBDiCB foreign key (MaSanBayDi) references SanBay(MaSanBay),
	constraint FK_SBDenCB foreign key (MaSanBayDen) references SanBay(MaSanBay)
)



create table ChiTietBay(
	MaCTBay nvarchar(10) primary key,
	MaChuyenBay nvarchar(10) not null,
	MaSanBayTG nvarchar(10),
	ThoiGianDung time,
	GhiChu text

	constraint FK_CBCTB foreign key (MaChuyenBay) references ChuyenBay(MaChuyenBay),
	constraint FK_SBTGCTB foreign key (MaSanBayTG) references SanBay(MaSanBay)
)

create table UuDai (
	MaUD nvarchar(10) primary key,
	MucUD int not null,
	ThoiGianUD date not null
)

create table DatVe(
	MaDatVe nvarchar(10) primary key,
	MaChuyenBay nvarchar(10) not null,
	SoLuongVe int not null,
	TaiKhoanDat nvarchar(50) not null,
	TrangThai nvarchar(50) not null default N'Chưa thanh toán',
	TongTien decimal(10,2) not null

	constraint FK_CBDV foreign key (MaChuyenBay) references ChuyenBay(MaChuyenBay),
	constraint FK_TKDV foreign key (TaiKhoanDat) references TaiKhoan(TenTK)
)

create table HanhKhach(
	MaHK nvarchar(10) primary key,
	MaChuyenBay nvarchar(10) not null,
	MaDatVe nvarchar(10) not null,
	TenHK nvarchar(50) not null,
	NgaySinh date not null,
	GioiTinh nvarchar(10) not null,
	QuocTich nvarchar(150) not null,
	CCCD nvarchar(50) not null

	constraint FK_MDVHK foreign key (MaDatVe) references DatVe(MaDatVe),
	constraint FK_MCBHK foreign key (MaChuyenBay) references ChuyenBay(MaChuyenBay)

)

create table HoaDon (
	MaHD nvarchar(10) primary key,
	MaDatVe nvarchar(10) not null,
	Ngay date not null,
	TaiKhoanDat nvarchar(50) not null,
	SoLuongVe int not null,
	GiaVe decimal(10,2) not null,
	MaUuDai nvarchar(10) not null,
	ThanhTien decimal(10,2) not null

	constraint FK_DVHD foreign key (MaDatVe) references DatVe(MaDatVe),
	constraint FK_TKHD foreign key (TaiKhoanDat) references TaiKhoan(TenTK),
	constraint FK_UDHD foreign key (MaUuDai) references UuDai(MaUD)
)

create table VeChuyenBay(
	MaVe nvarchar(10) primary key ,
	MaDatVe nvarchar(10) not null,
	MaHD nvarchar(10) not null,
	MaHK nvarchar(10) not null,
	TenHanhKhach nvarchar(50) not null,
	MaChuyenBay nvarchar(10) not null,
	NgayKhoiHanh date not null,
	GioKhoiHanh time not null,
	MaSanBayDi nvarchar(10) not null,
	MaSanBayDen nvarchar(10) not null
		
	constraint FK_SBDiVCB foreign key (MaSanBayDi) references SanBay(MaSanBay),
	constraint FK_SBDenVCB foreign key (MaSanBayDen) references SanBay(MaSanBay)

)

create table DoanhThu(
	Ngay date primary key,
	SoVeBan int not null,
	TongDoanhThu decimal(10,2) not null,
	LoiNhuan decimal(10,2) not null
)

drop table DoanhThu
use cnpmNC
go


alter table ChuyenBay
add TinhTrang nvarchar(50) not null default N'Chưa mở bán vé'

alter table ChuyenBay
drop column ThoiGianBay

alter table ChuyenBay
add GioBay int not null,
PhutBay int not null

alter table ChiTietBay
drop column ThoiGianDung

alter table ChiTietBay
add GioDung int,
PhutDung int


/*Dữ liệu sân bay*/
insert into SanBay
values('HAN',N'Sân bay Quốc tế Nội Bài',N'Hà Nội'),
('SGN',N'Sân bay Quốc tế Tân Sơn Nhất',N'Hồ Chí Minh'),
('DAD',N'Sân bay Quốc tế Đà Nẵng',N'Đà Nẵng'),
('VDO',N'Sân bay Quốc tế Vân Đồn',N'Quảng Ninh'),
('HPH',N'Sân bay Quốc tế Cát Bi',N'Hải Phòng'),
('VII',N'Sân bay Quốc tế Vinh',N'Nghệ An'),
('HUI',N'Sân bay Quốc tế Phú Bài',N'Huế'),
('CXR',N'Sân bay Quốc tế Cam Ranh',N'Khánh Hòa'),
('DLI',N'Sân bay Quốc tế Liên Khương',N'Lâm Đồng'),
('UIH',N'Sân bay Quốc tế Phù Cát',N'Bình Định')

alter table TaiKhoan 
alter column MatKhau nvarchar(50)

use cnpmNC
go

create trigger tinhDoanhThu on HoaDon
after insert
as
begin
	if(select Ngay
		from DoanhThu
		where Ngay = (select Ngay from inserted)) is not null 
		begin
				update DoanhThu
				set SoVeBan = SoVeBan + (select SoLuongVe
											from inserted),
				TongDoanhThu = TongDoanhThu + (select ThanhTien from inserted),
				LoiNhuan = ((TongDoanhThu + (select ThanhTien from inserted))*0.1)
				where Ngay = (select Ngay from inserted)
				end
	else
		begin
			insert into DoanhThu
			values(
			(select Ngay from inserted),
			(select sum(hd.SoLuongVe) from HoaDon hd inner join inserted i
				on hd.MaHD = i.MaHD
				where hd.Ngay = i.Ngay),
			(select sum(hd.ThanhTien) from HoaDon hd inner join inserted i
				on hd.MaHD = i.MaHD
				where hd.Ngay = i.Ngay),
			(select (sum(hd.ThanhTien)*0.1) from HoaDon hd inner join inserted i
				on hd.MaHD = i.MaHD
				where hd.Ngay = i.Ngay)
			)
		end
end

