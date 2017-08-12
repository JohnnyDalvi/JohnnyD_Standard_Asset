using UnityEngine;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    public Slider mySlider;
    void Start()
    {
        if (mySlider != null)
            LevelManager.instance.LoadNextLevelAsync(mySlider);

        else
            LevelManager.instance.LoadNextLevelAsync();
    }
}
