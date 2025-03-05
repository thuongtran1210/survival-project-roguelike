using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    enum State
    {
        Idle,
        Attack
    }
    private State state;

    [Header("Elements")]
    [SerializeField] private Transform hitDetectionTransform;
    [SerializeField] private BoxCollider2D hitCollider;
    [SerializeField] private float hitDetectionRadius;

    [Header("Settings")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask enemyMask;

    [Header("Atack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackDelay;
    private float attackTimer;
    [SerializeField] private Animator animator;
    [SerializeField] private List<Enemy> damageEnemies = new List<Enemy>();

    [Header("Animations")]
    [SerializeField] private float aimLerp;
    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                AutoAim();
                break;
            case State.Attack:
                Attacking();
                break;

        }

    }
    [NaughtyAttributes.Button]
    private void StartAttack()
    {
        animator.Play("Attack");
        state = State.Attack;
        damageEnemies.Clear();
        animator.speed = 1f / attackDelay;
    }

    private void Attacking()
    {
        Attack();

    }
    private void StopAttack()
    {
        state = State.Idle;
        damageEnemies.Clear();
    }

    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();
        Vector2 targetUpVector = Vector3.up;

        if (closestEnemy != null)
        {

            targetUpVector = (closestEnemy.transform.position - this.transform.position).normalized;
            transform.up = targetUpVector;
            ManageAttack();
        }
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
        IncrementAttackTimer();

    }

    private void ManageAttack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
            attackTimer = 0;
            StartAttack();
        }
    }
    private void IncrementAttackTimer()
    {
        attackTimer += Time.deltaTime;
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(hitDetectionTransform.position
            ,hitCollider.bounds.size
            ,hitDetectionTransform.localEulerAngles.z
            ,enemyMask);

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i].GetComponent<Enemy>();
            if (!damageEnemies.Contains(enemy))
            {
                enemy.TakeDamage(damage);
                damageEnemies.Add(enemy);
            }

        }

    }
    private Enemy GetClosestEnemy()
    {
        Enemy closestEnemy = null;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

        if (enemies.Length <= 0)
        {
            return null;
        }

        float minDistance = range;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemyChecked = enemies[i].GetComponent<Enemy>();
            float distanceToEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);
            if (distanceToEnemy < minDistance)
            {
                closestEnemy = enemyChecked;
                minDistance = distanceToEnemy;
            }
        }
        return closestEnemy;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(this.transform.position, range);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitDetectionTransform.position, hitDetectionRadius);

    }

}
