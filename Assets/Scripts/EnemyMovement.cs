using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player player;

    [Header("Spawn Indicator")]
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] SpriteRenderer spawnIndicator;
    private bool hasSpawned = false;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerDetectionRadius;

    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticles;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        if (player == null)
        {
            
            Destroy(gameObject);
        }
        // An renderer cua Enemy
        renderer.enabled = false;
        // Hien chi bao xuat hien
        
        spawnIndicator.enabled = true;
        // Animation (scale) cho chi bao xuat hien
        Vector3 targetScale = spawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnIndicator.gameObject,targetScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);

        // Hien thi ke thu sau khi thuc hien Anmation Indicator
        // An chi bao xuat hien
        // Thuc hien  FollowPlayer Attackk
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned)
            return;

        FollowPlayer();
        TryAttack();
    

      
    }
    //Thuc hien sau khi Animation Pingpong Indicator thuc hien xong
    private void SpawnSequenceCompleted()
    {
        // Hien renderer cua Enemy
        renderer.enabled = true;
        // An chi bao xuat hien

        spawnIndicator.enabled = false;
        hasSpawned = true;


    }
    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - this.transform.position).normalized;
        Vector2 targetPostion = (Vector2)this.transform.position + direction * moveSpeed * Time.deltaTime;
        this.transform.position = targetPostion;
    }
    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);
        if (distanceToPlayer <= playerDetectionRadius)
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
