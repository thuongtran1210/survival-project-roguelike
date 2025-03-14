using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        SetGameState(GameState.MENU);

    }
    public void StartGame()              => SetGameState(GameState.GAME);
    public void StartWeaponSelection()   => SetGameState(GameState.WEAPONSELECTION);
    public void StarShop()               => SetGameState(GameState.SHOP);


    public void SetGameState(GameState gameState)
    {
        IEnumerable<IGameStateListener> gameStateListeners = 
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IGameStateListener>();
        foreach (IGameStateListener gameStateListener in gameStateListeners)
        {
            gameStateListener.GameStateChangedCallBack(gameState);
        }

    }
    public void WaveCompletedCallBack()
    {
        if(Player.Instance.HasLeveledUp())
        {
            SetGameState(GameState.WAVETRANSITION);
        }
        else
        {
            SetGameState(GameState.SHOP);

        }
    }
    public void ManagerGameOver()
    {
        SceneManager.LoadScene(0);
    }
}
public interface IGameStateListener
{
    void GameStateChangedCallBack(GameState gameState);
}
