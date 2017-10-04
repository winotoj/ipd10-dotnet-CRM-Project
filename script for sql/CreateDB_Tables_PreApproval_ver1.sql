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

/* Table 1 - dbo.Users */

create table dbo.Users
(
	member_id int identity (1,1) not null,
	lastname varchar(40) not null,
	firstname varchar(40) not null,
	middleinitial varchar(40) null,
	username varchar(10),
	password varchar(20),
	salesrep_id int null,
	status bit not null
	constraint pk_Member primary key clustered (member_id asc)
)
;
go

/* Table 2 - dbo.Salesreps */

create table dbo.Salesreps
(
	salesrep_id int identity (1,1) not null,
	salesrep_no varchar(4) not null,
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
	name varchar(50) not null,     
	street varchar(50) not null,
	city varchar(50) not null,
	province varchar(2) not null,
	postal varchar(10) not null,
	phone varchar(15) not null,
	www varchar(50) null,
	created_date datetime null,
	status bit not null,
	salesrep_id int not null
	constraint pk_Customers primary key clustered (customer_id asc)
)
;
go


/* Table 4 - dbo.Contacts. */

create table dbo.Contacts
(
	contact_id int identity (1,1) not null,
	first_name varchar(50) not null,
	last_name varchar(50) not null,
	middleinitial varchar(40) null,
	salutation varchar(5) not null,
	title varchar(20) not null,
	email varchar(80) not null,
	phone varchar(15) not null,
	cell_phone varchar(15) null,
	note varchar(300) null,
	customer_id int not null
	constraint pk_Contacts primary key clustered (contact_id asc)
)
;
go

/* Table 4 dbo.Messages */
/* note is for conversation during phone call */
create table dbo.Messages
(
	message_id int identity (1,1) not null,
	customer_id int not null,
	prospect_id int not null,
	salesrep_id int not null,
	note varchar(500) not null,
	msg_date datetime not null
	constraint pk_Messages primary key clustered (message_id asc)
)
;
go

/* Table 5 dbo.Opportunities */

create table Opportunities
(
	opportunity_id int identity (1,1) not null,
	customer_id int not null,
	prospect_id int not null,
	salesrep_id int not null,
	status varchar(25) not null,
	stage varchar(25) not null,
	confidence_rating varchar(25) not null,
	description varchar(300) not null,
	revenue money not null,
	start_date datetime not null,
	end_date datetime not null,
	next_action varchar(300) not null
	constraint pk_Opportunities primary key clustered (opportunity_id asc)
)
;
go

/* Table 6 dbo.Sales */

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

/* table 7 dbo.Prospect */
create table dbo.Prospect
(
	prospect_id int identity (1,1) not null, 
	name varchar(50) not null,     
	street varchar(50) not null,
	city varchar(50) not null,
	province varchar(2) not null,
	postal varchar(10) not null,
	phone varchar(15) not null,
	www varchar(50) null,
	created_date datetime null,
	status bit not null,
	salesrep_id int not null
	constraint pk_Prospect primary key clustered (prospect_id asc)
)
;
go

/* table 8 dbo.Tasks */

create table dbo.Tasks
(
	task_id int identity (1,1) not null,
	salesrep_id int not null,
	contact_id int not null,
	schedule datetime not null,
	alarm bit not null,
	alarm_value int null,
	alarm_time varchar(15) null,
	activity varchar(500) not null,
	completed bit not null,
	assign_to int not null
	constraint pk_Tasks primary key clustered (task_id asc) 
)
;
go

/* table 9 dbo.appointments */
create table dbo.Appointments
(
	appointment_id int identity (1,1) not null,
	start_date datetime not null,
	end_date datetime not null,
	location varchar (50) not null,
	resource_id int not null,
	subject varchar (100) not null,
	note varchar (500) not null
	constraint pk_Appointments primary key clustered (appointment_id asc)
)
;
go

/* table 10 dbo.Resources */
create table dbo.Resources
(
	resource_id int identity (1,1) not null,
	description varchar(50) not null
	constraint pk_Resources primary key clustered (resource_id asc)
)
;
go

/* table 11 dbo.Emails */

create table dbo.Emails
(
	email_id int identity (1,1) not null,
	contact_id int not null,
	sent_date datetime not null,
	e_subject varchar(50),
	e_body varchar(500)
	constraint pk_Emails primary key clustered (email_id asc)
)
;
go

/* table

