using Godot;

[GlobalClass]
public partial class Score : Node
{
    [Export] public int WinScore { get; set; } = 10;

    private int _playerScore = 0;
    private int _aiScore = 0;

    [Signal]
    public delegate void ScoreUpdatedEventHandler(int playerScore, int aiScore);

    [Signal]
    public delegate void GameOverEventHandler(int winner);

    public void AddPoint(int direction)
    {
        if (direction > 0)
            _playerScore++;
        else
            _aiScore++;

        EmitSignal(SignalName.ScoreUpdated, _playerScore, _aiScore);

        if (_playerScore >= WinScore || _aiScore >= WinScore)
        {
            EmitSignal(SignalName.GameOver, _playerScore > _aiScore ? 1 : -1);
        }
    }
}