

alter table StockPriceByDay drop CONSTRAINT PK_StockPriceByDay
go


ALTER TABLE StockPriceByDay 
ADD CONSTRAINT PK_StockPriceByDay PRIMARY KEY CLUSTERED (StockTicker,PriceDate)