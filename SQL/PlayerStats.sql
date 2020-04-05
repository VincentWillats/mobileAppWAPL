SELECT  
		sub.PlayerID,
		EventTable.Season,
		--Total game Stats
		COUNT(*) as 'Total_Games',
		total_games.Total_Games_RankNo,
		Cast(total_games.[Top_%_Of_Games] as decimal(10,2)) as 'Top_%_Of_GamesPlayed',
		--Win Stats
		COUNT(case sub.Position when 1 then 1 else null end) as 'Wins',
		wins.Total_Wins_RankNo,
		CAST(wins.[Top_%_Of_Wins] as decimal(10,2)) as 'Top_%_Of_Wins',
		CAST(CAST(COUNT(case sub.Position when 1 then 1 else null end) as decimal(10,2)) / NULLIF(cast(Count(*) as decimal(10,2)), 0) as decimal(10,2)) as 'Win_PCT',	
		winPCT.Win_PCT_RankNo,
		CAST(winPCT.[Top_%_Of_Win%] as decimal(10,2)) as 'Top%_Of_Win%',
		-- Heads Up Stats
		COUNT(case when sub.Position IN(1,2) then 1 else null end) as 'Total_HU',
		TotalHU.Total_HU_RankNo, 
		CAST(TotalHU.[Top_%_of_HeadsUp] as decimal(10,2)) as 'Top_%_of_HeadsUP',
		CAST(CAST(COUNT(case sub.Position when 1 then 1 else null end) as decimal) / NULLIF(CAST(COUNT(case when sub.Position IN(1,2) then 1 else null end) as decimal), 0) as decimal(10,2)) as 'Win_HU_PCT',		
		HuWins.Win_HU_RankNo,
		CAST(HuWins.[Top_%_HU_Wins]as decimal(10,2)) as 'Top_%_HU_Wins', 
		-- Final Table Stats
		COUNT(case when sub.Position IN(1,2,3,4,5,6,7,8) then 1 else null end) as 'Final_Tables',
		FinalTables.Final_Tables_RankNo, 
		CAST(FinalTables.[Top_%_FT] as decimal(10,2)) as 'Top_%_Of_FTs',
		CAST(CAST(COUNT(case sub.Position when 1 then 1 else null end) as decimal) / NULLIF(CAST(COUNT(case when sub.Position IN(1,2,3,4,5,6,7,8) then 1 else null end) as decimal), 0) as decimal(10,2)) as 'Win_PCT_Final_Tables',
		FinalTablePCT.FT_Win_PCT_RankNo, 
		CAST(FinalTablePCT.[Top_%_FT_Win%]as decimal(10,2)) as 'TOP_%_FT_Win%',
		-- % of field beaten


		AVG(sub.r_field_beaten) AS 'AVG_PCT_FIELD_BEATEN'		
		FROM (SELECT er.PlayerID,
				er.EventID, 
				er.Position,				
				CAST(CAST((COUNT(*) OVER (PARTITION BY er.EventID)-er.Position+1) as decimal(10,2)) / NULLIF(CAST(COUNT(*) OVER (PARTITION BY er.EventID)as decimal(10,2)), 0) as decimal(10,2))  AS R_FIELD_BEATEN
FROM EventResults er) as sub

