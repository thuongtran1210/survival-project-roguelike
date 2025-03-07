using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Candy candyPrefab;
    [SerializeField] private Cash cashPrefab;
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
        bool shouldSpawnCash = Random.Range(0, 101) <= 20;

        GameObject dropable = shouldSpawnCash ? cashPrefab.gameObject : candyPrefab.gameObject;
        GameObject dropableIntance = Instantiate(dropable, enemyPositon, Quaternion.identity, transform);
        dropableIntance.name = $" Dropable: {Random.Range(1,5001)}";
    }

}
