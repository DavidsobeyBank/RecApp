CREATE TABLE [dbo].[Employee]
(
[EmployeeID] [int] NOT NULL IDENTITY(1, 1),
[EmployeeName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[EmployeeSurname] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[EmployeeEmail] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employee] ADD CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED  ([EmployeeID]) ON [PRIMARY]
GO
