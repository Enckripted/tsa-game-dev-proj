using System.Collections.Generic;
using UnityEngine;

public class Tooltip
{
    public string NameText
    {
        get
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(NameColor)}>{Name}</color>";
        }
    }
    public string Text
    {
        get
        {
            return string.Join("\n", Lines);
        }
    }

    protected string Name;
    protected Color NameColor;
    protected readonly List<string> Lines;

    public Tooltip(string name = "", Color color = default)
    {
        Name = name;
        NameColor = color != default ? color : Color.white;
        Lines = new List<string>();
    }

    public void AddLine(string text, bool bold = false, Color color = default)
    {
        string currentLine = "";
        if (bold) currentLine += "<b>";
        if (color != default) currentLine += $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>";
        currentLine += text;
        if (color != default) currentLine += "</color>";
        if (bold) currentLine += "</b>";
        Lines.Add(currentLine);
    }

    public void AddNewLine()
    {
        Lines.Add("");
    }

    public void CombineWith(Tooltip tooltip)
    {
        foreach (string line in tooltip.Lines)
        {
            Lines.Add(line);
        }
    }
}
