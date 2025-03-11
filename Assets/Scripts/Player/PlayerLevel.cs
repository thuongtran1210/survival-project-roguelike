using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerLevel : MonoBehaviour
{
    [Header("Settings")]
    private int requiredXp;
    private int currentXp;
    private int level;
    [Header("Visual")]
    [SerializeField] private Slider xpBar;
    [SerializeField] private TextMeshProUGUI levelText;
    private void Awake()
    {
        Candy.onColledted += CandyCollectedCallBack;
    }
    private void OnDestroy()
    {
        Candy.onColledted -= CandyCollectedCallBack;

    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateRequiredXp();
        UpdateVisual();
    }
    private void UpdateRequiredXp() => requiredXp = (level + 1) * 5;

    private void UpdateVisual()
    {
       xpBar.value = (float)currentXp / requiredXp;
        levelText.text = $"lvl: {level + 1}";
    }
    private void CandyCollectedCallBack(Candy candy)
    {
        currentXp ++;
        if (currentXp >= requiredXp)
        {
            LevelUp();
        }
        UpdateVisual();
    }
    private void LevelUp()
    {
        level++;
        currentXp = 0;
        UpdateRequiredXp();
    }


}
