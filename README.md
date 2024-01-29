# Overview
This application is part of Unosquare's Center of Excellence software project.
- Project name: D&D Organiser.
- Author: Ryan McKeever.

## Definitions
- `Game master` is the person who organises and runs games of Dungeons and Dragons. This is who this application is intended for.
- `Player` is anyone else who plays in the games created and run by the Game master.
- `Campaign` is a set of content used to flavour the experience of each game.
- `Game` is a collection of players running a set campaign, organised by a single Game master.
- `Character` can be split up into two different types:
    - `Player Character (PC)` is created by the player and used by them to interact with the game.
    - `Non-Player Character (NPC)` is created by the Game master and used by them to interact with the players within the game.

# Project specifications
## The problem
When running a game like Dungeons & Dragons, it can be difficult for the Game master to keep track of the details of all their games.
Each Game master can have multiple players across many campaigns. Players can be in multiple games run by the same Game master. The Game master might even want to track similarities between their games, such as when they started, how long they have lasted, how often they're played or similar content between thier games.

### Why does the problem need solved?
If the Game master's time can be taken away from having to organise their games, then more time can be spent adding more fun content to the games.
Increasing the enjoyment Game master's get from their games can also reflect on the player's experience, helping them enjoy their time playing more.
Fun is the whole point, after all.

# How will this be achieved?
## Minimum viable product
At a minimum, the application should help the Game master to automate most of the basic elements of their games.
The Game master should be able to;
- Create a new game.
- Create a new player.
- Assign players to each game, making sure that player details are consistent across games.

## Nice to have
Beyond the MVP, some extra features would enhance the effectiveness of the application.
In this case, the Game master should also be able to;
- Assign a campaign to each game, making sure that campaign details are consistent across games.
- Track details of individual games.
- Keep personal notes on individual games.
- Track statistics of all games, campaigns and players.
- Create characters categorized either as PC or NPC.
- Assign a character to a player or themselves, as appropriate.

# Domain model diagram
This diagram outlines the main domains for the application, along with the direction and a description of their interactions.
``` mermaid
flowchart
    GameMaster --> |Creates| Game
    GameMaster --> |Plays| Character
    GameMaster --> |Writes| Campaign
    Player --> |Plays| Character
    Campaign --> |Themes| Game
    Character --> |Exists within| Game
```

# Entity relationship diagram
This diagram outlines all the tables within the application's database, and the relationships between them.
``` mermaid
erDiagram 

game_masters ||--o{ games : ""
players ||--|{ characters : ""
players ||--|o game_masters : ""
campaigns ||--|{ games : ""
characters ||--|{ game_characters : ""
games ||--|{ game_characters : ""

game_masters 
{
    serial id PK
    varchar planning_notes
    timestamp created
    int player_id FK
}

players 
{
    serial id PK
    varchar email_address
    varchar password
    varchar name
    date date_of_birth
    timestamp created
}

games 
{
    serial id PK
    varchar details
    timestamp created
    int game_master_id FK
    int campaign_id FK
}

campaigns 
{
    serial id PK
    varchar name
    varchar theme
    varchar details
    varchar writer
    timestamp created
}

characters 
{
    serial id PK
    varchar name
    varchar race
    varchar class_levels
    timestamp created
    int player_id FK
}

game_characters 
{
    serial id PK
    int game_id FK
    int character_id FK
}
```

# API Specification
## Players
`GET /players` Return a list of all players.

**Response** `200 Ok`
```
[
    {
        "id": "1",
        "email_address": "john.smith@dnd.com",
        "name": "John Smith",
        "date_of_birth": "1990-01-01",
        "created": "2024-01-29 11:29:00.00"
    },
    {
        "id": "2",
        "email_address": "jane.doe@dnd.com",
        "name": "Jane Doe",
        "date_of_birth": "1990-01-02",
        "created": "2024-01-29 11:30:00.00"
    },
    {
        "id": "3",
        "email_address": "ryan.mck@dnd.com",
        "name": "Ryan McK",
        "date_of_birth": "1990-01-03",
        "created": "2024-01-29 11:31:00.00"
    }
]
```
<hr>
<br>

`GET /players/{id}` Return a player with the corresponding id.

**Response** `200 Ok`
```
{
    "id": "1",
    "email_address": "john.smith@dnd.com",
    "name": "John Smith",
    "date_of_birth": "1990-01-01",
    "created": "2024-01-29 11:29:00.00"
}
```

<hr>
<br>

`POST /players/{id}` Create a player.

**Request**
```
{
    "email_address": "joe.bloggs@dnd.com",
    "name": "Joe Bloggs",
    "date_of_birth": "1990-01-04",
    "password": "password123",
    "is_game_master": "false"
}
```

**Response** `201 Created`

<hr>
<br>

`PUT /players/{id}` Update a player.

**Request**
```
{
    "email_address": "john.smith@dnd.com",
    "name": "John Smith",
    "date_of_birth": "1990-01-01",
    "password": "password123",
}
```

**Response** `200 Ok`

<hr>
<br>

`DELETE /players/{id}` Delete a player.

**Response** `204 No Content`

<hr>
<br>

## Game masters
`GET /game-masters` Return a list of all game masters.

**Response** `200 Ok`
```
[
    {
        "id": "3",
        "name": "Ryan McK",
        "planning_notes": "Last session..."
        "created": "2024-01-29 11:31:00.00"
    }
]
```
<hr>
<br>

`GET /game-masters/{id}` Return a game master with the corresponding id.

**Response** `200 Ok`
```
{
    "id": "3",
    "name": "Ryan McK",
    "planning_notes": "Last session..."
    "created": "2024-01-29 11:31:00.00"
}
```

<hr>
<br>

`PUT /game-masters/{id}` Update a game master.

**Request**
```
{
    "planning_notes": "In the new session...",
},
```

**Response** `200 Ok`

<hr>
<br>

`DELETE /game-masters/{id}` Delete a game master.

**Response** `204 No Content`

<hr>
<br>