using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Candy candyPrefab;
    private void Awake()
    {
        Enemy.onPassedAway += EnemyPassedAwayCallBack;
    }
    private void OnDestroy()
    {
        Enemy.onPassedAway -= EnemyPassedAwayCallBack;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void EnemyPassedAwayCallBack(Vector2 enemyPositon)
    {
       Candy candyIntance = Instantiate(candyPrefab, enemyPositon, Quaternion.identity, transform);
        candyIntance.name = $" Candy: {Random.Range(1,5001)}";
    }

}
