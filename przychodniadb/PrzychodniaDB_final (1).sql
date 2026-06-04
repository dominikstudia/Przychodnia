USE [master]
GO

/****** Object:  Database [Przychodnia]    Script Date: 03.05.2026 19:48:56 ******/
CREATE DATABASE [Przychodnia]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Przychodnia', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL17.SQLEXPRESS\MSSQL\DATA\Przychodnia.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Przychodnia_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL17.SQLEXPRESS\MSSQL\DATA\Przychodnia_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Przychodnia] SET COMPATIBILITY_LEVEL = 170
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Przychodnia].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Przychodnia] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Przychodnia] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Przychodnia] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Przychodnia] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Przychodnia] SET ARITHABORT OFF 
GO
ALTER DATABASE [Przychodnia] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Przychodnia] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Przychodnia] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Przychodnia] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Przychodnia] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Przychodnia] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Przychodnia] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Przychodnia] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Przychodnia] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Przychodnia] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Przychodnia] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Przychodnia] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Przychodnia] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Przychodnia] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Przychodnia] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Przychodnia] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Przychodnia] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Przychodnia] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Przychodnia] SET  MULTI_USER 
GO
ALTER DATABASE [Przychodnia] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Przychodnia] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Przychodnia] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Przychodnia] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Przychodnia] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Przychodnia] SET OPTIMIZED_LOCKING = OFF 
GO
ALTER DATABASE [Przychodnia] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Przychodnia] SET QUERY_STORE = ON
GO
ALTER DATABASE [Przychodnia] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

USE [Przychodnia]
GO

