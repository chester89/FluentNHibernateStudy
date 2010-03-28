if exists(select * from sys.databases where name = 'FluentNH')
	drop database FluentNH

create database FluentNH
go
use FluentNH

create table Store (
	Id int primary key identity not null, 
	Name varchar(50)
)

create table Employee (
	Id int primary key identity not null, 
	LastName varchar(50), 
	FirstName varchar(50), 
	StoreId int not null
		foreign key references Store(Id)
)

create table Product (
	Id int primary key identity not null, 
	Price decimal, 
	Name varchar(50)
)

create table StoreProduct (
	ProductId int not null
		foreign key references Product(Id), 
	StoreId int not null
		foreign key references Store(Id)
)

