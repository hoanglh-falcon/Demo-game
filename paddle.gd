extends CharacterBody2D

@export_group("Movement")
@export var speed: float = 400.0
@export var is_ai: bool = false
@export var target_node: NodePath

var velocity: Vector2 = Vector2.ZERO

func _ready():
	if is_ai:
		var target = get_node_or_null(target_node)
		if not target:
			printerr("Paddle AI target not found!")

func _physics_process(delta):
	var target_y = position.y
	
	if not is_ai:
		var input = Input.get_axis("ui_up", "ui_down")
		velocity.y = input * speed
	else:
		var target = get_node_or_null(target_node)
		if target:
			target_y = target.position.y
			# Simple AI with deadzone to prevent jitter
			if position.y < target_y - 10:
				velocity.y = speed
			elif position.y > target_y + 10:
				velocity.y = -speed
			else:
				velocity.y = 0
	
	move_and_slide()
	
	# Clamp to screen bounds
	# TODO(balancer): Use screen size or container bounds
	position.y = clamp(position.y, -300, 300)

func handle_ball_hit(ball: CharacterBody2D):
	# Optional: Add sound or particle effects here
	pass