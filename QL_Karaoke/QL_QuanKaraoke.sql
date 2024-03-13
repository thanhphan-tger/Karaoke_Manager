/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     12/8/2022 10:34:48 PM                        */
/*==============================================================*/

CREATE DATABASE QL_Karaoke
GO
USE QL_Karaoke
GO

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIETHANG') and o.name = 'FK_CHITIETH_CHITIETHA_MATHANG')
alter table CHITIETHANG
   drop constraint FK_CHITIETH_CHITIETHA_MATHANG
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIETHANG') and o.name = 'FK_CHITIETH_CHITIETHA_HOADON')
alter table CHITIETHANG
   drop constraint FK_CHITIETH_CHITIETHA_HOADON
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADON') and o.name = 'FK_HOADON_GOM2_PHIEUDAT')
alter table HOADON
   drop constraint FK_HOADON_GOM2_PHIEUDAT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADON') and o.name = 'FK_HOADON_LAP_NHANVIEN')
alter table HOADON
   drop constraint FK_HOADON_LAP_NHANVIEN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MATHANG') and o.name = 'FK_MATHANG_THUOC_DANHMUCH')
alter table MATHANG
   drop constraint FK_MATHANG_THUOC_DANHMUCH
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MATHANG') and o.name = 'FK_MATHANG_THUOC_2_DOVITINH')
alter table MATHANG
   drop constraint FK_MATHANG_THUOC_2_DOVITINH
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PHIEUDATPHONG') and o.name = 'FK_PHIEUDAT_DAT_KHACHHAN')
alter table PHIEUDATPHONG
   drop constraint FK_PHIEUDAT_DAT_KHACHHAN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PHIEUDATPHONG') and o.name = 'FK_PHIEUDAT_NAM_PHONG')
alter table PHIEUDATPHONG
   drop constraint FK_PHIEUDAT_NAM_PHONG
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PHONG') and o.name = 'FK_PHONG_THUOC_3_LOAIPHON')
alter table PHONG
   drop constraint FK_PHONG_THUOC_3_LOAIPHON
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIETHANG')
            and   name  = 'CHITIETHANG2_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIETHANG.CHITIETHANG2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIETHANG')
            and   name  = 'CHITIETHANG_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIETHANG.CHITIETHANG_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHITIETHANG')
            and   type = 'U')
   drop table CHITIETHANG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DANHMUCHANG')
            and   type = 'U')
   drop table DANHMUCHANG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DOVITINH')
            and   type = 'U')
   drop table DOVITINH
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOADON')
            and   name  = 'GOM2_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOADON.GOM2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOADON')
            and   name  = 'LAP_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOADON.LAP_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOADON')
            and   type = 'U')
   drop table HOADON
go

if exists (select 1
            from  sysobjects
           where  id = object_id('KHACHHANG')
            and   type = 'U')
   drop table KHACHHANG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LOAIPHONG')
            and   type = 'U')
   drop table LOAIPHONG
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MATHANG')
            and   name  = 'THUOC_2_FK'
            and   indid > 0
            and   indid < 255)
   drop index MATHANG.THUOC_2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MATHANG')
            and   name  = 'THUOC_FK'
            and   indid > 0
            and   indid < 255)
   drop index MATHANG.THUOC_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MATHANG')
            and   type = 'U')
   drop table MATHANG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NHANVIEN')
            and   type = 'U')
   drop table NHANVIEN
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PHIEUDATPHONG')
            and   name  = 'NAM_FK'
            and   indid > 0
            and   indid < 255)
   drop index PHIEUDATPHONG.NAM_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PHIEUDATPHONG')
            and   name  = 'DAT_FK'
            and   indid > 0
            and   indid < 255)
   drop index PHIEUDATPHONG.DAT_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PHIEUDATPHONG')
            and   type = 'U')
   drop table PHIEUDATPHONG
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PHONG')
            and   name  = 'THUOC_3_FK'
            and   indid > 0
            and   indid < 255)
   drop index PHONG.THUOC_3_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PHONG')
            and   type = 'U')
   drop table PHONG
