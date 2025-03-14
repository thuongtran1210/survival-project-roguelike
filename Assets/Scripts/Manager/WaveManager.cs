using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Threading;
[RequireComponent(typeof(WaveManagerUI))]
public class WaveManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Player player;
    private WaveManagerUI ui;
    [Header("Settings")]
    [SerializeField] private float waveDuration;
    private float timer;
    private bool isTimerOn;
    private int curentWaveIndex;

    [Header("Waves")]
    [SerializeField] private Wave[] waves;
    private List<float> localCounter   = new List<float>();
    private void Awake()
    {
        ui = GetComponent<WaveManagerUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimerOn)
            return;
        if (timer < waveDuration)
        {
            ManageCurrentWave();
            string timerString = ((int)(waveDuration - timer)).ToString();  
            ui.UpdateTimerText(timerString); 
        }
        else
        {
            StartWaveTransition();
        }
    }

    private void StartWave(int waveIndex)
    {
        ui.UpdateWaveText($"Wave {curentWaveIndex+1} / {waves.Length}");
        localCounter.Clear();
        foreach (WaveSegment segment in waves[waveIndex].segments)
        {
            localCounter.Add(1);
        }
        timer = 0;
        isTimerOn = true;
    }

    private void ManageCurrentWave()
    {
        Wave currentWave = waves[curentWaveIndex];
        for (int i = 0; i < currentWave.segments.Count; i++)
        {
            WaveSegment segment = currentWave.segments[i];
            float tStart    = segment.tStartEnd.x / 100 * waveDuration;
            float tEnd      = segment.tStartEnd.y / 100 * waveDuration;

            if (timer < tStart || timer > tEnd)
                continue;
            float timeSinceSegmentStart = timer - tStart;

            float spawnDelay = 1f / segment.spawnFrequency;


            if (timeSinceSegmentStart / spawnDelay > localCounter[i])
            {
                Instantiate(segment.prefab, GetSpawnPostion(), Quaternion.identity, transform);
                localCounter[i]++;
            }
        }
        timer += Time.deltaTime;
    }
    private void StartWaveTransition()
    {
        isTimerOn = false;
        DefeatAllEnemies();
        curentWaveIndex++;
        if (curentWaveIndex >= waves.Length)
        {
            ui.UpdateTimerText("");
            ui.UpdateWaveText("State Competed");
            GameManager.Instance.SetGameState(GameState.STAGECOMPLETE);
        }
        else
        {
            GameManager.Instance.WaveCompletedCallBack();
        }
    }
    private void StartNextWave()
    {
        StartWave(curentWaveIndex);
    }
    private void DefeatAllEnemies()
    {
        transform.Clear();
    }
    private Vector2 GetSpawnPostion()
    {
        Vector2 direction = Random.onUnitSphere;
        Vector2 offset = direction.normalized * Random.Range(6,10);
        Vector2 targetPositon = (Vector2)player.transform.position + offset;

        targetPositon.x = Mathf.Clamp(targetPositon.x, -18, 18);
        targetPositon.y = Mathf.Clamp(targetPositon.y, -8, 8);
        return targetPositon;
    }

    public void GameStateChangedCallBack(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.GAME:
                StartNextWave();
                break;
            case GameState.GAMEOVER:
                isTimerOn = false;
                DefeatAllEnemies();
                break;
        }
    }
}
[System.Serializable]
public struct Wave
{
    public string name;
    public List<WaveSegment> segments;
}
[System.Serializable]
public struct WaveSegment
{
    [MinMaxSlider(0, 100)] public Vector2 tStartEnd;
    public float spawnFrequency;
    public GameObject prefab;
}


