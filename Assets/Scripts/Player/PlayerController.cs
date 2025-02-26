using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    private Rigidbody2D rig;
    [SerializeField] MobileJoystick playerJoystick;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        rig.velocity = playerJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;
    }
}
