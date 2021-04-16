USE [myDB]
GO
/****** Object:  StoredProcedure [dbo].[FactoryTable_insert]    Script Date: 2021-01-19 19:36:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[FactoryTable_insert] 
	@ToTable_TEXT TEXT,
	@Timestamp_unix_BIGINT BIGINT,
	@Datestamp_TEXT TEXT,
	@DeviationID_TEXT TEXT,
	@Bit1 BIT,
	@Bit2 BIT,
	@Bit3 BIT
AS
BEGIN
	SET NOCOUNT ON;
	
    insert into dbo.FactoryTable (Timestamp_unix_BIGINT, Datestamp_TEXT, DeviationID_TEXT, Bit1, Bit2, Bit3)
	values (@Timestamp_unix_BIGINT, @Datestamp_TEXT, @DeviationID_TEXT, @Bit1, @Bit2, @Bit3);
	
END


