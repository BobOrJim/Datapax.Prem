USE [myDB]
GO
/****** Object:  StoredProcedure [dbo].[FactoryTable_insert]    Script Date: 2021-05-10 10:26:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[FactoryTable_insert] 
	@ToTable_TEXT TEXT,
	@Timestamp_unix_BIGINT BIGINT,
	@Datestamp_TEXT TEXT,
	@DeviationID_TEXT TEXT,
	@Hub2Hub_KKS123_SystemVolt_Erratic BIT,
	@Hub2Hub_KKS123_SystemVolt_Low BIT,
	@Hub2Hub_KKS123_Retarder_LowCurrent BIT,
	@Hub2Hub_KKS123_AuxPressure_Low BIT,
	@Panna_flisinmatning_skruv1_Motorskydd BIT,
	@Panna_flisinmatning_skruv1_Sakerhetsbrytare BIT,
	@Panna_flisinmatning_skruv1_Varvtalsvakt BIT,
	@Panna_flisinmatning_skruv1_Nodstop BIT,
	@Panna_Fribord_flisinmating_pt1000 INT,
	@Panna_Fribord_askutmating_pt1000 INT,
	@Panna_Fribord_ForeBrannare_pt1000 INT,
	@Panna_Fribord_EfterBrannare_pt1000 INT,
	@Karlatornet_Ventilation_Franluft_HogTemp BIT,
	@Karlatornet_Ventilation_Franluft_LagTemp BIT,
	@Karlatornet_Brandlarm_Hiss1_Aktivt BIT,
	@Karlatornet_Brandlarm_Hiss2_Aktivt BIT,
	@Vestas_Verk12_Koppling_HogTemp BIT,
	@Vestas_Verk12_Koppling_LagOljeNiva BIT,
	@Vestas_Verk12_Koppling_TryckAvvikelse BIT,
	@Vestas_Verk12_Vaderstation_WatchDog BIT
AS
BEGIN
	SET NOCOUNT ON;
	
    insert into dbo.FactoryTable (Timestamp_unix_BIGINT, Datestamp_TEXT, DeviationID_TEXT, 
	Hub2Hub_KKS123_SystemVolt_Erratic, Hub2Hub_KKS123_SystemVolt_Low, Hub2Hub_KKS123_Retarder_LowCurrent, Hub2Hub_KKS123_AuxPressure_Low,
	Panna_flisinmatning_skruv1_Motorskydd, Panna_flisinmatning_skruv1_Sakerhetsbrytare, Panna_flisinmatning_skruv1_Varvtalsvakt, Panna_flisinmatning_skruv1_Nodstop,
	Panna_Fribord_flisinmating_pt1000, Panna_Fribord_askutmating_pt1000, Panna_Fribord_ForeBrannare_pt1000, Panna_Fribord_EfterBrannare_pt1000,
	Karlatornet_Ventilation_Franluft_HogTemp, Karlatornet_Ventilation_Franluft_LagTemp, Karlatornet_Brandlarm_Hiss1_Aktivt, Karlatornet_Brandlarm_Hiss2_Aktivt,
	Vestas_Verk12_Koppling_HogTemp, Vestas_Verk12_Koppling_LagOljeNiva, Vestas_Verk12_Koppling_TryckAvvikelse, Vestas_Verk12_Vaderstation_WatchDog)

	values (@Timestamp_unix_BIGINT, @Datestamp_TEXT, @DeviationID_TEXT, 
	@Hub2Hub_KKS123_SystemVolt_Erratic, @Hub2Hub_KKS123_SystemVolt_Low, @Hub2Hub_KKS123_Retarder_LowCurrent, @Hub2Hub_KKS123_AuxPressure_Low,
	@Panna_flisinmatning_skruv1_Motorskydd, @Panna_flisinmatning_skruv1_Sakerhetsbrytare, @Panna_flisinmatning_skruv1_Varvtalsvakt, @Panna_flisinmatning_skruv1_Nodstop,
	@Panna_Fribord_flisinmating_pt1000, @Panna_Fribord_askutmating_pt1000, @Panna_Fribord_ForeBrannare_pt1000, @Panna_Fribord_EfterBrannare_pt1000,
	@Karlatornet_Ventilation_Franluft_HogTemp, @Karlatornet_Ventilation_Franluft_LagTemp, @Karlatornet_Brandlarm_Hiss1_Aktivt, @Karlatornet_Brandlarm_Hiss2_Aktivt,
	@Vestas_Verk12_Koppling_HogTemp, @Vestas_Verk12_Koppling_LagOljeNiva, @Vestas_Verk12_Koppling_TryckAvvikelse, @Vestas_Verk12_Vaderstation_WatchDog)
END






