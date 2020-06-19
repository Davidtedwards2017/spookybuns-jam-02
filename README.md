# spookybuns-jam-02


## conversation
```
{
	"id": <characer_name>,
	"dialogues": [
		<node>,
		<node>
	]
}
```

## nodes

### basic dialogue node
```
{
	{
		"type": "basic",
		"id": "2", //unique id
		"who": "actor", //"actor" or "player",
		"expression": "angry",
		"value": "This is a mighty fine <color=red><b>world</b></color>.",
		"post": [
			"next:3",
			"flag:foo+1"
		]
	}
}
```


#### `expression`
`neutral` (default)

`happy`

`angry`

### choice dialogue node
```
{
	"type": "choice",
	"id": "4",
	"who": "player",
	"options": [
		{
			"id": "4.5",
			"value": "(remove this option)",
			"onlyonce": true,
			"post": [
				"next:4"
			]
		},
		{
			"id": "4.6",
			"value": "(new option)",
			"onlyonce": true,
			"condition": "selected.4.5=1",
			"post": [
				"next:4"
			]
		}
	],
	"post": [
		"flag:foo+1"
	]
}
```

### condition node
```
{
	"type": "condition",
	"id": 5,
	"condition": "grump=0",
	"if": [
		"next:6"
	],
	"else": []
}
```

## Post commands

### Next
Procced to next node with id value

syntax:
`next:{node id}`

examples:
`next:1.0.1`

### Flags
Sets/Updates flag values 

syntax:
`flag:{flagname}{operation}{value}`

examples:

`flag:grump+1`
`flag:nice=2`
`flag:blah-3`


### Events
Triggers events

syntax:
`event:{event name}`

#### Supported Events

##### Start Convesation
syntax:

`event:startconversation.{character name}`

##### Show Character
syntax:

`event:show.{character name}`

##### Hide Character
syntax:

`event:hide.{character name}`

##### Restart Game
syntax:

`event:game.restart`

##### End Game (not yet implemented)
syntax:

`event:game.end`

