USE [myDB]
GO
/****** Object:  StoredProcedure [dbo].[IOTable_getAllPostsInTable]    Script Date: 2021-05-10 10:28:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[IOTable_getAllPostsInTable] 
	@TABLE NVARCHAR(128) = NULL

AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Sql NVARCHAR(MAX);

	SET @Sql =	N'SELECT * FROM ' + QUOTENAME(@TABLE)
	EXEC(@Sql);

	/*SELECT * FROM IOEvenTable*/

END


	


