
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[Password] [varchar](250) NOT NULL,
	[Salt] [varchar](250) NOT NULL,
	[EmailAddress] [varchar](250) NULL,
	[IsAdmin] [bit] NULL
) ON [PRIMARY]
GO

CREATE TABLE Projects.dbo.Products
	(
	ID int NOT NULL IDENTITY (1, 1),
	Name varchar(250)
	)  ON [PRIMARY]
GO
ALTER TABLE Projects.dbo.Products SET (LOCK_ESCALATION = TABLE)
GO