/****** Object:  Table [dbo].[Doctors]    Script Date: 03.05.2026 19:48:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[DoctorID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[SpecializationID] [int] NOT NULL,
CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED 
(
	[DoctorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Patients]    Script Date: 03.05.2026 19:48:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[PatientID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Roles]    Script Date: 03.05.2026 19:48:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Rooms]    Script Date: 03.05.2026 19:48:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[RoomID] [int] IDENTITY(1,1) NOT NULL,
	[RoomNumber] [varchar](10) NOT NULL,
CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Specializations]    Script Date: 03.05.2026 19:48:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specializations](
	[SpecializationID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
CONSTRAINT [PK_Specializations] PRIMARY KEY CLUSTERED 
(
	[SpecializationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[UserRoles]    Script Date: 03.05.2026 19:48:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 03.05.2026 19:48:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[PasswordHash] [varchar](255) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[PESEL] [varchar](11) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[Gender] [int] NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[City] [varchar](100) NOT NULL,
	[PostalCode] [varchar](10) NOT NULL,
	[Street] [varchar](100) NULL,
	[HouseNumber] [varchar](10) NOT NULL,
	[ApartmentNumber] [varchar](10) NULL,
	[IsArchived] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[FailedLoginAttempts] [int] NOT NULL,
	[BlockedUntil] [datetime] NULL,
	[WymagaZmianyHasla] [bit] NOT NULL,
CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[PasswordHistory]    Script Date: 03.05.2026 20:35:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PasswordHistory](
	[PasswordHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[PasswordHash] [varchar](255) NOT NULL,
	[ModifiedAt] [datetime] NOT NULL,
CONSTRAINT [PK_PasswordHistory] PRIMARY KEY CLUSTERED 
(
	[PasswordHistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[VisitResults]    Script Date: 03.05.2026 19:48:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitResults](
	[ResultID] [int] IDENTITY(1,1) NOT NULL,
	[VisitID] [int] NOT NULL,
	[Symptoms] [varchar](1000) NULL,
	[Diagnosis] [varchar](1000) NULL,
	[Recommendations] [varchar](1000) NULL,
	[Medicines] [varchar](1000) NULL,
CONSTRAINT [PK_VisitResults] PRIMARY KEY CLUSTERED 
(
	[ResultID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Visits]    Script Date: 03.05.2026 19:48:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visits](
	[VisitID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[DoctorID] [int] NOT NULL,
	[RoomID] [int] NOT NULL,
	[VisitDateTime] [datetime] NOT NULL,
	[Status] [varchar](50) NOT NULL,
CONSTRAINT [PK_Visits] PRIMARY KEY CLUSTERED 
(
	[VisitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Doctors] ON 
INSERT [dbo].[Doctors] ([DoctorID], [UserID], [SpecializationID]) VALUES (2, 2, 6)
SET IDENTITY_INSERT [dbo].[Doctors] OFF
GO

SET IDENTITY_INSERT [dbo].[Patients] ON 
INSERT [dbo].[Patients] ([PatientID], [UserID]) VALUES (1, 3)
SET IDENTITY_INSERT [dbo].[Patients] OFF
GO

SET IDENTITY_INSERT [dbo].[Roles] ON 
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1, N'Administrator')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (2, N'Lekarz')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (4, N'Pacjent')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (3, N'Recepcjonista')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO

SET IDENTITY_INSERT [dbo].[Rooms] ON 
INSERT [dbo].[Rooms] ([RoomID], [RoomNumber]) VALUES (1, N'101')
INSERT [dbo].[Rooms] ([RoomID], [RoomNumber]) VALUES (2, N'102')
INSERT [dbo].[Rooms] ([RoomID], [RoomNumber]) VALUES (3, N'103')
INSERT [dbo].[Rooms] ([RoomID], [RoomNumber]) VALUES (4, N'201')
INSERT [dbo].[Rooms] ([RoomID], [RoomNumber]) VALUES (5, N'202')
SET IDENTITY_INSERT [dbo].[Rooms] OFF
GO

SET IDENTITY_INSERT [dbo].[Specializations] ON 
INSERT [dbo].[Specializations] ([SpecializationID], [Name]) VALUES (10, N'Dermatologia')
INSERT [dbo].[Specializations] ([SpecializationID], [Name]) VALUES (6, N'Kardiologia')
INSERT [dbo].[Specializations] ([SpecializationID], [Name]) VALUES (8, N'Neurologia')
INSERT [dbo].[Specializations] ([SpecializationID], [Name]) VALUES (9, N'Pediatria')
INSERT [dbo].[Specializations] ([SpecializationID], [Name]) VALUES (7, N'Stomatologia')
SET IDENTITY_INSERT [dbo].[Specializations] OFF
GO

INSERT [dbo].[UserRoles] ([UserID], [RoleID]) VALUES (1, 1)
INSERT [dbo].[UserRoles] ([UserID], [RoleID]) VALUES (2, 2)
INSERT [dbo].[UserRoles] ([UserID], [RoleID]) VALUES (3, 4)
GO

SET IDENTITY_INSERT [dbo].[Users] ON 
INSERT [dbo].[Users] ([UserID], [Login], [PasswordHash], [Email], [FirstName], [LastName], [PESEL], [BirthDate], [Gender], [Phone], [City], [PostalCode], [Street], [HouseNumber], [ApartmentNumber], [IsArchived], [CreatedAt], [FailedLoginAttempts], [BlockedUntil], [WymagaZmianyHasla]) VALUES (1, N'admin', N'admin123', N'admin@clinic.pl', N'System', N'Administrator', N'00000000000', CAST(N'1970-01-01' AS Date), 1, N'123456789', N'Warszawa', N'00-000', NULL, N'1', NULL, 0, CAST(N'2026-03-16T22:31:45.900' AS DateTime), 0, NULL, 0)
INSERT [dbo].[Users] ([UserID], [Login], [PasswordHash], [Email], [FirstName], [LastName], [PESEL], [BirthDate], [Gender], [Phone], [City], [PostalCode], [Street], [HouseNumber], [ApartmentNumber], [IsArchived], [CreatedAt], [FailedLoginAttempts], [BlockedUntil], [WymagaZmianyHasla]) VALUES (2, N'doctor1', N'doctor123', N'doctor1@clinic.pl', N'Jan', N'Kowalski', N'88010112345', CAST(N'1988-01-01' AS Date), 1, N'111222333', N'Warszawa', N'00-002', N'Lekarska', N'20', NULL, 0, CAST(N'2026-03-16T22:34:24.153' AS DateTime), 0, NULL, 0)
INSERT [dbo].[Users] ([UserID], [Login], [PasswordHash], [Email], [FirstName], [LastName], [PESEL], [BirthDate], [Gender], [Phone], [City], [PostalCode], [Street], [HouseNumber], [ApartmentNumber], [IsArchived], [CreatedAt], [FailedLoginAttempts], [BlockedUntil], [WymagaZmianyHasla]) VALUES (3, N'patient1', N'patient123', N'patient1@clinic.pl', N'Anna', N'Nowak', N'99010112345', CAST(N'1999-01-01' AS Date), 0, N'222333444', N'Warszawa', N'00-001', N'Kwiatowa', N'10', N'5', 0, CAST(N'2026-03-16T22:38:18.913' AS DateTime), 0, NULL, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO

SET IDENTITY_INSERT [dbo].[PasswordHistory] ON 
INSERT [dbo].[PasswordHistory] ([PasswordHistoryID], [UserID], [PasswordHash], [ModifiedAt]) VALUES (1, 1, N'admin123', CAST(N'2026-03-16T22:31:45.900' AS DateTime))
INSERT [dbo].[PasswordHistory] ([PasswordHistoryID], [UserID], [PasswordHash], [ModifiedAt]) VALUES (2, 2, N'doctor123', CAST(N'2026-03-16T22:34:24.153' AS DateTime))
INSERT [dbo].[PasswordHistory] ([PasswordHistoryID], [UserID], [PasswordHash], [ModifiedAt]) VALUES (3, 3, N'patient123', CAST(N'2026-03-16T22:38:18.913' AS DateTime))
SET IDENTITY_INSERT [dbo].[PasswordHistory] OFF
GO

SET IDENTITY_INSERT [dbo].[VisitResults] ON 
INSERT [dbo].[VisitResults] ([ResultID], [VisitID], [Symptoms], [Diagnosis], [Recommendations], [Medicines]) VALUES (2, 2, N'Ból glowy', N'Migrena', N'Odpoczynek i nawodnienie', N'Ibuprofen')
SET IDENTITY_INSERT [dbo].[VisitResults] OFF
GO

SET IDENTITY_INSERT [dbo].[Visits] ON 
INSERT [dbo].[Visits] ([VisitID], [PatientID], [DoctorID], [RoomID], [VisitDateTime], [Status]) VALUES (2, 1, 2, 1, CAST(N'2026-04-10T10:00:00.000' AS DateTime), N'Zarejestrowana')
SET IDENTITY_INSERT [dbo].[Visits] OFF
GO

/****** Object:  Index [UQ_Doctors_UserID]    Script Date: 03.05.2026 19:48:57 ******/
ALTER TABLE [dbo].[Doctors] ADD  CONSTRAINT [UQ_Doctors_UserID] UNIQUE NONCLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Patients_UserID]    Script Date: 03.05.2026 19:48:57 ******/
ALTER TABLE [dbo].[Patients] ADD  CONSTRAINT [UQ_Patients_UserID] UNIQUE NONCLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Roles_RoleName]    Script Date: 03.05.2026 19:48:57 ******/
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [UQ_Roles_RoleName] UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Rooms_RoomNumber]    Script Date: 03.05.2026 19:48:57 ******/
ALTER TABLE [dbo].[Rooms] ADD  CONSTRAINT [UQ_Rooms_RoomNumber] UNIQUE NONCLUSTERED 
(
	[RoomNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Specializations_Name]    Script Date: 03.05.2026 19:48:57 ******/
ALTER TABLE [dbo].[Specializations] ADD  CONSTRAINT [UQ_Specializations_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Users_PESEL]    Script Date: 03.05.2026 19:48:57 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ_Users_PESEL] UNIQUE NONCLUSTERED 
(
	[PESEL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Users_Login]    Script Date: 03.05.2026 19:48:57 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ_Users_Login] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Users_Email]    Script Date: 03.05.2026 19:48:57 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ_Users_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

