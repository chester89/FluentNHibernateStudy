if exists(select * from sys.databases where name = 'FluentNH')
begin
	use FluentNH
	go

		delete from Employee
		delete from StoreProduct
		delete from Product
		delete from Store
end