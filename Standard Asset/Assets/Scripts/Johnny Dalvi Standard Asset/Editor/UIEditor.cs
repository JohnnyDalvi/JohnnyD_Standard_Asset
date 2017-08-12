using Boo.Lang;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIEditor : EditorWindow
{
    #region Initializers

    public static string[] DefaultColorSettingsNames = { "Buttons", "Button's text", "Other Text" };
    public static List<string> CustomColorSettingsNames;
    public const string CustomSchemeNamesPlayerPrefName = "Custom_Schemes_Keyword";
    public const string NotChangingTagPrefName = "Not_Changing_Tag";

    public const char CustomSchemeNamesParserSplit = '\n';
    public static List<UIColorScheme> DefaultColorSchemes;
    public static List<UIColorScheme> CustomColorSchemes;
    public static string NotChangingTag;
    string newTag;
    string newScheme;

    int indexer;

    #endregion

    [MenuItem("Window/UI Editor")]
    static void OpenUIEditor()
    {
        UIEditor myWindow = EditorWindow.GetWindow<UIEditor>(false, "UI Editor");
        myWindow.minSize = new Vector2(700, 200);
    }

    void OnEnable()
    {
        DefaultColorSchemes = new List<UIColorScheme>();
        CustomColorSchemes = new List<UIColorScheme>();

        loadAllClasses();
    }

    void OnDisable()
    {
        saveAllClasses();

        //SAVE THE TAGS TO THE STRINGS SINCE IT IS LOADING FROM THEM
        //MAYBE YOU CAN CREATE AN PLAYER PREF WITH A CUSTOM PARSE WITHIN A SINGLE STRING

    }

    #region Schemes

    void loadAllClasses()
    {
        foreach (string colorSetting in DefaultColorSettingsNames)
        {
            DefaultColorSchemes.Add(loadScheme(colorSetting, true));
        }
        CustomColorSettingsNames = new List<string>();
        ProcessCustomTagsString();
        NotChangingTag = PlayerPrefs.GetString(NotChangingTagPrefName);
        foreach (string settingsName in CustomColorSettingsNames)
        {
            CustomColorSchemes.Add(loadScheme(settingsName, false));
        }
    }

    void ProcessCustomTagsString()
    {
        string FullString = PlayerPrefs.GetString(CustomSchemeNamesPlayerPrefName);
        string[] customSchemes = FullString.Split();
        foreach (string customScheme in customSchemes)
        {
            if (!string.IsNullOrEmpty(customScheme))
                CustomColorSettingsNames.Add(customScheme);
        }
    }

    void SaveCustomTags()
    {
        string FullString = "";
        foreach (string customColorSettingsName in CustomColorSettingsNames)
        {
            FullString += customColorSettingsName;
            if (CustomColorSettingsNames[CustomColorSettingsNames.Count - 1] != customColorSettingsName)
                FullString += CustomSchemeNamesParserSplit;
        }
        PlayerPrefs.SetString(CustomSchemeNamesPlayerPrefName, FullString);
    }

    void saveAllClasses()
    {
        foreach (UIColorScheme defaultColorScheme in DefaultColorSchemes)
        {
            saveScheme(defaultColorScheme);
        }

        foreach (UIColorScheme customColorScheme in CustomColorSchemes)
        {
            saveScheme(customColorScheme);
        }
        PlayerPrefs.SetString(NotChangingTagPrefName, NotChangingTag);
        SaveCustomTags();

    }

    UIColorScheme loadScheme(string name, bool isDefault)
    {
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString(name)))
        {
            string loadedTagNameName = name + " tag name";
            string loadedNormalColorName = name + " normal color";
            string loadedOutlineColorName = name + " outline color";
            string loadedOutlineSizeName = name + " outline size";

            string loadedTagName = PlayerPrefs.GetString(loadedTagNameName);
            float loadedOutlineSize = PlayerPrefs.GetFloat(loadedOutlineSizeName);
            Color loadedOutlineColor = RetrieveColor(loadedOutlineColorName);
            Color loadeNormalColor = RetrieveColor(loadedNormalColorName);
            UIColorScheme colorScheme = new UIColorScheme(name, loadeNormalColor, loadedOutlineColor, loadedOutlineSize, loadedTagName);

            return colorScheme;
        }
        else
        {
            Debug.Log("x");
            UIColorScheme colorScheme = new UIColorScheme(name, Color.gray, Color.gray, 2, name);
            if (isDefault)
                colorScheme.tagName = "";
            return colorScheme;
        }

    }

    void saveScheme(UIColorScheme scheme)
    {
        if (scheme != null)
        {
            string name = scheme.colorSchemeName;

            string loadedTagNameName = name + " tag name";
            string loadedNormalColorName = name + " normal color";
            string loadedOutlineColorName = name + " outline color";
            string loadedOutlineSizeName = name + " outline size";

            PlayerPrefs.SetString(loadedTagNameName, scheme.tagName);
            PlayerPrefs.SetFloat(loadedOutlineSizeName, scheme.outlineSize);
            PlayerPrefs.SetString(name, name);
            SaveColor(scheme.outlineColor, loadedOutlineColorName);
            SaveColor(scheme.normalColor, loadedNormalColorName);
        }
    }
    #endregion

    #region Interface

    void drawOnGUI(UIColorScheme scheme)
    {
        if (!scheme.isTag || scheme.tagName == DefaultColorSettingsNames[0] || scheme.tagName == DefaultColorSettingsNames[1] || scheme.tagName == DefaultColorSettingsNames[2])
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(scheme.colorSchemeName, GUILayout.Height(20), GUILayout.MaxWidth(100), GUILayout.MinWidth(50));
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Color:", GUILayout.Height(20), GUILayout.MaxWidth(50));
            scheme.normalColor = EditorGUILayout.ColorField(scheme.normalColor, GUILayout.Height(20), GUILayout.MaxWidth(100));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Outline Color :", GUILayout.Height(20), GUILayout.MaxWidth(100));
            scheme.outlineColor = EditorGUILayout.ColorField(scheme.outlineColor, GUILayout.Height(20), GUILayout.MaxWidth(100));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Outline Size :", GUILayout.Height(20), GUILayout.MaxWidth(100));
            scheme.outlineSize = EditorGUILayout.FloatField(scheme.outlineSize, GUILayout.Height(20), GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
        }
        else
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(scheme.colorSchemeName, GUILayout.Height(20), GUILayout.MaxWidth(100), GUILayout.MinWidth(50));
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Color:", GUILayout.Height(20), GUILayout.MaxWidth(50));
            scheme.normalColor = EditorGUILayout.ColorField(scheme.normalColor, GUILayout.Height(20), GUILayout.MaxWidth(100));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Tag :", GUILayout.Height(20), GUILayout.MaxWidth(50));
            scheme.tagName = EditorGUILayout.TagField(scheme.tagName, GUILayout.Height(20), GUILayout.MaxWidth(200));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Outline Color :", GUILayout.Height(20), GUILayout.MaxWidth(100));
            scheme.outlineColor = EditorGUILayout.ColorField(scheme.outlineColor, GUILayout.Height(20), GUILayout.MaxWidth(100));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Outline Size :", GUILayout.Height(20), GUILayout.MaxWidth(100));
            scheme.outlineSize = EditorGUILayout.FloatField(scheme.outlineSize, GUILayout.Height(20), GUILayout.MaxWidth(50));

            EditorGUILayout.Space();
            DeleteScheme(scheme);
            GUILayout.EndHorizontal();

        }
    }

    void OnGUI()
    {
        foreach (UIColorScheme defaultColorScheme in DefaultColorSchemes)
        {
            GUILayout.Space(2f);
            drawOnGUI(defaultColorScheme);
        }
        foreach (UIColorScheme customColorScheme in CustomColorSchemes)
        {
            GUILayout.Space(2f);
            drawOnGUI(customColorScheme);
        }

        GUILayout.Space(15f);
        SetNotChangingTag();
        GUILayout.Space(15f);
        AddScheme();
        GUILayout.Space(10f);

        if (GUILayout.Button("Change Color", GUILayout.Height(30), GUILayout.MaxWidth(150)))
        {
            ChangeUI();
        }
        Repaint();
    }

    void SetNotChangingTag()
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Choose the tag that won't be changed:", GUILayout.Height(20), GUILayout.MaxWidth(250));

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("     Not Changing Tag :", GUILayout.Height(20), GUILayout.MaxWidth(150));
        EditorGUILayout.Space();
        NotChangingTag = EditorGUILayout.TagField(NotChangingTag, GUILayout.Height(20), GUILayout.MaxWidth(300));
        GUILayout.EndHorizontal();
    }

    void AddScheme()
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Add a new Scheme:", GUILayout.Height(20), GUILayout.MaxWidth(150));
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Name:", GUILayout.Height(20), GUILayout.MaxWidth(80));
        newScheme = EditorGUILayout.TextField(newScheme, GUILayout.Height(20), GUILayout.MaxWidth(150));

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Tag :", GUILayout.Height(20), GUILayout.MaxWidth(80));
        newTag = EditorGUILayout.TagField(newTag, GUILayout.Height(20), GUILayout.MaxWidth(100));


        if (GUILayout.Button("Add Scheme by Tag", GUILayout.Height(20), GUILayout.MaxWidth(200)))
        {
            SchemeByTag(newTag, newScheme);
        }
        GUILayout.EndHorizontal();
    }

    void DeleteScheme(UIColorScheme scheme)
    {
        if (GUILayout.Button("Delete", GUILayout.Height(20)))
        {
            CustomColorSettingsNames.Remove(scheme.colorSchemeName);
            CustomColorSchemes.Remove(scheme);
        }
    }

    #endregion

    #region UImanagers

    void ChangeUI()
    {
        ButtonManager();
        foreach (UIColorScheme customColorScheme in CustomColorSchemes)
        {
            PanelManager(customColorScheme);
        }

        TextManager();

        SceneView.RepaintAll();
        EditorSceneManager.MarkAllScenesDirty();
    }

    void SchemeByTag(string newTag, string scheme)
    {
        foreach (string customColorSettingsName in CustomColorSettingsNames)
        {
            if (customColorSettingsName == scheme)
            {
                Debug.LogWarning("Please use an different scheme name.");
                return;
            }
        }
        if (string.IsNullOrEmpty(newTag))
        {
            Debug.LogWarning("Please assign an valid tag.");
            return;
        }
        if (string.IsNullOrEmpty(scheme))
        {
            Debug.LogWarning("Please assign an valid scheme name.");
            return;
        }
        UIColorScheme colorScheme = new UIColorScheme(scheme, Color.gray, Color.gray, 2, newTag);
        CustomColorSchemes.Add(colorScheme);
        CustomColorSettingsNames.Add(scheme);
        SceneView.RepaintAll();
        EditorSceneManager.MarkAllScenesDirty();


        GUILayout.EndHorizontal();

    }

    void PanelManager(UIColorScheme scheme)
    {
        indexer = 0;
        var panels = GameObject.FindGameObjectsWithTag(scheme.tagName);
        if (panels.Length > 0)
        {
            foreach (GameObject gObject in panels)
            {
                EditorUtility.SetDirty(gObject);
                Outline outline;
                Image myIm = gObject.GetComponent<Image>();

                if (myIm == null)
                {
                    Text myTx = gObject.GetComponent<Text>();
                    Undo.RecordObject(myTx, "TXT" + scheme.tagName + indexer);
                    EditorUtility.SetDirty(myTx);
                    myTx.color = scheme.normalColor;
                    outline = myTx.GetComponent<Outline>();
                }
                else
                {
                    Undo.RecordObject(myIm, "Image" + scheme.tagName + indexer);
                    EditorUtility.SetDirty(myIm);
                    myIm.color = scheme.normalColor;
                    outline = myIm.GetComponent<Outline>();
                }


                if (outline)
                {
                    Undo.RecordObject(outline, "Outline" + scheme.tagName + indexer);
                    EditorUtility.SetDirty(outline);
                    outline.effectColor = scheme.outlineColor;
                    outline.effectDistance = new Vector2(scheme.outlineSize, -scheme.outlineSize);
                }
                indexer++;
            }
            indexer = 0;
        }
    }

    void ButtonManager()
    {
        UIColorScheme buttonScheme = DefaultColorSchemes[0];
        UIColorScheme buttonTextScheme = DefaultColorSchemes[1];
        var buttons = FindObjectsOfType<Button>();
        Undo.RecordObjects(buttons, "Button");

        indexer = 0;

        ColorBlock cb = ColorBlock.defaultColorBlock;
        cb.normalColor = buttonScheme.normalColor;
        cb.highlightedColor = new Color(buttonScheme.normalColor.r + 0.2f, buttonScheme.normalColor.g + 0.2f, buttonScheme.normalColor.b + 0.2f);
        cb.pressedColor = new Color(buttonScheme.normalColor.r - 0.3f, buttonScheme.normalColor.g - 0.3f, buttonScheme.normalColor.b - 0.3f);
        cb.disabledColor = new Color(buttonScheme.normalColor.r - 0.3f, buttonScheme.normalColor.g - 0.3f, buttonScheme.normalColor.b - 0.3f, 0.3f);

        foreach (Button button in buttons)
        {
            bool isEqualToTag = false;
            foreach (UIColorScheme customColorScheme in CustomColorSchemes)
            {
                if (button.tag == customColorScheme.tagName || button.tag == NotChangingTag)
                    isEqualToTag = true;
            }
            if (isEqualToTag == false)
            {
                EditorUtility.SetDirty(button.gameObject);
                button.colors = cb;
                Outline bOutline = button.GetComponent<Outline>();
                if (bOutline)
                {
                    Undo.RecordObject(bOutline, "bOutline" + indexer);
                    EditorUtility.SetDirty(bOutline);
                    bOutline.effectColor = buttonScheme.outlineColor;
                    bOutline.effectDistance = new Vector2(buttonScheme.outlineSize, -buttonScheme.outlineSize);
                }
            }

            if (button.transform.childCount > 0)
            {
                GameObject text = button.transform.GetChild(0).gameObject;

                foreach (UIColorScheme customColorScheme in CustomColorSchemes)
                {
                    if (text.tag == customColorScheme.tagName || text.tag == NotChangingTag)
                        return;
                }

                Text txt = text.GetComponent<Text>();
                Undo.RecordObject(txt, "Text" + indexer);
                indexer++;
                EditorUtility.SetDirty(txt);
                txt.color = buttonTextScheme.normalColor;
                Outline tOutline = text.GetComponent<Outline>();
                if (tOutline)
                {
                    Undo.RecordObject(tOutline, "tOutline" + indexer);
                    EditorUtility.SetDirty(tOutline);
                    tOutline.effectColor = buttonTextScheme.outlineColor;
                    tOutline.effectDistance = new Vector2(buttonTextScheme.outlineSize, -buttonTextScheme.outlineSize);
                }
            }
        }
        indexer = 0;
    }

    void TextManager()
    {
        indexer = 0;
        var texts = FindObjectsOfType<Text>();
        UIColorScheme scheme = DefaultColorSchemes[2];
        Undo.RecordObjects(texts, "Texts");
        foreach (Text text in texts)
        {
            foreach (UIColorScheme customColorScheme in CustomColorSchemes)
            {
                if (text.tag == customColorScheme.tagName || text.tag == NotChangingTag)
                    return;
            }
            EditorUtility.SetDirty(text.gameObject);
            if (!text.transform.parent.GetComponent<Button>())
            {
                text.color = scheme.normalColor;
                Outline tOutline = text.GetComponent<Outline>();
                indexer++;
                if (tOutline)
                {
                    Undo.RecordObject(tOutline, "ttOutline" + indexer);
                    EditorUtility.SetDirty(tOutline);
                    tOutline.effectColor = scheme.normalColor;
                    tOutline.effectDistance = new Vector2(scheme.outlineSize, -scheme.outlineSize);
                }
            }

        }
        indexer = 0;
    }

    #endregion

    #region ColorManager

    void SaveColor(Color col, string colorName)
    {
        string redStr = "col" + colorName + "R";
        string greenStr = "col" + colorName + "G";
        string blueStr = "col" + colorName + "B";
        string alphaStr = "col" + colorName + "A";
        float red = col.r;
        float green = col.g;
        float blue = col.b;
        float alpha = col.a;

        SaveChannel(redStr, red);
        SaveChannel(greenStr, green);
        SaveChannel(blueStr, blue);
        SaveChannel(alphaStr, alpha);
    }

    void SaveChannel(string Key, float value)
    {
        PlayerPrefs.SetFloat(Key, value);
    }

    Color RetrieveColor(string colorName)
    {
        string redStr = "col" + colorName + "R";
        string greenStr = "col" + colorName + "G";
        string blueStr = "col" + colorName + "B";
        string alphaStr = "col" + colorName + "A";
        float red = RetrieveChannel(redStr);
        float green = RetrieveChannel(greenStr);
        float blue = RetrieveChannel(blueStr);
        float alpha = RetrieveChannel(alphaStr);
        Color myColor = new Color(red, green, blue, alpha);
        return myColor;
    }

    float RetrieveChannel(string Key)
    {
        return PlayerPrefs.GetFloat(Key);
    }

    #endregion
}