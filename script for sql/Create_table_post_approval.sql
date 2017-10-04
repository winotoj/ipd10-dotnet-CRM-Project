/* Library Assignment Creating Database, Schemas and Tables
	Script Date: Oct 2, 2017
	Developed By: Vaji and Winoto
*/



use master
;
go

/* Creating The Database */

create database CrmProject
on primary
(
	name = 'crmProject',
	filename = 'C:\sqlProject\crmProject.mdf',
	size = 10MB,
	filegrowth = 2MB,
	maxsize = 200MB
)

log on
(
	
	name = 'library_log',
	filename = 'C:\sqlProject\crmProject_log.ldf',
	size = 2MB,
	filegrowth = 10%,
	maxsize = 25MB
)
go

/* Creating the Schemas */


/* Creating the Tables */

use CrmProject
;
go
drop table dbo.users
;
go


/* Table 1 - dbo.Salesreps */

create table dbo.Salesreps
(
	salesrep_id int identity (1,1) not null,
	lastname varchar(40) not null,
	firstname varchar(40) not null,
	username varchar(10),
	password varchar(20),
	status bit not null,
	phone varchar(15) not null,
	cellphone varchar(15) not null,
	email varchar(80) not null
	constraint pk_Salesreps primary key clustered (salesrep_id asc)
)
;
go

/* Table 3 - dbo.Companies */

create table dbo.Customers
(
	Customer_id int identity (1,1) not null, 
	company_name varchar(50) not null,     
	street varchar(50) not null,
	city varchar(50) not null,
	province varchar(2) not null,
	postal varchar(10) not null,
	phone varchar(15) not null,
	contact_firstname varchar(40) not null,
	contact_lastname varchar(40) not null,
	created_date datetime not null,
	status bit not null,
	salesrep_id int not null
	constraint pk_Customers primary key clustered (customer_id asc)
)
;
go



/* Table 3 dbo.Messages */
/* note is for conversation during phone call */
create table dbo.Messages
(
	message_id int identity (1,1) not null,
	customer_id int not null,
	note varchar(1000) not null,
	type varchar(25) not null,
	msg_date datetime not null
	constraint pk_Messages primary key clustered (message_id asc)
)
;
go



/* Table 4 dbo.Sales */

create table Sales
(
	sales_id int identity (1,1) not null,
	customer_id int not null,
	purchase_date datetime,
	amount money
	constraint pk_Sales primary key clustered (sales_id asc)
)
;
go

/* table 5 dbo.schedule */
create table dbo.Schedules
(
	schedule_id int identity (1,1) not null, 
	type varchar(25) not null,     
	note varchar(1000) not null,
	completed bit not null,
	scheduled_date datetime not null,
	created_date datetime null,
	salesrep_id int not null
	constraint pk_Prospect primary key clustered (schedule_id asc)
)
;
go

