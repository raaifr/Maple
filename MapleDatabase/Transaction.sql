CREATE TABLE [dbo].[Transaction]
(
	[id] INT IDENTITY(1,1) NOT NULL, 
    [user_id] INT NOT NULL, 
    [book_id] INT NOT NULL, 
    [borrow_date] DATE NOT NULL, 
    [return_date] DATE NULL, 
    [due] INT NOT NULL DEFAULT 14, 

    CONSTRAINT [FK_Transaction_User] FOREIGN KEY ([user_id]) REFERENCES [dbo].[User]([id]),
    CONSTRAINT [FK_Transaction_Book] FOREIGN KEY ([book_id]) REFERENCES [dbo].[Book]([id]), 
    CONSTRAINT [CK_Transaction_returndate] CHECK (CAST([borrow_date] as date) <= CAST(GETDATE() as date)), 
    CONSTRAINT [PK_Transaction] PRIMARY KEY ([id])
)
