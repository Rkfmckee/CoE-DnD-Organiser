create table if not exists game_masters_test (
    id serial primary key,
    planning_notes varchar,
    created timestamp,
    player_id int not null,
    constraint fk_player foreign key (player_id)
    	references players(id) on delete cascade
);