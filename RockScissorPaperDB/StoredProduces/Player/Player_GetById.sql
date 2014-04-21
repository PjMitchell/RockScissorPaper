CREATE PROCEDURE [dbo].[Player_GetById]
	@PlayerId int
AS
RETURN 
SELECT * 

FROM Player
WHERE PlayerId = @PlayerId