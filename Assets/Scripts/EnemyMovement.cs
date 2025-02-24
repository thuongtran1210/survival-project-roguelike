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
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        if (player == null)
        {
            Debug.LogWarning("Khong thay Object nao co ten Player");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.transform.position - this.transform.position).normalized;
        Vector2 targetPostion =(Vector2)this.transform.position + direction * moveSpeed * Time.deltaTime;
        this.transform.position = targetPostion;
    }
}
