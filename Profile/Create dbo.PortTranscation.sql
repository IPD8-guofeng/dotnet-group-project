USE [StockTrade]
GO

/****** Object: Table [dbo].[PortTranscation] Script Date: 9/12/2016 1:43:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PortTranscation] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [portId]      INT           NOT NULL,
    [StockTicker] VARCHAR (10)  NULL,
    [Type]        INT           NULL,
    [Date]        DATE          NOT NULL,
    [Share]       INT           NULL,
    [Price]       FLOAT (53)    NULL,
    [Notes]       VARCHAR (250) NULL
);


