SELECT StockTicker, ((SELECT SUM(Quantity) AS sumBuy FROM [Transaction] WHERE ActionType = 1 GROUP BY StockTicker, ActionType) - (SELECT SUM(Quantity) AS sumSell FROM[Transaction] WHERE ActionType = 2 GROUP BY StockTicker, ActionType)) AS StockQuantity, ((SELECT SUM(Quantity*Price) AS sumBuyCost FROM [Transaction] WHERE ActionType = 1 GROUP BY StockTicker, ActionType) - (SELECT SUM(Quantity*Price) AS sumSellMoney FROM[Transaction] WHERE ActionType = 2 GROUP BY StockTicker, ActionType)) AS sumTotalCost FROM[Transaction] GROUP BY StockTicker

SELECT StockTicker, ((SELECT SUM(Quantity) AS sumCost FROM [Transaction] WHERE ActionType = 1 GROUP BY StockTicker, ActionType) - (SELECT SUM(Quantity) AS sumCurrent FROM[Transaction] WHERE ActionType = 2 GROUP BY StockTicker, ActionType)) AS StockQuantity FROM[Transaction] GROUP BY StockTicker
SELECT StockTicker, (SELECT SUM(Quantity) AS sumCost FROM [Transaction] WHERE ActionType = 1 GROUP BY StockTicker, ActionType) FROM[Transaction] GROUP BY StockTicker


SELECT StockTicker, ((SELECT SUM(Quantity) AS sumCost FROM [Transaction] WHERE ActionType = 1 GROUP BY StockTicker)-(SELECT SUM(Quantity) AS sumCost FROM [Transaction] WHERE ActionType = 2 GROUP BY StockTicker)) AS ttt FROM [Transaction] 

SELECT StockTicker, SUM(Quantity*Price) as sumQ FROM[Transaction] WHERE ActionType = 1 GROUP BY StockTicker

SELECT  StockTicker , ((SELECT SUM(Quantity*Price) as sumQ FROM[Transaction] WHERE ActionType = 1 GROUP BY StockTicker)-(SELECT SUM(Quantity*Price) as sumQ FROM[Transaction] WHERE ActionType = 1 GROUP BY StockTicker)) FROM [Transaction]  GROUP BY StockTicker

SELECT StockTicker, SUM(Quantity) as sumQ FROM[Transaction] WHERE ActionType = 1 GROUP BY StockTicker
SELECT StockTicker, ((SELECT SUM(Quantity) AS sumBuy FROM [Transaction] WHERE ActionType = 1 GROUP BY StockTicker, ActionType) - (SELECT SUM(Quantity) AS sumSell FROM[Transaction] WHERE ActionType = 2 GROUP BY StockTicker, ActionType)) AS StockQuantity, ((SELECT SUM(Quantity*Price) AS sumBuyCost FROM [Transaction] WHERE ActionType = 1 GROUP BY StockTicker, ActionType) - (SELECT SUM(Quantity*Price) AS sumSellMoney FROM[Transaction] WHERE ActionType = 2 GROUP BY StockTicker, ActionType)) AS sumTotalCost FROM[Transaction] GROUP BY StockTicker

---solution for total cost if price goes to minus 
SELECT StockTicker, SUM(Quantity*Price) AS sumCost, SUM(Quantity) AS sumCost FROM[Transaction] GROUP BY StockTicker