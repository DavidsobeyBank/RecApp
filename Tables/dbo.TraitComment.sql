CREATE TABLE [dbo].[TraitComment]
(
[TraitCommentID] [int] NOT NULL IDENTITY(1, 1),
[PanelID] [int] NOT NULL,
[TraitID] [int] NOT NULL,
[Comment] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Score] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[TraitComment] ADD CONSTRAINT [PK_TraitComment] PRIMARY KEY CLUSTERED  ([TraitCommentID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TraitComment] ADD CONSTRAINT [FK_TraitComment_PanelMember] FOREIGN KEY ([PanelID]) REFERENCES [dbo].[PanelMember] ([PanelID])
GO
ALTER TABLE [dbo].[TraitComment] ADD CONSTRAINT [FK_TraitComment_TraitCategory] FOREIGN KEY ([TraitID]) REFERENCES [dbo].[TraitCategory] ([TraitID])
GO
