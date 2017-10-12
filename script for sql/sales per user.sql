SELECT u.username, sum(s.amount)
FROM (Salesreps AS u INNER JOIN Customers AS c ON u.username = c.salesrep_id) INNER JOIN Sales AS s ON c.customer_id = s.customer_id WHERE YEAR(S.purchase_date) = YEAR(GETDATE())-1 GROUP BY u.username;

SELECT u.username, MONTH(s.purchase_date) as mth, s.amount
FROM (Salesreps AS u INNER JOIN Customers AS c ON u.username = c.salesrep_id) INNER JOIN Sales AS s ON c.customer_id = s.customer_id WHERE YEAR(S.purchase_date) = YEAR(DATEADD(DD, -1, GETDATE()));
;
GO

SELECT u.username, MONTH(s.purchase_date) as mth, sum(s.amount) as amt
FROM (Salesreps AS u INNER JOIN Customers AS c ON u.username = c.salesrep_id) INNER JOIN Sales AS s ON c.customer_id = s.customer_id 
WHERE YEAR(S.purchase_date) = YEAR(DATEADD(DD, -1, GETDATE())) group by MONTH(s.purchase_date), u.username 
order by MONTH(s.purchase_date)
;
GO




