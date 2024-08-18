using UnityEngine;

public class Coin : MonoBehaviour
{

    private CoinsManager _coinsManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MazePlayer player = collision.gameObject.GetComponent<MazePlayer>();
        if (player != null)
        {
            _coinsManager.OnCoinPicked();
            Destroy(this.gameObject);
        }
    }

    public void Setup(CoinsManager manager)
        => _coinsManager = manager;
}
