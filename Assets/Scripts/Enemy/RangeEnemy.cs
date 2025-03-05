using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(EnemyMovement), typeof(RangeEnemyAttack))]
public class RangeEnemy : MonoBehaviour
{
    [Header("Components")]
    private EnemyMovement movement;
    private RangeEnemyAttack attack;

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

    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticles;

    [Header("Attack")]
    [SerializeField] private float playerDetectionRadius;
    [Header("Actions")]
    public static Action<int, Vector2> onDamageTaken;
    // Start is called before the first frame update
    void Start()
    {
        this.health = maxHealth;

        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<RangeEnemyAttack>();
        player = FindAnyObjectByType<Player>();

        attack.StorePlayer(player);
        if (player == null)
        {

            Destroy(gameObject);
        }
        StartSpawnSequence();

    }

    // Update is called once per frame
    void Update()
    {
        if (!renderer.enabled)
            return;

        ManageAttack();

    }

    private void ManageAttack()
    {
        float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);

        if (distanceToPlayer > playerDetectionRadius)
            movement.FollowPlayer();
        else
            TryAttack();

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
        collider.enabled = true;
        movement.StorePlayer(player);
    }
    private void SetRendererVisibility(bool visibility = true)
    {
        renderer.enabled = visibility;
        spawnIndicator.enabled = !visibility;
    }

    private void TryAttack()
    {
        attack.AutoAim();
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
