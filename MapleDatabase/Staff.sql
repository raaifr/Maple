CREATE TABLE [dbo].[Staff]
(
	[id] INT IDENTITY(1,1) NOT NULL, 
    [first_name] NVARCHAR(50) NOT NULL, 
    [last_name] NVARCHAR(50) NULL, 
    [email] NVARCHAR(500) NOT NULL, 
    [dob] DATE NOT NULL, 
    [contact] BIGINT NOT NULL,
    [employment_date] DATE NOT NULL, 
    [employee_id] NVARCHAR(200) NOT NULL, 
    [role] INT NOT NULL,
    [password] NVARCHAR(500) NOT NULL,

    CONSTRAINT [PK_Staff] PRIMARY KEY ([id]), 
    CONSTRAINT [AK_Staff_emp_id] UNIQUE ([employee_id]),
    CONSTRAINT [CK_Staff_dob] CHECK (CAST([dob] as date) <= CAST(GETDATE() as date)), 
    CONSTRAINT [FK_Staff_StaffRole] FOREIGN KEY ([role]) REFERENCES [dbo].[StaffRole]([id])
)
