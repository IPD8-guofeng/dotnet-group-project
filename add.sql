USE [StockTrade]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 9/8/2016 3:23:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