go

/*==============================================================*/
/* Table: CHITIETHANG                                           */
/*==============================================================*/
create table CHITIETHANG (
   MAMH                 varchar(30)             not null,
   MAHD                 varchar(30)             not null,
   SOLUONG              int                  null,
   THANHTIEN			float				null,
   constraint PK_CHITIETHANG primary key (MAMH, MAHD)
)
go

/*==============================================================*/
/* Index: CHITIETHANG_FK                                        */
/*==============================================================*/
create index CHITIETHANG_FK on CHITIETHANG (MAMH ASC)
go

/*==============================================================*/
/* Index: CHITIETHANG2_FK                                       */
/*==============================================================*/
create index CHITIETHANG2_FK on CHITIETHANG (MAHD ASC)
go

/*==============================================================*/
/* Table: DANHMUCHANG                                           */
/*==============================================================*/
create table DANHMUCHANG (
   MADM                 varchar(30)             not null,
   TENDM                nvarchar(50)             null,
   constraint PK_DANHMUCHANG primary key nonclustered (MADM)
)
go

/*==============================================================*/
/* Table: DOVITINH                                              */
/*==============================================================*/
create table DOVITINH (
   MADVT                varchar(30)             not null,
   TENDVT               nvarchar(50)             null,
   constraint PK_DOVITINH primary key nonclustered (MADVT)
)
go

/*==============================================================*/
/* Table: HOADON                                                */
/*==============================================================*/
create table HOADON (
   MAHD                 varchar(30)             not null,
   MAPHIEU              varchar(30)             null,
   MANV                 varchar(30)             null,
   NGAYLAP              datetime             null,
   GIORA                datetime             null,
   GIAMGIA              FLOAT         null,
   TONGTIEN             float                default 0,
   TINHTRANG			int				default 0,
   constraint PK_HOADON primary key nonclustered (MAHD)
)
go

/*==============================================================*/
/* Index: LAP_FK                                                */
/*==============================================================*/
create index LAP_FK on HOADON (
MANV ASC
)
go

/*==============================================================*/
/* Index: GOM2_FK                                               */
/*==============================================================*/
create index GOM2_FK on HOADON (
MAPHIEU ASC
)
go

/*==============================================================*/
/* Table: KHACHHANG                                             */
/*==============================================================*/
create table KHACHHANG (
   MAKH                 varchar(30)             not null,
   CMND                 varchar(12)             null,
   TENKH                nvarchar(50)             null,
   NGAYSINH             datetime             null,
   SDT                  varchar(10)             null,
   LOAIKH               nvarchar(30)             null,
   DCHI                 nvarchar(100)            null,
   DIEMTICHLUY			int						default 0,
   constraint PK_KHACHHANG primary key nonclustered (MAKH)
)
go

/*==============================================================*/
/* Table: LOAIPHONG                                             */
/*==============================================================*/
create table LOAIPHONG (
   MALOAI               varchar(30)             not null,
   TENLOAI              nvarchar(50)             null,
   constraint PK_LOAIPHONG primary key nonclustered (MALOAI)
)
go

/*==============================================================*/
/* Table: MATHANG                                               */
/*==============================================================*/
create table MATHANG (
   MAMH                 varchar(30)             not null,
   MADM                 varchar(30)             null,
   MADVT                varchar(30)             null,
   TENMH                nvarchar(50)             null,
   DONGIA               float                null,
   constraint PK_MATHANG primary key nonclustered (MAMH)
)
go

/*==============================================================*/
/* Index: THUOC_FK                                              */
/*==============================================================*/
create index THUOC_FK on MATHANG (
MADM ASC
)
go

/*==============================================================*/
/* Index: THUOC_2_FK                                            */
/*==============================================================*/
create index THUOC_2_FK on MATHANG (
MADVT ASC
)
go

