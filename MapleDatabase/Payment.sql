CREATE TABLE [dbo].[Payment]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [user_id] INT NOT NULL, 
    [payment_type] INT NOT NULL, 
    [payment_date] DATE NOT NULL, 
    [amount] DECIMAL(18,2) NOT NULL, 
    [remarks] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_Payment_User] FOREIGN KEY ([user_id]) REFERENCES [dbo].[User](id),
    CONSTRAINT [FK_Payment_PaymentType] FOREIGN KEY ([payment_type]) REFERENCES [dbo].[PaymentType](id)
)
