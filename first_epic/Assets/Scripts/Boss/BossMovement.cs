public class BossMovement
{

    private UnityEngine.Transform _bossTr;

    public BossMovement(UnityEngine.Transform targetTr)
    {
        _bossTr = targetTr;
    }

    ~BossMovement()
    {
    }

    public bool MoveToDestination(UnityEngine.Vector2 destination)
    {
        float movementSpeed = 10.0f * UnityEngine.Time.deltaTime;
        _bossTr.transform.position =
            UnityEngine.Vector2.MoveTowards(_bossTr.position, destination, movementSpeed);
        return UnityEngine.Vector2.Distance(_bossTr.position, destination) < 0.001f;
    }
}
