using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("This GameObject Don't Destroy on Load")]
    static LevelManager _instance;

    #region Initializers

    public static LevelManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();
                if (_instance == null)
                {
                    GameObject levelManager = Instantiate(Resources.Load("Master")) as GameObject;
                    _instance = levelManager.GetComponent<LevelManager>();
                }
            }
            return _instance;
        }
    }

    public Slider mySlider;
    public GameObject loadingPanel;

    private void OnEnable()
    {
        Master.OnSceneLoaded += DisableLoadingPanel;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (mySlider != null)
                LoadNextLevelAsync(mySlider);

            else
                LoadNextLevelAsync();
        }
    }

    void DisableLoadingPanel()
    {
        loadingPanel.SetActive(false);
    }

    Slider EnableLoadingPanel()
    {
        loadingPanel.SetActive(true);
        return mySlider;
    }


    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion


    public void LoadLevel(string name, bool isAsync)
    {
        if (!isAsync)
            SceneManager.LoadScene(name);
        if (isAsync)
        {
            LoadLevelAsync(name);
        }
    }

  
    public void LoadLevel(string name, bool isAsync, Slider slider)
    {
        if (!isAsync)
            SceneManager.LoadScene(name);
        if (isAsync)
        {
            LoadLevelAsync(name);
        }
    }

    public void QuitRequest()
    {
        Application.Quit();
    }

    public void LoadLastLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void StartAgain()
    {
        SceneManager.LoadScene("Start");
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevelAsync(Slider slider)
    {
        LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1, slider);
    }
    public void LoadNextLevelAsync()
    {
        LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    #region LoadInTime

    public void LoadNextLevelin(float seconds)
    {
        StartCoroutine(loadin(seconds));
    }


    public void LoadLevelin(float seconds, string name)
    {
        StartCoroutine(loadlevelin(seconds, name));
    }

    IEnumerator loadin(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator loadlevelin(float seconds, string levelname)
    {
        yield return new WaitForSeconds(seconds);
        LoadLevel(levelname, true);
    }

    #endregion

    #region AsyncLoad

    public void LoadLevelAsync(int scene, Slider slider)
    {
        AsyncOperation aSyncLoad = SceneManager.LoadSceneAsync(scene);
        StartCoroutine(AsyncLoad(aSyncLoad, slider));
    }

    public void LoadLevelAsync(string scene, Slider slider)
    {
        AsyncOperation aSyncLoad = SceneManager.LoadSceneAsync(scene);
        StartCoroutine(AsyncLoad(aSyncLoad, slider));
    }

    public void LoadLevelAsync(int scene)
    {
        Slider slider = EnableLoadingPanel();
        AsyncOperation aSyncLoad = SceneManager.LoadSceneAsync(scene);
        StartCoroutine(AsyncLoad(aSyncLoad, slider));
    }

    public void LoadLevelAsync(string scene)
    {
        Slider slider = EnableLoadingPanel();
        AsyncOperation aSyncLoad = SceneManager.LoadSceneAsync(scene);
        StartCoroutine(AsyncLoad(aSyncLoad, slider));

    }

    IEnumerator AsyncLoad(AsyncOperation aSyncLoad, Slider slider)
    {
        //aSyncLoad.allowSceneActivation = false;
        while (aSyncLoad.progress <= 0.89f)
        {
            yield return new WaitForSeconds(0.01f);
            if (slider != null)
                slider.GetComponent<Slider>().value = aSyncLoad.progress;
            yield return null;
        }
        while (aSyncLoad.progress <= 0.9f)
        {
            yield return new WaitForSeconds(0.01f);

            aSyncLoad.allowSceneActivation = true;
            yield return aSyncLoad;
        }
    }


    #endregion
}
