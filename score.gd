extends Control

@export_group("UI")
@export var label_player: Label
@export var label_ai: Label

var score_player: int = 0
var score_ai: int = 0

func _ready():
	var ball = get_tree().get_first_node_in_group("ball")
	if ball:
		ball.goal_scored.connect(_on_goal_scored)
	else:
		printerr("No ball found in group 'ball'")

func _on_goal_scored(scorer: int):
	if scorer == 1:
		score_player += 1
		if label_player:
			label_player.text = str(score_player)
	elif scorer == -1:
		score_ai += 1
		if label_ai:
			label_ai.text = str(score_ai)