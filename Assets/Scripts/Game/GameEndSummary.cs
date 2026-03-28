public enum GameEndReason
{
    Victory,
    GameOver
}

public readonly struct GameEndSummary
{
    public GameEndReason Reason { get; }
    public int SessionScore { get; }
    public int BestScore { get; }
    public int Level { get; }

    public GameEndSummary(GameEndReason reason, int sessionScore, int bestScore, int level)
    {
        Reason = reason;
        SessionScore = sessionScore;
        BestScore = bestScore;
        Level = level;
    }
}
