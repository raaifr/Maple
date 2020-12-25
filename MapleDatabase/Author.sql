CREATE TABLE [dbo].[Author]
(
	[id] INT IDENTITY(1,1) NOT NULL, 
    [author_firstname] NVARCHAR(50) NOT NULL, 
    [author_middlename] NVARCHAR(50) NULL, 
    [author_lastname] NVARCHAR(50) NULL,

    CONSTRAINT [PK_Author] PRIMARY KEY ([id])
)