inner join 
(
SELECT PlayerID,
		Total_Games_RankNo,
		Total_Games,
		'Top_%_Of_Games' = CAST(Total_Games_RankNo as decimal) / CAST(
			(SELECT Count(DISTINCT PlayerID)			
			FROM EventResults) as decimal)

	FROM(SELECT PlayerID,
			COUNT(*) as 'Total_Games',
			ROW_NUMBER() OVER(ORDER BY COUNT(*) desc, PlayerID) as 'Total_Games_RankNo'
FROM EventResults
group by PlayerID) sub
group by PlayerID, Total_Games_RankNo, Total_Games
) 
as total_games on sub.PlayerID = total_games.PlayerID
inner join 
(
SELECT PlayerID,
	
		sub.Wins,
		Total_Wins_RankNo,
		'Top_%_Of_Wins' = CAST(Total_Wins_RankNo as decimal) / CAST(
			(SELECT Count(DISTINCT PlayerID)			
			FROM EventResults) as decimal)
	FROM(SELECT PlayerID,
				COUNT(case Position when 1 then 1 else null end) as 'Wins',
			ROW_NUMBER() OVER(ORDER BY COUNT(case Position when 1 then 1 else null end) desc, PlayerID) as 'Total_Wins_RankNo'
From EventResults
group by PlayerID) sub
group by PlayerID, sub.Wins, Total_Wins_RankNo
) as wins on sub.PlayerID = wins.PlayerID
inner join
(
SELECT PlayerID,	
		sub.Win_PCT,
		Win_PCT_RankNo,
		'Top_%_Of_Win%' = CAST(Win_PCT_RankNo as decimal) / CAST(
			(SELECT Count(DISTINCT PlayerID)			
			FROM EventResults) as decimal)
	FROM(SELECT PlayerID,
		Win_PCT =
				CAST(CAST(COUNT(case Position when 1 then 1 else null end) as decimal(10,2)) / NULLIF(cast(Count(*) as decimal(10,2)), 0) as decimal(10,2)),
			ROW_NUMBER() OVER(ORDER BY (CAST(CAST(COUNT(case Position when 1 then 1 else null end) as decimal(10,2)) / NULLIF(cast(Count(*) as decimal(10,2)), 0) as decimal(10,2))) desc) as 'Win_PCT_RankNo'
From EventResults
group by PlayerID) sub
group by PlayerID, sub.Win_PCT, Win_PCT_RankNo
) as winPCT on sub.PlayerID = winPCT.PlayerID
inner join
(
SELECT PlayerID,	
		sub.Total_HU,
		Total_HU_RankNo,
		'Top_%_of_HeadsUp' = CAST(Total_HU_RankNo as decimal) / CAST(
			(SELECT Count(DISTINCT PlayerID)			
			FROM EventResults) as decimal)
	FROM(SELECT PlayerID,
				COUNT(case when Position IN(1,2) then 1 else null end) as 'Total_HU',
			ROW_NUMBER() OVER(ORDER BY COUNT(case when Position IN(1,2) then 1 else null end) desc, PlayerID) as 'Total_HU_RankNo'
From EventResults
group by PlayerID) sub
group by PlayerID, sub.Total_HU, Total_HU_RankNo
) as TotalHU on sub.PlayerID = TotalHU.PlayerID
inner join
(
SELECT PlayerID,	
		sub.Win_HU_PCT,
		Win_HU_RankNo,
		'Top_%_HU_Wins' = CAST(Win_HU_RankNo as decimal) / CAST(
			(SELECT Count(DISTINCT PlayerID)			
			FROM EventResults) as decimal)
	FROM(SELECT PlayerID,
				CAST(CAST(COUNT(case Position when 1 then 1 else null end) as decimal) / NULLIF(CAST(COUNT(case when Position IN(1,2) then 1 else null end) as decimal), 0) as decimal(10,2)) as 'Win_HU_PCT',
			ROW_NUMBER() OVER(ORDER BY CAST(CAST(COUNT(case Position when 1 then 1 else null end) as decimal) / NULLIF(CAST(COUNT(case when Position IN(1,2) then 1 else null end) as decimal), 0) as decimal(10,2)) desc, PlayerID) as 'Win_HU_RankNo'
From EventResults
group by PlayerID) sub
group by PlayerID, sub.Win_HU_PCT, Win_HU_RankNo
) as HuWins on sub.PlayerID = HuWins.PlayerID
inner join
(
SELECT PlayerID,	
		sub.Final_Tables,
		Final_Tables_RankNo,
		'Top_%_FT' = CAST(Final_Tables_RankNo as decimal) / CAST(
			(SELECT Count(DISTINCT PlayerID)			
			FROM EventResults) as decimal)
	FROM(SELECT PlayerID,
				COUNT(case when Position IN(1,2,3,4,5,6,7,8) then 1 else null end) as 'Final_Tables',

			ROW_NUMBER() OVER(ORDER BY COUNT(case when Position IN(1,2,3,4,5,6,7,8) then 1 else null end) desc, PlayerID) as 'Final_Tables_RankNo'
From EventResults
group by PlayerID) sub
group by PlayerID, sub.Final_Tables, Final_Tables_RankNo
) as FinalTables on sub.PlayerID = FinalTables.PlayerID
inner join
(
SELECT PlayerID,	
		sub.FT_Win_PCT,
		FT_Win_PCT_RankNo,
		'Top_%_FT_Win%' = CAST(FT_Win_PCT_RankNo as decimal) / CAST(
			(SELECT Count(DISTINCT PlayerID)			
			FROM EventResults) as decimal)
	FROM(SELECT PlayerID,
				CAST(CAST(COUNT(case Position when 1 then 1 else null end) as decimal) / NULLIF(CAST(COUNT(case when Position IN(1,2,3,4,5,6,7,8) then 1 else null end) as decimal), 0) as decimal(10,2)) as 'FT_Win_PCT',

			ROW_NUMBER() OVER(ORDER BY CAST(CAST(COUNT(case Position when 1 then 1 else null end) as decimal) / NULLIF(CAST(COUNT(case when Position IN(1,2,3,4,5,6,7,8) then 1 else null end) as decimal), 0) as decimal(10,2)) desc, PlayerID) as 'FT_Win_PCT_RankNo'
From EventResults
group by PlayerID) sub
group by PlayerID, sub.FT_Win_PCT, FT_Win_PCT_RankNo
) as FinalTablePCT on sub.PlayerID = FinalTablePCT.PlayerID
inner join
(
SELECT EventID, Season
FROM Event
) as EventTable on sub.EventID = EventTable.EventID

------ CHANGE sub.playerID to what player and EventTable.Season to what season -------
where sub.PlayerID = 2 and EventTable.Season = 2
------

GROUP BY sub.PlayerID, total_games.Total_Games_RankNo, total_games.[Top_%_Of_Games], wins.Total_Wins_RankNo, wins.[Top_%_Of_Wins], winPCT.[Top_%_Of_Win%], winPCT.Win_PCT_RankNo, TotalHU.Total_HU_RankNo, TotalHU.[Top_%_of_HeadsUp], HuWins.[Top_%_HU_Wins], HuWins.Win_HU_RankNo, FinalTables.Final_Tables_RankNo, FinalTables.[Top_%_FT], FinalTablePCT.FT_Win_PCT_RankNo, FinalTablePCT.[Top_%_FT_Win%], EventTable.Season
order by AVG_PCT_FIELD_BEATEN


