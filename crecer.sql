USE [master]
GO
/****** Object:  Database [crecer]    Script Date: 9/10/2019 09:38:25 ******/
CREATE DATABASE [crecer]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'crecer', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\crecer.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'crecer_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\crecer_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [crecer] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [crecer].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [crecer] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [crecer] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [crecer] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [crecer] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [crecer] SET ARITHABORT OFF 
GO
ALTER DATABASE [crecer] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [crecer] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [crecer] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [crecer] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [crecer] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [crecer] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [crecer] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [crecer] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [crecer] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [crecer] SET  ENABLE_BROKER 
GO
ALTER DATABASE [crecer] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [crecer] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [crecer] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [crecer] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [crecer] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [crecer] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [crecer] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [crecer] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [crecer] SET  MULTI_USER 
GO
ALTER DATABASE [crecer] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [crecer] SET DB_CHAINING OFF 
GO
ALTER DATABASE [crecer] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [crecer] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [crecer] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [crecer] SET QUERY_STORE = OFF
GO
USE [crecer]
GO
/****** Object:  Table [dbo].[tblCargo]    Script Date: 9/10/2019 09:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCargo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](64) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCategoria]    Script Date: 9/10/2019 09:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCategoria](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[nombre_categoria] [varchar](64) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCliente]    Script Date: 9/10/2019 09:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[nombre_compañia] [varchar](64) NULL,
	[direccion] [varchar](64) NULL,
	[telefono] [varchar](64) NULL,
	[email] [varchar](64) NULL,
	[ruta_imagen] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCompra]    Script Date: 9/10/2019 09:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCompra](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[id_proveedor] [int] NOT NULL,
	[id_empleado] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
	[id_metodopago] [int] NOT NULL,
	[fecha] [date] NOT NULL,
	[cantidad_compra] [int] NOT NULL,
	[precio_compra] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK__tblCompr__3213E83FA567D86A] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCxC]    Script Date: 9/10/2019 09:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCxC](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[id_venta] [int] NULL,
	[fecha_limite] [date] NULL,
	[abonado] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCxP]    Script Date: 9/10/2019 09:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCxP](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[id_compra] [int] NULL,
	[fecha_limite] [date] NULL,
	[abonado] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDevoluciones]    Script Date: 9/10/2019 09:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDevoluciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[id_compra] [int] NULL,
	[id_venta] [int] NULL,
	[cantidad] [int] NULL,
	[fecha] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblEmpleado]    Script Date: 9/10/2019 09:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmpleado](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[id_cargo] [int] NULL,
	[nombre] [varchar](64) NULL,
	[apellido] [varchar](64) NULL,
	[dui] [varchar](10) NULL,
	[nit] [varchar](17) NULL,
	[edad] [int] NULL,
	[direccion] [varchar](64) NULL,
	[telefono] [varchar](64) NULL,
	[email] [varchar](64) NULL,
	[ruta_imagen] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[dui] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[dui] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblKardex]    Script Date: 9/10/2019 09:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblKardex](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[id_producto] [int] NULL,
	[id_compra] [int] NULL,
	[id_venta] [int] NULL,
	[fecha] [date] NULL,
	[cantidad_inicial] [int] NULL,
	[id_devolucion] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblMetodoPago]    Script Date: 9/10/2019 09:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMetodoPago](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](64) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblProducto]    Script Date: 9/10/2019 09:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProducto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[id_categoria] [int] NULL,
	[nombre_producto] [varchar](64) NULL,
	[precio] [decimal](7, 2) NULL,
	[cantidad] [int] NULL,
	[imagen_producto] [varchar](255) NULL,
	[descripcion] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblProveedor]    Script Date: 9/10/2019 09:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProveedor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](64) NULL,
	[nombre_contacto] [varchar](128) NULL,
	[cargo_contacto] [varchar](64) NULL,
	[direccion] [varchar](64) NULL,
	[telefono] [varchar](64) NULL,
	[email] [varchar](64) NULL,
	[ruta_imagen] [varchar](255) NULL,
 CONSTRAINT [PK__tblProve__3213E83F46F9990B] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblVenta]    Script Date: 9/10/2019 09:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVenta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [int] NOT NULL,
	[id_empleado] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
	[id_metodopago] [int] NOT NULL,
	[fecha] [date] NOT NULL,
	[cantidad_venta] [int] NOT NULL,
	[precio_final] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK__tblVenta__3213E83F3C7980D8] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblCompra]  WITH CHECK ADD  CONSTRAINT [FK__tblCompra__id_em__4E88ABD4] FOREIGN KEY([id_empleado])
REFERENCES [dbo].[tblEmpleado] ([Id])
GO
ALTER TABLE [dbo].[tblCompra] CHECK CONSTRAINT [FK__tblCompra__id_em__4E88ABD4]
GO
ALTER TABLE [dbo].[tblCompra]  WITH CHECK ADD  CONSTRAINT [FK__tblCompra__id_pr__4F7CD00D] FOREIGN KEY([id_proveedor])
REFERENCES [dbo].[tblProveedor] ([Id])
GO
ALTER TABLE [dbo].[tblCompra] CHECK CONSTRAINT [FK__tblCompra__id_pr__4F7CD00D]
GO
ALTER TABLE [dbo].[tblCompra]  WITH CHECK ADD  CONSTRAINT [FK_tblCompra_tblMetodoPago] FOREIGN KEY([id_metodopago])
REFERENCES [dbo].[tblMetodoPago] ([Id])
GO
ALTER TABLE [dbo].[tblCompra] CHECK CONSTRAINT [FK_tblCompra_tblMetodoPago]
GO
ALTER TABLE [dbo].[tblCompra]  WITH CHECK ADD  CONSTRAINT [FK_tblCompra_tblProducto] FOREIGN KEY([id_producto])
REFERENCES [dbo].[tblProducto] ([Id])
GO
ALTER TABLE [dbo].[tblCompra] CHECK CONSTRAINT [FK_tblCompra_tblProducto]
GO
ALTER TABLE [dbo].[tblCxC]  WITH CHECK ADD FOREIGN KEY([id_venta])
REFERENCES [dbo].[tblVenta] ([Id])
GO
ALTER TABLE [dbo].[tblCxP]  WITH CHECK ADD  CONSTRAINT [FK_tblCxP_tblCompra] FOREIGN KEY([id_compra])
REFERENCES [dbo].[tblCompra] ([Id])
GO
ALTER TABLE [dbo].[tblCxP] CHECK CONSTRAINT [FK_tblCxP_tblCompra]
GO
ALTER TABLE [dbo].[tblDevoluciones]  WITH CHECK ADD FOREIGN KEY([id_compra])
REFERENCES [dbo].[tblCompra] ([Id])
GO
ALTER TABLE [dbo].[tblDevoluciones]  WITH CHECK ADD FOREIGN KEY([id_venta])
REFERENCES [dbo].[tblVenta] ([Id])
GO
ALTER TABLE [dbo].[tblEmpleado]  WITH CHECK ADD FOREIGN KEY([id_cargo])
REFERENCES [dbo].[tblCargo] ([Id])
GO
ALTER TABLE [dbo].[tblKardex]  WITH CHECK ADD FOREIGN KEY([id_devolucion])
REFERENCES [dbo].[tblDevoluciones] ([Id])
GO
ALTER TABLE [dbo].[tblKardex]  WITH CHECK ADD FOREIGN KEY([id_producto])
REFERENCES [dbo].[tblProducto] ([Id])
GO
ALTER TABLE [dbo].[tblProducto]  WITH CHECK ADD FOREIGN KEY([id_categoria])
REFERENCES [dbo].[tblCategoria] ([Id])
GO
ALTER TABLE [dbo].[tblVenta]  WITH CHECK ADD  CONSTRAINT [FK__tblVenta__id_cli__5AEE82B9] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[tblCliente] ([Id])
GO
ALTER TABLE [dbo].[tblVenta] CHECK CONSTRAINT [FK__tblVenta__id_cli__5AEE82B9]
GO
ALTER TABLE [dbo].[tblVenta]  WITH CHECK ADD  CONSTRAINT [FK__tblVenta__id_emp__5BE2A6F2] FOREIGN KEY([id_empleado])
REFERENCES [dbo].[tblEmpleado] ([Id])
GO
ALTER TABLE [dbo].[tblVenta] CHECK CONSTRAINT [FK__tblVenta__id_emp__5BE2A6F2]
GO
ALTER TABLE [dbo].[tblVenta]  WITH CHECK ADD  CONSTRAINT [FK_tblVenta_tblMetodoPago] FOREIGN KEY([id_metodopago])
REFERENCES [dbo].[tblMetodoPago] ([Id])
GO
ALTER TABLE [dbo].[tblVenta] CHECK CONSTRAINT [FK_tblVenta_tblMetodoPago]
GO
ALTER TABLE [dbo].[tblVenta]  WITH CHECK ADD  CONSTRAINT [FK_tblVenta_tblProducto] FOREIGN KEY([id_producto])
REFERENCES [dbo].[tblProducto] ([Id])
GO
ALTER TABLE [dbo].[tblVenta] CHECK CONSTRAINT [FK_tblVenta_tblProducto]
GO
/****** Object:  StoredProcedure [dbo].[getKardex]    Script Date: 9/10/2019 09:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getKardex] 
AS   
	OPEN ck;

	PRINT 'DAMN'
	CLOSE ck;
GO
USE [master]
GO
ALTER DATABASE [crecer] SET  READ_WRITE 
GO
