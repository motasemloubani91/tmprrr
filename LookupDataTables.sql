USE [MicroclearLight_July23]
GO
/****** Object:  Table [GCC].[GCC_CountryCodesLookup]    Script Date: 12/5/2025 11:19:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GCC].[GCC_CountryCodesLookup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](255) NULL,
	[Alpha3Code] [nvarchar](255) NULL,
	[ArabicDescription] [nvarchar](255) NULL,
	[EnglishDescription] [nvarchar](255) NULL,
	[Notes] [nvarchar](255) NULL,
	[LocationId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [SECONDARY]
) ON [SECONDARY]
GO
/****** Object:  Table [GCC].[GCC_CurrencyCodesLookup]    Script Date: 12/5/2025 11:19:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GCC].[GCC_CurrencyCodesLookup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](255) NULL,
	[ArabicDescription] [nvarchar](255) NULL,
	[EnglishDescription] [nvarchar](255) NULL,
	[Notes] [nvarchar](255) NULL,
	[CurrencyId] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [SECONDARY]
) ON [SECONDARY]
GO
/****** Object:  Table [GCC].[GCC_CustomsDataTypesLookup]    Script Date: 12/5/2025 11:19:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GCC].[GCC_CustomsDataTypesLookup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](255) NULL,
	[ArabicDescription] [nvarchar](255) NULL,
	[EnglishDescription] [nvarchar](255) NULL,
	[Notes] [nvarchar](255) NULL,
	[CCPCode] [varchar](10) NULL,
	[CCPIds] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [SECONDARY]
) ON [SECONDARY]
GO
/****** Object:  Table [GCC].[GCC_CustomsPortTypesLookup]    Script Date: 12/5/2025 11:19:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GCC].[GCC_CustomsPortTypesLookup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](255) NULL,
	[ArabicDescription] [nvarchar](255) NULL,
	[EnglishDescription] [nvarchar](255) NULL,
	[Notes] [nvarchar](255) NULL,
	[GCCPortType] [int] NULL,
	[LocationTypeTypeId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [SECONDARY]
) ON [SECONDARY]
GO
/****** Object:  Table [GCC].[GCC_DocumentTypesLookup]    Script Date: 12/5/2025 11:19:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GCC].[GCC_DocumentTypesLookup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](255) NULL,
	[ArabicDescription] [nvarchar](255) NULL,
	[EnglishDescription] [nvarchar](255) NULL,
	[Notes] [nvarchar](max) NULL,
	[DocumentId] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [SECONDARY]
) ON [SECONDARY] TEXTIMAGE_ON [SECONDARY]
GO
/****** Object:  Table [GCC].[GCC_DueNumberStatusesLookup]    Script Date: 12/5/2025 11:19:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GCC].[GCC_DueNumberStatusesLookup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](255) NULL,
	[ArabicDescription] [nvarchar](255) NULL,
	[EnglishDescription] [nvarchar](255) NULL,
	[Notes] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [SECONDARY]
) ON [SECONDARY] TEXTIMAGE_ON [SECONDARY]
GO
/****** Object:  Table [GCC].[GCC_GCCPortCodesLookup]    Script Date: 12/5/2025 11:19:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GCC].[GCC_GCCPortCodesLookup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](10) NULL,
	[Country] [nvarchar](100) NULL,
	[EnglishPortType] [nvarchar](100) NULL,
	[ArabicPortType] [nvarchar](100) NULL,
	[ArabicDescription] [nvarchar](255) NULL,
	[EnglishDescription] [nvarchar](255) NULL,
	[LocationId] [int] NULL,
	[CountryId] [int] NULL,
 CONSTRAINT [PK__GCC_GCCP__3214EC074C3034DF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [SECONDARY]
) ON [SECONDARY]
GO
/****** Object:  Table [GCC].[GCC_LanguageCodesLookup]    Script Date: 12/5/2025 11:19:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GCC].[GCC_LanguageCodesLookup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](10) NULL,
	[LanguageFamily] [nvarchar](200) NULL,
	[ArabicLanguageName] [nvarchar](200) NULL,
	[EnglishLanguageName] [nvarchar](200) NULL,
	[NativeName] [nvarchar](200) NULL,
 CONSTRAINT [PK__GCC_Lang__3214EC07F0EBC080] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [SECONDARY]
) ON [SECONDARY]
GO
/****** Object:  Table [GCC].[GCC_PackageTypesLookup]    Script Date: 12/5/2025 11:19:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GCC].[GCC_PackageTypesLookup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](10) NULL,
	[EnglishName] [nvarchar](200) NULL,
	[ArabicName] [nvarchar](200) NULL,
	[AdditionalEnglishDescription] [nvarchar](max) NULL,
	[AdditionalArabicDescription] [nvarchar](max) NULL,
	[EnglishCategory] [nvarchar](100) NULL,
	[ArabicCategory] [nvarchar](100) NULL,
	[TypeId] [nvarchar](250) NULL,
 CONSTRAINT [PK__GCC_Pack__3214EC0759FA5C6E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [SECONDARY]
) ON [SECONDARY] TEXTIMAGE_ON [SECONDARY]
GO
/****** Object:  Table [GCC].[GCC_PaymentMethodsLookup]    Script Date: 12/5/2025 11:19:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GCC].[GCC_PaymentMethodsLookup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](255) NULL,
	[ArabicDescription] [nvarchar](255) NULL,
	[EnglishDescription] [nvarchar](255) NULL,
	[Notes] [nvarchar](max) NULL,
	[TypeId] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [SECONDARY]
) ON [SECONDARY] TEXTIMAGE_ON [SECONDARY]
GO
/****** Object:  Table [GCC].[GCC_RiskResultsLookup]    Script Date: 12/5/2025 11:19:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GCC].[GCC_RiskResultsLookup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](255) NULL,
	[ArabicDescription] [nvarchar](255) NULL,
	[EnglishDescription] [nvarchar](255) NULL,
	[Notes] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [SECONDARY]
) ON [SECONDARY] TEXTIMAGE_ON [SECONDARY]
GO
/****** Object:  Table [GCC].[GCC_UnitCodesLookup]    Script Date: 12/5/2025 11:19:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GCC].[GCC_UnitCodesLookup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Quantity] [nvarchar](max) NULL,
	[Code] [nvarchar](max) NOT NULL,
	[ArabicDescription] [nvarchar](max) NULL,
	[EnglishDescription] [nvarchar](max) NULL,
	[ConversionFactor] [nvarchar](max) NULL,
	[Symbol] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
	[MeasurementUnitId] [nvarchar](250) NULL,
 CONSTRAINT [PK__GCC_Unit__3214EC0702720F00] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [SECONDARY]
) ON [SECONDARY] TEXTIMAGE_ON [SECONDARY]
GO
/****** Object:  Table [GCC].[GCC_ValuationMethodsLookup]    Script Date: 12/5/2025 11:19:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GCC].[GCC_ValuationMethodsLookup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](255) NULL,
	[ArabicDescription] [nvarchar](255) NULL,
	[EnglishDescription] [nvarchar](255) NULL,
	[Notes] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [SECONDARY]
) ON [SECONDARY]
GO
