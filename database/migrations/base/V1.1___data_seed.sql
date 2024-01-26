do $$

begin
	
	insert into campaigns 
		(name, theme, details, writer, created)
	values
		('Waterdeep: Dragon Heist',
		'Fantasy',
		'Levels 1 - 5, set in the city of Waterdeep.',
		'Christopher Perkins',
		current_timestamp);
	
	insert into campaigns 
		(name, theme, details, writer, created)
	values
		('Waterdeep: Dungeon of the Mad Mage',
		'Fantasy',
		'Levels 5 - 20, extension to Waterdeep: Dragon Heist.',
		'Christopher Perkins',
		current_timestamp);
	
	insert into campaigns 
		(name, theme, details, writer, created)
	values
		('Candlekeep Mysteries',
		'Fantasy',
		'17 short adventures surrounding mysterious books.',
		'Sarah Madsen',
		current_timestamp);

end $$