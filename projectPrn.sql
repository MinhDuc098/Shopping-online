create table category(
categoryId int IDENTITY(1,1) PRIMARY KEY,
categoryName varchar(50)
)

create table product(
productId int IDENTITY(1,1) PRIMARY KEY,
productName varchar(50),
UnitPrice float,
UnitInStoke int,
[Image] varchar(max),
categoryId int references category(categoryId)
)

create table Account(
AccountId int IDENTITY(1,1) PRIMARY KEY,
userName varchar(50),
[password] varchar(50),
email varchar(100),
[address] varchar(max),
phoneNumber varchar(15)
)

create table OrderLine(
OrderLineId int IDENTITY(1,1) PRIMARY KEY,
AccountId int references Account(AccountId),
ProductId int references product(productId),
quantity int
)