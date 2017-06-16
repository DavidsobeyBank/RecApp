CREATE TABLE [dbo].[Student]
(
[StudentID] [int] NOT NULL,
[StudentName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StudentSurname] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StudentDOB] [datetime] NOT NULL,
[StudentUniversity] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StudentDegree] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StudentYearofStudy] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StudentPhoto] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StudentBio] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Student] ADD CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED  ([StudentID]) ON [PRIMARY]
GO
