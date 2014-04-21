CREATE PROCEDURE [dbo].[Game_GetGamesPlayed]
AS
RETURN 
SELECT Count(*)

FROM roshambogame

WHERE GameStatus = 6