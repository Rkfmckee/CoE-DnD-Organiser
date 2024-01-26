do $$

declare PLAYER_ID integer;
declare GAME_MASTER_ID integer;
declare CHARACTER_ID integer;
declare GAME_ID integer;

begin
	
	insert into players
		("email_address", "password", "name", "date_of_birth", "created")
	values
		('john.smith@dnd.com',
		'password123',
		'John Smith',
		'1990-01-01',
		current_timestamp);
	
	insert into players
		("email_address", "password", "name", "date_of_birth", "created")
	values
		('jane.doe@dnd.com',
		'password123',
		'Jane Doe',
		'1990-01-02',
		current_timestamp);
	
	insert into players
		("email_address", "password", "name", "date_of_birth", "created")
	values
		('ryan.mck@dnd.com',
		'password123',
		'Ryan McK',
		'1990-01-03',
		current_timestamp)
	returning id into PLAYER_ID;

	insert into game_masters
		("planning_notes", "created", "player_id")
	values
		('Take notes on current session',
		current_timestamp,
		PLAYER_ID)
	returning id into GAME_MASTER_ID;

	insert into characters
		("name", "race", "class_levels", "created", "player_id")
	values
		('Caymus',
		'High Elf',
		'17 Wizard (Bladesigning)',
		current_timestamp,
		PLAYER_ID)
	returning id into CHARACTER_ID;

	insert into games
		("details", "created", "game_master_id", "campaign_id")
	values
		('From Tales from the Yawning Portal, into Dungeon of the Mad Mage',
		current_timestamp,
		GAME_MASTER_ID,
		2)
	returning id into GAME_ID;

	insert into game_characters
		("game_id", "character_id")
	values
		(GAME_ID,
		CHARACTER_ID);

end $$