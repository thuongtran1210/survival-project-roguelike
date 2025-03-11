using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cash : DropableCurrency
{
    [Header("Actions")]
    public static Action<Cash> onColledted;
    protected override void Collected()
    {
        onColledted?.Invoke(this);
    }
}
