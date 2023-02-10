using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance => instance;
    
    [SerializeField] private String mainMenuScene;
    [SerializeField] private String gameScene;
    [SerializeField] private String scoreMenuScene;
    
    [SerializeField] private VoidGameEvent startGameEvent;
    [SerializeField] private VoidGameEvent exitGameEvent;
    [SerializeField] private VoidGameEvent creditGameEvent;
    [SerializeField] private VoidGameEvent mainMenuEvent;
    

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    void Start()
    {
        SetRatio(16, 9);
    }
    
    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode) {
        if (SceneManager.GetActiveScene().name == gameScene) { }
    }

    private void OnEnable() {
        startGameEvent.AddCallback(OnStartGame);
        exitGameEvent.AddCallback(OnExitGame);
        creditGameEvent.AddCallback(OnCreditGame);
        mainMenuEvent.AddCallback(OnMainMenuGame);
    }
    
    private void OnMainMenuGame()
    {
        AudioManager.Instance.ReggaeMusic(false);
        AudioManager.Instance.RockMusic(false);

        SceneManager.LoadScene(mainMenuScene);
    }
    
    private void OnStartGame() {
        
        SceneManager.LoadScene(gameScene);
       
    }
    
    private void OnCreditGame() {
        SceneManager.LoadScene(scoreMenuScene);
    }
    
    private void OnExitGame() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
    }
    
    void SetRatio(float w, float h)
    {
        if ((((float)Screen.width) / ((float)Screen.height)) > w / h)
        {
            Screen.SetResolution((int)(((float)Screen.height) * (w / h)), Screen.height, true);
        }
        else
        {
            Screen.SetResolution(Screen.width, (int)(((float)Screen.width) * (h / w)), true);
        }
    }
}
