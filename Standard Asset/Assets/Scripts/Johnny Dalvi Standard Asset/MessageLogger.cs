using UnityEngine;
using UnityEngine.UI;

public class MessageLogger : MonoBehaviour
{
    public Text chatText, SystemText;
    public GameObject chatWindow, SystemWindow;
    public Image chatImage;
    static MessageLogger _instance;
    GameObject openWindow;
    public static MessageLogger instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MessageLogger>();
            }
            return _instance;
        }
    }

    void Start()
    {
        _instance = this;
        chatWindow.SetActive(false);
        SystemWindow.SetActive(false);
    }

    public static void DisplaySystemMessage(string message)
    {
        Master.PauseGame();
        instance.SystemWindow.SetActive(true);
        instance.SystemText.text = message;
        instance.openWindow = instance.SystemWindow;
    }

    public static void DisplayChatText(string message, Sprite image, bool pause)
    {
        DisplayChatText(message, pause);
        instance.chatImage.sprite = image;
    }

    public static void DisplayChatText(string message, bool pause)
    {
        if (pause)
            Master.PauseGame();
        instance.chatWindow.SetActive(true);
        instance.chatText.text = message;
        instance.openWindow = instance.chatWindow;
    }

    public void CloseWindow()
    {
        if (Master.isPaused)
            Master.UnPauseGame();
        instance.openWindow.SetActive(false);
    }
}
