using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MenuMethods : MonoBehaviour
{
    GUIOverallOptions _guiOverallOptions;
    public GameObject optionsWindow, winWindow, loseWindow;
    public Slider volumeSlider;
    public Slider musicSlider;

    static MenuMethods _instance;
    public static MenuMethods instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MenuMethods>();
                if (_instance == null)
                    throw new Exception("There is no Menu Method in the game, please tweak the Master AudioController in order to fix the reference problem within the following methods: PlayButtonSound(), PlayWinSound() and PlayLoseSound().");
            }
            return _instance;
        }
    }
    [HideInInspector]
    public AudioSource source;
    int Timer;
    void Adressingvalue()
    {
        volumeSlider.value = AudioController.instance.currentMasterVol;
        musicSlider.value = AudioController.instance.currentMusic;
    }

    void Update()
    {
        Timer += 1;
    }

    private void OnEnable()
    {
        Master.OnWin += OpenWinPanel;
        Master.OnLose += OpenLosePanel;
    }


    private void OnDisable()
    {
        Master.OnWin -= OpenWinPanel;
        Master.OnLose -= OpenLosePanel;
    }


    void Start()
    {
        _instance = this;
        source = GetComponent<AudioSource>();
        Adressingvalue();

        Master.StartGame(); // DEBUG, YOU CAN CHANGE IT TO START UPON ANOTHER TRIGGER

    }

    public void WinGame() // DEBUG, THIS IS JUST TO LINK TO THE BUTTON TO SHOW HOW IT WORKS
    {
        Master.WinGame();
    }

    public void LoseGame() // DEBUG, THIS IS JUST TO LINK TO THE BUTTON TO SHOW HOW IT WORKS
    {
        Master.LoseGame();
    }

    public void StartGame()
    {
        LevelManager.instance.LoadLevel("Game", true);
    }

    public void QuitApplication()
    {
        LevelManager.instance.QuitRequest();
    }

    public void PlayButtonSound()
    {
        AudioController.PlayButtonSound();
    }

    public void changeVolumes()
    {
        if (Timer >= 10)
        {
            AudioController.instance.ChangeMusicVolume(musicSlider.value);
            AudioController.instance.ChangeMasterVolume(volumeSlider.value);
        }
        else
        {
            Adressingvalue();
        }
    }

    public void BackToMenu()
    {
        LevelManager.instance.LoadLevel("Start", true);
    }

    public void OpenOptions()
    {
        if (!Master.isPaused)
            Master.PauseGame();
        else
            Master.UnPauseGame();
        Timer = 0;
        optionsWindow.SetActive(!optionsWindow.activeInHierarchy);
        AudioController.instance.SaveSoundConfigs();
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void OpenWinPanel()
    {
        if (Master.isPlaying)
        {
            winWindow.SetActive(true);
        }
    }

    public void OpenLosePanel()
    {
        if (Master.isPlaying)
        {
            loseWindow.SetActive(true);
        }
    }
}