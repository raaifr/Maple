CREATE TABLE [dbo].[User]
(
	[id] INT IDENTITY(1,1) NOT NULL, 
    [first_name] NVARCHAR(100) NOT NULL, 
    [last_name] NVARCHAR(100) NULL, 
    [contact] BIGINT NOT NULL, 
    [email] NVARCHAR(500) NOT NULL, 
    [country] NVARCHAR(50) NULL, 
    [nic] NVARCHAR(50) NOT NULL, 
    [password] NVARCHAR(500) NOT NULL, 
    [membership] NVARCHAR(50) NOT NULL, 
    [dob] DATE NOT NULL,

    CONSTRAINT [PK_User] PRIMARY KEY ([id]), 
    CONSTRAINT [AK_NIC] UNIQUE ([nic]),
    CONSTRAINT [CK_User_dob] CHECK (CAST([dob] as date) <= CAST(GETDATE() as date))
)

