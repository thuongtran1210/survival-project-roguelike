using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int maxHealth;
    private int health;
    [Header("Elements")]
    [SerializeField] private Slider healthSilder;
    [SerializeField] private TextMeshProUGUI healthText;


    // Start is called before the first frame update
    void Start()
    {
       this.health = maxHealth;
        UpdateUI();

    }
    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        this.health -= realDamage;

        UpdateUI();

        

        if (health <= 0)
        {
            PassAway();
        }
    }


    private void PassAway()
    {
        Debug.Log("Die");
        SceneManager.LoadScene(0);
    }
    private void UpdateUI()
    {
        float healthBarValue = (float)this.health / maxHealth;
        healthSilder.value = healthBarValue;
        healthText.text = health + " / " + maxHealth;
    }
}
