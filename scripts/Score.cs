using Godot;

public partial class Score : Label
{
    [Export]
    public int ScoreIncrement { get; set; } = 1; // TODO(balancer): Adjust based on game balance

    [Export]
    public int InitialScore { get; set; } = 0;

    private int _currentScore;

    public override void _Ready()
    {
        _currentScore = InitialScore;
        UpdateDisplay();
    }

    public void AddScore(int amount = 0)
    {
        if (amount <= 0)
            amount = ScoreIncrement;

        _currentScore += amount;
        UpdateDisplay();
        EmitSignal(SignalName.ScoreChanged, _currentScore);
    }

    public void SetScore(int score)
    {
        _currentScore = score;
        UpdateDisplay();
        EmitSignal(SignalName.ScoreChanged, _currentScore);
    }

    public void ResetScore()
    {
        _currentScore = InitialScore;
        UpdateDisplay();
        EmitSignal(SignalName.ScoreChanged, _currentScore);
    }

    private void UpdateDisplay()
    {
        Text = $"Score: {_currentScore}";
    }

    public override void _ExitTree()
    {
        // Godot 4 C# automatically unsubscribes signals when the node is freed.
        // No manual cleanup required for standard signal usage.
    }
}