extends CharacterBody2D

## Paddle controller for Player and AI.
## @export var speed: float = 350.0
## @export var is_ai: bool = false

@export var speed: float = 350.0
@export var is_ai: bool = false

func _physics_process(delta: float) -> void:
	if is_ai:
		_update_ai(delta)
	else:
		_update_player(delta)
	
	move_and_slide()

func _update_player(delta: float) -> void:
	## Player paddle reads input axis.
	var direction: float = Input.get_axis("ui_up", "ui_down")
	velocity.y = direction * speed

func _update_ai(delta: float) -> void:
	## AI paddle tracks ball.y with a max speed cap.
	var ball: CharacterBody2D = get_node("/root/Main/Ball")
	if ball:
		var target_y: float = ball.position.y
		var direction: float = sign(target_y - position.y)
		velocity.y = direction * speed