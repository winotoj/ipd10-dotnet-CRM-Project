CREATE VIEW v_Sales_LatestPurchase AS
select * from(
	select
		sales_id,
		customer_id,
		amount,
		purchase_date,
		row_number() over(partition by customer_id order by purchase_date desc) as rn
		from
		dbo.sales) t
		where t.rn = 1;
;
go

SELECT * fROM customers as c LEFT JOIN v_Sales_LatestPurchase as v
on c.customer_id = v.customer_id
;
go