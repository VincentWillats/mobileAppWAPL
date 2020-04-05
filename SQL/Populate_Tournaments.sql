/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [TournamentID]
      ,[TournamentTypeID]
      ,[PermitID]
      ,[DateOfTournament]
      ,[PayoutStructureID]
      ,[TotalPrizePool]
  FROM [WAPL].[dbo].[Tournaments]