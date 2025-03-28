using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon Data", menuName = " Scriptable Objects/New WeaponData", order = 0)]

public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int PuchasePrice { get; private set; }

    [field: SerializeField] public Weapon Prefab   { get; private set; }

    [HorizontalLine]
    [SerializeField] private float attack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float criticalChane;
    [SerializeField] private float criticalPercent;
    [SerializeField] private float range;
    public Dictionary<Stat, float> BaseStat
    {
        get
        {
            return new Dictionary<Stat, float>
            {
                {Stat.Attack,                attack },
                {Stat.AttackSpeed,           attackSpeed },
                {Stat.CriticalChance,        criticalChane},
                {Stat.CritilcalPercent,      criticalPercent},
                {Stat.Range,                 range}
            };

        }
        private set { }
    }
    public float GetStatValue(Stat stat)
    {
        foreach (KeyValuePair<Stat, float> kvp in BaseStat)
        {
            if (kvp.Key == stat)
                return kvp.Value; 
        }
        Debug.LogError("Stat khong ton tai");
        return 0;
    }
}
