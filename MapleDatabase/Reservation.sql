CREATE TABLE [dbo].[Reservation]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [user_id] int NOT NULL, 
    [book_id] int NOT NULL, 
    [reserve_date] DATE NOT NULL, 
    CONSTRAINT [FK_Reservations_ToUser] FOREIGN KEY ([user_id]) REFERENCES [dbo].[User]([id]),
    CONSTRAINT [FK_Reservations_ToBook] FOREIGN KEY ([book_id]) REFERENCES [dbo].[Book]([id])
)
