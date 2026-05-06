extends CharacterBody2D

## Ball physics and collision handling.
## signal goal_scored(side: String)
## @export var initial_speed: float = 300.0

signal goal_scored(side: String)

@export var initial_speed: float = 300.0

func _ready() -> void:
	velocity = Vector2.RIGHT * initial_speed

func _physics_process(delta: float) -> void:
	var collision: KinematicCollision2D = move_and_collide(velocity * delta)
	if collision:
		var normal: Vector2 = collision.get_normal()
		if normal.x != 0:
			# Side wall collision
			var side: String = "left" if position.x < 400 else "right"
			goal_scored.emit(side)
			reset()
		else:
			# Top/Bottom wall or Paddle collision
			velocity = velocity.bounce(normal)

func reset() -> void:
	position = Vector2(400, 300)
	var rng := RandomNumberGenerator.new()
	rng.randomize()
	var direction: float = 1.0 if rng.randf() > 0.5 else -1.0
	velocity = Vector2.RIGHT * initial_speed * direction