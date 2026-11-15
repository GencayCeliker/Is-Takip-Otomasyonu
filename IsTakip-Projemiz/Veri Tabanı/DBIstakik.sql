/* DBIstakik veritabanı - SQL Server'dan dışa aktarıldı (2026-11-15)
   SSMS ile açıp Execute (F5) ile çalıştırın. */
IF DB_ID(N'DBIstakik') IS NULL CREATE DATABASE [DBIstakik];
GO
USE [DBIstakik];
GO

CREATE TABLE [dbo].[TBLCEKSENET] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Firma] int NULL,
    [Tur] char(5) NULL,
    [Tutar] decimal(18,2) NULL,
    [Aciklama] nvarchar(500) NULL,
    [YazmaTarihi] date NULL,
    [OdemeTarihi] date NULL,
    [Durum] bit NULL,
    CONSTRAINT [PK_TBLCEKSENET] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [dbo].[TblDepartman] (
    [DEPID] int IDENTITY(1,1) NOT NULL,
    [DEPARTMANADI] nvarchar(50) NULL,
    [Durum] bit NULL DEFAULT ((1)),
    CONSTRAINT [PK_TblDepartman] PRIMARY KEY ([DEPID])
);
GO

CREATE TABLE [dbo].[TBLFIRMA] (
    [FirmaID] int IDENTITY(1,1) NOT NULL,
    [FirmaAdi] nvarchar(50) NULL,
    [Adres] nvarchar(100) NULL,
    [Telefon] nchar(15) NULL,
    [Durum] bit NULL DEFAULT ((1)),
    CONSTRAINT [PK_TBLFIRMA] PRIMARY KEY ([FirmaID])
);
GO

CREATE TABLE [dbo].[TBLFIRMAHAREKET] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Urun] int NULL,
    [Aciklama] nvarchar(500) NULL,
    [Tutar] decimal(18,2) NULL,
    [Tarih] date NULL,
    [Adet] int NULL,
    [Fırma] int NULL,
    CONSTRAINT [PK_TBLFIRMAHAREKET] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[TBLFIRMAODEME] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Firma] int NULL,
    [Tarih] date NULL,
    [Tutar] decimal(18,2) NULL,
    [Aciklama] nvarchar(500) NULL,
    CONSTRAINT [PK_TBLFIRMAODEME] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [dbo].[TBLKASA] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TUR] nvarchar(5) NULL,
    [TUTAR] decimal(18,0) NULL,
    [ACIKLAMA] nvarchar(max) NULL,
    [Tarih] date NULL,
    CONSTRAINT [PK_TBLKASA] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[TBLKATEGORI] (
    [KategoriId] int IDENTITY(1,1) NOT NULL,
    [KategoriAdi] nvarchar(50) NULL,
    CONSTRAINT [PK_TBLKATEGORI] PRIMARY KEY ([KategoriId])
);
GO

CREATE TABLE [dbo].[TBLKULLANICI] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AdSoyad] nvarchar(50) NULL,
    [KullaniciAdi] nvarchar(50) NULL,
    [Sifre] nvarchar(50) NULL,
    [Personel] bit NULL,
    [Departman] bit NULL,
    [CekSenet] bit NULL,
    [Musteri] bit NULL,
    [StokTakip] bit NULL,
    [Kasa] bit NULL,
    [Urunler] bit NULL,
    [VeriTabni] bit NULL,
    [Yetki] char(1) NULL,
    [Mail] nvarchar(100) NULL,
    CONSTRAINT [PK_TBLKULLANICI] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [dbo].[TBLMUSTERI] (
    [MusterıId] int IDENTITY(1,1) NOT NULL,
    [MusteriAd] nvarchar(50) NULL,
    [MusteriSoyadı] nvarchar(50) NULL,
    [Telefon] nvarchar(20) NULL,
    [IslemSayisi] int NULL,
    [IlkIslemtarihi] date NULL,
    [Durum] bit NULL,
    CONSTRAINT [PK_TBLMUSTERI] PRIMARY KEY ([MusterıId])
);
GO

CREATE TABLE [dbo].[TBLMUSTERIBORC] (
    [BorcId] int IDENTITY(1,1) NOT NULL,
    [Tarih] date NULL,
    [Tutar] decimal(18,2) NULL,
    [Aciklama] nvarchar(max) NULL,
    [Musteri] int NULL,
    CONSTRAINT [PK_TBLMUSTERIBORC] PRIMARY KEY ([BorcId])
);
GO

