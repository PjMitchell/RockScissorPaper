Delimiter ££

CREATE FUNCTION GetSelectionPosition (selection int, buttonOrder varchar(10))
Returns Int DETERMINISTIC
Begin
Return
CASE selection
WHEN 1 THEN
	CASE buttonOrder
	When 'RSP' then 1
	When 'RPS' then 1
	When 'SRP' then 2 
	When 'PRS' then 2
	When 'SPR' then 3
	When 'PSR' then 3
	Else 0
	END
WHEN 2 THEN
	CASE buttonOrder
	When 'SRP' then 1
	When 'SPR' then 1
	When 'RSP' then 2
	When 'PSR' then 2
	When 'PRS' then 3
	When 'RPS' then 3
	Else 0
	END
WHEN 3 THEN
	CASE buttonOrder
	When 'PRS' then 1
	When 'PSR' then 1
	When 'RPS' then 2
	When 'SPR' then 2
	When 'RSP' then 3
	When 'SRP' then 3
	Else 0
	END 
ELSE 0
END;

End ££