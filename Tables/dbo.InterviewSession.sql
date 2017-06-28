CREATE TABLE [dbo].[InterviewSession]
(
[SessionID] [int] NOT NULL IDENTITY(1, 1),
[SessionDescription] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SessionName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[InterviewSession] ADD CONSTRAINT [PK_InterviewSession] PRIMARY KEY CLUSTERED  ([SessionID]) ON [PRIMARY]
GO
