USE [myDB]
GO
/****** Object:  StoredProcedure [dbo].[PictureTable_createPictureTemplateTable]    Script Date: 2021-05-10 10:29:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[PictureTable_createPictureTemplateTable] 
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
				'[PictureFileNamePrefix_TEXT] TEXT NULL,' + /* Tex grind kamera, topkamera osv */
				'[FilePathCurrent_TEXT] TEXT NULL,' +
				'[FileNameCurrent_TEXT] TEXT NULL,' +
				'[FileEndingCurrent_TEXT] TEXT NULL,' +
				'[FilePathWork_TEXT] TEXT NULL,' +
				'[FileNameWork_TEXT] TEXT NULL,' +
				'[FileEndingWork_TEXT] TEXT NULL,' +

				'[FilePathKeep_TEXT] TEXT NULL,' +
				'[FileNameKeep_TEXT] TEXT NULL,' +
				'[FileEndingKeep_TEXT] TEXT NULL,' +

				'[FilePathSpare1_TEXT] TEXT NULL,' +
				'[FileNameSpare1_TEXT] TEXT NULL,' +
				'[FileEndingSpare1_TEXT] TEXT NULL,' +

				'[FilePathSpare2_TEXT] TEXT NULL,' +
				'[FileNameSpare2_TEXT] TEXT NULL,' +
				'[FileEndingSpare2_TEXT] TEXT NULL,' +

				'[IsLabeledForGarbageCollector_BIT] BIT NULL,' +
				'[SpareBit_BIT] BIT NULL' +
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