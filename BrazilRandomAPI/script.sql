USE [Datasets]
GO

/****** Object:  Table [dbo].[Endereco]    Script Date: 26/06/2023 02:31:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Endereco](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CEP] [varchar](50) NULL,
	[Cidade] [varchar](300) NULL,
	[Estado] [varchar](300) NULL,
	[Bairro] [varchar](300) NULL,
	[Logradouro] [varchar](300) NULL,
 CONSTRAINT [PK_Endereco] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [Datasets]
GO

/****** Object:  Table [dbo].[Pessoa]    Script Date: 26/06/2023 02:32:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pessoa](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](1000) NULL,
	[Frequencia] [varchar](200) NULL,
	[Rank] [varchar](200) NULL,
	[Sexo] [varchar](200) NULL,
 CONSTRAINT [PK_Pessoa_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