CREATE TABLE [dbo].[TBLMUSTERIODEME] (
    [OdemeId] int IDENTITY(1,1) NOT NULL,
    [Tarih] date NULL,
    [Tutar] decimal(18,2) NULL,
    [Aciklama] nvarchar(max) NULL,
    [Musteri] int NULL,
    CONSTRAINT [PK_TBLMUSTERIODEME] PRIMARY KEY ([OdemeId])
);
GO

CREATE TABLE [dbo].[TBLPersonel] (
    [PRSNLID] int IDENTITY(1,1) NOT NULL,
    [DPMID] int NULL,
    [ADI] nvarchar(50) NULL,
    [SOYADI] nvarchar(50) NULL,
    [DOGUMYERI] nvarchar(50) NULL,
    [DOGUMTARIHI] date NULL,
    [MAAS] decimal(18,2) NULL,
    [MEDENI] bit NULL,
    [RESIM] nvarchar(150) NULL,
    [TELEFON] nvarchar(50) NULL,
    [EMAIL] nvarchar(50) NULL,
    [ADRES] nvarchar(150) NULL,
    [DURUM] bit NULL,
    CONSTRAINT [PK_TBLPersonel] PRIMARY KEY ([PRSNLID])
);
GO

CREATE TABLE [dbo].[TBLPersonelOdeme] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TARIH] date NULL,
    [TUR] nvarchar(50) NULL,
    [ACIKLAMA] nvarchar(500) NULL,
    [TUTAR] decimal(18,2) NULL,
    [PERSONELID] int NULL,
    CONSTRAINT [PK_TBLPersonelOdeme] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[TBLURUNLER] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UrunAdi] nvarchar(50) NULL,
    [Marka] nvarchar(50) NULL,
    [AlisFiyati] decimal(18,2) NULL,
    [SatisFiyati] decimal(18,2) NULL,
    [StokAdeti] int NULL,
    [Kategori] int NULL,
    [Firma] int NULL,
    [Durum] bit NULL,
    CONSTRAINT [PK_TBLURUNLER] PRIMARY KEY ([Id])
);
GO

SET IDENTITY_INSERT [dbo].[TBLCEKSENET] ON;
INSERT INTO [dbo].[TBLCEKSENET] ([Id], [Firma], [Tur], [Tutar], [Aciklama], [YazmaTarihi], [OdemeTarihi], [Durum]) VALUES (1, 1, N'Çek  ', 5000.00, N'qeqewqe', '2025-10-10', '2025-11-10', 1);
INSERT INTO [dbo].[TBLCEKSENET] ([Id], [Firma], [Tur], [Tutar], [Aciklama], [YazmaTarihi], [OdemeTarihi], [Durum]) VALUES (2, 2, N'Senet', 25000.00, N'weqwe', '2025-05-02', '2025-07-02', 0);
INSERT INTO [dbo].[TBLCEKSENET] ([Id], [Firma], [Tur], [Tutar], [Aciklama], [YazmaTarihi], [OdemeTarihi], [Durum]) VALUES (3, 1, N'Senet', 50000.00, N'qeqwe', '2025-10-15', '2025-12-15', 1);
SET IDENTITY_INSERT [dbo].[TBLCEKSENET] OFF;
GO

SET IDENTITY_INSERT [dbo].[TblDepartman] ON;
INSERT INTO [dbo].[TblDepartman] ([DEPID], [DEPARTMANADI], [Durum]) VALUES (1, N'İnsan Kaynakları', 1);
INSERT INTO [dbo].[TblDepartman] ([DEPID], [DEPARTMANADI], [Durum]) VALUES (2, N'Bilgi İşlem', 1);
INSERT INTO [dbo].[TblDepartman] ([DEPID], [DEPARTMANADI], [Durum]) VALUES (3, N'Muhasebe', 1);
INSERT INTO [dbo].[TblDepartman] ([DEPID], [DEPARTMANADI], [Durum]) VALUES (4, N'Satış', 1);
INSERT INTO [dbo].[TblDepartman] ([DEPID], [DEPARTMANADI], [Durum]) VALUES (5, N'İnsan Kaynakları', 0);
INSERT INTO [dbo].[TblDepartman] ([DEPID], [DEPARTMANADI], [Durum]) VALUES (6, N'Personel', 1);
INSERT INTO [dbo].[TblDepartman] ([DEPID], [DEPARTMANADI], [Durum]) VALUES (7, N'İsan', 0);
SET IDENTITY_INSERT [dbo].[TblDepartman] OFF;
GO

