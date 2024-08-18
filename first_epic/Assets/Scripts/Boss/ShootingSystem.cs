using UnityEngine.EventSystems;

public class ShootingSystem
{

    private ObjectPooling _pooling;
    private UnityEngine.Transform _bossTr;

    public ShootingSystem(UnityEngine.GameObject bulletPref, UnityEngine.Transform bossTr)
    {
        _pooling = new ObjectPooling(bulletPref, 10);
        _bossTr = bossTr;
    }

    ~ShootingSystem()
    {
        _pooling = null;
        _bossTr = null;
    }

    public void ShootByDirection(UnityEngine.Vector2 dir)
    {
        Bullet tempBullet = _pooling.GenerateObject(_bossTr.position);
        float angle = UnityEngine.Mathf.Atan2(dir.y, dir.x) * UnityEngine.Mathf.Rad2Deg;
        tempBullet.transform.rotation = UnityEngine.Quaternion.AngleAxis(angle, UnityEngine.Vector3.forward);
        tempBullet.OnActivated(dir);
    }

    public void ShootBySides()
    {
        ShootByDirection(UnityEngine.Vector2.up);
        ShootByDirection(UnityEngine.Vector2.right);
        ShootByDirection(UnityEngine.Vector2.down);
        ShootByDirection(UnityEngine.Vector2.left);
    }
}
