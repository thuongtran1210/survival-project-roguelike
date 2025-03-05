using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionTest : MonoBehaviour
{
    public Action <int> myAction;
    // Start is called before the first frame update
    void Start()
    {
        myAction += DebugANumber;

        myAction?.Invoke(5);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void DebugANumber(int number)
    {
        Debug.Log(number);
    }
    private void DebugAString()
    {
        Debug.Log("Nam mlem");
    }
}
