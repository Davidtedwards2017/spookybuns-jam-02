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
		"who": "actor", //"actor" or "player"
		"value": "This is a mighty fine <color=red><b>world</b></color>.",
		"post": [
			"next:3",
			"flag:foo+1"
		]
	}
}
```

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