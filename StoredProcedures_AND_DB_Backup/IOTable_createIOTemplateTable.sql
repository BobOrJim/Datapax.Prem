USE [myDB]
GO
/****** Object:  StoredProcedure [dbo].[IOTable_createIOTemplateTable]    Script Date: 2021-05-10 10:27:23 ******/
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
				'[Hub2Hub_KKS123_SystemVolt_Erratic] BIT NULL,' +
				'[Hub2Hub_KKS123_SystemVolt_Low] BIT NULL,' +
				'[Hub2Hub_KKS123_Retarder_LowCurrent] BIT NULL,' +
				'[Hub2Hub_KKS123_AuxPressure_Low] BIT NULL,' +
				'[Panna_flisinmatning_skruv1_Motorskydd] BIT NULL,' +
				'[Panna_flisinmatning_skruv1_Sakerhetsbrytare] BIT NULL,' +
				'[Panna_flisinmatning_skruv1_Varvtalsvakt] BIT NULL,' +
				'[Panna_flisinmatning_skruv1_Nodstop] BIT NULL,' +
				'[Panna_Fribord_flisinmating_pt1000] INT NULL,' +
				'[Panna_Fribord_askutmating_pt1000] INT NULL,' +
				'[Panna_Fribord_ForeBrannare_pt1000] INT NULL,' +
				'[Panna_Fribord_EfterBrannare_pt1000] INT NULL,' +
				'[Karlatornet_Ventilation_Franluft_HogTemp] BIT NULL,' +
				'[Karlatornet_Ventilation_Franluft_LagTemp] BIT NULL,' +
				'[Karlatornet_Brandlarm_Hiss1_Aktivt] BIT NULL,' +
				'[Karlatornet_Brandlarm_Hiss2_Aktivt] BIT NULL,' +
				'[Vestas_Verk12_Koppling_HogTemp] BIT NULL,' +
				'[Vestas_Verk12_Koppling_LagOljeNiva] BIT NULL,' +
				'[Vestas_Verk12_Koppling_TryckAvvikelse] BIT NULL,' +
				'[Vestas_Verk12_Vaderstation_WatchDog] BIT NULL' +
				')'
	EXEC(@Sql);
END

