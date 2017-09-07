using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    public Slider mySlider;
    public GameObject loadingPanel;
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (mySlider != null)
                LevelManager.instance.LoadNextLevelAsync(mySlider);

            else
                LevelManager.instance.LoadNextLevelAsync();
        }
        else
        {
            loadingPanel.SetActive(false);
        }
    }

    void EnableDisablePanel(bool switcher)
    {
        loadingPanel.SetActive(switcher);
    }
}
