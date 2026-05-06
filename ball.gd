extends CharacterBody2D

var speed = 400
var direction = Vector2(1, 1).normalized()

func _ready():
	direction = Vector2(randf_range(-1, 1), randf_range(-1, 1)).normalized()

func _physics_process(delta):
	var velocity = direction * speed
	var collision = move_and_collide(velocity * delta)
	
	if collision:
		direction = direction.bounce(collision.get_normal())
		position = collision.get_position() + direction * delta * speed * 0.1
	
	if position.x < 0:
		get_node("../Score").add_point("right")
		reset()
	elif position.x > 800:
		get_node("../Score").add_point("left")
		reset()

func reset():
	position = Vector2(400, 300)
	direction = Vector2(randf_range(-1, 1), randf_range(-1, 1)).normalized()