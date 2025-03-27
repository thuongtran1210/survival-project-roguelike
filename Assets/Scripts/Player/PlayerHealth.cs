using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerHealth : MonoBehaviour, IPlayerStatsDependency
{
    [Header("Settings")]
    [SerializeField] private int baseMaxHealth;
    private float maxHealth;
    private float health;
    private float armor;
    private float lifeSteal;
    private float dodge;
    private float healthRecoverySpeed;
    private float healthRecoveryValue;
    private float healthRecoveryTimer;
    private float healthRecoveryDuration;

    [Header("Elements")]
    [SerializeField] private Slider healthSilder;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Actions")]
    public static Action<Vector2> onAttackDodge;
    private void Awake()
    {
        Enemy.onDamageTaken += EnemyTookDamageCallBack;
    }
    private void OnDestroy()
    {
        Enemy.onDamageTaken -= EnemyTookDamageCallBack;

    }

    private void EnemyTookDamageCallBack(int dame, Vector2 enemyPos, bool isCriticalHit)
    {
        if (health >= maxHealth)
            return;
        float lifeStealValue = dame * lifeSteal;
        float healthToAdd = Math.Min(lifeStealValue, (maxHealth - health));

        health += healthToAdd;
        UpdateUI();
    }

    // Start is called before the first frame update
    void Start()
    {


    }
    void Update()
    {
        if(health < maxHealth)
        {
            RecoverHealth();
        }
    }

    private void RecoverHealth()
    {
        healthRecoveryTimer += Time.deltaTime;
        if (healthRecoveryTimer > healthRecoveryDuration)
        {
            healthRecoveryTimer = 0;
            float healthToAdd = Mathf.Min(.1f, maxHealth - health);
            health += healthToAdd;
            UpdateUI() ;
        }
    }

    public void TakeDamage(int damage)
    {
        if(ShouldDodge())
        {
            onAttackDodge?.Invoke(transform.position); 
            return;
        }

        float realDamage = damage * Mathf.Clamp(1 - (armor / 1000), 0, 10000);
        realDamage =  Mathf.Min(realDamage, health);
        this.health -= realDamage;

        Debug.Log($"Dame thuc: {realDamage}");

        UpdateUI();

        

        if (health <= 0)
        {
            PassAway();
        }
    }
    private bool ShouldDodge()
    {
       return Random.Range(0f, 100f) < dodge;
    }


    private void PassAway()
    {
        Debug.Log("Die");
        GameManager.Instance.SetGameState(GameState.GAMEOVER);
    }
    private void UpdateUI()
    {
        float healthBarValue = this.health / maxHealth;
        healthSilder.value = healthBarValue;
        healthText.text = (int)health + " / " + maxHealth;
    }

    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        float addedHealth =  playerStatsManager.GetStatValue(Stat.MaxHealth);
        maxHealth = baseMaxHealth + (int)addedHealth;
        maxHealth = Mathf.Max(maxHealth, 1);

        this.health = maxHealth;
        UpdateUI();
        armor = playerStatsManager.GetStatValue(Stat.Armor);

        lifeSteal = playerStatsManager.GetStatValue (Stat.LifeSteal) /100;

        dodge = playerStatsManager.GetStatValue(Stat.Dodge);

        healthRecoverySpeed = Mathf.Max (.0001f, playerStatsManager.GetStatValue(Stat.HealthRecoverySpeed));
        healthRecoveryDuration = 1f / healthRecoverySpeed;
    }
}
