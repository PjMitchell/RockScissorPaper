DROP PROCEDURE IF EXISTS `Gameround_GetGroupedByDate`;
DELIMITER $$


CREATE PROCEDURE `Gameround_GetGroupedByDate` ()

BEGIN 

SELECT Date(StartDate) as 'Date',
 SUM(CASE WHEN SelectionId = 1 THEN 1 ELSE 0 END) as Rock,
 SUM(CASE WHEN SelectionId = 2 THEN 1 ELSE 0 END) as Scissor,
 SUM(CASE WHEN SelectionId = 3 THEN 1 ELSE 0 END) as Paper
 FROM roundselectionstatistics
 Group by Date(StartDate)
 Order by Date(StartDate);
END$$



DELIMITER ;