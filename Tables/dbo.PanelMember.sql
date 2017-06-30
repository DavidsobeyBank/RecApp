CREATE TABLE [dbo].[PanelMember]
(
[PanelID] [int] NOT NULL IDENTITY(1, 1),
[EmployeeID] [int] NOT NULL,
[InterviewID] [int] NOT NULL,
[PannelScore] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PanelMember] ADD CONSTRAINT [PK_Pannel] PRIMARY KEY CLUSTERED  ([PanelID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PanelMember] ADD CONSTRAINT [FK_Panel_Employee] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[PanelMember] ADD CONSTRAINT [FK_Panel_Interview] FOREIGN KEY ([InterviewID]) REFERENCES [dbo].[Interview] ([InterviewID])
GO