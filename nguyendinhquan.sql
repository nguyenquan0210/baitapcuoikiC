create database nguyendinhquanDB

CREATE TABLE UserAccount(
  ID CHAR(15) NOT NULL PRIMARY KEY CONSTRAINT IDUserAccount DEFAULT DBO.auto_idUserAccount(),
  UserName char(30) not NULL,
  Password char(200) not NULL,
  Status int NULL  DEFAULT(0)
  
) 

CREATE TABLE san_pham (
  ID CHAR(15) NOT NULL PRIMARY KEY CONSTRAINT IDsan_pham DEFAULT DBO.auto_idsan_pham(),
  ID_l char(15) NOT NULL foreign key references loai_sp(ID),
  Name nvarchar(100)  NOT NULL,
  UnitCost int NOT NULL,
  Description nvarchar(256) NULL ,
  Image nvarchar(50)  NOT NULL,
  Status int  NULL DEFAULT(0),
) 

CREATE TABLE  loai_sp (
  ID CHAR(15) NOT NULL PRIMARY KEY CONSTRAINT IDloai_sp DEFAULT DBO.auto_idloai_sp(),
  Name nvarchar(128)  NOT NULL,
  Description nvarchar(256) NULL 
)

CREATE FUNCTION auto_idloai_sp()
RETURNS VARCHAR(15)
AS
BEGIN
	DECLARE @id VARCHAR(15)
	IF (SELECT COUNT(ID) FROM loai_sp) = 0
		SET @id = '0'
	ELSE
		SELECT @id = MAX(RIGHT(ID, 5)) FROM loai_sp
		SELECT @id = CASE
			WHEN @id = 99999 THEN CONVERT(VARCHAR,GETDATE(),112) + 'LM00001'
			WHEN @id >= 0 and @id < 9 THEN CONVERT(VARCHAR,GETDATE(),112) + 'LM0000' + CONVERT(CHAR, CONVERT(INT, @id) + 1)
			WHEN @id >= 9 THEN CONVERT(VARCHAR,GETDATE(),112) + 'LM000' + CONVERT(CHAR, CONVERT(INT, @id) + 1)
			WHEN @id >= 99 THEN CONVERT(VARCHAR,GETDATE(),112) + 'LM00' + CONVERT(CHAR, CONVERT(INT, @id) + 1)
			WHEN @id >= 999 THEN CONVERT(VARCHAR,GETDATE(),112) + 'LM0' + CONVERT(CHAR, CONVERT(INT, @id) + 1)
			WHEN @id >= 9999 THEN CONVERT(VARCHAR,GETDATE(),112) + 'LM' + CONVERT(CHAR, CONVERT(INT, @id) + 1)
		END
	RETURN @id
END
CREATE FUNCTION auto_idUserAccount()
RETURNS VARCHAR(15)
AS
BEGIN
	DECLARE @id VARCHAR(15)
	IF (SELECT COUNT(id) FROM UserAccount) = 0
		SET @id = '0'
	ELSE
		SELECT @id = MAX(RIGHT(id, 5)) FROM UserAccount
		SELECT @id = CASE
			WHEN @id = 99999 THEN CONVERT(VARCHAR,GETDATE(),112) + 'US00001'
			WHEN @id >= 0 and @id < 9 THEN CONVERT(VARCHAR,GETDATE(),112) + 'US0000' + CONVERT(CHAR, CONVERT(INT, @id) + 1)
			WHEN @id >= 9 THEN CONVERT(VARCHAR,GETDATE(),112) + 'US000' + CONVERT(CHAR, CONVERT(INT, @id) + 1)
			WHEN @id >= 99 THEN CONVERT(VARCHAR,GETDATE(),112) + 'US00' + CONVERT(CHAR, CONVERT(INT, @id) + 1)
			WHEN @id >= 999 THEN CONVERT(VARCHAR,GETDATE(),112) + 'US0' + CONVERT(CHAR, CONVERT(INT, @id) + 1)
			WHEN @id >= 9999 THEN CONVERT(VARCHAR,GETDATE(),112) + 'US' + CONVERT(CHAR, CONVERT(INT, @id) + 1)
		END
	RETURN @id
END

CREATE FUNCTION auto_idsan_pham()
RETURNS VARCHAR(15)
AS
BEGIN
	DECLARE @id VARCHAR(15)
	IF (SELECT COUNT(ID) FROM san_pham) = 0
		SET @id = '0'
	ELSE
		SELECT @id = MAX(RIGHT(ID, 5)) FROM san_pham
		SELECT @id = CASE
			WHEN @id = 99999 THEN CONVERT(VARCHAR,GETDATE(),112) + 'SP00001'
			WHEN @id >= 0 and @id < 9 THEN CONVERT(VARCHAR,GETDATE(),112) + 'SP0000' + CONVERT(CHAR, CONVERT(INT, @id) + 1)
			WHEN @id >= 9 THEN CONVERT(VARCHAR,GETDATE(),112) + 'SP000' + CONVERT(CHAR, CONVERT(INT, @id) + 1)
			WHEN @id >= 99 THEN CONVERT(VARCHAR,GETDATE(),112) + 'SP00' + CONVERT(CHAR, CONVERT(INT, @id) + 1)
			WHEN @id >= 999 THEN CONVERT(VARCHAR,GETDATE(),112) + 'SP0' + CONVERT(CHAR, CONVERT(INT, @id) + 1)
			WHEN @id >= 9999 THEN CONVERT(VARCHAR,GETDATE(),112) + 'SP' + CONVERT(CHAR, CONVERT(INT, @id) + 1)
		END
	RETURN @id
END