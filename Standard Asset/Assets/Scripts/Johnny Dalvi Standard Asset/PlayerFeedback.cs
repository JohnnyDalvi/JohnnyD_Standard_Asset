using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFeedback : MonoBehaviour
{
    #region Initializers

    static GameObject _textPrefab;
    private static GameObject _imagePrefab;
    private static RectTransform _canvasRectTransform;
    static PlayerFeedback _instance;

    #endregion

    #region PropertiesInitializers

    private static GameObject textPrefab
    {
        get
        {
            if (_textPrefab == null)
            {
                _textPrefab = Resources.Load("TextPrefab") as GameObject;
            }
            return _textPrefab;
        }
    }
    private static GameObject imagePrefab
    {
        get
        {
            if (_imagePrefab == null)
            {
                _imagePrefab = Resources.Load("ImagePrefab") as GameObject;
            }
            return _imagePrefab;
        }
    }

    public static PlayerFeedback instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerFeedback>();
            }
            return _instance;
        }
    }

    private static RectTransform canvasRectTransform
    {
        get
        {
            if (_canvasRectTransform == null)
            {
                _canvasRectTransform = instance.GetComponent<RectTransform>();
                if (_canvasRectTransform == null)
                    _canvasRectTransform = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
            }
            return _canvasRectTransform;
        }
    }


    #endregion

    void Start()
    {
        _instance = this;
        _canvasRectTransform = GetComponent<RectTransform>();
    }

    Vector2 WorldToCanvas(Vector3 worldPosition)
    {
        Vector3 position = Camera.main.WorldToViewportPoint(worldPosition);
        if (position == Vector3.zero)
            position = Camera.main.WorldToViewportPoint(new Vector3(worldPosition.x, worldPosition.y, Camera.main.transform.position.z + 1));

        float xPos = (position.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f);
        float yPos = (position.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f);
        return new Vector2(xPos, yPos);
    }

    //ADD OVERLOAD TO IMAGE SIZE AND TEXT COLOR

    //Statics methods to instantiate a object into the canvas in a relative position to a world coordinate seen from a Camera
    #region FromWorldToCanvas

    //TEXT METHODS

    public static Text TextFromWorldToCanvas(string text, Vector3 worldPosition, float timerToDestroy, Vector2 CanvasMoveVelocity)
    {
        Vector2 CanvasPos = instance.WorldToCanvas(worldPosition);
        Text prefab = instance.InstantiatePref(CanvasPos, text, timerToDestroy, CanvasMoveVelocity);
        return prefab;

    }
    public static Text TextFromWorldToCanvas(string text, Color color, Vector3 worldPosition, float timerToDestroy, Vector2 CanvasMoveVelocity)
    {
        Vector2 CanvasPos = instance.WorldToCanvas(worldPosition);
        Text prefab = instance.InstantiatePref(CanvasPos, text, color, timerToDestroy, CanvasMoveVelocity);
        return prefab;
    }

    public static Text TextFromWorldToCanvas(string text, Vector3 worldPosition, float timerToDestroy)
    {
        Vector2 CanvasPos = instance.WorldToCanvas(worldPosition);
        Text prefab = instance.InstantiatePref(CanvasPos, text, timerToDestroy);
        return prefab;
    }
    public static Text TextFromWorldToCanvas(string text, Color color, Vector3 worldPosition, float timerToDestroy)
    {
        Vector2 CanvasPos = instance.WorldToCanvas(worldPosition);
        Text prefab = instance.InstantiatePref(CanvasPos, text, color, timerToDestroy);
        return prefab;
    }

    public static Text TextFromWorldToCanvas(Vector3 worldPosition)
    {
        Vector2 CanvasPos = instance.WorldToCanvas(worldPosition);
        GameObject prefab = instance.InstantiateText(CanvasPos, "TEXT");
        Destroy(prefab.GetComponent<FloatingObject>());
        return prefab.GetComponent<Text>();
    }

    //IMAGES METHODS


    public static Image ImageFromWorldToCanvas(Sprite image, Vector3 worldPosition, float timerToDestroy, Vector2 CanvasMoveVelocity)
    {
        Vector2 CanvasPos = instance.WorldToCanvas(worldPosition);
        Image prefab = instance.InstantiatePref(CanvasPos, image, timerToDestroy, CanvasMoveVelocity);
        return prefab;
    }

    public static Image ImageFromWorldToCanvas(Sprite image, Vector2 rectSize, Vector3 worldPosition, float timerToDestroy, Vector2 CanvasMoveVelocity)
    {
        Vector2 CanvasPos = instance.WorldToCanvas(worldPosition);
        Image prefab = instance.InstantiatePref(CanvasPos, image, rectSize, timerToDestroy, CanvasMoveVelocity);
        return prefab;
    }

    public static Image ImageFromWorldToCanvas(Sprite image, Vector3 worldPosition, float timerToDestroy)
    {
        Vector2 CanvasPos = instance.WorldToCanvas(worldPosition);
        Image prefab = instance.InstantiatePref(CanvasPos, image, timerToDestroy);
        return prefab;
    }

    public static Image ImageFromWorldToCanvas(Sprite image, Vector2 rectSize, Vector3 worldPosition, float timerToDestroy)
    {
        Vector2 CanvasPos = instance.WorldToCanvas(worldPosition);
        Image prefab = instance.InstantiatePref(CanvasPos, image, rectSize, timerToDestroy);
        return prefab;
    }

    public static Image ImageFromWorldToCanvas(Vector3 worldPosition, Sprite image)
    {
        Vector2 CanvasPos = instance.WorldToCanvas(worldPosition);
        GameObject prefab = instance.InstantiateImage(CanvasPos, image);
        Destroy(prefab.GetComponent<FloatingObject>());
        return prefab.GetComponent<Image>();
    }


    #endregion

    //Methods to instantiate the prefab into the Canvas
    #region InstantiatePref

    //TEXT METHODS

    Text InstantiatePref(Vector2 canvasPosition, string text, float timer)
    {
        GameObject prefab = InstantiateText(canvasPosition, text);
        Destroy(prefab, timer);
        return prefab.GetComponent<Text>();
    }

    Text InstantiatePref(Vector2 canvasPosition, string text, Color color, float timer)
    {
        GameObject prefab = InstantiateText(canvasPosition, text);
        prefab.GetComponent<Text>().color = color;
        Destroy(prefab, timer);
        return prefab.GetComponent<Text>();

    }

    Text InstantiatePref(Vector2 canvasPosition, string text, float timer, Vector2 moveVector2)
    {
        GameObject prefab = InstantiateText(canvasPosition, text);
        prefab.GetComponent<FloatingObject>().MovementVector3 = new Vector3(moveVector2.x, moveVector2.y, 0);
        Destroy(prefab, timer);
        return prefab.GetComponent<Text>();
    }

    Text InstantiatePref(Vector2 canvasPosition, string text, Color color, float timer, Vector2 moveVector2)
    {
        GameObject prefab = InstantiateText(canvasPosition, text);
        prefab.GetComponent<FloatingObject>().MovementVector3 = new Vector3(moveVector2.x, moveVector2.y, 0);
        prefab.GetComponent<Text>().color = color;
        Destroy(prefab, timer);
        return prefab.GetComponent<Text>();
    }

    //IMAGES METHODS

    Image InstantiatePref(Vector2 canvasPosition, Sprite image, float timer)
    {
        GameObject prefab = InstantiateImage(canvasPosition, image);
        Destroy(prefab, timer);
        return prefab.GetComponent<Image>();
    }

    Image InstantiatePref(Vector2 canvasPosition, Sprite image, Vector2 rectSize, float timer)
    {
        GameObject prefab = InstantiateImage(canvasPosition, image);
        prefab.GetComponent<RectTransform>().sizeDelta = rectSize;
        Destroy(prefab, timer);
        return prefab.GetComponent<Image>();
    }

    Image InstantiatePref(Vector2 canvasPosition, Sprite image, float timer, Vector2 moveVector2)
    {
        GameObject prefab = InstantiateImage(canvasPosition, image);
        prefab.GetComponent<FloatingObject>().MovementVector3 = new Vector3(moveVector2.x, moveVector2.y, 0);
        Destroy(prefab, timer);
        return prefab.GetComponent<Image>();
    }

    Image InstantiatePref(Vector2 canvasPosition, Sprite image, Vector2 rectSize, float timer, Vector2 moveVector2)
    {
        GameObject prefab = InstantiateImage(canvasPosition, image);
        prefab.GetComponent<FloatingObject>().MovementVector3 = new Vector3(moveVector2.x, moveVector2.y, 0);
        prefab.GetComponent<RectTransform>().sizeDelta = rectSize;
        Destroy(prefab, timer);
        return prefab.GetComponent<Image>();
    }

    #endregion

    GameObject InstantiateText(Vector2 canvasPosition, string text)
    {
        GameObject prefab = Instantiate(textPrefab, Vector3.zero, Quaternion.identity, canvasRectTransform) as GameObject;
        prefab.GetComponent<RectTransform>().anchoredPosition = canvasPosition;
        prefab.GetComponent<Text>().text = text;
        return prefab;
    }

    GameObject InstantiateImage(Vector2 canvasPosition, Sprite image)
    {
        GameObject prefab = Instantiate(imagePrefab, Vector3.zero, Quaternion.identity, canvasRectTransform) as GameObject;
        prefab.GetComponent<RectTransform>().anchoredPosition = canvasPosition;
        prefab.GetComponent<Image>().sprite = image;

        return prefab;
    }

}
