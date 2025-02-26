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
    [SerializeField] private float moveSpeed;
    void Update()
    {
        if (player != null)
            FollowPlayer(); 
    }
    public void StorePlayer(Player player)
    {
        this.player = player;
    }
    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - this.transform.position).normalized;
        Vector2 targetPostion = (Vector2)this.transform.position + direction * moveSpeed * Time.deltaTime;
        this.transform.position = targetPostion;
    }
}
