using Godot;
using System;

public partial class Ball : CharacterBody2D
{
    [Signal]
    public delegate void GoalScoredEventHandler(string side);

    [Export] public float InitialSpeed = 300.0f;
    [Export] public float SpeedMultiplier = 1.05f;

    private Vector2 _velocity;
    private bool _isMoving = false;

    public override void _Ready()
    {
        Reset();
    }

    public void Reset()
    {
        GlobalPosition = new Vector2(400, 300);
        _velocity = Vector2.Zero;
        _isMoving = false;
        
        // Random start direction
        float angle = GD.RandfRange(-Math.PI / 4, Math.PI / 4);
        if (GD.Randf() > 0.5f) angle += Math.PI; // Randomize X direction
        
        _velocity = new Vector2(Math.Cos(angle), Math.Sin(angle)) * InitialSpeed;
        _isMoving = true;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!_isMoving) return;

        // Move and Collide
        KinematicCollision2D collision = MoveAndCollide(_velocity * (float)delta);

        if (collision != null)
        {
            // Bounce
            _velocity = _velocity.Bounce(collision.GetNormal());
            
            // Increase speed slightly on hit
            _velocity = _velocity.Normalized() * (_velocity.Length() * SpeedMultiplier);
            
            // Cap max speed
            if (_velocity.Length() > 800.0f)
            {
                _velocity = _velocity.Normalized() * 800.0f;
            }
        }

        // Check for goals (passed walls)
        if (GlobalPosition.X < 0)
        {
            EmitSignal(SignalName.GoalScored, "right");
            Reset();
        }
        else if (GlobalPosition.X > 800)