/*==============================================================*/
/* Table: NHANVIEN                                              */
/*==============================================================*/
create table NHANVIEN (
   MANV                 varchar(30)             not null,
   TENNV                nvarchar(50)             null,
   NGSINH               datetime             null,
   GIOITINH             nvarchar(5)              null,
   DIACHI               nvarchar(100)            null,
   CCCD                 varchar(15)            not null,
   PASSWORD             varchar(50)             null,
   QUYENDN              varchar(20)             null,
   CHUCVU               nvarchar(20)             null,
   SDTH                 varchar(11)             null,
   constraint PK_NHANVIEN primary key nonclustered (MANV)
)
go

/*==============================================================*/
/* Table: PHIEUDATPHONG                                         */
/*==============================================================*/
create table PHIEUDATPHONG (
   MAPHIEU              varchar(30)             not null,
   MAKH                 varchar(30)             null,
   MAPHONG              varchar(30)             null,
   GIOVAO               datetime             null,
   constraint PK_PHIEUDATPHONG primary key nonclustered (MAPHIEU)
)
go

/*==============================================================*/
/* Index: DAT_FK                                                */
/*==============================================================*/
create index DAT_FK on PHIEUDATPHONG (
MAKH ASC
)
go

/*==============================================================*/
/* Index: NAM_FK                                                */
/*==============================================================*/
create index NAM_FK on PHIEUDATPHONG (
MAPHONG ASC
)
go

/*==============================================================*/
/* Table: PHONG                                                 */
/*==============================================================*/
create table PHONG (
   MAPHONG              varchar(30)             not null,
   MALOAI               varchar(30)             not null,
   GIA                  float                null,
   TTPHONG              nvarchar(30)             null,
   SUCCHUA              int                  null,
   constraint PK_PHONG primary key nonclustered (MAPHONG)
)
go

/*==============================================================*/
/* Index: THUOC_3_FK                                            */
/*==============================================================*/
create index THUOC_3_FK on PHONG (
MALOAI ASC
)
go

alter table CHITIETHANG
   add constraint FK_CHITIETH_CHITIETHA_MATHANG foreign key (MAMH)
      references MATHANG (MAMH)
go

alter table CHITIETHANG
   add constraint FK_CHITIETH_CHITIETHA_HOADON foreign key (MAHD)
      references HOADON (MAHD)
go

alter table HOADON
   add constraint FK_HOADON_GOM2_PHIEUDAT foreign key (MAPHIEU)
      references PHIEUDATPHONG (MAPHIEU)
go

alter table HOADON
   add constraint FK_HOADON_LAP_NHANVIEN foreign key (MANV)
      references NHANVIEN (MANV)
go

alter table MATHANG
   add constraint FK_MATHANG_THUOC_DANHMUCH foreign key (MADM)
      references DANHMUCHANG (MADM)
go

alter table MATHANG
   add constraint FK_MATHANG_THUOC_2_DOVITINH foreign key (MADVT)
      references DOVITINH (MADVT)
go

alter table PHIEUDATPHONG
   add constraint FK_PHIEUDAT_DAT_KHACHHAN foreign key (MAKH)
      references KHACHHANG (MAKH)
go

alter table PHIEUDATPHONG
   add constraint FK_PHIEUDAT_NAM_PHONG foreign key (MAPHONG)
      references PHONG (MAPHONG)
go

alter table PHONG
   add constraint FK_PHONG_THUOC_3_LOAIPHON foreign key (MALOAI)
      references LOAIPHONG (MALOAI)
go

alter table NHANVIEN
add constraint uni_CCCD unique(CCCD)

----------------------------------------------------------------------------------------------------------------------
INSERT INTO DANHMUCHANG
VALUES
('DM01', N'Rượu'),
('DM02', N'Bia'),
('DM03', N'Nước ngọt'),
('DM04', N'Thức ăn')

-----------------------------------------------------------------------------------------------------------------------
INSERT INTO DOVITINH
VALUES
('DVT01', N'Chai'),
('DVT03', N'Cái'),
('DVT04', N'Kg'),
('DVT05', N'Bịt'),
('DVT07', N'Lon')

