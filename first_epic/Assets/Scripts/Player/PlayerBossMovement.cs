using System.Collections;
using System.Collections.Generic;

public class PlayerBossMovement
{

    public event System.Action<GameEnumerators.BossPlayerMovementType> OnMoveAnimated = default;
    public event System.Action OnResetAnimation = default;

    private BossPlayer _bossPlayer;

    private delegate System.Collections.IEnumerator OnDirectionMovement();
    private Dictionary<GameEnumerators.BossPlayerMovementType, OnDirectionMovement> _movements =
        new Dictionary<GameEnumerators.BossPlayerMovementType, OnDirectionMovement>();

    private bool _canMove = true;
    private UnityEngine.Camera _camera;
    private UnityEngine.Vector2 _cameraSize;

    public PlayerBossMovement(BossPlayer player)
    {
        _bossPlayer = player;

        _movements[GameEnumerators.BossPlayerMovementType.up] = MoveUp;
        _movements[GameEnumerators.BossPlayerMovementType.right] = MoveRight;
        _movements[GameEnumerators.BossPlayerMovementType.down] = MoveDown;
        _movements[GameEnumerators.BossPlayerMovementType.left] = MoveLeft;

        _camera = UnityEngine.Camera.main;
        float height = _camera.orthographicSize * 2;
        float width = height * _camera.aspect;
        _cameraSize = new UnityEngine.Vector2(width, height);
    }

    ~PlayerBossMovement()
    {
    }

    public void MoveByDirection(UnityEngine.Vector2 dir)
    {
        float distance = UnityEngine.Vector2.Distance(UnityEngine.Vector2.zero, dir);
        if (distance > 0.5f && _canMove && CanMoveToDest(dir))
            MoveByType(GetMovementTypeByDir(dir));
    }

    private void MoveByType(GameEnumerators.BossPlayerMovementType type)
    {
        if (type == GameEnumerators.BossPlayerMovementType.none)
            return;

        _canMove = false;
        OnMoveAnimated?.Invoke(type);
        _bossPlayer.StartCoroutine(_movements[type]());
    }

    private bool CanMoveToDest(UnityEngine.Vector2 dir)
    {
        bool checkObstacles = IsAnyObstacleAhead(dir);
        bool isWalckableArea = IsWalckableArea(dir);
        return !checkObstacles && isWalckableArea;
    }

    private bool IsAnyObstacleAhead(UnityEngine.Vector2 dir)
    {
        UnityEngine.Vector2 origin = (UnityEngine.Vector2)_bossPlayer.transform.position;
        float distance = GameConstants.PLAYER_BOSS_MOVEMENT_DISTANCE;
        UnityEngine.RaycastHit2D[] hits = UnityEngine.Physics2D.CircleCastAll(origin, 1.0f, dir, distance);
        foreach (UnityEngine.RaycastHit2D hit in hits)
        {
            BossPlayer player = hit.transform.GetComponent<BossPlayer>();
            if (player == null)
                return true;
        }
        return false;
    }

    private bool IsWalckableArea(UnityEngine.Vector2 dir)
    {
        UnityEngine.Vector2 destDir = dir * GameConstants.PLAYER_BOSS_MOVEMENT_DISTANCE;
        UnityEngine.Vector2 curPlayerPos = _bossPlayer.transform.position;
        UnityEngine.Vector2 destPos = curPlayerPos + destDir;
        UnityEngine.Vector2 distLimits = _cameraSize * 0.5f;
        bool horArea = destPos.x < distLimits.x && destPos.x > -distLimits.x;
        bool verArea = destPos.y < distLimits.y && destPos.y > -distLimits.y;
        return horArea && verArea;
    }

    private IEnumerator MoveUp()
    {
        UnityEngine.Vector2 dest = (UnityEngine.Vector2)_bossPlayer.transform.position +
            UnityEngine.Vector2.up * GameConstants.PLAYER_BOSS_MOVEMENT_DISTANCE;
        yield return new UnityEngine.WaitUntil(() => ReachedDestination(dest));
        yield return new UnityEngine.WaitForSeconds(GameConstants.PLAYER_BOSS_MOVEMENT_DELAY);
        _canMove = true;
        OnResetAnimation?.Invoke();
    }

    private IEnumerator MoveRight()
    {
        UnityEngine.Vector2 dest = (UnityEngine.Vector2)_bossPlayer.transform.position +
            UnityEngine.Vector2.right * GameConstants.PLAYER_BOSS_MOVEMENT_DISTANCE;
        yield return new UnityEngine.WaitUntil(() => ReachedDestination(dest));
        yield return new UnityEngine.WaitForSeconds(GameConstants.PLAYER_BOSS_MOVEMENT_DELAY);
        _canMove = true;
        OnResetAnimation?.Invoke();
    }

    private IEnumerator MoveDown()
    {
        UnityEngine.Vector2 dest = (UnityEngine.Vector2)_bossPlayer.transform.position +
            UnityEngine.Vector2.down * GameConstants.PLAYER_BOSS_MOVEMENT_DISTANCE;
        yield return new UnityEngine.WaitUntil(() => ReachedDestination(dest));
        yield return new UnityEngine.WaitForSeconds(GameConstants.PLAYER_BOSS_MOVEMENT_DELAY);
        _canMove = true;
        OnResetAnimation?.Invoke();
    }

    private IEnumerator MoveLeft()
    {
        UnityEngine.Vector2 dest = (UnityEngine.Vector2)_bossPlayer.transform.position +
            UnityEngine.Vector2.left * GameConstants.PLAYER_BOSS_MOVEMENT_DISTANCE;
        yield return new UnityEngine.WaitUntil(() => ReachedDestination(dest));
        yield return new UnityEngine.WaitForSeconds(GameConstants.PLAYER_BOSS_MOVEMENT_DELAY);
        _canMove = true;
        OnResetAnimation?.Invoke();
    }

    private bool ReachedDestination(UnityEngine.Vector2 destination)
    {
        float speed = GameConstants.PLAYER_BOSS_MOVEMENT_SPEED * UnityEngine.Time.deltaTime;
        _bossPlayer.transform.position = UnityEngine.Vector2.MoveTowards(_bossPlayer.transform.position, destination, speed);
        return UnityEngine.Vector2.Distance(_bossPlayer.transform.position, destination) < 0.01f;
    }

    private GameEnumerators.BossPlayerMovementType GetMovementTypeByDir(UnityEngine.Vector2 dir)
    {
        UnityEngine.Vector2Int tempDir = UnityEngine.Vector2Int.RoundToInt(dir);
        if (!GameConstants.BOSS_MOVEMENT_TYPE.ContainsKey(tempDir))
            return GameEnumerators.BossPlayerMovementType.none;
        return GameConstants.BOSS_MOVEMENT_TYPE[tempDir];
    }

    public void StopMovement()
    {
        UnityEngine.Debug.Log("Movement is stoped => " + UnityEngine.Time.time);

        _canMove = true;
        _bossPlayer.StopAllCoroutines();
        OnResetAnimation?.Invoke();
    }
}