CREATE TABLE [dbo].[PaymentType]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [payment_name] NVARCHAR(50) NOT NULL
)
