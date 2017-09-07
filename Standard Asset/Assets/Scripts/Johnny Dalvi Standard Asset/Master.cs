using UnityEngine;
using UnityEngine.SceneManagement;

public class Master : MonoBehaviour
{
    #region EventInitializers

    public delegate void SceneLoaded();

    public static event SceneLoaded OnSceneLoaded;

    public delegate void Pause();

    public static event Pause OnPause;

    public delegate void Start();

    public static event Start OnStart;

    public delegate void Win();

    public static event Win OnWin;

    public delegate void Lose();

    public static event Lose OnLose;

    public delegate void UnPause();

    public static event UnPause OnUnPause;

    public delegate void Reset();

    public static event Reset OnReset;

    #endregion


    static bool printEvents
    {
        get { return instance._printEvents; }
    }
    public bool _printEvents;

    public static float time
    {
        get { return Time.time; }
    }
    public static Master instance;


    public static bool isPaused
    {
        get
        {
            if (Time.timeScale == 0) return true;
            return false;
        }
    }
    public static bool isPlaying;



    void Awake()
    {
        SceneManager.sceneLoaded += loadScene;
        instance = this;
    }

    void loadScene(Scene scene, LoadSceneMode mode)
    {
        if (OnSceneLoaded != null)
            OnSceneLoaded();
        if (isPaused)
            UnPauseGame();
#if UNITY_EDITOR
        if (printEvents)
            print("OnSceneLoaded() Event on " + time.ToString("0.0s"));
#endif
    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
        isPlaying = false;


        if (OnPause != null)
            OnPause();
#if UNITY_EDITOR
        if (printEvents)
            print("OnPause() Event on " + time.ToString("0.0s"));
#endif
    }

    public static void StartGame()
    {
        isPlaying = true;

#if UNITY_EDITOR
        if (printEvents)
            print("OnStart() Event on " + time.ToString("0.0s"));
#endif
    }

    public static void UnPauseGame()
    {
        Time.timeScale = 1;
        isPlaying = true;

        if (OnUnPause != null)
            OnUnPause();
#if UNITY_EDITOR
        if (printEvents)
            print("OnUnPause() Event on " + time.ToString("0.0s"));
#endif
    }

    public static void ResetGame()
    {
        if (OnReset != null)
            OnReset();
        if (isPaused)
            UnPauseGame();
#if UNITY_EDITOR
        if (printEvents)
            print("OnReset() Event on " + time.ToString("0.0s"));
#endif
    }

    public static void WinGame()
    {
        if (OnWin != null)
            OnWin();
#if UNITY_EDITOR
        if (printEvents)
            print("OnWin() Event on " + time.ToString("0.0s"));
#endif
        isPlaying = false;
    }

    public static void LoseGame()
    {
        if (OnLose != null)
            OnLose();
#if UNITY_EDITOR
        if (printEvents)
            print("OnLose() Event on " + time.ToString("0.0s"));
#endif
        isPlaying = false;

    }
}
