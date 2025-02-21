using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform target;
    [Header("Settings")]
    [SerializeField] private Vector2 minMaxXY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Camera khong tim thay muc tieu");
            return;
        }
        Vector3 targetPosition = target.position;
        targetPosition.z = -10;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -minMaxXY.x, minMaxXY.x);
        targetPosition.y = Mathf.Clamp(targetPosition.x, -minMaxXY.y, minMaxXY.y);

        this.transform.position = targetPosition;


        
    }
}
