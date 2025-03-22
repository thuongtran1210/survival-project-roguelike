using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private CharacterDataSO playerData;
    [Header("Settings")]
    private Dictionary<Stat, float> playerStats = new Dictionary<Stat, float>();
    private Dictionary<Stat, float> addends = new Dictionary<Stat, float>();
    private void Awake()
    {
        playerStats = playerData.BaseStat;
        foreach (KeyValuePair<Stat, float> kvp in playerStats)
            addends.Add(kvp.Key, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayerStats();
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddPlayerStat(Stat stat, float value)
    {
        if (addends.ContainsKey(stat))
            addends[stat] += value;
        else
            Debug.LogError($"Key {stat} khong tim thay trong StatsData");

        UpdatePlayerStats();
    }
    public float GetStatValue(Stat stat)
    {
        float value = playerStats[stat] + addends[stat];
        return value;
    }

    private void UpdatePlayerStats()
    {
        IEnumerable<IPlayerStatsDependency> playerStatsDependency =
    FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
    .OfType<IPlayerStatsDependency>();
        foreach (IPlayerStatsDependency denpendency in playerStatsDependency)
        {
            denpendency.UpdateStats(this);
        }
    }

}
