USE [TestAuthorization]
GO
/****** Object:  Table [dbo].[TRole]    Script Date: 8/17/2016 12:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT ((1)),
	[CreatedDate] [datetime] NOT NULL DEFAULT ('2000/01/01'),
	[UpdatedBy] [int] NOT NULL DEFAULT ((1)),
	[UpdatedDate] [datetime] NOT NULL DEFAULT ('2000/01/01'),
 CONSTRAINT [PK_TRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TUser]    Script Date: 8/17/2016 12:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRole] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Password] [varchar](32) NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT ((1)),
	[CreatedDate] [datetime] NOT NULL DEFAULT ('2000/01/01'),
	[UpdatedBy] [int] NOT NULL DEFAULT ((1)),
	[UpdatedDate] [datetime] NOT NULL DEFAULT ('2000/01/01'),
 CONSTRAINT [PK_TUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TView]    Script Date: 8/17/2016 12:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TView](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdParent] [varchar](250) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[GroupName] [nvarchar](50) NULL,
 CONSTRAINT [PK_TView] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TViewAccess]    Script Date: 8/17/2016 12:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TViewAccess](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdView] [int] NOT NULL,
	[IdRole] [int] NOT NULL,
	[AccessRight] [int] NOT NULL,
 CONSTRAINT [PK_TAccessibility] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[TRole] ON 

INSERT [dbo].[TRole] ([Id], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Admins', 1, CAST(N'2016-10-10 00:00:00.000' AS DateTime), 1, CAST(N'2016-10-10 00:00:00.000' AS DateTime))
INSERT [dbo].[TRole] ([Id], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Quest', 1, CAST(N'2016-10-10 00:00:00.000' AS DateTime), 1, CAST(N'2016-10-10 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[TRole] OFF
SET IDENTITY_INSERT [dbo].[TUser] ON 

INSERT [dbo].[TUser] ([Id], [IdRole], [Name], [Password], [Status], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, 1, N'Huy', N'123456789', 0, 1, CAST(N'2016-08-16 19:03:59.437' AS DateTime), 1, CAST(N'2016-08-16 19:03:59.437' AS DateTime))
INSERT [dbo].[TUser] ([Id], [IdRole], [Name], [Password], [Status], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, 2, N'Quest', N'123456789', 0, 1, CAST(N'2000-01-01 00:00:00.000' AS DateTime), 1, CAST(N'2000-01-01 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[TUser] OFF
SET IDENTITY_INSERT [dbo].[TView] ON 

INSERT [dbo].[TView] ([Id], [IdParent], [Name], [GroupName]) VALUES (1, N'1', N'Home', N'Home')
SET IDENTITY_INSERT [dbo].[TView] OFF
SET IDENTITY_INSERT [dbo].[TViewAccess] ON 

INSERT [dbo].[TViewAccess] ([Id], [IdView], [IdRole], [AccessRight]) VALUES (1, 1, 1, 2)
INSERT [dbo].[TViewAccess] ([Id], [IdView], [IdRole], [AccessRight]) VALUES (2, 1, 2, 1)
SET IDENTITY_INSERT [dbo].[TViewAccess] OFF
ALTER TABLE [dbo].[TUser]  WITH CHECK ADD  CONSTRAINT [FK_TUser_TRole] FOREIGN KEY([IdRole])
REFERENCES [dbo].[TRole] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TUser] CHECK CONSTRAINT [FK_TUser_TRole]
GO
ALTER TABLE [dbo].[TViewAccess]  WITH CHECK ADD  CONSTRAINT [FK_TAccessibility_TRole] FOREIGN KEY([IdRole])
REFERENCES [dbo].[TRole] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TViewAccess] CHECK CONSTRAINT [FK_TAccessibility_TRole]
GO
ALTER TABLE [dbo].[TViewAccess]  WITH CHECK ADD  CONSTRAINT [FK_TAccessibility_TView] FOREIGN KEY([IdView])
REFERENCES [dbo].[TView] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TViewAccess] CHECK CONSTRAINT [FK_TAccessibility_TView]
GO