----------------------------------------------------------------------------------------------------------------------------
INSERT INTO MATHANG
VALUES
('MH001', 'DM03', 'DVT07', N'Coca Cola', 15000),
('MH002', 'DM03', 'DVT07', N'Pepsi', 15000),
('MH003', 'DM02', 'DVT07', N'Tiger', 20000),
('MH004', 'DM02', 'DVT07', N'Heniken', 20000),
('MH005', 'DM04', 'DVT05', N'Khô gà', 60000),
('MH006', 'DM04', 'DVT05', N'Khô bò', 80000),
('MH007', 'DM04', 'DVT05', N'Trái cây', 50000),
('MH008', 'DM04', 'DVT05', N'Snack', 26000),
('MH009', 'DM02', 'DVT07', N'Sài Gòn', 15000),
('MH010', 'DM02', 'DVT07', N'Laru', 15000),
('MH011', 'DM01', 'DVT01', N'Hennessy', 1915000),
('MH012', 'DM01', 'DVT01', N'Lucente', 2215000),
('MH013', 'DM01', 'DVT01', N'Almaviva', 1550000),
('MH014', 'DM01', 'DVT01', N'Château Lagrange', 1500000),
('MH015', 'DM02', 'DVT07', N'Budweiser', 35000)

------------------------------------------------------------------------------------------------------------------------------------
SET DATEFORMAT DMY
INSERT INTO KHACHHANG
VALUES
('KH00001', '2345678987', N'Phan Trường Thạnh', '04/03/2002', '0397483552', N'Khách thường', N'Đồng Tháp',10),
('KH00002', '5678954321', N'Nguyễn Thanh Long', '30/03/2002', '0392346563', N'Khách thường', N'Long An',5),
('KH00003', '6748935864', N'Nguyễn Thái Bảo', '22/11/2002', '0397552987', N'Khách thường', N'TP.HCM',5),
('KH00004', '2448384546', N'Trần Lê Anh Tuấn', '08/09/2002', '0394623902', N'Khách thường', N'TP.HCM',10),
('KH00005', '2374698237', N'Nguyễn Thị Nhỏ', '15/07/2002', '0394463533', N'Khách thường', N'Nghệ An',10),
('KH00006', '9028409328', N'Võ Thị Sáu', '10/10/2002', '0394773629', N'Khách thường', N'Bình Định',15),
('KH00007', '2349829042', N'Nguyễn Văn Huệ', '16/05/2002', '0399674533', N'Khách thường', N'Ninh Thuận',15)

-------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO LOAIPHONG
VALUES
('LP01',N'Tiêu chuẩn'),
('LP02',N'Gia đình'),
('LP03',N'Vip tổng thống'),
('LP04',N'Hạng thương gia')

-------------------------------------------------------------------------------------------------------------------------------------
SET DATEFORMAT DMY
INSERT INTO NHANVIEN
VALUES
('NV0001', N'Nguyễn Thanh Long', '17/01/1982', N'Nam', N'Long An', '2001206902', 'QL123', N'Admin', N'Quản lý','0389432737'),
('NV0002', N'Phan Trường Thạnh', '04/09/1985', N'Nam', N'Đồng Tháp', '2001207034', 'QL123', N'Admin', N'Quản lý','0397299207'),
('NV0003', N'Nguyễn Thái Bảo', '24/12/1986', N'Nam', N'TP.HCM', '2001206918', 'QL123', N'Admin', N'Quản lý','0906309259'),
('NV0004', N'Trần Kim Phụng', '14/07/1985', N'Nữ', N'Thanh Hóa', '2001207018', 'LT112', N'Account', N'Lễ tân','0978778794'),
('NV0005', N'Trịnh Đình Ánh', '15/11/1995', N'Nữ', N'Đồng Tháp', '2001206811', 'TV123', N'Account', N'Tạp vụ','0986607814'),
('NV0006', N'Huỳnh Kim Chi', '18/10/1996', N'Nữ', N'Long An', '2001206831', 'PV123', N'Account', N'Phục vụ','0995086538'),
('NV0007', N'Đậu Quang Ánh', '03/12/1994', N'Nữ', N'Lâm Đồng', '2001206810', 'PV122', N'Account', N'Phục vụ','0329220274'),
('NV0008', N'Nguyễn Thế Anh', '15/12/1996', N'Nam', N'Bình Định', '2001206801', 'BV543', N'Account', N'Bảo vệ','0986178943'),
('NV0009', N'Tô Ánh Nguyệt', '02/05/1995', N'Nam', N'Nam Định', '2001206931', 'BV576', N'Account', N'Bảo vệ','0984519565'),
('NV0010', N'Nguyễn Thế Bình', '04/06/1996', N'Nam', N'Thái Bình', '2001206910', 'KH097', N'Account', N'Kho','0939231260')

