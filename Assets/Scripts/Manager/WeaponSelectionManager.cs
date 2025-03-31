using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Transform containersPerent;
    [SerializeField] private WeaponSelectionContainer weaponContainerPrefab;
    [SerializeField] private PlayerWeapons playerWeapons;

    [Header("Data")]
    [SerializeField] private WeaponDataSO[] starterWeapon;
    private WeaponDataSO selectedWeapon;
    private int initialWeaponLevel;
    public void GameStateChangedCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAME:

                if(selectedWeapon == null)
                    return;
                playerWeapons.AddWeapon(selectedWeapon, initialWeaponLevel);
                selectedWeapon = null;
                initialWeaponLevel = 0;
                break;
            case GameState.WEAPONSELECTION:
                Configure();
                break;
        }
    }
    [NaughtyAttributes.Button]
    private void Configure()
    {
        containersPerent.Clear();

        for (int i = 0; i < 3; i++)
        {
            GenerateWeaponContainer();
        }
    }
    private void GenerateWeaponContainer()
    {
        WeaponSelectionContainer containerIntance = Instantiate(weaponContainerPrefab, containersPerent);
        WeaponDataSO weaponData = starterWeapon[Random.Range(0, starterWeapon.Length)];
        int level = Random.Range(0, 4);
        containerIntance.Configure(weaponData.Sprite, weaponData.Name, level);
        containerIntance.Button.onClick.RemoveAllListeners();
        containerIntance.Button.onClick.AddListener(()=> WeaponSelectedCallback(containerIntance, weaponData,level));
    }
    private void WeaponSelectedCallback(WeaponSelectionContainer containerIntance, WeaponDataSO weaponData,int level)
    {
        selectedWeapon = weaponData;
        foreach(WeaponSelectionContainer container in containersPerent.GetComponentsInChildren<WeaponSelectionContainer>() )
        {
            if (container == containerIntance)
                container.Select();
            else
                container.DeSelect();
        }
    }
}