/****** Object:  Index [UQ_VisitResults_VisitID]    Script Date: 03.05.2026 19:48:57 ******/
ALTER TABLE [dbo].[VisitResults] ADD  CONSTRAINT [UQ_VisitResults_VisitID] UNIQUE NONCLUSTERED 
(
	[VisitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

/****** Object:  Index [IX_PasswordHistory_User_ModifiedAt]    Script Date: 03.05.2026 20:35:00 ******/
CREATE NONCLUSTERED INDEX [IX_PasswordHistory_User_ModifiedAt] ON [dbo].[PasswordHistory]
(
	[UserID] ASC,
	[ModifiedAt] DESC,
	[PasswordHistoryID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsArchived] DEFAULT ((0)) FOR [IsArchived]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedAt] DEFAULT (getdate()) FOR [CreatedAt]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_FailedLoginAttempts] DEFAULT ((0)) FOR [FailedLoginAttempts]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_WymagaZmianyHasla] DEFAULT ((0)) FOR [WymagaZmianyHasla]
GO

ALTER TABLE [dbo].[PasswordHistory] ADD  CONSTRAINT [DF_PasswordHistory_ModifiedAt] DEFAULT (getdate()) FOR [ModifiedAt]
GO

ALTER TABLE [dbo].[Visits] ADD  CONSTRAINT [DF_Visits_Status] DEFAULT ('Zarejestrowana') FOR [Status]
GO

ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD  CONSTRAINT [FK_Doctors_Specializations] FOREIGN KEY([SpecializationID])
REFERENCES [dbo].[Specializations] ([SpecializationID])
GO
ALTER TABLE [dbo].[Doctors] CHECK CONSTRAINT [FK_Doctors_Specializations]
GO

ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD  CONSTRAINT [FK_Doctors_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Doctors] CHECK CONSTRAINT [FK_Doctors_Users]
GO

ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_Users]
GO

ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO

ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO

