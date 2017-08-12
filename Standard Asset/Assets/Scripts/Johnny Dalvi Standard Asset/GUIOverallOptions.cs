using UnityEngine;
using UnityEngine.UI;

public class GUIOverallOptions : MonoBehaviour
{
    public Font overallFont;

    void Awake()
    {
        changeAllFonts();
        Master.OnSceneLoaded += changeAllFonts;
    }

    public void changeAllFonts()
    {
        foreach (Text text in FindObjectsOfType<Text>())
        {
            text.font = overallFont;
            text.fontStyle = FontStyle.Bold;
        }
    }
}