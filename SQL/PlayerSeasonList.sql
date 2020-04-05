SELECT ET.Season

  FROM [dbo].[EventResults] as ER
  inner join
  (
  SELECT EventID,
		Season
		from Event
  ) as ET on ER.EventID = ET.EventID

  where PlayerID = 2
  group by Season
  order by Season desc