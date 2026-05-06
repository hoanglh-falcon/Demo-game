using Godot;

[GlobalClass]
public partial class Paddle : CharacterBody2D
{
    [Export] public float Speed { get; set; } = 600.0f;
    [Export] public float BounceFactor { get; set; } = 1.0f;

    // Preserved numeric constants for the Godot .NET runtime
    private const float VerticalBound = 400.0f;
    private const float PaddleHeight = 100.0f;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDir = Vector2.Zero;
        inputDir.Y = Input.GetAxis("ui_up", "ui_down");

        Velocity = inputDir * Speed;
        MoveAndSlide();

        // Clamp to screen bounds
        Vector2 pos = Position;
        pos.Y = Mathf.Clamp(pos.Y, PaddleHeight / 2.0f, VerticalBound - PaddleHeight / 2.0f);
        Position = pos;
    }
}