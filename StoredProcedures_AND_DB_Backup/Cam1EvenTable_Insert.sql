USE [myDB]
GO
/****** Object:  StoredProcedure [dbo].[Cam1EvenTable_Insert]    Script Date: 2021-05-10 10:22:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Cam1EvenTable_Insert] 
	@ToTable_TEXT TEXT,
	@Timestamp_unix_BIGINT BIGINT,
	@Datestamp_TEXT TEXT,
	@DeviationID_TEXT TEXT,
	@PictureFileNamePrefix_TEXT TEXT,

	@FilePathCurrent_TEXT TEXT,
	@FileNameCurrent_TEXT TEXT,
	@FileEndingCurrent_TEXT TEXT,

	@FilePathWork_TEXT TEXT,
	@FileNameWork_TEXT TEXT,
	@FileEndingWork_TEXT TEXT,

	@FilePathKeep_TEXT TEXT,
	@FileNameKeep_TEXT TEXT,
	@FileEndingKeep_TEXT TEXT,

	@FilePathSpare1_TEXT TEXT,
	@FileNameSpare1_TEXT TEXT,
	@FileEndingSpare1_TEXT TEXT,

	@FilePathSpare2_TEXT TEXT,
	@FileNameSpare2_TEXT TEXT,
	@FileEndingSpare2_TEXT TEXT,

	@IsLabeledForGarbageCollector_BIT BIT,
	@SpareBit_BIT BIT
AS
BEGIN
	SET NOCOUNT ON;
	
    insert into dbo.Cam1EvenTable (Timestamp_unix_BIGINT, Datestamp_TEXT, DeviationID_TEXT, PictureFileNamePrefix_TEXT, 
	FilePathCurrent_TEXT, FileNameCurrent_TEXT, FileEndingCurrent_TEXT,
	FilePathWork_TEXT, FileNameWork_TEXT, FileEndingWork_TEXT,
	FilePathKeep_TEXT, FileNameKeep_TEXT, FileEndingKeep_TEXT,
	FilePathSpare1_TEXT, FileNameSpare1_TEXT, FileEndingSpare1_TEXT,
	FilePathSpare2_TEXT, FileNameSpare2_TEXT, FileEndingSpare2_TEXT,
	IsLabeledForGarbageCollector_BIT, SpareBit_BIT)
	
	values (@Timestamp_unix_BIGINT, @Datestamp_TEXT, @DeviationID_TEXT, @PictureFileNamePrefix_TEXT, 
	@FilePathCurrent_TEXT, @FileNameCurrent_TEXT, @FileEndingCurrent_TEXT, 
	@FilePathWork_TEXT, @FileNameWork_TEXT, @FileEndingWork_TEXT,
	@FilePathKeep_TEXT, @FileNameKeep_TEXT, @FileEndingKeep_TEXT,
	@FilePathSpare1_TEXT, @FileNameSpare1_TEXT, @FileEndingSpare1_TEXT,
	@FilePathSpare2_TEXT, @FileNameSpare2_TEXT, @FileEndingSpare2_TEXT,
	@IsLabeledForGarbageCollector_BIT, @SpareBit_BIT);
	
END


