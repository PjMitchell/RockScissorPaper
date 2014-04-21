CREATE PROCEDURE [dbo].[Game_CountActive]
AS
RETURN
 
SELECT Count(*) AS result
FROM roshambogame
WHERE GameStatus != 6 and StartDate > DATEADD(MINUTE, 30, GETUTCDATE())
GO
