using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWepon : Weapon
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


    [Header("Settings")]
    [SerializeField] private List<Enemy> damageEnemies = new List<Enemy>();

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




    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(
              hitDetectionTransform.position
            , hitCollider.bounds.size
            , hitDetectionTransform.localEulerAngles.z
            , enemyMask);

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


}
