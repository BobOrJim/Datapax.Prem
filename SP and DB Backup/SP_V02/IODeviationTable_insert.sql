USE [myDB]
GO
/****** Object:  StoredProcedure [dbo].[IODeviationTable_insert]    Script Date: 2021-04-16 15:04:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[IODeviationTable_insert] 
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
	
    insert into dbo.IODeviationTable (Timestamp_unix_BIGINT, Datestamp_TEXT, DeviationID_TEXT, Bit1, Bit2, Bit3)
	values (@Timestamp_unix_BIGINT, @Datestamp_TEXT, @DeviationID_TEXT, @Bit1, @Bit2, @Bit3);
	
END


