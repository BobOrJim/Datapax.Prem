USE [myDB]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[PictureTable_cutPostsBetweenInTable] 
	@TABLE NVARCHAR(128) = NULL,
	@startTime NVARCHAR(MAX) = 0,
	@endTime NVARCHAR(MAX) = 0

AS
BEGIN
	DECLARE @Sql NVARCHAR(MAX);
	
	
	Set @Sql = 'SELECT * FROM ' + QUOTENAME(@TABLE) + 'WHERE Timestamp_unix_BIGINT > ' + @startTime + ' AND Timestamp_unix_BIGINT < ' + @endTime;
	EXEC(@Sql);


END
