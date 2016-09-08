USE [StockTrade]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

/******Transaction******/
CREATE TABLE [dbo].[Transaction](
	[TransID] [int] NOT NULL,
	[StockTicker] [varchar](10) NOT NULL,
	[Price] [float] NOT NULL,
	[Unit] [int] NOT NULL,
	[Action] [bit] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[TransID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/******PortfolioSummary******/

CREATE TABLE [dbo].[PortfolioSummary](
	[PortfolioID] [int] NOT NULL,
	[StockTicker] [varchar](10) NOT NULL,
 CONSTRAINT [PK_PortfolioSummary] PRIMARY KEY CLUSTERED 
(
	[PortfolioID] ASC,
	[StockTicker] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



/****stock******/
CREATE TABLE [dbo].[Stock](
	[StockTicker] [varchar](10) NOT NULL,
	[StockName] [varchar](50) NOT NULL,
	[StockCatagory] [varchar](50) NULL,
    [StockCommonShare] [int] NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[StockTicker] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** StockPriceByDay ******/
CREATE TABLE [dbo].[StockPriceByDay](
	[StockTicker] [varchar](10) NOT NULL,
	[OpenPrice] [float] NOT NULL,
	[ClosePrice] [float] NOT NULL,
	[HighestPrice] [float] NOT NULL,
	[LowestPrice] [float] NOT NULL,
	[TransAmount] [float] NOT NULL,
	[PriceDate] [date] NOT NULL,
 CONSTRAINT [PK_StockPriceByDay] PRIMARY KEY CLUSTERED 
(
	[StockTicker] ASC
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
	[StockTicker] [varchar](10) NOT NULL,
 CONSTRAINT [PK_TrendLine] PRIMARY KEY CLUSTERED 
(
	[TrendLineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_PADDING OFF
GO

