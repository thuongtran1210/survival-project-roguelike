using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Components")]
    protected EnemyMovement movement;

    [Header("Health")]
    [SerializeField] protected int maxHealth;
    protected int health;

    [Header("Elements")]
    protected Player player;

    [Header("Spawn Indicator")]
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] protected SpriteRenderer spawnIndicator;
    protected bool hasSpawned = false;
    [SerializeField] protected Collider2D _collider;

    [Header("Effects")]
    [SerializeField] protected ParticleSystem passAwayParticles;

    [Header("Attack")]
    [SerializeField] protected float playerDetectionRadius;

    [Header("Actions")]
    public static Action<int, Vector2, bool> onDamageTaken;
    public static Action<Vector2> onPassedAway;
    // Start is called before the first frame update
     protected virtual void Start()
    {
        this.health = maxHealth;
        movement = GetComponent<EnemyMovement>();

        player = FindAnyObjectByType<Player>();
        if (player == null)
        {
            Destroy(gameObject);
        }
        StartSpawnSequence();
    }

    // Update is called once per frame
    protected bool CanAttack()
    {
        return _renderer.enabled;

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
        SetRendererVisibility();
        hasSpawned = true;
        _collider.enabled = true;
        movement.StorePlayer(player);
    }
    private void SetRendererVisibility(bool visibility = true)
    {
        _renderer.enabled = visibility;
        spawnIndicator.enabled = !visibility;
    }
    public void TakeDamage(int damage, bool isCriticalHit)
    {
        int realDamage = Mathf.Min(damage, health);
        this.health -= realDamage;


        onDamageTaken?.Invoke(damage, this.transform.position, isCriticalHit);

        if (health <= 0)
        {
            PassAway();
        }
    }
    private void PassAway()
    {
        onPassedAway?.Invoke(transform.position);

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
