CREATE TABLE [dbo].[StaffRole]
(
	[id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [role_name] NVARCHAR(20) NOT NULL, 
    [access] NVARCHAR(MAX) NULL, 
    [accessCode] INT NOT NULL
)
