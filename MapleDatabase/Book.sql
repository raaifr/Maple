CREATE TABLE [dbo].[Book]
(
	[id] INT IDENTITY(1,1) NOT NULL, 
    [series_title] NVARCHAR(50) NULL, 
    [book_title] NVARCHAR(50) NOT NULL,
    [publisher] INT NOT NULL, 
    [isbn] NVARCHAR(20) NULL, 
    [ddc] DECIMAL(6,3) NOT NULL, 
    [tags] NVARCHAR(MAX) NULL, 
    [year] INT NULL , 
    [stock] INT NOT NULL DEFAULT 1, 
    [shelf_id] INT NOT NULL, 

    CONSTRAINT [FK_Book_ToPublisher] FOREIGN KEY ([publisher]) REFERENCES [dbo].[Publisher]([id]),
    CONSTRAINT [FK_Book_ToShelf] FOREIGN KEY ([shelf_id]) REFERENCES [dbo].[Shelf]([id]),
    CONSTRAINT [PK_Book] PRIMARY KEY ([id]) 


)
