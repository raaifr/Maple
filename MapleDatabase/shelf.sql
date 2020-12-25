CREATE TABLE [dbo].[Shelf]
(
	[id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [shelf_number] NVARCHAR(50) NULL, 
    [building] NVARCHAR(50) NULL
)
