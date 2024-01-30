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

`POST /players` Create a player.

**Request**
```
{
    "email_address": "joe.bloggs@dnd.com",
    "name": "Joe Bloggs",
    "date_of_birth": "1990-01-04",
    "password": "password123"
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
    "password": "password123"
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

`POST /game-masters` Create a game master.

**Request**
```
{
    "planning_notes": "Last session..."
}
```

**Response** `201 Created`

<hr>
<br>

`PUT /game-masters/{id}` Update a game master.

**Request**
```
{
    "planning_notes": "In the new session..."
}
```

**Response** `200 Ok`

<hr>
<br>

`DELETE /game-masters/{id}` Delete a game master.

**Response** `204 No Content`

<hr>
<br>

## Campaigns
`GET /campaigns` Return a list of all campaigns.

**Response** `200 Ok`
```
[
    {
        "id": "1",
        "name": "Waterdeep: Dragon Heist",
		"theme": "Fantasy",
		"details": "Levels 1 - 5, set in the city of Waterdeep.",
		"writer": "Christopher Perkins",
        "created": "2024-01-29 10:00:00.00"
    }
]
```
<hr>
<br>

`GET /campaigns/{id}` Return a campaign with the corresponding id.

**Response** `200 Ok`
```
{
    "id": "1",
    "name": "Waterdeep: Dragon Heist",
    "theme": "Fantasy",
    "details": "Levels 1 - 5, set in the city of Waterdeep.",
    "writer": "Christopher Perkins",
    "created": "2024-01-29 10:00:00.00"
}
```

<hr>
<br>

`POST /campaigns` Create a campaign.

**Request**
```
{
    "name": "Tales from the Yawning Portal",
    "theme": "Fantasy",
    "details": "Visit the famous Yawning Portal tavern.",
    "writer": "Mike Mearls"
}
```

**Response** `201 Created`

<hr>
<br>

`PUT /campaigns/{id}` Update a campaign.

**Request**
```
{
    "name": "Tales from the Yawning Portal",
    "theme": "Fantasy",
    "details": "Visit the famous Yawning Portal tavern, in Waterdeep!",
    "writer": "Mike Mearls"
}
```

**Response** `200 Ok`

<hr>
<br>

`DELETE /campaigns/{id}` Delete a campaign.

**Response** `204 No Content`

<hr>
<br>

## Characters
`GET /characters` Return a list of all characters.

**Response** `200 Ok`
```
[
    {
        "id": "1",
        "name": "Raymel the Wizard",
		"race": "High Elf",
		"class_levels": "12 Wizard (Bladesinging)",
        "created": "2024-01-29 11:40:00.00",
        "player": {
            "id": "1",
            "name": "John Smith"
        }
    },
    {
        "id": "2",
        "name": "Timbr",
		"race": "Warforged",
		"class_levels": "17 Artificer",
        "created": "2024-01-29 11:41:00.00",
        "player": {
            "id": "3",
            "name": "Ryan McK"
        }
    }
]
```
<hr>
<br>

`GET /characters/{id}` Return a character with the corresponding id.

**Response** `200 Ok`
```
{
    "id": "2",
    "name": "Timbr",
    "race": "Warforged",
    "class_levels": "17 Artificer",
    "created": "2024-01-29 11:41:00.00",
    "player": {
        "id": "3",
        "name": "Ryan McK"
    }
}
```

<hr>
<br>

`POST /characters` Create a character.

**Request**
```
{
    "name": "Rhondana",
    "race": "Lizardfolk",
    "class_levels": "12 Druid (Circle of the Moon)",
    "player_id": "2"
}
```

**Response** `201 Created`

<hr>
<br>

`PUT /characters/{id}` Update a character.

**Request**
```
{
    "name": "Rhondana",
    "race": "Lizardfolk",
    "class_levels": "12 Druid (Circle of the Moon)"
}
```

**Response** `200 Ok`

<hr>
<br>

`DELETE /characters/{id}` Delete a character.

**Response** `204 No Content`

<hr>
<br>

## Games
`GET /games` Return a list of all games.

**Response** `200 Ok`
```
[
    {
        "id": "1",
        "details": "A homebrew game based on Waterdeep: Dragon Heist.",
        "created": "2024-01-29 11:50:00.00",
        "game_master": {
            "id": "3",
            "name": "Ryan McK"
        },
        "campaign_id": {
            "id": "1",
            "name": "Waterdeep: Dragon Heist"
        },
        "characters": [{
            "id": "1",
            "name": "Raymel the Wizard",
            "player": {
                "id": "1",
                "name": "John Smith"
            }
        },
        {
            "id": "2",
            "name": "Timbr",
            "player": {
                "id": "3",
                "name": "Ryan McK"
            }
        }]
    }
]
```
<hr>
<br>

`GET /games/{id}` Return a game with the corresponding id.

**Response** `200 Ok`
```
{
    "id": "1",
    "details": "A homebrew game based on Waterdeep: Dragon Heist.",
    "created": "2024-01-29 11:50:00.00",
    "game_master": {
        "id": "3",
        "name": "Ryan McK"
    },
    "campaign_id": {
        "id": "1",
        "name": "Waterdeep: Dragon Heist"
    },
    "characters": [{
        "id": "1",
        "name": "Raymel the Wizard",
        "player": {
            "id": "1",
            "name": "John Smith"
        }
    },
    {
        "id": "2",
        "name": "Timbr",
        "player": {
            "id": "3",
            "name": "Ryan McK"
        }
    }]
}
```

<hr>
<br>

`POST /games` Create a game.

**Request**
```
{
    "details": "Started playing one campaign, then transitioned into another.",
    "game_master_id": "3",
    "campaign_id": "2"
}
```

**Response** `201 Created`

<hr>
<br>

`PUT /games/{id}` Update a game.

**Request**
```
{
    "details": "Started playing one campaign, then transitioned into another, then another.",
    "game_master_id": "3",
    "campaign_id": "2"
}
```

**Response** `200 Ok`

<hr>
<br>

`DELETE /games/{id}` Delete a game.

**Response** `204 No Content`

<hr>
<br>

## Game characters
`GET /games/{id}/characters` Return a list of all characters in the game with the corresponding id.

**Response** `200 Ok`
```
[
    {
        "character_id": "1",
        "name": "Raymel the Wizard",
        "race": "High Elf",
        "class_levels": "12 Wizard (Bladesinging)",
        "player": {
            "id": "1",
            "name": "John Smith"
        }
    },
    {
        "character_id": "2",
        "name": "Timbr",
        "race": "Warforged",
        "class_levels": "17 Artificer",
        "player": {
            "id": "3",
            "name": "Ryan McK"
        }
    }
]
```

<hr>
<br>

`POST /games/{id}/characters/{id}` Add a character to a game.

**Response** `201 Created`

<hr>
<br>

`DELETE /games/{id}/characters/{id}` Remove a character from a game.

**Response** `204 No Content`