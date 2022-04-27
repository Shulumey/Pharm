USE [Pharm]
GO
/****** Object:  Table [dbo].[ActiveSubstances]    Script Date: 26.04.2022 19:15:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActiveSubstances](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ActiveSubstances] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medications]    Script Date: 26.04.2022 19:15:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubstanceId] [int] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Medications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicationsChangeHistory]    Script Date: 26.04.2022 19:15:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicationsChangeHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MedicationId] [int] NOT NULL,
	[SourceChangeId] [int] NOT NULL,
	[Parameter] [nvarchar](50) NOT NULL,
	[ValueBefore] [nvarchar](1000) NULL,
	[ValueAfter] [nvarchar](1000) NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_MedicationsChangeHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SourceChanges]    Script Date: 26.04.2022 19:15:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SourceChanges](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_ModificationTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Medications]  WITH CHECK ADD  CONSTRAINT [FK_Medications_ActiveSubstances] FOREIGN KEY([SubstanceId])
REFERENCES [dbo].[ActiveSubstances] ([Id])
GO
ALTER TABLE [dbo].[Medications] CHECK CONSTRAINT [FK_Medications_ActiveSubstances]
GO
ALTER TABLE [dbo].[MedicationsChangeHistory]  WITH CHECK ADD  CONSTRAINT [FK_MedicationsChangeHistory_SourceChanges] FOREIGN KEY([SourceChangeId])
REFERENCES [dbo].[SourceChanges] ([Id])
GO
ALTER TABLE [dbo].[MedicationsChangeHistory] CHECK CONSTRAINT [FK_MedicationsChangeHistory_SourceChanges]
GO
