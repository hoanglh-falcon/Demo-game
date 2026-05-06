extends CharacterBody2D

@export_group("Physics")
@export var speed: float = 400.0
@export var initial_angle: float = PI / 4

signal goal_scored(scorer: int)

func _ready():
	add_to_group("ball")
	reset()

func reset():
	position = Vector2.ZERO
	velocity = Vector2(speed * cos(initial_angle), speed * sin(initial_angle))
	# Randomize starting direction
	if randi() % 2 == 0:
		velocity.x = -velocity.x

func _physics_process(delta):
	var collision = move_and_collide(velocity * delta)
	if collision:
		handle_collision(collision)
	else:
		check_goals()

func handle_collision(collision: KinematicCollision2D):
	var normal = collision.get_normal()
	velocity = velocity.bounce(normal)
	
	# If it hit a paddle, adjust angle based on hit position
	var collider = collision.get_collider()
	if collider is CharacterBody2D and collider.has_method("handle_ball_hit"):
		collider.handle_ball_hit(self)

func check_goals():
	# TODO(balancer): Use actual screen bounds or container size
	if position.x < -400:
		emit_signal("goal_scored", 1) # Player scores
		reset()
	elif position.x > 400:
		emit_signal("goal_scored", -1) # AI scores
		reset()