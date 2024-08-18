public class CoinUI
{

    private TMPro.TextMeshProUGUI _scoreText;
    private CoinScore _score;

    public CoinUI(CoinScore score, TMPro.TextMeshProUGUI scoreText)
    {
        _score = score;
        _scoreText = scoreText;
    }

    ~CoinUI()
    {
    }

    public void UpdateScoreText()
        => _scoreText.text = _score.Score.ToString();
}
