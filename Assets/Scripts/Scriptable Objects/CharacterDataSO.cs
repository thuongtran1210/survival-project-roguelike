using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
[CreateAssetMenu(fileName = "Character Data", menuName =" Scriptable Objects/New CharacterData", order = 0)]
public class CharacterDataSO :ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int PuchasePrice { get; private set; }

    [HorizontalLine]
    [SerializeField] private float attack; 
    [SerializeField] private float attackSpeed; 
    [SerializeField] private float criticalChane; 
    [SerializeField] private float criticalPercent; 
    [SerializeField] private float moveSpeed; 
    [SerializeField] private float maxHealth; 
    [SerializeField] private float range; 
    [SerializeField] private float healthRecoverSpeed; 
    [SerializeField] private float armor; 
    [SerializeField] private float luck; 
    [SerializeField] private float dodge;
    [SerializeField] private float lifeSteal; 

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
                {Stat.MoveSpeed,             moveSpeed },
                {Stat.MaxHealth,             maxHealth },
                {Stat.Range,                 range},
                {Stat.HealthRecoverySpeed,   healthRecoverSpeed},
                {Stat.Luck,                  luck},
                {Stat.Dodge,                 dodge },
                {Stat.Armor,                 armor},
                {Stat.LifeSteal,             lifeSteal},
                
            };

        }
        private set { } 
    }
}
