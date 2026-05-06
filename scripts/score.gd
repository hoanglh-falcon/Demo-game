extends Label

## Score tracking and display.
## var left_score: int = 0
## var right_score: int = 0

var left_score: int = 0
var right_score: int = 0

func _ready() -> void:
	var ball: CharacterBody2D = get_node("/root/Main/Ball")
	if ball:
		ball.goal_scored.connect(_on_goal)

func _on_goal(side: String) -> void:
	if side == "left":
		right_score += 1
	else:
		left_score += 1
	text = str(left_score) + " - " + str(right_score)