CREATE TABLE [dbo].[InterviewStatus]
(
[StatusID] [int] NOT NULL IDENTITY(1, 1),
[StatusName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[InterviewStatus] ADD CONSTRAINT [PK_InterviewStatus] PRIMARY KEY CLUSTERED  ([StatusID]) ON [PRIMARY]
GO
