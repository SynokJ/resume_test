using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private Gameplay _gameplay;
    [SerializeField] private TMPro.TextMeshProUGUI _scoreText;

    public event System.Action OnCoinCollected = default;

    private CoinScore _score = default;
    private CoinUI _ui = default;

    private void Start()
    {
        _score = new CoinScore();
        _ui = new CoinUI(_score, _scoreText);

        OnCoinCollected += _score.IncreaseScore;
        OnCoinCollected += _ui.UpdateScoreText;
    }

    private void OnDestroy()
    {
        OnCoinCollected -= _score.IncreaseScore;
        OnCoinCollected -= _ui.UpdateScoreText;
    }

    public void OnCoinPicked()
    {
        OnCoinCollected?.Invoke();
        _gameplay.CheckForWin(status: _score.AreCoinsCollected());
    }
}
