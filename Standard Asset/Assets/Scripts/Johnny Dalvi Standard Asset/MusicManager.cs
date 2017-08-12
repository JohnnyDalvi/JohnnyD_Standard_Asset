using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    public AudioClip[] musics;
    AudioSource musicPlayer;

    void Start()
    {
        Master.OnSceneLoaded += ChangeMusic;
        musicPlayer = GetComponent<AudioSource>();
    }

    void ChangeMusic()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        if (sceneNumber <= musics.Length - 1)
            musicPlayer.clip = musics[sceneNumber];
        musicPlayer.Play();
    }

}