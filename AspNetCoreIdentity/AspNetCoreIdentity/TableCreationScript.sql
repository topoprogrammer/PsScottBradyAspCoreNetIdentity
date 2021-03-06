USE [AspIdentityCoreDb]
GO

CREATE TABLE [dbo].[CustomUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
CONSTRAINT [PK_PluralsightUsers] PRIMARY KEY CLUSTERED(
	[Id] ASC
))
GO