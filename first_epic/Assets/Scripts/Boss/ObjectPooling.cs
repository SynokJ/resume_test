public class ObjectPooling
{
    private System.Collections.Generic.Queue<Bullet> _pool =
        new System.Collections.Generic.Queue<Bullet>();

    public ObjectPooling(UnityEngine.GameObject objectPref, int size)
    {
        for (int i = 0; i < size; ++i)
        {
            UnityEngine.GameObject tempObj = UnityEngine.GameObject.Instantiate(objectPref);
            Bullet bullet = tempObj.AddComponent<Bullet>();
            bullet.SetVisibilityStatus(false);
            _pool.Enqueue(bullet);
        }
    }

    ~ObjectPooling()
    {
        foreach (Bullet obj in _pool)
            UnityEngine.GameObject.Destroy(obj.gameObject);
        _pool.Clear();
    }

    public Bullet GenerateObject(UnityEngine.Vector2 pos)
    {
        Bullet tempBullet = _pool.Dequeue();
        tempBullet.SetVisibilityStatus(true);
        tempBullet.transform.position = pos;
        _pool.Enqueue(tempBullet);
        return tempBullet;
    }
}
