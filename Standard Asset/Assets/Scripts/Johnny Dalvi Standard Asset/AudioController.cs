using UnityEngine;

public class AudioController : MonoBehaviour
{
    static AudioController _instance;
    public AudioClip loseSound;
    public AudioClip winSound;
    public AudioClip buttonSound;


    AudioSource audioSource;
    AudioSource MenuSource;
    PlayerPreferences playPrefs;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public static AudioController instance
    {
        get
        {
            if (_instance == null)
            {
                return FindObjectOfType<AudioController>();
            }
            return _instance;
        }
    }


    public static void PlayButtonSound()
    {
        MenuMethods.instance.source.PlayOneShot(instance.buttonSound);
    }
    public void PlayLoseSound()
    {
        MenuMethods.instance.source.PlayOneShot(instance.loseSound);
    }
    public void PlayWinSound()
    {
        MenuMethods.instance.source.PlayOneShot(instance.winSound);
    }

    public float currentMasterVol
    {
        get { return _currentMasterVol; }
    }
    public float currentMusic
    {
        get { return _currentMusic; }
    }
    float _currentMasterVol;
    float _currentMusic;



    void Start()
    {

        Master.OnUnPause += SaveSoundConfigs;
        Master.OnLose += PlayLoseSound;
        Master.OnWin += PlayWinSound;

        playPrefs = GetComponent<PlayerPreferences>();
        _currentMasterVol = playPrefs.SavedMASTERVOL;
        _currentMusic = playPrefs.SavedMUSIC;
        ChangeMasterVolume(currentMasterVol);
        ChangeMusicVolume(currentMusic);
    }

    public void ChangeMusicVolume(float volume)
    {
        _currentMusic = volume;
        audioSource.volume = volume;
    }

    public void ChangeMasterVolume(float volume)
    {
        _currentMasterVol = volume;
        AudioListener.volume = volume;
    }

    public void SaveSoundConfigs()
    {
        playPrefs.saveSoundsConfig(currentMusic, currentMasterVol);
    }
}