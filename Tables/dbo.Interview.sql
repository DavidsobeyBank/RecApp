CREATE TABLE [dbo].[Interview]
(
[InterviewID] [int] NOT NULL IDENTITY(1, 1),
[StudentID] [int] NOT NULL,
[InterviewDate] [datetime] NOT NULL,
[SessionID] [int] NULL,
[Room] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[OverallComment] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[OverallScore] [decimal] (2, 2) NULL,
[StatusID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Interview] ADD CONSTRAINT [PK_Interview] PRIMARY KEY CLUSTERED  ([InterviewID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Interview] ADD CONSTRAINT [FK_Interview_InterviewSession] FOREIGN KEY ([SessionID]) REFERENCES [dbo].[InterviewSession] ([SessionID])
GO
ALTER TABLE [dbo].[Interview] ADD CONSTRAINT [FK_Interview_InterviewStatus] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[InterviewStatus] ([StatusID])
GO
ALTER TABLE [dbo].[Interview] ADD CONSTRAINT [FK_Interview_Student] FOREIGN KEY ([StudentID]) REFERENCES [dbo].[Student] ([StudentID])
GO
