using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]

public class Bullet : MonoBehaviour
{
    [Header("Elements")]
    private Rigidbody2D rig;
    private Collider2D _collider;
    private RangeWeapon rangeWeapon;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask enemyMask;
    private bool isCriticalHit;
    private int damage;
    private Enemy target;
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

    }
    public void Configure(RangeWeapon rangeWeapon)
    {
         this.rangeWeapon = rangeWeapon;
    }
    public void Shoot(int damge, Vector2 direction, bool isCriticalHit)
    {
        Invoke("Release", 1);
        this.damage = damge;  
        this.isCriticalHit = isCriticalHit;
        transform.right = direction;
        rig.velocity = direction * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (target != null)
            return;
        
        if(IsInLayerMask(collider.gameObject.layer, enemyMask))
        {
            target = collider.GetComponent<Enemy>();
            CancelInvoke();

            Attack(target);

            Release();
        }
    }

    private void Attack(Enemy enemy)
    {
        enemy.TakeDamage(damage, isCriticalHit);
    }
    private void Release()
    {
        if (!gameObject.activeSelf)
            return;
        rangeWeapon.ReleaseBullet(this);  
    }
    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask.value & (1 << layer)) !=0; 
    }

    public void Reload()
    {
        target = null;

        rig.velocity = Vector2.zero;
        _collider.enabled = true;
    }
}