ALTER TABLE [dbo].[PasswordHistory]  WITH CHECK ADD  CONSTRAINT [FK_PasswordHistory_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[PasswordHistory] CHECK CONSTRAINT [FK_PasswordHistory_Users]
GO

ALTER TABLE [dbo].[VisitResults]  WITH CHECK ADD  CONSTRAINT [FK_VisitResults_Visits] FOREIGN KEY([VisitID])
REFERENCES [dbo].[Visits] ([VisitID])
GO
ALTER TABLE [dbo].[VisitResults] CHECK CONSTRAINT [FK_VisitResults_Visits]
GO

ALTER TABLE [dbo].[Visits]  WITH CHECK ADD  CONSTRAINT [FK_Visits_Doctors] FOREIGN KEY([DoctorID])
REFERENCES [dbo].[Doctors] ([DoctorID])
GO
ALTER TABLE [dbo].[Visits] CHECK CONSTRAINT [FK_Visits_Doctors]
GO

ALTER TABLE [dbo].[Visits]  WITH CHECK ADD  CONSTRAINT [FK_Visits_Patients] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patients] ([PatientID])
GO
ALTER TABLE [dbo].[Visits] CHECK CONSTRAINT [FK_Visits_Patients]
GO

ALTER TABLE [dbo].[Visits]  WITH CHECK ADD  CONSTRAINT [FK_Visits_Rooms] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Rooms] ([RoomID])
GO
ALTER TABLE [dbo].[Visits] CHECK CONSTRAINT [FK_Visits_Rooms]
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [CK_Users_Gender] CHECK  (([Gender]=(1) OR [Gender]=(0)))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK_Users_Gender]
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [CK_Users_PESEL_Length] CHECK  ((len([PESEL])=(11)))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK_Users_PESEL_Length]
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [CK_Users_Phone_Length] CHECK  ((len([Phone])=(9)))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK_Users_Phone_Length]
GO

/****** Object:  Trigger [dbo].[TR_PasswordHistory_KeepLast3]    Script Date: 03.05.2026 20:40:00 ******/
CREATE TRIGGER [dbo].[TR_PasswordHistory_KeepLast3]
ON [dbo].[PasswordHistory]
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;

	WITH RankedPasswords AS
	(
		SELECT
			ph.PasswordHistoryID,
			ROW_NUMBER() OVER (
				PARTITION BY ph.UserID
				ORDER BY ph.ModifiedAt DESC, ph.PasswordHistoryID DESC
			) AS RowNumber
		FROM [dbo].[PasswordHistory] ph
		WHERE ph.UserID IN (SELECT DISTINCT UserID FROM inserted)
	)
	DELETE ph
	FROM [dbo].[PasswordHistory] ph
	INNER JOIN RankedPasswords rp ON rp.PasswordHistoryID = ph.PasswordHistoryID
	WHERE rp.RowNumber > 3;
END
GO

USE [master]
GO

ALTER DATABASE [Przychodnia] SET  READ_WRITE 
GO

