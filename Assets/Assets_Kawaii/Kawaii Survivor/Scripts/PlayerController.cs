using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] MobileJoystick playerJoystick;
    [SerializeField] private float moveSpeed;
    
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

        rig.velocity = playerJoystick.GetMoveVector() * moveSpeed *  Time.deltaTime;
    }
    private void FixedUpdate()
    {
        
    }
}
