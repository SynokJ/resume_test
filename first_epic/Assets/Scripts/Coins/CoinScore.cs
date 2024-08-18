public class CoinScore
{
    public int Score { get => _currentScore; }
    private int _currentScore = 0;

    public CoinScore()
    {
    }

    ~CoinScore()
    {
    }

    public void IncreaseScore()
    {
        _currentScore++;
        if(_currentScore > GameConstants.COINS_SCORE_TARGET)
            _currentScore = GameConstants.COINS_SCORE_TARGET;
    }

    public bool AreCoinsCollected()
        => _currentScore == GameConstants.COINS_SCORE_TARGET;
}
