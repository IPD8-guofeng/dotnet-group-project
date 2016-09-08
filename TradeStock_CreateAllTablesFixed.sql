USE [StockTrade]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

/******Transaction******/
CREATE TABLE [dbo].[Transaction](
	[TransID] [int] NOT NULL,
	[StockID] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Unit] [int] NOT NULL,
	[Action] [bit] NOT NULL,
 CONSTRAINT [PK_PortfolioPosition] PRIMARY KEY CLUSTERED 
(
	[TransID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/******PortfolioSummary******/

CREATE TABLE [dbo].[PortfolioSummary](
	[PortfolioID] [int] NOT NULL,
	[StockID] [int] NOT NULL,
 CONSTRAINT [PK_PortfolioSummary] PRIMARY KEY CLUSTERED 
(
	[PortfolioID] ASC,
	[StockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



/****stock******/
CREATE TABLE [dbo].[Stock](
	[StockID] [int] NOT NULL,
	[StockName] [varchar](50) NOT NULL,
	[StockDesc] [varchar](200) NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[StockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** StockPriceByDay ******/
CREATE TABLE [dbo].[StockPriceByDay](
	[StockID] [int] NOT NULL,
	[OpenPrice] [float] NOT NULL,
	[ClosePrice] [float] NOT NULL,
	[HighestPrice] [float] NOT NULL,
	[LowestPrice] [float] NOT NULL,
	[TransAmount] [float] NOT NULL,
	[PriceDateAndTime] [datetime] NOT NULL,
 CONSTRAINT [PK_StockPriceByDay] PRIMARY KEY CLUSTERED 
(
	[StockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** TrendLine******/
CREATE TABLE [dbo].[TrendLine](
	[TrendLineID] [int] NOT NULL,
	[PositionStartX] [float] NOT NULL,
	[PositionStartY] [float] NOT NULL,
	[PositionEndX] [float] NOT NULL,
	[PositionEndY] [float] NOT NULL,
	[Length] [float] NOT NULL,
	[Color] [varchar](50) NOT NULL,
	[Width] [int] NOT NULL,
	[StockID] [int] NOT NULL,
 CONSTRAINT [PK_TrendLine] PRIMARY KEY CLUSTERED 
(
	[TrendLineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_PADDING OFF
GO

