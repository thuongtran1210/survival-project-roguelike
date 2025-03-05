using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
[RequireComponent(typeof(EnemyMovement))]
public class MeleeEnemy : MonoBehaviour
{
    [Header("Components")] 
    private EnemyMovement movement;

    [Header("Elements")]
    private Player player;

    [Header("Health")]
    [SerializeField] private int maxHealth;
    
    private int health;

    [Header("Spawn Indicator")]
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    private bool hasSpawned = false;
    [SerializeField] private Collider2D collider;

    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    [SerializeField] private float playerDetectionRadius;
    private float attackDelay;
    private float attackTimer;
    [Header("Actions")]
    public static Action<int , Vector2> onDamageTaken;

    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticles;
    // Start is called before the first frame update
    void Start()
    {
        this.health = maxHealth;
 
        movement = GetComponent<EnemyMovement>();
        player = FindAnyObjectByType<Player>();
        if (player == null)
        {

            Destroy(gameObject);
        }
        StartSpawnSequence();

        attackDelay = 1f / attackFrequency;
        
    }
    private void StartSpawnSequence()
    {
        SetRendererVisibility(false);
        Vector3 targetScale = spawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);
    }

    private void SpawnSequenceCompleted()
    {
        SetRendererVisibility(true);
        hasSpawned = true;
        collider.enabled =  true;
        movement.StorePlayer(player);
    }
    private void SetRendererVisibility(bool visibility = true)
    {
        renderer.enabled = visibility;
        spawnIndicator.enabled = !visibility;
    }

    // Update is called once per frame
    void Update()
    {
        if (!renderer.enabled)
            return;
        if (attackTimer >= attackDelay)
        {
            TryAttack();
        }
        else
        {
            Wait();
        }
        movement.FollowPlayer();
    }
    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }
    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);
        if (distanceToPlayer <= playerDetectionRadius)
        {
            Attack();
        }

    }
    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        this.health -= realDamage;
      

        onDamageTaken?.Invoke(damage, this.transform.position);

        if (health <= 0)
        {
            PassAway();
        }
    }
    private void Attack()
    {
        attackTimer = 0f;
        player.TakeDamage(damage);  
    }

    private void PassAway()
    {
        // Huy lien ket voi object parent
        passAwayParticles.transform.SetParent(null);
        passAwayParticles.Play();
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, playerDetectionRadius);
    }
}
