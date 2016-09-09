USE [stocktrade]


/****** Object:  Table [dbo].[StockPriceByDay]    Script Date: 9/9/2016 8:35:48 AM ******/
DROP TABLE [dbo].[StockPriceByDay]
GO



/****** Object:  Table [dbo].[StockPriceByDay]    Script Date: 9/9/2016 8:34:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[StockPriceByDay](
	[StockTicker] [varchar](10) NOT NULL,
	[PriceDate] [date] NOT NULL,
	[OpenPrice] [float] NOT NULL,
	[ClosePrice] [float] NOT NULL,
	[HighestPrice] [float] NOT NULL,
	[LowestPrice] [float] NOT NULL,
	[TransAmount] [float] NOT NULL,
	
 CONSTRAINT [PK_StockPriceByDay] PRIMARY KEY CLUSTERED 
(
	[StockTicker] ASC,
	[PriceDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO


