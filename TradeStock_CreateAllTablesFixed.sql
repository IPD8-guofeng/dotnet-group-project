USE [StockTrade]
GO

/****** Object:  Table [dbo].[PortfolioPosition]    Script Date: 9/7/2016 10:32:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/***PortfolioPosition***/
CREATE TABLE [dbo].[PortfolioPosition](
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

GO

USE [StockTrade]
GO

/****** Object:  Table [dbo].[PortfolioSummary]    Script Date: 9/7/2016 10:40:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PortfolioSummary](
	[PortfolioID] [int] NOT NULL,
	[StockID] [int] NOT NULL,
 CONSTRAINT [PK_PortfolioSummary] PRIMARY KEY CLUSTERED 
(
	[PortfolioID] ASC,
	[StockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



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


USE [StockTrade]
GO

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

GO

SET ANSI_PADDING OFF
GO

