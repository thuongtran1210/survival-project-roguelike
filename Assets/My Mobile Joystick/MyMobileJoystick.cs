using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyMobileJoystick : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickKnob;

    [Header("Settings")]
    private bool canControl;
    private Vector3 clickedPositon;
    private Vector3 move;
    [SerializeField] private float moveFactor;
    // Start is called before the first frame update
    void Start()
    {
        HideJoystick();
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl)
        {
            ControlJoystick();
        }
    }
    public void ClickedJoystickZoneCallBack()
    {
        clickedPositon = Input.mousePosition;
        joystickOutline.position = clickedPositon;
        ShowJoystick();
        canControl = true;
       
    }
    private void ShowJoystick()
    {
        joystickOutline.gameObject.SetActive(true);
        canControl = true;
    }
    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);
        canControl = false;
    }
    private void ControlJoystick()
    {
        Vector3 currentPositon = Input.mousePosition;
        Vector3 direction = currentPositon - clickedPositon;

        float moveMagnitude = direction.magnitude * moveFactor / Screen.width;
        moveMagnitude = Mathf.Min(moveMagnitude, joystickOutline.rect.width / 2);

        move = direction.normalized * moveMagnitude;

        Vector3 targetPosition = clickedPositon + move;

        joystickKnob.position = targetPosition;

        if(Input.GetMouseButtonUp(0))
        {
            HideJoystick();
        }
    }
    public Vector3 GetMove()
    {
        return move;
    }
}
