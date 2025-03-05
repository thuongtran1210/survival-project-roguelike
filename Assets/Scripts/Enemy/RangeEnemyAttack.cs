using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RangeEnemyAttack : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private EnemyBullet bulletPrefab;
    private Player player;

    [Header("Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    private float attackDelay;
    private float attackTimer;

    [Header("Bullet Pooling")]
    private ObjectPool<EnemyBullet> bulletPool;
    // Start is called before the first frame update
    void Start()
    {
        attackDelay = 1f / attackFrequency;
        attackTimer = attackDelay;

        bulletPool = new ObjectPool<EnemyBullet>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }
    private EnemyBullet CreateFunction()
    {
        EnemyBullet bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        bulletInstance.Configure(this);
        return bulletInstance;
    }
    private void ActionOnGet(EnemyBullet enemyBullet)
    {
        enemyBullet.Reload();
       enemyBullet.transform.position = shootingPoint.position;
        enemyBullet.gameObject.SetActive(true);
    }

    private void ActionOnRelease(EnemyBullet enemyBullet)
    {
        enemyBullet.gameObject.SetActive(false);
    }
    public void ReleaseBullet(EnemyBullet enemyBullet)
    {
        bulletPool.Release(enemyBullet);
    }
    private void ActionOnDestroy(EnemyBullet enemyBullet)
    {
        Destroy(enemyBullet.gameObject);
    }
    void Update()
    {
        
    }
    public void StorePlayer(Player player)
    {
        this.player = player;
    }


    public void AutoAim()
    {
        
        ManageShooting();
    }

    private void ManageShooting()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
            attackTimer = 0;
            Shoot();
        }
    }
    Vector2 gizmosDerection;
    private void Shoot()
    {
        Vector2 direction = (player.GetCenter() - (Vector2)shootingPoint.position).normalized;
        EnemyBullet bulletInstance = bulletPool.Get();
        bulletInstance.Shoot(damage, direction);
    }

} 