SET IDENTITY_INSERT [dbo].[TBLFIRMA] ON;
INSERT INTO [dbo].[TBLFIRMA] ([FirmaID], [FirmaAdi], [Adres], [Telefon], [Durum]) VALUES (1, N'Aydınlar Şirket', N'EQWE', N'456123456789   ', 0);
INSERT INTO [dbo].[TBLFIRMA] ([FirmaID], [FirmaAdi], [Adres], [Telefon], [Durum]) VALUES (2, N'IKEA', N'EQWEQW', N'10000000001    ', 1);
INSERT INTO [dbo].[TBLFIRMA] ([FirmaID], [FirmaAdi], [Adres], [Telefon], [Durum]) VALUES (3, N'Koçtaş', N'eqweq', N'123567894563   ', 1);
INSERT INTO [dbo].[TBLFIRMA] ([FirmaID], [FirmaAdi], [Adres], [Telefon], [Durum]) VALUES (4, N'Kerem', N'İstanbul', N'05550000001    ', 1);
INSERT INTO [dbo].[TBLFIRMA] ([FirmaID], [FirmaAdi], [Adres], [Telefon], [Durum]) VALUES (5, N'Gencay', N'İstanbul', N'012345678912   ', 1);
INSERT INTO [dbo].[TBLFIRMA] ([FirmaID], [FirmaAdi], [Adres], [Telefon], [Durum]) VALUES (6, N'Yusuf', N'Çorum', N'05550000002    ', 0);
INSERT INTO [dbo].[TBLFIRMA] ([FirmaID], [FirmaAdi], [Adres], [Telefon], [Durum]) VALUES (7, N'Ahmetaa', N'qeqweqwe', N'123123123      ', 0);
SET IDENTITY_INSERT [dbo].[TBLFIRMA] OFF;
GO

SET IDENTITY_INSERT [dbo].[TBLFIRMAHAREKET] ON;
INSERT INTO [dbo].[TBLFIRMAHAREKET] ([ID], [Urun], [Aciklama], [Tutar], [Tarih], [Adet], [Fırma]) VALUES (1, 1, N'-', 2500.00, '2025-12-13', 10, 1);
INSERT INTO [dbo].[TBLFIRMAHAREKET] ([ID], [Urun], [Aciklama], [Tutar], [Tarih], [Adet], [Fırma]) VALUES (2, 2, N'-', 30000.00, '2025-12-14', 10, 2);
SET IDENTITY_INSERT [dbo].[TBLFIRMAHAREKET] OFF;
GO

SET IDENTITY_INSERT [dbo].[TBLFIRMAODEME] ON;
INSERT INTO [dbo].[TBLFIRMAODEME] ([Id], [Firma], [Tarih], [Tutar], [Aciklama]) VALUES (1, 1, '2025-12-05', 50000.00, N'-');
INSERT INTO [dbo].[TBLFIRMAODEME] ([Id], [Firma], [Tarih], [Tutar], [Aciklama]) VALUES (2, 2, '2025-10-09', 10000.00, N'-');
INSERT INTO [dbo].[TBLFIRMAODEME] ([Id], [Firma], [Tarih], [Tutar], [Aciklama]) VALUES (3, 3, '2025-12-02', 20000.00, N'-');
INSERT INTO [dbo].[TBLFIRMAODEME] ([Id], [Firma], [Tarih], [Tutar], [Aciklama]) VALUES (4, 1, '2025-12-02', 10000.00, N'ewwerw');
SET IDENTITY_INSERT [dbo].[TBLFIRMAODEME] OFF;
GO

