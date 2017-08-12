using UnityEngine;

public class UIColorScheme
{
    public string colorSchemeName;
    public Color normalColor = Color.gray;
    public Color outlineColor = Color.black;
    public float outlineSize = 2;
    public string tagName;
    public bool isTag
    {
        get
        {
            if (!string.IsNullOrEmpty(tagName))
            {
                return true;
            }
            return false;
        }
    }

    public UIColorScheme(string name, Color normalColor, Color outlineColor, float outlineSize, string tagName)
    {
        this.colorSchemeName = name;
        this.normalColor = normalColor;
        this.outlineColor = outlineColor;
        this.outlineSize = outlineSize;
        this.tagName = tagName;
    }
}