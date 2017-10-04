/*  Creating Database, Schemas and Tables
	Script Date: Oct 2, 2017
	Developed By: Vaji and Winoto
*/



/****** Altering Tables, Adding Foreign Keys *****/

use CrmProject
;
go

/* add foriegn key into sales */

alter table dbo.Customers
add
	constraint fk_customers_salesreps foreign key (salesrep_id)
	references dbo.salesreps (salesrep_id)
;
go

/* add foreign key into messages */

alter table dbo.messages
add
	constraint fk_messages_customers foreign key (customer_id)
	references dbo.customers (customer_id)
;
go


/* add foreign key into Sales */
alter table dbo.Sales
add
	constraint fk_sales_customers foreign key (customer_id)
	references dbo.customers (customer_id)
;
go

/* add foreign key into Schedules */
alter table dbo.schedules
add
	constraint fk_Schedules_Salesreps foreign key (salesrep_id)
	references dbo.salesreps (salesrep_id)
	;
	go


