--Select Tournaments.TournamentID,
--		DateOfTournament,	
--		Venues.Name,
--		PlayerCount,
--		FirstName,
--		LastName,
--		TotalPrizePool,
--		Position01,
--		sum(TotalPrizePool * Position01) as 'FirstPrize'
		
		
--from (((((TournamentDetails
--	inner join Tournaments	on Tournaments.TournamentID			= TournamentDetails.TournamentID)
--	inner join Players		on TournamentDetails.PlayerID		= Players.PlayerID)
--	inner join PayoutStructures	on Tournaments.PayoutStructureID	= PayoutStructures.PayoutStructureID)
--	inner join Permits on Tournaments.PermitID = Permits.PermitID)
--	inner join Venues on Permits.VenueID = Venues.VenueID)
--	where TournamentDetails.Position = 1 and DateOfTournament < convert(varchar(10), getdate(), 111)
--	group by Tournaments.TournamentID, DateOfTournament, FirstName, LastName, TotalPrizePool, Position01,  PlayerCount, Venues.Name

Select  Venues.Name,
        DateOfTournament,        
        FirstName,
        LastName,
        sum(TotalPrizePool* Position01) as 'FirstPrize'



from (((((TournamentDetails
    inner join Tournaments  on Tournaments.TournamentID         = TournamentDetails.TournamentID)

    inner join Players on TournamentDetails.PlayerID		= Players.PlayerID)
	inner join PayoutStructures on Tournaments.PayoutStructureID	= PayoutStructures.PayoutStructureID)
	inner join Permits on Tournaments.PermitID = Permits.PermitID)
	inner join Venues on Permits.VenueID = Venues.VenueID)
	where TournamentDetails.Position = 1 and DateOfTournament < convert(varchar(10), getdate(), 111)
	group by Tournaments.TournamentID, DateOfTournament, FirstName, LastName, TotalPrizePool, Position01,  PlayerCount, Venues.Name


