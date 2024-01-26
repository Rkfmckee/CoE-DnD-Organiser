create table if not exists players (
    id serial primary key,
    email_address varchar not null,
    password varchar not null,
    name varchar,
    date_of_birth date,
    created timestamp not null
);

create table if not exists game_masters (
    id serial primary key,
    planning_notes varchar,
    created timestamp,
    player_id int,
    constraint fk_player foreign key (player_id)
    	references players(id) on delete cascade
);

create table if not exists campaigns (
    id serial primary key,
    name varchar not null,
    theme varchar not null,
    details varchar,
    writer varchar not null,
    created timestamp not null
);

create table if not exists characters (
    id serial primary key,
    name varchar not null,
    race varchar not null,
    class_levels varchar not null,
    created timestamp not null,
    player_id int,
    constraint fk_player foreign key (player_id)
    	references players(id) on delete cascade
);

create table if not exists games (
    id serial primary key,
    details varchar,
    created timestamp,
    game_master_id int,
    campaign_id int,
    constraint fk_game_master foreign key (game_master_id)
    	references game_masters(id) on delete cascade,
    constraint fk_campaign foreign key (campaign_id)
    	references campaigns(id) on delete cascade
);

create table if not exists game_characters (
    id serial primary key,
    game_id int,
    character_id int,
    constraint fk_game foreign key (game_id)
    	references games(id) on delete cascade,
    constraint fk_character foreign key (character_id)
    	references characters(id) on delete cascade
);

