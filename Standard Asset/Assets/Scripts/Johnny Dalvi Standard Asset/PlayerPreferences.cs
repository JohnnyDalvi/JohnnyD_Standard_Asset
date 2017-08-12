using UnityEngine;

public class PlayerPreferences : MonoBehaviour
{
    const string SOUND_MASTER_VOLUME = "PlayerPref_Sound_Volume";
    const string SOUND_MUSIC = "PlayerPref_Sound_Music";
    const string EXECUTION_NUMBER = "PlayerPref_Number_Of_Executions";

    bool isFirstTime
    {
        get
        {
            if (PlayerPrefs.GetInt(EXECUTION_NUMBER) == 0) return true;
            return false;
        }
    }

    public float SavedMASTERVOL
    {
        get { return PlayerPrefs.GetFloat(SOUND_MASTER_VOLUME); }
    }
    public float SavedMUSIC
    {
        get { return PlayerPrefs.GetFloat(SOUND_MUSIC); }
    }

    void Awake()
    {
        if (isFirstTime)
        {
            PlayerPrefs.SetFloat(SOUND_MASTER_VOLUME, 1);
            PlayerPrefs.SetFloat(SOUND_MUSIC, 1);
        }
        PlayerPrefs.SetInt(EXECUTION_NUMBER, PlayerPrefs.GetInt(EXECUTION_NUMBER) + 1);
    }

    public void saveSoundsConfig(float music, float master)
    {
        PlayerPrefs.SetFloat(SOUND_MASTER_VOLUME, master);
        PlayerPrefs.SetFloat(SOUND_MUSIC, music);
    }
}
