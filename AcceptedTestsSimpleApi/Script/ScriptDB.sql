IF OBJECT_ID('dbo.Cliente') IS NULL
BEGIN
	Create table Cliente (Id int primary key not null identity, Nome varchar(30), SobreNome varchar(30))
END

TRUNCATE TABLE Cliente