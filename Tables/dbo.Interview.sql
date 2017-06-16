CREATE TABLE [dbo].[Interview]
(
[InterviewID] [int] NOT NULL,
[StudentID] [int] NOT NULL,
[InterviewDate] [datetime] NOT NULL,
[InterviewComment1] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[InterviewComment2] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[InterviewComment3] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[InterviewComment4] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[InterviewComment5] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[InterviewComment6] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SessionID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Interview] ADD CONSTRAINT [PK_Interview] PRIMARY KEY CLUSTERED  ([InterviewID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Interview] ADD CONSTRAINT [FK_Interview_InterviewSession] FOREIGN KEY ([SessionID]) REFERENCES [dbo].[InterviewSession] ([SessionID])
GO
ALTER TABLE [dbo].[Interview] ADD CONSTRAINT [FK_Interview_Student] FOREIGN KEY ([StudentID]) REFERENCES [dbo].[Student] ([StudentID])
GO
