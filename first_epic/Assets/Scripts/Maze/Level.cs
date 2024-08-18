using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private MazePlayer _player;
    [SerializeField] private CoinsManager _coinsManager;

    private Maze _maze = null;
    private WallGenerator _wallGenerator = null;
    private RoomGenerator _roomGenerator = null;
    private CoinGenerator _coinGenerator = null;
    private EnemyGenerator _enemyGenerator = null;
    private PlayerGenerator _playerGenerator = null;
    private BossPortalGenerator _bossGenerator = null;
    private RandomPathGenerator _rndPathgenerator = null;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        _maze = new Maze();

        _rndPathgenerator = new RandomPathGenerator(_maze);
        _rndPathgenerator.Generate();
        _rndPathgenerator.Generate();
        _rndPathgenerator.Generate();
        _rndPathgenerator.Generate();

        _roomGenerator = new RoomGenerator(_maze);
        _roomGenerator.Generate();

        _wallGenerator = new WallGenerator(_maze);
        _wallGenerator.Generate();

        _player.transform.position = _rndPathgenerator.GetStartNode().PosOrigin;

        _enemyGenerator = new EnemyGenerator(_rndPathgenerator);
        _enemyGenerator.Generate();
        _enemyGenerator.Generate();
        _enemyGenerator.Generate();

        _bossGenerator = new BossPortalGenerator(_rndPathgenerator);
        _bossGenerator.Generate();

        _coinGenerator = new CoinGenerator(_rndPathgenerator, _coinsManager);
        _coinGenerator.Generate();
        _coinGenerator.Generate();
        _coinGenerator.Generate();

        //_playerGenerator = new PlayerGenerator();

    }
}