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


    public void LoadLevel(string name)
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }
        SceneManager.LoadScene(name);
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
        StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1, slider));
    }
    public void LoadNextLevelAsync()
    {
        StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1));
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
        LoadLevel(levelname);
    }

    #endregion

    #region AsyncLoad

    IEnumerator LoadLevelAsync(int scene, Slider slider)
    {
        AsyncOperation aSyncLoad = SceneManager.LoadSceneAsync(scene);
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

    IEnumerator LoadLevelAsync(int scene)
    {
        AsyncOperation aSyncLoad = SceneManager.LoadSceneAsync(scene);
        //aSyncLoad.allowSceneActivation = false;
        while (aSyncLoad.progress <= 0.89f)
        {
            yield return new WaitForSeconds(0.01f);
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
