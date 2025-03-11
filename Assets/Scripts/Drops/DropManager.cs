using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class DropManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Candy candyPrefab;
    [SerializeField] private Cash cashPrefab;
    [SerializeField] private Chest chestPrefab;

    [Header("Settings")]
    [SerializeField] [Range(0,100)] private int cashDropChance;
    [SerializeField] [Range(0, 100)] private int chestDropChance;

    [Header("Pooling")]
    private ObjectPool<Candy> candyPool;
    private ObjectPool<Cash> cashPool;
    private void Awake()
    {
        Enemy.onPassedAway += EnemyPassedAwayCallBack;
        Candy.onColledted += ReleaseCandy;
        Cash.onColledted += ReleaseCash;
    }
    private void OnDestroy()
    {
        Enemy.onPassedAway -= EnemyPassedAwayCallBack;
        Candy.onColledted -= ReleaseCandy;
        Cash.onColledted -= ReleaseCash;

    }


    // Start is called before the first frame update
    void Start()
    {
       candyPool = new ObjectPool<Candy>(
             CandyCreateFunction
           , CandyActionOnGet
           , CandyActionOnRelease
           , CandyActionOnDestroy);
       cashPool = new ObjectPool<Cash>(
             CashCreateFunction
           , CashActionOnGet
           , CashActionOnRelease
           , CashActionOnDestroy);
    }
    // Candy pool funcition
    private Candy CandyCreateFunction()              => Instantiate(candyPrefab, transform);
    private void CandyActionOnGet(Candy candy)       => candy.gameObject.SetActive(true);
    private void CandyActionOnRelease(Candy candy)   => candy.gameObject.SetActive(false);
    private void CandyActionOnDestroy(Candy candy)   => Destroy(candy.gameObject);
    // Cash pool funcition
    private Cash CashCreateFunction()                => Instantiate(cashPrefab, transform);
    private void CashActionOnGet(Cash cash)          => cash.gameObject.SetActive(true);
    private void CashActionOnRelease(Cash cash)      => cash.gameObject.SetActive(false);
    private void CashActionOnDestroy(Cash cash)      => Destroy(cash.gameObject);


    // Update is called once per frame
    void Update()
    {

    }
    private void EnemyPassedAwayCallBack(Vector2 enemyPositon)
    {
        bool shouldSpawnCash = Random.Range(0, 101) <= cashDropChance;

        DropableCurrency dropable = shouldSpawnCash ? cashPool.Get() : candyPool.Get();

        dropable.transform.position = enemyPositon;

        TryDropChest(enemyPositon);
    }

    private void TryDropChest(Vector2 spawnPositon)
    {
        bool shouldSpawnChest = Random.Range(0, 101) <= chestDropChance;
        if (!shouldSpawnChest)
            return;
        Instantiate(chestPrefab, spawnPositon, Quaternion.identity,transform);
    }

    private void ReleaseCandy(Candy candy)   => candyPool.Release(candy);
    private void ReleaseCash(Cash cash)      => cashPool.Release(cash);


}
