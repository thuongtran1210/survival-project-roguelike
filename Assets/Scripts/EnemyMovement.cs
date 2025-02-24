using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player player;
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 3f;
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
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        TryAttack();
      
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