-----------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO PHONG
VALUES
('P001', 'LP01', 150000, N'Trống', 10),
('P002', 'LP01', 150000, N'Trống', 20),
('P003', 'LP01', 150000, N'Trống', 15),
('P004', 'LP01', 150000, N'Trống', 25),
('P005', 'LP02', 140000, N'Trống', 10),
('P006', 'LP02', 140000, N'Trống', 15),
('P007', 'LP02', 140000, N'Trống', 20),
('P008', 'LP04', 420000, N'Trống', 30),
('P009', 'LP04', 420000, N'Trống', 25),
('P010', 'LP03', 800000, N'Trống', 30)
GO

CREATE TRIGGER trg_UpdateTTien ON CHITIETHANG
FOR UPDATE, INSERT
AS
	BEGIN
		DECLARE @maMH VARCHAR(10), @soLuong INT, @maHD VARCHAR(10)
		SELECT  @maMH = MAMH, @soLuong = SOLUONG, @maHD = MAHD from inserted
		UPDATE CHITIETHANG
		SET THANHTIEN = @soLuong * (SELECT DONGIA FROM MATHANG WHERE MAMH = @maMH)
		WHERE MAHD = @maHD AND MAMH = @maMH
	END
GO

CREATE TRIGGER trg_UpdateTTienForHD ON HOADON
FOR UPDATE
AS
	BEGIN
		DECLARE @maHD VARCHAR(10), @gioRA DATETIME, @tienPhong FLOAT, @maPhong VARCHAR(10), @maPhieu VARCHAR(10)

		SELECT @maHD = MAHD, @gioRA = GIORA from inserted
		SELECT @maPhong = MAPHONG FROM PHIEUDATPHONG ct, HOADON h WHERE ct.MAPHIEU = h.MAPHIEU AND MAHD = @maHD
		SELECT @tienPhong = GIA FROM PHONG WHERE MAPHONG = @maPhong
		
		UPDATE HOADON
		SET TONGTIEN = ROUND((DATEDIFF(MINUTE,NGAYLAP, @gioRA)/60.0) * @tienPhong * (1 - GIAMGIA), 0) + dbo.udf_SumTTienMH(@maHD)
		WHERE MAHD = @maHD

		UPDATE PHONG
		SET TTPHONG = N'Trống'
		WHERE MAPHONG = @maPhong
	END
GO

CREATE FUNCTION udf_SumTTienMH(@maHD VARCHAR(10))
RETURNS FLOAT
AS
	BEGIN
		RETURN (SELECT SUM(THANHTIEN) FROM CHITIETHANG WHERE MAHD = @maHD)
	END
GO


SELECT * FROM DANHMUCHANG
SELECT * FROM DOVITINH
SELECT * FROM LOAIPHONG
SELECT * FROM NHANVIEN
SELECT * FROM MATHANG

SELECT * FROM KHACHHANG
SELECT * FROM PHONG

SELECT * FROM PHIEUDATPHONG

SELECT * FROM HOADON
SELECT * FROM CHITIETHANG

