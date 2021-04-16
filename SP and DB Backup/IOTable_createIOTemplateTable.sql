USE [myDB]
GO
/****** Object:  StoredProcedure [dbo].[IOTable_createIOTemplateTable]    Script Date: 2021-01-19 20:20:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[IOTable_createIOTemplateTable] 
@TABLE NVARCHAR(128) = NULL
AS 
BEGIN 
	SET NOCOUNT ON;
	DECLARE @Sql NVARCHAR(MAX);

	SET @Sql =	N'CREATE TABLE ' + QUOTENAME(@TABLE) +
				'(' +
				'[Id] INT NOT NULL PRIMARY KEY IDENTITY,' +
				'[ToTable_TEXT] TEXT NULL,' +
				'[Timestamp_unix_BIGINT] BIGINT NULL,' +
				'[Datestamp_TEXT] TEXT NULL,' +
				'[DeviationID_TEXT] TEXT NULL,' +
				'[Bit1] BIT NULL,' +
				'[Bit2] BIT NULL,' +
				'[Bit3] BIT NULL' +
				')'

	EXEC(@Sql);

END


/*

ALTER PROCEDURE [dbo].[IOTable_createIOTemplateTable] 
@TABLE NVARCHAR(128) = NULL
AS 
BEGIN 
	SET NOCOUNT ON;
	DECLARE @Sql NVARCHAR(MAX);

	SET @Sql =	N'CREATE TABLE ' + QUOTENAME(@TABLE) +
				'(' +
				'[Id] INT NOT NULL PRIMARY KEY IDENTITY,' +
				'[ToTable_TEXT] TEXT NULL,' +
				'[Timestamp_unix_BIGINT] BIGINT NULL,' +
				'[Datestamp_TEXT] TEXT NULL,' +
				'[DeviationID_TEXT] TEXT NULL,' +
				'[Bit1] BIT NULL,' +
				'[Bit2] BIT NULL,' +
				'[Bit3] BIT NULL' +
				')'

	EXEC(@Sql);

END

*/