CREATE TABLE [dbo].[TraitCategory]
(
[TraitID] [int] NOT NULL IDENTITY(1, 1),
[TraitName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TraitDescription] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TraitCategory] ADD CONSTRAINT [PK_TraitCategory] PRIMARY KEY CLUSTERED  ([TraitID]) ON [PRIMARY]
GO
