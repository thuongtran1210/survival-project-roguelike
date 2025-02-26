using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [Header("Components")] 
    private EnemyMovement movement;
    [Header("Elements")]
    private Player player;

    [Header("Spawn Indicator")]
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] SpriteRenderer spawnIndicator;
    private bool hasSpawned = false;

    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    [SerializeField] private float playerDetectionRadius;
    private float attackDelay;
    private float attackTimer;

    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticles;
    // Start is called before the first frame update
    void Start()
    {
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
        if (attackTimer >= attackDelay)
        {
            TryAttack();
        }
        else
        {
            Wait();
        }
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
