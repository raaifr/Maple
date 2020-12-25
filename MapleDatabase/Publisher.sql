CREATE TABLE [dbo].[Publisher]
(
	[id] INT IDENTITY(1,1) NOT NULL, 
    [publisher_name] NVARCHAR(300) NOT NULL, 
    [address] NVARCHAR(MAX) NULL,

    CONSTRAINT [PK_Publisher] PRIMARY KEY ([id])
)
