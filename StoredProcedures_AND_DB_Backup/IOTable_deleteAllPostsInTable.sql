USE [myDB]
GO
/****** Object:  StoredProcedure [dbo].[IOTable_deleteAllPostsInTable]    Script Date: 2021-05-10 10:28:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[IOTable_deleteAllPostsInTable] 
@TABLE NVARCHAR(128) = NULL
AS 
BEGIN 
	SET NOCOUNT ON;
	DECLARE @Sql NVARCHAR(MAX);

	SET @Sql =	N'TRUNCATE TABLE ' +QUOTENAME(@TABLE)

	EXEC(@Sql);

END


/*
DELETE FROM [FactoryTable] WHERE Datestamp_TEXT='2021-01-18 23:20:11';

SET @Sql =	N'DELETE FROM dbo.' + QUOTENAME(@TABLE)
			+ N' WHERE Timestamp_unix_BIGINT=' + @POST


DELETE FROM [FactoryTable] WHERE Timestamp_unix_BIGINT = [1611010848228]


SET @Sql =	N'DELETE FROM dbo.' + QUOTENAME(@TABLE)
			+ N' WHERE Timestamp_unix_BIGINT=' + QUOTENAME(@POST)

*/