SET IDENTITY_INSERT [dbo].[TBLKASA] ON;
INSERT INTO [dbo].[TBLKASA] ([ID], [TUR], [TUTAR], [ACIKLAMA], [Tarih]) VALUES (1, N'Gelir', 1500, N'Borç Ödendi', '2025-10-10');
INSERT INTO [dbo].[TBLKASA] ([ID], [TUR], [TUTAR], [ACIKLAMA], [Tarih]) VALUES (2, N'Gider', 2650, N'Elektirik Faturası Ödendi', '2025-10-10');
INSERT INTO [dbo].[TBLKASA] ([ID], [TUR], [TUTAR], [ACIKLAMA], [Tarih]) VALUES (3, N'Gelir', 50000, N'qweqwe:D', '2025-11-05');
INSERT INTO [dbo].[TBLKASA] ([ID], [TUR], [TUTAR], [ACIKLAMA], [Tarih]) VALUES (5, N'Gider', 10000, N'Faturalar', '2025-11-05');
INSERT INTO [dbo].[TBLKASA] ([ID], [TUR], [TUTAR], [ACIKLAMA], [Tarih]) VALUES (6, N'Gider', 20000, N'Koçtaş Firmasına Ödeme', '2025-12-02');
INSERT INTO [dbo].[TBLKASA] ([ID], [TUR], [TUTAR], [ACIKLAMA], [Tarih]) VALUES (7, N'Gider', 10000, N'Aydınlar Şirket Firmasına Ödeme', '2025-12-02');
SET IDENTITY_INSERT [dbo].[TBLKASA] OFF;
GO

