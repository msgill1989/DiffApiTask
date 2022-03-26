# DiffApiTask
This is Diff API Task

This solution uses 2 SQL tables and their creation script is given below,

USE [DiffTask]
GO

/****** Object:  Table [dbo].[RightTable]    Script Date: 2022-03-26 2:32:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RightTable](
	[Id] [int] NULL,
	[DiffData] [varchar](200) NULL
) ON [PRIMARY]
GO


USE [DiffTask]
GO

/****** Object:  Table [dbo].[LeftTable]    Script Date: 2022-03-26 2:32:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LeftTable](
	[Id] [int] NULL,
	[DiffData] [varchar](200) NULL
) ON [PRIMARY]
GO


