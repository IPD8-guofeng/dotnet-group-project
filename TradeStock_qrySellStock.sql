SELECT StockTicker, ((SELECT SUM(Quantity) AS sumBuy FROM [Transaction] WHERE ActionType = 1 GROUP BY StockTicker, ActionType) - (SELECT SUM(Quantity) AS sumSell FROM[Transaction] WHERE ActionType = 2 GROUP BY StockTicker, ActionType)) AS StockQuantity, ((SELECT SUM(Quantity*Price) AS sumBuyCost FROM [Transaction] WHERE ActionType = 1 GROUP BY StockTicker, ActionType) - (SELECT SUM(Quantity*Price) AS sumSellMoney FROM[Transaction] WHERE ActionType = 2 GROUP BY StockTicker, ActionType)) AS sumTotalCost FROM[Transaction] GROUP BY StockTicker


SELECT StockTicker, (( SELECT SUM(Quantity*Price) AS sumBuyCost FROM [Transaction] WHERE ActionType = 1 GROUP BY StockTicker) - (SELECT SUM(Quantity*Price) AS sumSellMoney FROM[Transaction] WHERE ActionType = 2 GROUP BY StockTicker)) AS sumCost FROM[Transaction] GROUP BY StockTicker

SELECT StockTicker, 
(
( SELECT SUM(Quantity) AS sumBuyQty FROM [Transaction] WHERE ActionType = 1 GROUP BY StockTicker) 
- (SELECT SUM(Quantity) AS sumSellQty FROM [Transaction] WHERE ActionType = 2 GROUP BY StockTicker)
) AS sumQty 
FROM[Transaction] 
GROUP BY StockTicker

