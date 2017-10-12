SELECT u.username, sum(s.amount)
FROM (Salesreps AS u INNER JOIN Customers AS c ON u.username = c.salesrep_id) INNER JOIN Sales AS s ON c.customer_id = s.customer_id WHERE YEAR(S.purchase_date) = YEAR(GETDATE())-1 GROUP BY u.username;

SELECT u.username, sum(s.amount)
FROM (Salesreps AS u INNER JOIN Customers AS c ON u.username = c.salesrep_id) INNER JOIN Sales AS s ON c.customer_id = s.customer_id WHERE MONTH(S.purchase_date) = MONTH(DATEADD(DD, -1, GETDATE())) GROUP BY u.username;
;
GO

SELECT u.username, s.amount, s.purchase_date
FROM (Salesreps AS u INNER JOIN Customers AS c ON u.username = c.salesrep_id) INNER JOIN Sales AS s ON c.customer_id = s.customer_id WHERE MONTH(S.purchase_date) = MONTH(DATEADD(DD, -1, GETDATE()))-1 order BY u.username;
;
GO