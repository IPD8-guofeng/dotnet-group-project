USE [StockTrade]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

/******Transaction******/
CREATE TABLE [dbo].[Transaction](
	[TransID] [int] IDENTITY(1,1) PRIMARY KEY,
	[StockTicker] [varchar](10) NOT NULL,
	[Price] [float] NOT NULL,
	[Quantity] [int] NOT NULL,
	[ActionType] [int] NOT NULL,
)


/******PortfolioSummary******/

CREATE TABLE [dbo].[PortfolioSummary](
	[PortfolioID] [int] IDENTITY(1,1) PRIMARY KEY,
	[StockTicker] [varchar](10) NOT NULL,
)



/****stock******/
CREATE TABLE [dbo].[Stock](
	[StockTicker] [varchar](10) NOT NULL PRIMARY KEY,
	[StockName] [varchar](50) NOT NULL,
	[StockCatagory] [varchar](50) NULL,
    [StockCommonShare] [int] NULL,
)


/****** StockPriceByDay ******/
CREATE TABLE [dbo].[StockPriceByDay](
	[StockTicker] [varchar](10) NOT NULL ,
    [PriceDate] [date] NOT NULL ,
	[OpenPrice] [float] NOT NULL,
	[ClosePrice] [float] NOT NULL,
	[HighestPrice] [float] NOT NULL,
	[LowestPrice] [float] NOT NULL,
	[TransAmount] [float] NOT NULL,
	primary key (StockTicker, PriceDate),
)

/****** TrendLine******/
CREATE TABLE [dbo].[TrendLine](
	[TrendLineID] [int] IDENTITY(1,1) PRIMARY KEY,
	[PositionStartX] [float] NOT NULL,
	[PositionStartY] [float] NOT NULL,
	[PositionEndX] [float] NOT NULL,
	[PositionEndY] [float] NOT NULL,
	[Length] [float] NOT NULL,
	[Color] [varchar](50) NOT NULL,
	[Width] [int] NOT NULL,
	[StockTicker] [varchar](10) NOT NULL,
)

SET ANSI_PADDING OFF
GO

