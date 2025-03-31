using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerWeapons : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private WeaponPosition[] weaponsPositons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddWeapon(WeaponDataSO selectedWeapon ,int weaponLevel)
    {
        //Instantiate(selectedWeapon.Prefab, weaponsParent);
        weaponsPositons[Random.Range(0, weaponsPositons.Length)].AssignWeapon(selectedWeapon.Prefab, weaponLevel);
    }
}
