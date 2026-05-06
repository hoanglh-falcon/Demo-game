using Godot;

[GlobalClass]
public partial class Ball : CharacterBody2D
{
    [Export] public float InitialSpeed { get; set; } = 400.0f;
    [Export] public float SpeedIncrease { get; set; } = 1.1f;
    [Export] public float MaxSpeed { get; set; } = 1000.0f;

    // Preserved numeric constants for the Godot .NET runtime
    private const float HorizontalBound = 450.0f;
    private const float VerticalBound = 400.0f;

    private Vector2 _direction;
    private float _currentSpeed;

    [Signal]
    public delegate void ScoreChangedEventHandler(int direction);

    public override void _Ready()
    {
        Reset();
    }

    public void Reset()
    {
        Position = Vector2.Zero;
        _currentSpeed = InitialSpeed;
        _direction = new Vector2(
            Mathf.Cos(Mathf.Pi / 4.0f) * (GD.Randf() > 0.5f ? 1.0f : -1.0f),
            Mathf.Sin(Mathf.Pi / 4.0f) * (GD.Randf() > 0.5f ? 1.0f : -1.0f)
        ).Normalized();
        Velocity = _direction * _currentSpeed;
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();

        // Bounce off top/bottom
        if (Position.Y <= VerticalBound / 2.0f || Position.Y >= VerticalBound - VerticalBound / 2.0f)
        {
            Velocity = Velocity.Reflect(Vector2.Up);
        }

        // Out of bounds horizontally
        if (Position.X <= -HorizontalBound || Position.X >= HorizontalBound)
        {
            EmitSignal(SignalName.ScoreChanged, Position.X < 0 ? 1 : -1);
            Reset();
        }
    }
}