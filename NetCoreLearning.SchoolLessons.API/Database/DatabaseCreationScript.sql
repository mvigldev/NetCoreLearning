USE [master]
GO
/****** Object:  Database [SchoolLessons]    Script Date: 5/30/2019 10:43:54 AM ******/
CREATE DATABASE [SchoolLessons]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SchoolLessons', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER16\MSSQL\DATA\SchoolLessons.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SchoolLessons_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER16\MSSQL\DATA\SchoolLessons_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SchoolLessons] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SchoolLessons].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SchoolLessons] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SchoolLessons] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SchoolLessons] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SchoolLessons] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SchoolLessons] SET ARITHABORT OFF 
GO
ALTER DATABASE [SchoolLessons] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SchoolLessons] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SchoolLessons] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SchoolLessons] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SchoolLessons] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SchoolLessons] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SchoolLessons] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SchoolLessons] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SchoolLessons] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SchoolLessons] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SchoolLessons] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SchoolLessons] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SchoolLessons] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SchoolLessons] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SchoolLessons] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SchoolLessons] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SchoolLessons] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SchoolLessons] SET RECOVERY FULL 
GO
ALTER DATABASE [SchoolLessons] SET  MULTI_USER 
GO
ALTER DATABASE [SchoolLessons] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SchoolLessons] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SchoolLessons] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SchoolLessons] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SchoolLessons] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SchoolLessons', N'ON'
GO
ALTER DATABASE [SchoolLessons] SET QUERY_STORE = OFF
GO
USE [SchoolLessons]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [SchoolLessons]
GO
/****** Object:  Table [dbo].[EventLogs]    Script Date: 5/30/2019 10:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventLogs](
	[EventId] [uniqueidentifier] NOT NULL,
	[EventTypeName] [nvarchar](128) NOT NULL,
	[State] [tinyint] NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[TimesSent] [int] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_EventLogs_1] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GradeAssignments]    Script Date: 5/30/2019 10:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GradeAssignments](
	[Id] [uniqueidentifier] NOT NULL,
	[ProfessorId] [uniqueidentifier] NOT NULL,
	[StudentId] [uniqueidentifier] NOT NULL,
	[LessonId] [uniqueidentifier] NOT NULL,
	[Grade] [char](1) NOT NULL,
 CONSTRAINT [PK_GradeAssignments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lessons]    Script Date: 5/30/2019 10:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lessons](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Lessons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Professors]    Script Date: 5/30/2019 10:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Professors](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Professors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 5/30/2019 10:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'21fc639b-0075-45f2-9aeb-018c8b1b5138', N'Lesson 2')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'9b2f0250-1912-4e07-9d7c-024bf960deae', N'Lesson 6')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'dd8148e7-491b-4a2b-aff5-09781d278ba5', N'Lesson 36')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'528fccec-8d72-48f7-aa01-1748633967a2', N'Lesson 35')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'3480b92e-ec3d-4c41-a138-18541dab5bd5', N'Lesson 30')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'f5e6f96f-ab0a-4659-9176-1c6eb194cc3d', N'Lesson 20')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'2b5c33b2-8af9-4a2b-a482-216a1c40ccac', N'Lesson 1')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'389ea227-a1f2-43d0-9d60-294e3a2f922a', N'Lesson 15')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'5e67f72a-cbf5-4622-aa19-2fda05c5c6ba', N'Lesson 19')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'218544cd-08ed-454f-895d-30bd4498c731', N'Lesson 26')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'64af9fc6-71cc-4b15-b221-39def9af1d31', N'Lesson 14')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'd45c43fe-dcd6-4222-8867-4bca914afd3e', N'Lesson 4')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'6318ffb8-4320-4b6e-9141-53ad9f50b7df', N'Lesson 38')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'a4966c72-0d84-4b71-811d-56f1704493ba', N'Lesson 28')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'015e41c1-3344-4d92-9272-6590fb095376', N'Lesson 11')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'844d5754-36bd-48e2-b402-65da7305ad34', N'Lesson 17')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'b5b40016-9dfc-4f38-8a5f-7134c72adcc5', N'Lesson 43')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'54ee2b1e-f741-4368-bf4c-7c02a7a1b737', N'Lesson 32')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'29d68398-dd8c-4849-a0e7-7f5329c65cf2', N'Lesson 21')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'cd1237e2-35bd-4580-a44a-82800873ea7e', N'Lesson 41')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'b7264e0b-bf7e-46b4-8517-85d3323f1a9d', N'Lesson 46')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'7c68e713-c58b-45f6-9db4-889170c041cd', N'Lesson 49')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'f9dbaab1-96c6-46c9-961b-8aca84b1de99', N'Lesson 9')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'04cbf7c4-551d-443c-b166-8bf832c4d53f', N'Lesson 40')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'f79d211c-12fc-4da3-9330-8d982a1b2e7e', N'Lesson 7')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'f31fda72-b161-4d60-818b-986bd7246f0b', N'Lesson 42')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'10bad492-273f-444e-a96a-a107be26007b', N'Lesson 10')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'b4472bfb-9b1e-49d6-af41-a883039a5bdf', N'Lesson 12')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'f452dd17-a95d-43a7-b3e5-aa0333036667', N'Lesson 33')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'38118479-2052-482e-b6fb-af9b09705032', N'Lesson 27')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'3386ab8f-e1f3-4df0-a854-b142a76c6fae', N'Lesson 29')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'5d514d8b-8058-46ae-bc91-b8c50172552a', N'Lesson 22')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'9699ab24-f2b9-4389-9615-b8d6786ba95b', N'Lesson 24')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'dbdc1526-402b-42ea-80fa-bb2aa0a223e6', N'Lesson 13')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'77091608-02e8-4759-af78-be89907af1f9', N'Lesson 23')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'a3eec36a-317c-4b23-bbe5-c62b563522ab', N'Lesson 47')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'f9e1e33e-f354-403d-8bc5-d02320c1cf89', N'Lesson 5')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'0e8d5bdb-d0ff-4031-90a2-d1040b658717', N'Lesson 31')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'67d90dbf-5d4d-4705-8c78-d21904ef8a3e', N'Lesson 37')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'f7404f35-eafa-4509-9f2c-d33ea81bb219', N'Lesson 39')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'fb71ac99-0e6c-45d0-bf35-d67104694edd', N'Lesson 45')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'7a23374f-284a-42b0-9750-d7ab15845413', N'Lesson 44')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'03953a78-1252-49d7-bd03-db791c0dbe48', N'Lesson 50')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'122e95da-8ee5-4cd4-8348-e7156e09a688', N'Lesson 16')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'f5d12d3c-b829-4755-9a6c-e8bc4eb814bc', N'Lesson 25')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'2afe9fcb-6fb3-449c-9db8-efbc64ae6559', N'Lesson 18')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'13dd2bb2-f768-42c2-a49d-f17f0ec4b91d', N'Lesson 8')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'a16ec052-a10d-4d9b-ac77-f1e2d7ac5e6c', N'Lesson 34')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'89aeec18-26b1-4aab-868f-feadd66391b4', N'Lesson 3')
GO
INSERT [dbo].[Lessons] ([Id], [Name]) VALUES (N'c8358d0a-177c-4255-b5c5-ff9deb8ed794', N'Lesson 48')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'36024276-8c1c-4ef1-a26a-0f0770de4ef1', N'Professor 5')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'4236b7d7-3800-46be-8ccd-1127983a9306', N'Professor 1')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'023f78f0-9294-4669-b285-21f9a8a0259c', N'Professor 2')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'57f76017-c365-4197-bc41-292313399e3c', N'Professor 3')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'f8275fb9-26bf-4b28-90ab-2e81559ee26d', N'Professor 4')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'57cc6859-cc0d-4875-b198-2f3e9aa9f0e9', N'Professor 6')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'7e6ff83e-12e8-4635-81de-5bb04c3e0566', N'Professor 7')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'551be203-dfc8-4bb7-b018-5f4491eb8a72', N'Professor 20')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'73443df6-1c3d-420e-927f-6a1c1cf08012', N'Professor 16')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'79e0f613-fe4c-4636-8c8b-7be14e88f475', N'Professor 18')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'f6b4f2a0-c628-4bfb-9510-875e3eed3439', N'Professor 8')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'a4b88ddb-cd4a-4c52-af43-8dc9cfaff55a', N'Professor 9')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'74d4c316-0c7c-465d-8b4a-a99b0544608c', N'Professor 15')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'fc984a61-2bf8-4135-a971-c0c56c2febdd', N'Professor 17')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'457a0100-8cd7-40f1-814c-c3913899082e', N'Professor 14')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'45fe03eb-4372-4688-9501-c392c75ae335', N'Professor 13')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'3f1a37cd-b99c-480f-9a34-eba339ba940d', N'Professor 12')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'48e7663c-2ce9-451d-aa39-f40f9605d219', N'Professor 11')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'ed6e5d2e-627a-4060-bc93-f7094e731c42', N'Professor 10')
GO
INSERT [dbo].[Professors] ([Id], [Name]) VALUES (N'1cb12275-3508-4950-8401-f97ff2af3429', N'Professor 19')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd80a9191-dc43-47c0-b0bb-0033d8f88bca', N'Student 113')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1e991039-fd7d-49d3-98f1-0054cff1fb01', N'Student 183')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'0ff038e5-3132-4878-afff-00ea24645dda', N'Student 245')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'6ebc4f64-c1bc-4560-967c-017ef531086b', N'Student 14')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'851813ff-a608-4d94-bb0e-020ca3b31a8b', N'Student 483')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'3bbe73e3-9473-424e-bf93-034929108352', N'Student 361')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'568df2bb-f582-4939-84c7-0353e214b414', N'Student 336')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'9034da3c-0709-4055-b75f-03a1368bdc7d', N'Student 254')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'bd1f25a2-1128-4243-a3c6-03a6d82246db', N'Student 216')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'6630b028-7e39-4d8a-b67c-042d1498daeb', N'Student 172')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'3109f51f-ac45-4638-a098-063e4092ed37', N'Student 284')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e2cf7e7e-01c6-45f2-a542-064898aa3462', N'Student 405')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8d91389f-e0a7-40fa-ace4-06ef04445f33', N'Student 259')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'35122957-b3e9-4923-a6ca-07143bd0282c', N'Student 307')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4451838d-b80d-40d8-a476-071a3a13851a', N'Student 130')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2474674b-4fc8-4a72-94da-07312a5ca989', N'Student 412')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'dd04fda3-1f59-4500-9bc4-0761ede78039', N'Student 363')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'6cca6715-80c2-4942-a438-07e688cc772b', N'Student 344')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ddb355be-f09a-406c-871a-091e392094f6', N'Student 490')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'525a3a3c-1cc5-4e38-8ed0-09fdb87c4eeb', N'Student 63')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'289252b5-52a9-498e-acf9-0a88ed7d4561', N'Student 277')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'50ddc110-822e-4ed1-b7dd-0a9470b75317', N'Student 313')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'042b115b-e929-4abd-a092-0ae2de2be96b', N'Student 407')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'5c30680c-f8e0-43bc-924a-0bd8c96f1c0d', N'Student 159')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4aa4a1c3-cfa6-4223-9263-0bdf16b61276', N'Student 101')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a5236c29-81f5-4925-9c30-0bfae4cda3fa', N'Student 297')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'9f359fb8-dffe-4ae6-99a0-0c341a9556b1', N'Student 165')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a4774ffe-9205-449d-8bdc-0c7279261877', N'Student 463')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ca45e315-38ba-4e69-a7ff-0c860c69f5f6', N'Student 429')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4cbc891d-5968-4554-a229-0d7f031e4644', N'Student 365')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e4dc53a5-aedb-4450-ad1f-0e4a9e136e5d', N'Student 220')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'5826a0c0-014b-4f0e-b5b9-1192d2debe36', N'Student 76')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b00d8622-0ba3-4d2a-bb1d-122c09d97775', N'Student 187')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd56602b9-4353-4f3f-b2aa-1267e868176b', N'Student 194')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'5a0fd75d-76de-4d3c-ae23-13d84bd9a7be', N'Student 402')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e975ebf0-c7a9-40b8-96d8-142a3422a611', N'Student 431')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'12d55b1b-2888-4170-be86-14d63f9ddbcb', N'Student 270')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8bc2e2fc-a220-4666-91f7-151ef4c1a578', N'Student 156')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'41acc67f-7048-4975-9b56-15bebd3d2c16', N'Student 324')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e436b1e3-3d61-4fe4-8bb4-16f9731a75cf', N'Student 102')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'03bf795f-5a51-4c1b-9898-1702acf1abb9', N'Student 406')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'0912b917-1de7-46d3-9b26-17a8613813cd', N'Student 106')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'accc81da-17ed-4825-a185-17c0501e335e', N'Student 15')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'6ee28eef-a1c0-43a6-add2-1847259a1a75', N'Student 441')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8b16cae8-e2ee-49d8-8c95-1a810a342458', N'Student 457')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'99c0fad0-a277-4eca-82b1-1af1fc45e372', N'Student 255')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'050bfbac-05ea-4d06-9334-1c10e48bb727', N'Student 206')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'cbc36c7f-6aef-4f03-84a9-1d6bf7f821e6', N'Student 104')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a913b201-d624-42b0-bb27-1f5b7b936ced', N'Student 403')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'abd45da7-44ca-473c-944a-1f5c6b427b13', N'Student 285')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'95f3f945-adb2-4023-8e42-1f77a3a88616', N'Student 52')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f8a3b037-5c9e-42cb-9b5f-1f966ab96a63', N'Student 499')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f6891c37-5159-4249-a8f3-1f9c97b58353', N'Student 23')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a7f038f0-2ebd-46c3-9ad6-20179cffabac', N'Student 438')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'cbd41be6-65cd-44c7-b5f6-202736a0d296', N'Student 3')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f12b0067-2dd2-4965-be8e-203af69be4dc', N'Student 105')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'6489bd0b-addb-4c84-842d-2079d9f6d772', N'Student 136')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'09ba2239-9634-40a9-9cb6-212236d003c5', N'Student 481')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'40b79b55-c015-4cee-963c-21dfda74acec', N'Student 265')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c0fb8e96-89d0-4a1e-9436-21f23243188b', N'Student 371')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1a25cfba-ca87-4991-b74d-223713b6326c', N'Student 334')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'fc171713-d260-483c-98cd-224e324a44d9', N'Student 124')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'de78179a-2ebd-48e3-a4e3-226c0b13a855', N'Student 108')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ec1c6160-edc6-43dd-86dc-22ff2f46150b', N'Student 437')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'bbc415b1-bba7-414f-9c91-230cc08ed35f', N'Student 306')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e3978435-3460-4817-b5be-231c266e55c0', N'Student 70')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'03df5f89-b5e3-418e-9098-23949f9bf90e', N'Student 339')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'10e8bf6a-32bc-4ab8-8c7c-2394b65677f9', N'Student 305')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'9c64858a-b86e-49fc-9cc1-23f278abc812', N'Student 71')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c02fa3a6-b074-4e8d-801c-24b04b35db54', N'Student 411')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd4a9d2c1-39e5-4a66-b42c-2601c8f0f753', N'Student 68')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'bb6acbe8-d67c-46c9-bfef-26092c603d09', N'Student 10')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'16c6aba6-2f47-45b9-b530-264841dc0723', N'Student 39')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c4d15386-7544-40e2-af62-2648cb3cc093', N'Student 484')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'5bae2f3f-8a09-49de-b0a5-27dc46d2746c', N'Student 103')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'97a6bf36-6eb2-440a-8ec9-27f022d851cb', N'Student 498')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e811bd9a-d356-4eec-9b0e-28ebaf570431', N'Student 6')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8cde0fec-43d7-41b0-814e-28f507788911', N'Student 356')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'17c0cdd6-b082-4110-9528-2936f6a75da1', N'Student 445')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f9c811a5-fdd2-4d46-9643-299e0057c609', N'Student 211')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'99fd199c-7fe4-4f39-8e0c-29bc072f3cef', N'Student 34')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'fd9e25ff-6fad-444f-96af-2b4b7283db43', N'Student 30')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'783b2119-9186-4543-9744-2bdd449c7b4f', N'Student 119')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'64308ade-3de6-48a1-8d8b-2d08f49297ea', N'Student 467')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a55696e3-e5d9-4a4e-80cc-3082fdd965ce', N'Student 283')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'afb4c15b-5aed-4df6-ab01-3219e4a223f7', N'Student 224')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'babdc2e4-21b6-4186-a407-3246613d4356', N'Student 235')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ce80c7ab-9538-45d0-a396-3292ba81cad7', N'Student 140')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'db29e785-11ef-4658-ad08-32c8bc66d039', N'Student 58')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'814fe39c-c4bf-470e-9250-336f164436aa', N'Student 362')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a3929ff4-6656-4ebd-9785-34641fde2fbd', N'Student 222')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'87d7d753-10d6-4778-b987-3580cf7887c9', N'Student 296')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8dfba5db-99c0-486d-8338-35892fd29c01', N'Student 213')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'713337ad-297a-42da-885e-35bfa6595f0f', N'Student 94')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4e7f1d17-a90d-4473-97cd-364ac548f4a5', N'Student 346')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b897be57-3951-4ec6-8ce2-36514a132a80', N'Student 146')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a474c16b-e655-42e9-ac4b-370dd4a2fbea', N'Student 476')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd59d0a0a-93dd-4e7a-b6a2-38c43c435b2c', N'Student 201')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'dbc41afb-bc1b-4674-b2de-3a70c928fd30', N'Student 229')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c4253f7c-6d21-4562-aa75-3b2215621805', N'Student 326')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2a038a6a-31e5-428a-803d-3c0ac9cd2707', N'Student 421')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'79dcdedb-151c-43aa-b454-3c4a1a7ed8dd', N'Student 48')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'eb21258c-68d7-49db-a386-3c8ac160455d', N'Student 295')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c49f5dbb-bc0e-4c79-8f4b-3c999e5b4ef2', N'Student 301')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1317db38-9854-471b-b789-3d6475fe0939', N'Student 345')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ffda4d9b-72a4-49d5-a9c7-3df9c4c2072a', N'Student 99')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a4aefbbd-3170-4fa8-a046-3e6a52e717b1', N'Student 88')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'532ee5b6-e8bd-4ab9-908d-3ecc0ea1654b', N'Student 400')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd515e154-ff8f-4d02-a259-3f058241274e', N'Student 51')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'fd71436d-0dd8-41c0-863b-3f2828902a03', N'Student 427')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b64118e6-2edc-4597-bbe7-3fce8e78dc69', N'Student 73')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'3802c2f3-2011-4a92-9c6f-3fd987224214', N'Student 264')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c92fdf32-b149-4901-951a-4181e4cf25a1', N'Student 60')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ce10edb1-17ae-4ca8-a5d8-41da5be7bbbe', N'Student 300')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'372628a5-ffb2-45af-88dd-424804cccc0c', N'Student 424')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'be12e742-cc8f-49e5-abcc-43af1753aa46', N'Student 83')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a3b7780b-a4d2-4205-9256-44400d33f22a', N'Student 367')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4714c184-fdb6-4e91-8031-44442b00fadb', N'Student 20')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b34c66e5-94e7-492c-b2a2-44aee251cad3', N'Student 459')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'837c6451-b2e0-44c3-8a34-45571e379a47', N'Student 267')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b5c416c1-02dc-40ec-a049-455b99364451', N'Student 290')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'260b3371-d144-4afb-8a66-45b518ac9bf1', N'Student 186')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'6ad571a3-8fa0-4836-9e96-45dd694a7836', N'Student 494')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'94d00862-d1ef-4f03-b444-46db497be98c', N'Student 157')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ee2007f2-a969-485f-ad6a-4784915b9861', N'Student 452')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b1532c67-1614-4d2a-ba86-4830b1682bbf', N'Student 333')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c5342855-b5ce-4d81-a0cf-48969af3c6da', N'Student 111')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'62c238a5-7c4f-4dc6-adb0-48d1eb090323', N'Student 358')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'742a1f8e-3e87-4668-9992-496b2a0b569a', N'Student 302')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'85ac4fe4-28a0-4fd5-9014-4abbea30decf', N'Student 180')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e8862534-1b4c-4408-b5ba-4affc4ef8554', N'Student 409')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd7f98cd8-13ba-454b-bf63-4b3c2e104516', N'Student 204')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a37cf0f7-b0cb-4063-ac2c-4ba0a555a9cd', N'Student 171')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'5d3d0865-1c5d-4327-96ac-4c0e6977471d', N'Student 132')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'7f76eced-2b9b-4ef4-a2b5-4c4ed6922752', N'Student 210')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'5f38158d-cd61-4fa2-a7d7-4c5be5d08f64', N'Student 240')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'28a9950c-3ed5-4cf7-a5eb-4d7ee0636fb0', N'Student 57')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'534a0c3b-ebd3-4dc6-915a-4dbd792cb04e', N'Student 46')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'54a0df52-5409-4694-9f87-4de757d27c03', N'Student 144')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ad9dbc78-4eb3-4293-83d8-4e175f8f1e1d', N'Student 316')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'542bfc31-4e31-470c-8778-4e3582cb8d7c', N'Student 212')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a238fd36-60ae-417f-99e8-4e5fa08f63b3', N'Student 420')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'5f3026f0-a4f1-44d3-842b-5053e6b178c6', N'Student 141')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c078f5ac-a456-4e9f-81de-507f9aed90ef', N'Student 380')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd6a25b6f-fc55-492f-9f1c-51b35e0da491', N'Student 423')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'7d2b979e-2b97-416b-a29e-522ce0955691', N'Student 397')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'da83c41b-cc0b-4c72-97c7-526e1f5d56da', N'Student 2')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'242396a4-689b-4cdd-9bc9-52d88f14925e', N'Student 319')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'9552f832-03f4-49af-ae95-52ec250d9678', N'Student 115')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f26c3dd4-84fa-4553-bcf3-530859368872', N'Student 381')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'bb5bd83e-000d-44e9-b902-53cceac63514', N'Student 238')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b9cf9702-3976-4d28-9688-53df2d00f69d', N'Student 422')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd9a6c377-bdfe-41be-af82-545f2f1f1803', N'Student 274')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'39132b2f-2143-457f-966f-553abea050d2', N'Student 493')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'59cf21ae-86c4-4206-8e8e-562912e610af', N'Student 12')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'fc68e689-0573-47c3-8811-567d83f1ab3d', N'Student 42')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8233362e-9bbb-47bc-91b6-56c0359b8721', N'Student 458')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'64ee0f22-5896-473b-9c4e-56c6d919cae2', N'Student 262')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f25b7201-250b-458a-8b2d-57c917af2fcf', N'Student 465')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'7be00d8b-fa7a-4766-a7b2-57ca2bbe0091', N'Student 497')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'719818b5-0c3e-461d-a1da-57f6b8f633fd', N'Student 310')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'65687f38-1739-4cb9-8456-5851827f50e9', N'Student 366')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'429386a3-3020-4219-ad94-59820b5a27c1', N'Student 415')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b32e876b-3224-41f2-b44a-5b0ec3a93aaa', N'Student 203')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2fa38fce-ddd6-4d51-b172-5bb1fb6db3c1', N'Student 460')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd0613b8b-bc44-458d-a730-5bc8d4a668eb', N'Student 428')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e4eca63e-bc6d-4281-a257-5d0840c30757', N'Student 13')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'fb3e2bb7-d593-468d-bd5c-5d86b88276e4', N'Student 315')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'78c1cc0d-01a5-410e-9130-5e683f4acbc4', N'Student 218')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'53507bff-ec1f-425b-bc42-5e7f43d0a524', N'Student 82')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f6cedce9-d89d-4ddb-8fdd-5f1702194460', N'Student 387')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'43e9dbb0-9aaa-4e5b-9afd-5fd13ed29465', N'Student 17')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b8ff8c53-6471-4322-80f8-5fda0481badb', N'Student 170')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'55a3674f-6e40-473d-947d-601b8cb3f79d', N'Student 158')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'491d0900-c8a7-4864-a721-601f24b902fa', N'Student 462')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'51f727cd-c629-444e-b369-6090a4581de9', N'Student 167')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'3cac9600-b0fe-4f6b-a3f3-612c3e07f021', N'Student 24')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'03f45698-d1fc-4022-9517-6132999f0385', N'Student 351')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2e9bd845-c644-4c3f-920a-61530d4cce1c', N'Student 239')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'fdee5503-9010-41fe-8e4b-61d2d69d6df4', N'Student 237')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'0b891ab1-97db-4216-a8d8-62c1e29692c1', N'Student 434')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b6d44f9d-8a6a-4f8a-a705-64de839814ab', N'Student 443')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ea005d71-5d8a-4f6c-930a-64e9c4d1f946', N'Student 129')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'31f4e017-b474-474a-980c-6661d0ed4ca7', N'Student 496')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8178551a-241a-4a88-8c50-668c7797487d', N'Student 161')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'29119022-4781-457d-8809-671e312c7535', N'Student 372')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'517b435d-3362-4b16-99d5-67debf437818', N'Student 11')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2299f60c-1b83-4522-8688-681ad92e37be', N'Student 414')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2bd6beed-d3de-4b3f-8d91-68e83ab5bfb9', N'Student 163')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ddf25b84-a247-4169-b2a1-68f087eb8752', N'Student 227')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'9edfb547-a96e-481a-8ea8-691eb4c9b987', N'Student 299')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'76c7f38c-3406-409b-826b-691faa52e1b3', N'Student 349')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1b367e1d-cc8a-4f31-81b3-69674a8664b5', N'Student 359')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c4ebe675-2d1d-4692-9899-6a51ac3d73ae', N'Student 81')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'44629102-1802-4e9f-97d2-6b5dda3dccb1', N'Student 219')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1ada0a6e-9a6a-44ff-b936-6ba8d2c233e6', N'Student 472')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'feba6abc-ee76-44fe-91f9-6c398df135df', N'Student 257')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'68098acf-983b-494a-aeb3-6efcd1714bc7', N'Student 232')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b841e76e-a368-417a-a049-6f05249c7d77', N'Student 47')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'adb9542f-fb0f-4e72-a811-70e4bccf853d', N'Student 449')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'261be174-5bf1-4805-b436-71066d549877', N'Student 260')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'0c0aa290-e1c0-498f-aa53-71a46f57b9c9', N'Student 482')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd23b0248-a13b-4aaf-8bae-71e921a7dd5a', N'Student 276')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b041b717-7893-4d11-b379-73344bd802cf', N'Student 248')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'7bc9c5dc-9ee9-499f-83bd-735d1865a262', N'Student 291')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'355e09e0-6d2b-48b3-b635-742c862ded83', N'Student 35')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'7c6bf4a1-35ac-4a3a-9c62-746fb2f1168a', N'Student 151')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'bd77430e-779d-45c4-bcef-74cb1d52ea9c', N'Student 142')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'14ad1bef-37cf-4365-83ff-7560877fe2e4', N'Student 487')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'10080c05-d5e0-479a-ab93-75a15f00bc68', N'Student 32')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1f7d0221-e450-4313-8a62-7937e34caea7', N'Student 278')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f1007892-40b0-41d8-afc9-796803a2d36e', N'Student 97')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4b6e4173-44d3-4b3f-bacd-7a9f7f13bed7', N'Student 143')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'fed69021-56ab-4686-8533-7aaa11bd0254', N'Student 396')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a474d410-f37d-4398-b99d-7add20629c6a', N'Student 18')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'597e21de-1d3a-487c-a153-7af75d081ce9', N'Student 31')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8b81b6e5-a243-403b-9da1-7b0861d1c176', N'Student 473')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'58d63e10-29b9-4814-bdb6-7b2a60ee3689', N'Student 320')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'7cdb7104-d9e0-43e9-9e48-7c4a3b3c1dee', N'Student 65')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8b0570db-5332-47f2-874a-7cca15c2ae0b', N'Student 249')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'528571c6-ae0e-4379-af0a-7e3af7c339f4', N'Student 440')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1cc76b5f-950b-42ad-8623-7ea8bce6cd7b', N'Student 62')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e4809365-2c6c-4661-afd3-80433ecc2a13', N'Student 45')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a67ba1e6-9567-46de-a26a-8195729e8866', N'Student 379')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'59ad6c9f-8f90-446f-a64f-836c0e8e17e3', N'Student 477')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'38be2579-a3f2-4935-998d-84fd29964cfe', N'Student 341')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ddba6872-7d98-45df-ada8-854ddf3878d4', N'Student 325')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'61d49ea0-528e-4c2d-8e84-8623cbb255b3', N'Student 1')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'fbea9766-dea4-4de9-9852-8635ce6f556d', N'Student 78')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'34c4907e-cce5-47af-a308-86631e992ade', N'Student 469')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8420d3f9-45b8-442e-849f-86e81710ad9c', N'Student 337')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'33d3b910-52d7-4cca-b31d-86faa07d4dde', N'Student 332')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8e2057c8-8372-43b4-beb8-8726f62072e4', N'Student 117')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd4ad5dd5-9a00-4c15-b36b-87c3a69fc3ce', N'Student 40')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4c9324ec-5bca-421d-bce2-87cd8607205a', N'Student 392')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'47afe086-df56-409b-95d7-88425f2cf500', N'Student 66')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'9d95c78c-f739-4187-9941-8885297400cc', N'Student 395')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2f8c3c48-8d15-4e68-a0a8-88adcc3195b8', N'Student 217')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'68371991-7e9e-427d-af7e-89619f318cc7', N'Student 199')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1f43a72f-ab8e-4c47-a2c1-896e1982ca93', N'Student 25')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'94566613-fcbb-460c-8bb4-89b52649a3af', N'Student 114')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'095b2886-0296-4747-9857-8a0942ad5e9f', N'Student 153')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'09b5284b-c7af-41f2-b7b1-8a4287d068c1', N'Student 294')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd2b0a85c-9597-4e34-8987-8ac120caf436', N'Student 190')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'3570d80b-a2b5-4953-8548-8ace6fd11838', N'Student 133')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'96769cdc-76d2-4259-88dc-8b3676e18367', N'Student 374')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2fe2b4bb-5c53-42ef-84ad-8b4f54200e6a', N'Student 311')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'5f305e08-7eb4-46a1-aa83-8b618e27fb61', N'Student 174')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'60f3cf0e-d4d6-4f5a-a6b1-8baa13e6a9ed', N'Student 61')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2b4be173-97a7-4d9d-9952-8bd96c46baa3', N'Student 252')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'cbaff51b-1042-4578-bba7-8c322ce76aab', N'Student 425')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'fb286101-d9df-45d1-86c3-8c5a30f0f42d', N'Student 468')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'0915cd8c-aafa-4204-bd77-8c600b9c016d', N'Student 244')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a44262df-ca6a-487a-b652-8d47a725c6c9', N'Student 80')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'43034402-a4e9-437d-8f80-8d8df9f49b0b', N'Student 9')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c9998eec-6091-46f1-be8b-8e41b8e0b255', N'Student 128')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'726f20b7-f556-4007-be48-8fa65ba2bbef', N'Student 470')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ba239b4b-e66d-49ad-b588-8fc59e26a88e', N'Student 226')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'18059488-9424-45a9-95f8-9008ab63e9cb', N'Student 272')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2517ac3f-1094-44b3-9b45-90a42cd4c122', N'Student 16')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'77b4a98b-6012-4540-8d8f-91992a949887', N'Student 418')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'7657bbe7-1ff1-45db-8362-91e21889157f', N'Student 388')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'bb048419-c625-4e39-9243-92ebaedcff95', N'Student 419')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'6c237bac-7fc7-40a8-94c9-92f9494152fb', N'Student 456')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'fe212f09-2a2a-4d63-91dd-933670e70845', N'Student 191')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c5a160d9-9df9-4ce7-8f96-93fec17698a7', N'Student 471')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f90ffd8f-dbe7-462f-9ef5-94f8c0400dd2', N'Student 84')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'bd8b8021-61b6-4574-9d54-9538b0af76f9', N'Student 230')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2d7b082e-7ea2-4b3e-9fca-9773f958ee21', N'Student 169')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'3ad4ef35-c6ac-44c1-b782-977ebcf01511', N'Student 454')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'457cc79a-95c6-43d8-95c8-9829f06b7805', N'Student 179')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'70777b5b-a495-4530-873d-9846a2f24ff6', N'Student 178')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'bef7584e-fc8a-4c6d-a88a-988f6c47f66a', N'Student 398')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'7f955174-1e10-4694-8cfc-99779cbf8a71', N'Student 343')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b3866b3a-8b7c-4f49-b9d5-99939bfe5aa9', N'Student 342')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'dce17d05-dc26-44b6-bff0-9a6e97665765', N'Student 486')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'49d587aa-bf8a-4e24-bde8-9a739706c791', N'Student 192')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'59757372-581e-400a-9079-9abcebe62f5d', N'Student 85')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'dadb54f8-eb61-4f03-b18b-9ac94246102a', N'Student 189')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'eaa600eb-e1b3-45b0-8f27-9b18d290a59f', N'Student 109')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f7bf1660-b30e-424a-a373-9b64b5f77dc6', N'Student 200')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1a76800d-8d59-4aa3-a3b8-9b6d659a0505', N'Student 214')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'5d2f9d89-1642-4088-8448-9b8a529a3f05', N'Student 488')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1eeda331-f89f-4248-9c82-9c54428521f3', N'Student 394')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ba676eb7-7931-4329-8206-9cb49b01db14', N'Student 480')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'03518d70-35d8-44a7-a806-9cd6a1017fd6', N'Student 150')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e5f4b49f-8b2e-4413-a8d9-9d37b2ed258e', N'Student 182')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e24af44e-0bf8-43ff-8139-9ea9540bfdfc', N'Student 304')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'6b8a6aca-7871-438f-9fad-9ede6485d888', N'Student 491')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'af42cdfb-1d06-41e6-b3a2-9eeda08d9d3f', N'Student 275')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'0b060b36-8c9f-4b24-b2b0-9ef8cb7ca2f3', N'Student 375')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'6ca4857e-03cd-42fa-922a-9f3d7ad63889', N'Student 198')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'037a8077-a85d-40cd-83e5-a02fb93830ef', N'Student 357')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1889e655-3a6d-44f4-aa21-a0a8ea00f06d', N'Student 430')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'09f7d904-7b8a-41cd-b207-a0e666e18fb3', N'Student 148')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'97637d73-e15f-4456-9eac-a15e6c61e0ac', N'Student 376')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'3b94c358-20f3-433d-8f9f-a2095f0a451a', N'Student 125')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4ee9c5c5-7991-4a96-bca0-a2d23f0ec05b', N'Student 225')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b0cc502a-038d-45b5-8a31-a2fc909d2b74', N'Student 250')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'dd9ce4ad-cced-4aa7-a117-a365269576ba', N'Student 479')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'10492768-c38c-49c2-9761-a3eba24f086d', N'Student 258')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'3ec19303-6baa-47f8-a9d7-a494d36570ad', N'Student 417')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2eda1ed3-c65b-4f03-98cf-a49fc0a37177', N'Student 279')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'491769c3-e707-487e-88c3-a4f27110a9c1', N'Student 202')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2a2420bf-6d81-4851-bfd2-a602d5644948', N'Student 49')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd3e4357f-1adb-4eab-8d7b-a60d0e0f2137', N'Student 160')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'dd42d821-f9dc-491d-b03e-a64f9f0d8263', N'Student 149')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c5845603-fe62-40b0-9ee5-a657947ec4a3', N'Student 122')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'147d99bf-35de-4f26-ae2e-a6bfcb07c1e3', N'Student 432')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'146e4ee3-0c8f-4125-9f32-a70750b4d3a0', N'Student 205')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'80699cdd-8dc9-46b4-8582-a72f8e06502e', N'Student 123')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4a23f956-e43d-43fc-9aca-a8fc70270f7c', N'Student 489')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'513ac037-2f59-4cde-9310-a972a25386e8', N'Student 184')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4423ef40-afa9-49f7-a0ea-a9b3fed9ab0c', N'Student 53')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a46f7550-4af2-4b72-b170-aa0b8eee4946', N'Student 228')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2a77c5a3-3253-472f-ad36-aa56243cd9d4', N'Student 256')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b3f8a67c-f87b-4435-94c4-aa8e338b7b3a', N'Student 354')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'08b46cf2-d854-445b-a9b0-ab5ce85c6c6a', N'Student 389')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1b3c07fa-f348-43ea-9d32-ab860917639a', N'Student 347')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8b41d197-8f4b-4f95-a5c0-ab94d4c6aa7c', N'Student 120')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'5edad6bf-cd2b-426d-b627-ad4753b93af0', N'Student 288')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a512c69a-6a16-4dcc-a11e-ae15dd927718', N'Student 91')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'05e910e8-17dc-4af7-95a7-ae412ee175d3', N'Student 408')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'fafde05a-adfe-4a59-8300-ae9f7a13f113', N'Student 474')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'cf016741-40f9-4d7c-9c92-affffeb5cd52', N'Student 197')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1b8615a9-5a48-4614-b92d-b065b1a73b90', N'Student 382')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a44e8d67-8c8c-4d2e-bbd6-b1815eaa79cb', N'Student 413')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'cb10c18a-abda-4009-b1c5-b1d0dfbf26a7', N'Student 37')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1d535fa2-885d-43f5-a470-b29f0c4f4f81', N'Student 22')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'0deffd0a-3149-4e55-bb6c-b33a64f5032e', N'Student 4')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'cb2e7360-af51-494b-a34e-b52aa10f7d6b', N'Student 426')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'43477cdb-20cc-4d3e-8c40-b58b45d97a00', N'Student 112')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'97c77e51-e697-4073-b960-b5980da66341', N'Student 54')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e0faecae-1fed-41c1-a85f-b5be90e95f8b', N'Student 286')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e07cb85a-e550-422d-a28f-b6cee1e014da', N'Student 74')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'dda139a8-94b6-4ee9-8b09-b709b9b626b4', N'Student 327')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'5e56ff6a-a6bf-4457-a6f7-b7187ff4758d', N'Student 384')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e59c5470-5fb1-496f-aba1-b75de295b218', N'Student 193')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2bb4aad4-17b7-4c77-9ab7-b80c324aa0fb', N'Student 338')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'47894f74-6e6c-443d-8109-b81753ed147a', N'Student 292')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'67950245-f2dc-43fa-9f26-b99323bfb994', N'Student 251')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2f07a87d-c778-44f1-8c60-b9d87003905b', N'Student 100')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a420e725-dc64-46d4-86f4-ba51091aac6d', N'Student 410')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a2e14138-013e-4b75-b5a8-ba7c6a7e6dba', N'Student 41')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ec169a89-7cac-4183-b0b5-bb042f96411d', N'Student 26')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e790e132-2da4-4f44-8571-bb1daf0fa04f', N'Student 404')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b973c9ee-4443-4edc-a588-bb24edd12aa7', N'Student 436')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4f8a91ae-50c7-4210-af72-bb79c33ecadb', N'Student 162')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'af149dca-2427-4dae-8528-bbccb8e9bb72', N'Student 329')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f0b0f7bd-efa2-45ae-883c-bc2172061acf', N'Student 475')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd57217dd-f374-4129-9d09-bc2f91e98591', N'Student 177')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'0b6ece53-3541-4049-8190-bc815c2d973f', N'Student 466')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd9a50225-92b3-45f4-afd4-bc8b8e2540f9', N'Student 135')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'eddc9b34-c364-4ba1-a171-bcb89397454b', N'Student 393')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c114db34-5f93-40db-80ab-bd38084ad889', N'Student 383')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e9f3878b-0898-41cf-9d56-bd93cbd69867', N'Student 21')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'420c39a9-94fe-40b0-b952-bd9e6a40d6f9', N'Student 147')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e785e2eb-4520-4d20-b0f1-be6e0c3a017a', N'Student 455')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'62dd6d9b-14f5-4312-aa32-bf57350ee97e', N'Student 263')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e52738bf-776c-457f-8581-bfa341b4285f', N'Student 234')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'cf937abd-7c5b-47c2-abe8-bfcc85265018', N'Student 355')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'9fcb22ce-2798-49dc-9638-c02a79db2127', N'Student 330')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e967a5ea-da0a-4506-a007-c06018fc1289', N'Student 64')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ea38b706-63de-4a1e-b47c-c0b9c1ad72f0', N'Student 96')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a9c8b437-452f-46b4-b6be-c1333f977ae6', N'Student 451')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'3cccb54f-beb3-48ed-8d72-c163f2938111', N'Student 373')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'3c70a6fa-7e3c-4fed-8289-c2219d9ce300', N'Student 195')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'7d2f3b51-3763-4ba0-8795-c2249555b99e', N'Student 446')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'52f4ebbd-0377-4d00-ab81-c2546b097772', N'Student 261')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'cb3b0960-32f0-4e0d-a1f7-c2cdaa783055', N'Student 318')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1fde9671-adcf-4fa4-a743-c2d8a7abac03', N'Student 131')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'35b29980-18e2-4fed-a8d0-c2ebc9ed6068', N'Student 293')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c9496762-562c-4587-b0a6-c316d45483ba', N'Student 77')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'840a42b1-2df9-4f0b-9e2d-c36c29147815', N'Student 8')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f9e8b27e-365a-4b9f-984b-c481e26c35ea', N'Student 289')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'5da9567d-3743-4f13-b801-c48c6f0ec151', N'Student 56')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'3eef6225-397b-47ca-9c5d-c49062273b72', N'Student 331')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'79d6695d-6950-4d25-bfc1-c5cd1153eb0f', N'Student 90')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'430feef8-bb53-4b0c-b8b7-c9374db4a43a', N'Student 309')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'288759bb-248d-4e40-a7cf-c9d8d5f2f920', N'Student 348')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a9717491-8d8e-4f44-8ac2-ca99cc23db06', N'Student 36')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'3464f57f-a23b-44e0-82fe-cac3523b493e', N'Student 221')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e7d2be86-7a4b-4d05-825b-cad9a1ec105a', N'Student 72')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd5f66a3a-9fac-43f0-9405-caeb7563eedf', N'Student 399')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'002cfee8-f77a-41df-9360-cbc8963dbbcc', N'Student 442')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'50405e74-f19a-49b5-b17f-cc80f639ce68', N'Student 215')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a3ebf861-aeb3-485a-8ec1-cceae8ee7534', N'Student 28')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'33016c57-1882-4535-b7cb-cdbdc8196d3b', N'Student 154')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'546c782d-829f-415a-bf33-ceb8536e7dcc', N'Student 69')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'26d70c83-e205-4a9d-a8c0-cef5f2518081', N'Student 433')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'065cd20e-1bfb-44a9-912c-cfb05e10a712', N'Student 368')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1cbbbd19-c13c-4bf2-b6e8-cfdce54f6a39', N'Student 335')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'eeaf4ad1-48dd-4562-8263-d0205c27cb2c', N'Student 209')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'cf64a6d6-8d51-4eeb-bdd1-d02e7aad9d17', N'Student 303')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'99dc3459-107f-4fef-a7b3-d0ce1ce6051a', N'Student 5')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f0437ec6-0bfa-4b8d-b904-d0dbd6da51e1', N'Student 353')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'9216c782-c7fe-4f9c-b4a0-d2f0cdb629c4', N'Student 317')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'0759b1c3-fd68-4a08-8153-d3333b098868', N'Student 121')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8716a8cc-81be-4250-8271-d3c62226f52b', N'Student 322')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'95a97881-4831-4cb4-a990-d45747e2a22b', N'Student 246')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'eb790b45-168f-4ddf-864f-d62f983f77db', N'Student 188')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e9e98dd3-078a-4b7e-977f-d66260aee8c9', N'Student 67')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'377321f8-db45-4a7b-b81f-d754eec63a4c', N'Student 231')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'54c6ce13-ff7e-4ab1-bcbf-d82220d8801f', N'Student 207')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8b1d9208-ba93-48f7-b2c8-d91032a43899', N'Student 447')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ea20725f-2886-4836-b150-d92356f98a0c', N'Student 79')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2e3bbb93-c37b-474a-9554-d971eab10e3c', N'Student 152')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'425c47e0-b7cd-4625-b0dd-da3084c4ebac', N'Student 308')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4944154c-9039-4b02-a841-db2ea5b58e85', N'Student 145')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'012e7437-4ba6-46b7-9f19-dc64f0f92a1b', N'Student 241')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'87531945-b8a1-4cbd-b559-dc89e914267d', N'Student 314')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'99150282-1170-4fbb-adf0-dd2d4f461ea6', N'Student 377')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'24a8fdf6-e5bb-4048-b4af-dd66ba88d4a6', N'Student 266')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8929807a-a325-4574-a37b-dd79dd2a277e', N'Student 164')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'58ae2ab4-e375-4c9c-b306-ddb3db763d14', N'Student 87')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'91737685-d1b8-4827-a106-ddc279af2f02', N'Student 312')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'36862829-b514-4763-b21f-ddde37e5c850', N'Student 378')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'048cfc96-0795-4459-abbe-df819e9ef51b', N'Student 323')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1436d1ab-53ef-42f6-bdfa-dfa278de7a4a', N'Student 298')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c09bf8a1-4284-41ea-9bb9-e016ade8b952', N'Student 247')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd33e8261-e12b-4760-9e09-e093115bdec7', N'Student 401')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a395aea5-1b00-4dc7-9c3e-e0964f4b4834', N'Student 59')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'5dc5b50f-5c93-4789-bd2b-e14f47650b26', N'Student 453')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a327374c-838c-4ac5-886e-e182347f8ec5', N'Student 176')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4511e50e-be0c-405a-bb83-e18469147af0', N'Student 282')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'39053003-e132-4042-87f5-e20d5ecb0eaa', N'Student 175')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'491b3abf-560f-4a4b-ac58-e218f0d8bf62', N'Student 110')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e5f6fe85-a3ed-4aa9-89a4-e23d74dee147', N'Student 95')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'bd29b7ce-71f8-471e-8030-e3a46ef38c66', N'Student 435')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'cd2ee2a0-a9c9-414c-bca2-e458d753f707', N'Student 385')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'248b9fe4-5c63-4f2c-960e-e4c5a95f7908', N'Student 271')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'42a98441-0283-4f2c-b710-e5223e59ec76', N'Student 444')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8a40f71d-231b-4f8c-85b0-e531afa4177d', N'Student 478')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'878a5436-110a-48c9-8d83-e5dc02c315dd', N'Student 273')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'faa78fcd-f9fd-4967-8bb6-e6e93c8523ed', N'Student 98')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2e49a282-5611-4362-b876-e70c8eed829b', N'Student 116')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'eef210fb-c384-4ac3-805e-e8484240dffa', N'Student 233')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e763f24b-7f21-479f-98d3-e84c17cdcaec', N'Student 92')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4c078835-bc42-4fa9-a9e3-e8b93cf9d2b7', N'Student 185')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'76331305-7592-4660-aac3-e91713c855cd', N'Student 352')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'70111245-df99-4f35-b1c9-e9465a753d2c', N'Student 75')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'264c9614-2b92-40b4-93d3-eb28846ef68b', N'Student 44')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f6f49617-ed76-4514-a56f-eb3e2675c79b', N'Student 166')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1a31ffef-e961-4e0c-a84f-eb58db89df78', N'Student 340')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'a6266abe-b5e1-4959-88e3-ebab9cb9a8bf', N'Student 416')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ef211624-2fad-4532-bba2-ebc2333b29c2', N'Student 223')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b8f7f3b6-53c1-4874-b873-ec6844a05212', N'Student 350')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'd8b0141b-e5bb-452b-a034-ecb80f2eabad', N'Student 450')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'6fb725fa-9293-4d4f-8f8e-eccbf749e36b', N'Student 269')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e5519096-acef-4c3d-a1b9-ecd5d85035db', N'Student 321')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'3b385205-a006-4988-bbc6-ed117c086083', N'Student 196')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b9cd6ade-a61f-4e91-b327-ed36fead4ea1', N'Student 364')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4410c1f6-9d9e-4714-a6ae-ed5f78bcff41', N'Student 495')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'6bf3e4cb-9417-43b0-9e94-ed9107bd417e', N'Student 89')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'cc6fbc6c-6a85-4646-8ed7-edc8c6e931bb', N'Student 370')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4752596e-d30f-400b-8c50-edd820f65e19', N'Student 287')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'fb8930c6-4b23-484c-8f3a-eddc94fe8844', N'Student 118')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'0bb67855-df75-4945-bcaf-edf454ceff4d', N'Student 369')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f3da078e-bbb4-424d-8a45-ee5cd0634b20', N'Student 328')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'48e8724f-0b23-43ef-9193-ee92f9f23045', N'Student 173')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'87d6078d-d564-498c-9853-eedc9f443319', N'Student 29')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'05db0c0f-c36f-42f9-af24-ef5cc0572a75', N'Student 208')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'fd8637ee-1e98-4e61-926a-ef9059816c3b', N'Student 236')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'165c014b-5572-4f04-9c61-f0f3416f474a', N'Student 19')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4dee75ad-a11d-4e64-a8fe-f18f420eb701', N'Student 464')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4fdb33fa-c3e9-4bca-afcc-f280f0366083', N'Student 280')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c69c3ae6-f54c-4d83-b11c-f3230ec20b88', N'Student 281')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'e9539e76-0cd4-4e27-a797-f3a39f84796c', N'Student 7')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'15c21f4f-9773-4718-8fb0-f46e77e8f529', N'Student 386')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'febc5f54-822a-4bf3-b14d-f4cced5f2a37', N'Student 138')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2103acfa-1b3a-4055-972b-f4d78dc7ccfa', N'Student 360')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'8f153327-9433-4b33-a353-f52c4f6e0724', N'Student 439')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'9b55f143-49a2-4639-b710-f6bfd52aaacd', N'Student 448')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'7a376719-203b-45b1-9cb6-f7dc66dc1438', N'Student 139')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c8623cac-0d61-41d1-a868-f83b31a1a350', N'Student 155')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'eadf7af9-da4a-4c4a-9224-f8502e55eb3d', N'Student 27')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'761742b1-71de-48b9-bce4-f876c1b5aec3', N'Student 55')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'13c9b424-1aa8-4ebf-8783-f88bb70bfc40', N'Student 43')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'5f907b19-c777-4e9b-805b-f88db5afc51c', N'Student 137')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b31bbe41-2eca-43d0-843a-f8d145177bf8', N'Student 50')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'155a173f-3933-401f-a372-f9d4a3ec5854', N'Student 33')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'cdc4c619-1faf-42a4-b866-fa31b36bb121', N'Student 93')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'0e4f85f0-2611-4a96-b56d-fa3dfe03414a', N'Student 390')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'10e9425f-4020-4afa-ba4e-fa95148a28e8', N'Student 500')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'242c491f-c617-4c58-9713-fae2b09155dc', N'Student 134')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'3d03898e-2322-4b2c-bfbd-fb69584f77c9', N'Student 107')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'b4883567-e2f4-495c-88a5-fbf2e3bdd8c7', N'Student 168')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2bb5bc61-014a-4b6a-b2a5-fc07b21cacef', N'Student 268')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c9e3540d-8f2a-406f-bc53-fc271f0fb968', N'Student 253')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'ae83920a-2018-4fb3-8a6b-fc2d30b58848', N'Student 461')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'c72c04ae-173e-4f35-9995-fc60b778afac', N'Student 181')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'417402ac-eacd-4c5a-a808-fc8884ecf788', N'Student 86')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'2e769b39-2819-48a1-afe5-fd7430884812', N'Student 485')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'1a6802df-1927-4cdb-b77c-fd7dfbff8e14', N'Student 127')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'4552ece4-d78e-4739-886b-fdd12e7713da', N'Student 492')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'589a8b3b-11de-433a-9ee2-fde23c671e28', N'Student 391')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'0a9dde48-5bde-41d0-ab31-ff49c80e6856', N'Student 126')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'f6a59a0d-b9df-4672-afba-ff8cbee56f1b', N'Student 38')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'7fedf093-49aa-4d92-93f9-ffa483baace2', N'Student 242')
GO
INSERT [dbo].[Students] ([Id], [Name]) VALUES (N'0b2d4136-9a9d-4181-8e08-ffd4ffcfccd4', N'Student 243')
GO
ALTER TABLE [dbo].[GradeAssignments]  WITH CHECK ADD  CONSTRAINT [FK_GradeAssignments_Lessons] FOREIGN KEY([LessonId])
REFERENCES [dbo].[Lessons] ([Id])
GO
ALTER TABLE [dbo].[GradeAssignments] CHECK CONSTRAINT [FK_GradeAssignments_Lessons]
GO
ALTER TABLE [dbo].[GradeAssignments]  WITH CHECK ADD  CONSTRAINT [FK_GradeAssignments_Professors] FOREIGN KEY([ProfessorId])
REFERENCES [dbo].[Professors] ([Id])
GO
ALTER TABLE [dbo].[GradeAssignments] CHECK CONSTRAINT [FK_GradeAssignments_Professors]
GO
ALTER TABLE [dbo].[GradeAssignments]  WITH CHECK ADD  CONSTRAINT [FK_GradeAssignments_Students] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([Id])
GO
ALTER TABLE [dbo].[GradeAssignments] CHECK CONSTRAINT [FK_GradeAssignments_Students]
GO
USE [master]
GO
ALTER DATABASE [SchoolLessons] SET  READ_WRITE 
GO