SET IDENTITY_INSERT [dbo].[TBLKATEGORI] ON;
INSERT INTO [dbo].[TBLKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (1, N'Küçük Ev Aletleri');
INSERT INTO [dbo].[TBLKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (2, N'Bilgisayar');
INSERT INTO [dbo].[TBLKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (3, N'Beyaz Eşya');
SET IDENTITY_INSERT [dbo].[TBLKATEGORI] OFF;
GO

SET IDENTITY_INSERT [dbo].[TBLKULLANICI] ON;
INSERT INTO [dbo].[TBLKULLANICI] ([Id], [AdSoyad], [KullaniciAdi], [Sifre], [Personel], [Departman], [CekSenet], [Musteri], [StokTakip], [Kasa], [Urunler], [VeriTabni], [Yetki], [Mail]) VALUES (1, N'İsmail Küçükali', N'ik', N'61', 1, 1, 1, 1, 1, 1, 1, 1, N'Y', N'gencay@mail.com');
INSERT INTO [dbo].[TBLKULLANICI] ([Id], [AdSoyad], [KullaniciAdi], [Sifre], [Personel], [Departman], [CekSenet], [Musteri], [StokTakip], [Kasa], [Urunler], [VeriTabni], [Yetki], [Mail]) VALUES (2, N'Ahmet Çöl', N'ac', N'123', 0, 0, 1, 0, 1, 0, 1, 0, N'k', N'ahmet@mail.com');
SET IDENTITY_INSERT [dbo].[TBLKULLANICI] OFF;
GO

SET IDENTITY_INSERT [dbo].[TBLMUSTERI] ON;
INSERT INTO [dbo].[TBLMUSTERI] ([MusterıId], [MusteriAd], [MusteriSoyadı], [Telefon], [IslemSayisi], [IlkIslemtarihi], [Durum]) VALUES (1, N'Zeynep', N'Yeşil', N'10000000001', 0, '2020-01-14', 1);
INSERT INTO [dbo].[TBLMUSTERI] ([MusterıId], [MusteriAd], [MusteriSoyadı], [Telefon], [IslemSayisi], [IlkIslemtarihi], [Durum]) VALUES (2, N'Ahmet', N'ÇAKAR', N'10000000002', 0, '2011-10-25', 1);
INSERT INTO [dbo].[TBLMUSTERI] ([MusterıId], [MusteriAd], [MusteriSoyadı], [Telefon], [IslemSayisi], [IlkIslemtarihi], [Durum]) VALUES (3, N'Ali', N'Cabbar', N'5358645580', NULL, '2025-11-25', 1);
SET IDENTITY_INSERT [dbo].[TBLMUSTERI] OFF;
GO

SET IDENTITY_INSERT [dbo].[TBLMUSTERIBORC] ON;
INSERT INTO [dbo].[TBLMUSTERIBORC] ([BorcId], [Tarih], [Tutar], [Aciklama], [Musteri]) VALUES (1, '2025-10-12', 560.00, N'..', 1);
INSERT INTO [dbo].[TBLMUSTERIBORC] ([BorcId], [Tarih], [Tutar], [Aciklama], [Musteri]) VALUES (2, '2025-10-11', 780.00, N'..', 2);
SET IDENTITY_INSERT [dbo].[TBLMUSTERIBORC] OFF;
GO

SET IDENTITY_INSERT [dbo].[TBLMUSTERIODEME] ON;
INSERT INTO [dbo].[TBLMUSTERIODEME] ([OdemeId], [Tarih], [Tutar], [Aciklama], [Musteri]) VALUES (1, '2025-11-07', 300.00, N'...', 1);
INSERT INTO [dbo].[TBLMUSTERIODEME] ([OdemeId], [Tarih], [Tutar], [Aciklama], [Musteri]) VALUES (2, '2025-11-06', 250.00, N'...', 2);
SET IDENTITY_INSERT [dbo].[TBLMUSTERIODEME] OFF;
GO

SET IDENTITY_INSERT [dbo].[TBLPersonel] ON;
INSERT INTO [dbo].[TBLPersonel] ([PRSNLID], [DPMID], [ADI], [SOYADI], [DOGUMYERI], [DOGUMTARIHI], [MAAS], [MEDENI], [RESIM], [TELEFON], [EMAIL], [ADRES], [DURUM]) VALUES (1, 2, N'Ahmet', N'Uysal', N'Ankara', '2000-06-20', 8300.00, 1, N'g.jpg', N'10000000003', N'ahmet@mail.com', N'zile', 1);
INSERT INTO [dbo].[TBLPersonel] ([PRSNLID], [DPMID], [ADI], [SOYADI], [DOGUMYERI], [DOGUMTARIHI], [MAAS], [MEDENI], [RESIM], [TELEFON], [EMAIL], [ADRES], [DURUM]) VALUES (2, 1, N'Serap', N'Şeker', N'İstanbul', '2001-10-10', 56200.00, 0, N'serap.jpg', N'2345678912', N'serap@mail.com', N'Turhal', 1);
INSERT INTO [dbo].[TBLPersonel] ([PRSNLID], [DPMID], [ADI], [SOYADI], [DOGUMYERI], [DOGUMTARIHI], [MAAS], [MEDENI], [RESIM], [TELEFON], [EMAIL], [ADRES], [DURUM]) VALUES (3, 2, N'Ahmet', N'Çakıcı', N'İstanbul', '2025-10-07', 18000.00, 1, N'g.jpg', N'10000000001', N'kere@mail.com', N'ist', 1);
INSERT INTO [dbo].[TBLPersonel] ([PRSNLID], [DPMID], [ADI], [SOYADI], [DOGUMYERI], [DOGUMTARIHI], [MAAS], [MEDENI], [RESIM], [TELEFON], [EMAIL], [ADRES], [DURUM]) VALUES (4, 2, N'Ali', N'Veli', N'ankar', '2025-10-22', 50000.00, 0, N'pexels-garvin-st-villier-3311574.jpg', N'123456789221', N'bekar@mail.com', N'qeqweqweqw', 1);
INSERT INTO [dbo].[TBLPersonel] ([PRSNLID], [DPMID], [ADI], [SOYADI], [DOGUMYERI], [DOGUMTARIHI], [MAAS], [MEDENI], [RESIM], [TELEFON], [EMAIL], [ADRES], [DURUM]) VALUES (5, 2, N'Gonca', N'Yılmaz', N'ist', '2025-10-22', 500000.00, 1, N'Gemini_Generated_Image_8jplbn8jplbn8jpl.png', N'5349872565', N'qeqweqw@mail.com', N'qweqweqwe', 0);
SET IDENTITY_INSERT [dbo].[TBLPersonel] OFF;
GO

SET IDENTITY_INSERT [dbo].[TBLPersonelOdeme] ON;
INSERT INTO [dbo].[TBLPersonelOdeme] ([ID], [TARIH], [TUR], [ACIKLAMA], [TUTAR], [PERSONELID]) VALUES (1, '2025-11-07', N'MAAŞ', N'-', 1500.00, 3);
INSERT INTO [dbo].[TBLPersonelOdeme] ([ID], [TARIH], [TUR], [ACIKLAMA], [TUTAR], [PERSONELID]) VALUES (2, '2025-11-07', N'PRİM', N'-', 15000.00, 4);
INSERT INTO [dbo].[TBLPersonelOdeme] ([ID], [TARIH], [TUR], [ACIKLAMA], [TUTAR], [PERSONELID]) VALUES (3, '2025-11-07', N'AVANS', N'-', 1000.00, 4);
INSERT INTO [dbo].[TBLPersonelOdeme] ([ID], [TARIH], [TUR], [ACIKLAMA], [TUTAR], [PERSONELID]) VALUES (4, '2025-11-07', N'MAAŞ', N'-', 15000.00, 1);
INSERT INTO [dbo].[TBLPersonelOdeme] ([ID], [TARIH], [TUR], [ACIKLAMA], [TUTAR], [PERSONELID]) VALUES (5, '2025-11-07', N'MAAŞ', N'-', 2000.00, 5);
SET IDENTITY_INSERT [dbo].[TBLPersonelOdeme] OFF;
GO

SET IDENTITY_INSERT [dbo].[TBLURUNLER] ON;
INSERT INTO [dbo].[TBLURUNLER] ([Id], [UrunAdi], [Marka], [AlisFiyati], [SatisFiyati], [StokAdeti], [Kategori], [Firma], [Durum]) VALUES (1, N'Kahve Makinesi', N'Arçelik', 1500.00, 2750.00, 10, 1, 1, 1);
INSERT INTO [dbo].[TBLURUNLER] ([Id], [UrunAdi], [Marka], [AlisFiyati], [SatisFiyati], [StokAdeti], [Kategori], [Firma], [Durum]) VALUES (2, N'Laptop', N'Casper', 25000.00, 32500.00, 27, 2, 2, 1);
SET IDENTITY_INSERT [dbo].[TBLURUNLER] OFF;
GO

ALTER TABLE [dbo].[TBLCEKSENET] ADD CONSTRAINT [FK_TBLCEKSENET_TBLFIRMA] FOREIGN KEY ([Firma]) REFERENCES [dbo].[TBLFIRMA] ([FirmaID]);
GO
ALTER TABLE [dbo].[TBLFIRMAHAREKET] ADD CONSTRAINT [FK_TBLFIRMAHAREKET_TBLFIRMA] FOREIGN KEY ([Fırma]) REFERENCES [dbo].[TBLFIRMA] ([FirmaID]);
GO
ALTER TABLE [dbo].[TBLFIRMAHAREKET] ADD CONSTRAINT [FK_TBLFIRMAHAREKET_TBLURUNLER] FOREIGN KEY ([Urun]) REFERENCES [dbo].[TBLURUNLER] ([Id]);
GO
ALTER TABLE [dbo].[TBLFIRMAODEME] ADD CONSTRAINT [FK_TBLFIRMAODEME_TBLFIRMA] FOREIGN KEY ([Firma]) REFERENCES [dbo].[TBLFIRMA] ([FirmaID]);
GO
ALTER TABLE [dbo].[TBLMUSTERIBORC] ADD CONSTRAINT [FK_TBLMUSTERIBORC_TBLMUSTERI] FOREIGN KEY ([Musteri]) REFERENCES [dbo].[TBLMUSTERI] ([MusterıId]);
GO
ALTER TABLE [dbo].[TBLMUSTERIODEME] ADD CONSTRAINT [FK_TBLMUSTERIODEME_TBLMUSTERI] FOREIGN KEY ([Musteri]) REFERENCES [dbo].[TBLMUSTERI] ([MusterıId]);
GO
ALTER TABLE [dbo].[TBLPersonel] ADD CONSTRAINT [FK_TBLPersonel_TblDepartman] FOREIGN KEY ([DPMID]) REFERENCES [dbo].[TblDepartman] ([DEPID]);
GO
ALTER TABLE [dbo].[TBLPersonelOdeme] ADD CONSTRAINT [FK_TBLPersonelOdeme_TBLPersonel] FOREIGN KEY ([PERSONELID]) REFERENCES [dbo].[TBLPersonel] ([PRSNLID]);
GO
ALTER TABLE [dbo].[TBLURUNLER] ADD CONSTRAINT [FK_TBLURUNLER_TBLFIRMA] FOREIGN KEY ([Firma]) REFERENCES [dbo].[TBLFIRMA] ([FirmaID]);
GO
ALTER TABLE [dbo].[TBLURUNLER] ADD CONSTRAINT [FK_TBLURUNLER_TBLKATEGORI] FOREIGN KEY ([Kategori]) REFERENCES [dbo].[TBLKATEGORI] ([KategoriId]);
GO
