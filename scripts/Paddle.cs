using Godot;
using System;

public partial class Paddle : CharacterBody2D
{
    [Export] public float Speed = 350.0f;
    [Export] public float AI_Speed = 300.0f;
    [Export] public bool IsAI = false;

    private CharacterBody2D _ball;

    public override void _Ready()
    {
        _ball = GetNode<CharacterBody2D>("/root/Main/Ball");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = Vector2.Zero;

        if (IsAI)
        {
            // AI Logic: Track ball Y
            float targetY = _ball.GlobalPosition.Y;
            float diff = targetY - GlobalPosition.Y;

            if (Math.Abs(diff) > 5.0f) // Deadzone
            {
                direction.Y = Math.Sign(diff);
            }
        }
        else
        {
            // Player Logic: Input Axis
            direction.Y = Input.GetAxis("ui_up", "ui_down");
        }

        velocity = direction * (IsAI ? AI_Speed : Speed);
        MoveAndSlide();

        // Clamp to screen bounds
        GlobalPosition = new Vector2(
            Mathf.Clamp(GlobalPosition.X, 10, 790),
            Mathf.Clamp(GlobalPosition.Y, 50, 550)
        );
    }
}