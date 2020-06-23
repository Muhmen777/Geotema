/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [BrugerID]
      ,[fuldeNavn]
      ,[brugernavn]
      ,[Kodeord]
      ,[gentagKodeord]
      ,[tildeltRettighed]
  FROM [GeoTema].[dbo].[Brugere]

SELECT TOP (1000) [LandID]
      ,[Land]
      ,[Verdensdel]
  FROM [GeoTema].[dbo].[Land]

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [RangID]
      ,[Rang]
      ,[Fodselsrate]
      ,[LandID]
  FROM [GeoTema].[dbo].[Rang]

