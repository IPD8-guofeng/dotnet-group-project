BULK
INSERT StockPriceByDay
FROM 'H:\x\dotnet-group-project\Useful Link\yahoo\DataCombine\DataCombine\data\SP500_history.csv'
WITH
(
FIELDTERMINATOR = ',',
ROWTERMINATOR = '\n'
)
GO
--Check the content of the table.
SELECT *
FROM StockPriceByDay
GO