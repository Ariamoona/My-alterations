
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private ObjectPool<Bullet> _pool;

    public void SetPool(ObjectPool<Bullet> pool) => _pool = pool;

    private void OnBecameInvisible() => _pool?.ReturnToPool(this);

    private void OnCollisionEnter2D() => _pool?.ReturnToPool(this);
}


public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _startPoolSize = 10;
    [SerializeField] private int _maxPoolSize = 30;
    [SerializeField] private Transform _bulletsContainer;

    private ObjectPool<Bullet> _bulletPool;

    private void Awake()
    {
        _bulletPool = new ObjectPool<Bullet>(
            _bulletPrefab,
            _startPoolSize,
            _maxPoolSize,
            _bulletsContainer
        );
    }

    public void Shoot()
    {
        Bullet bullet = _bulletPool.GetFromPool();
        if (bullet == null) return;

        bullet.transform.position = transform.position;
        bullet.SetPool(_bulletPool);
       
    }
}