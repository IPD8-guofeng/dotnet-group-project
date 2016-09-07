USE [TradeStock]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
SET ANSI_PADDING ON

/****** Create tables ********************/
/********Stock*************/
CREATE TABLE [dbo].[Stock](
	[StockID] [int] NOT NULL,
	[StockName] [varchar](50) NOT NULL,
	[StockDesc] [varchar](200) NULL,
	[StockRiskType] [int] NOT NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[StockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/********Transaction*************/
CREATE TABLE [dbo].[Transaction](
	[TransID] [int] NOT NULL,
	[StockID] [int] NOT NULL,
	[StockUnit] [int] NOT NULL,
	[StockPrice] [float] NOT NULL,
	[AccountID] [int] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[TransID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/******Account ******/
CREATE TABLE [dbo].[Account](
	[AccountID] [int] NOT NULL,
	[AccountType] [varchar](50) NOT NULL,
	[AccountBalance] [float] NOT NULL,
	[ClientID] [int] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/******Client******/
CREATE TABLE [dbo].[Client](
	[ClientID] [int] NOT NULL,
	[ClientName] [varchar](50) NOT NULL,
	[ClientAddress] [varchar](100) NOT NULL,
	[ClientPhone] [varchar](10) NOT NULL,
	[ClientRiskType] [int] NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/******StocksOwned******/
CREATE TABLE [dbo].[StocksOwned](
	[StockID] [int] NOT NULL,
	[StockUnit] [int] NOT NULL,
	[ClientID] [int] NOT NULL,
	[StockCost] [float] NOT NULL,
	[StockCurrentPrice] [float] NOT NULL,
 CONSTRAINT [PK_StocksOwned] PRIMARY KEY CLUSTERED 
(
	[StockID] ASC,
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** StockPriceByMinute******/
CREATE TABLE [dbo].[StockPriceByMinute](
	[StockID] [int] NOT NULL,
	[PriceByMinute] [float] NOT NULL,
	[CurrentPrice] [float] NOT NULL,
	[AskPrice] [float] NOT NULL,
	[BidPrice] [float] NOT NULL,
	[PriceDateAndTime] [datetime] NOT NULL,
 CONSTRAINT [PK_StockPriceByMinute] PRIMARY KEY CLUSTERED 
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
	[PriceDateAndTime] [datetime] NOT NULL,
 CONSTRAINT [PK_StockPriceByDay] PRIMARY KEY CLUSTERED 
(
	[StockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

USE [TradeStock]
GO

/****** WatchList ******/
CREATE TABLE [dbo].[WatchList](
	[WatchListID] [int] NOT NULL,
	[WatchListName] [varchar](50) NOT NULL,
	[WatchListDesc] [varchar](200) NULL,
	[ClientID] [int] NOT NULL,
 CONSTRAINT [PK_WatchList] PRIMARY KEY CLUSTERED 
(
	[WatchListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


USE [TradeStock]
GO

/****** Object:  Table [dbo].[WatchListDetail]    Script Date: 06/09/2016 9:23:17 PM ******/
CREATE TABLE [dbo].[WatchListDetail](
	[WatchListID] [int] NOT NULL,
	[StockID] [int] NOT NULL,
	[ClientID] [int] NOT NULL,
 CONSTRAINT [PK_WatchListDetail] PRIMARY KEY CLUSTERED 
(
	[WatchListID] ASC,
	[StockID] ASC,
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

















