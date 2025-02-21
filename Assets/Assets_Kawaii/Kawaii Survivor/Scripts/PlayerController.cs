using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private float speed;
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        
    }
    private void FixedUpdate()
    {
        rig.velocity = moveDirection * speed;
    }
}
