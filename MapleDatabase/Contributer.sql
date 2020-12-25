CREATE TABLE [dbo].[Contributer]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [author_id] int NULL, 
    [book_id] int NULL, 
    CONSTRAINT [FK_Contributer_ToBook] FOREIGN KEY ([book_id]) REFERENCES [dbo].[Book]([id]), 
    CONSTRAINT [FK_Contributer_ToAuthor] FOREIGN KEY ([author_id]) REFERENCES [dbo].[Author]([id])
)
