using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : DropableCurrency
{
    [Header("Actions")]
    public static Action<Candy> onColledted;
    protected override void Collected()
    {
        onColledted?.Invoke(this);
    }
}
