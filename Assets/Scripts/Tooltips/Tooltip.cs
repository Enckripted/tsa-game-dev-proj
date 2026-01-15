using System.Collections.Generic;
using UnityEngine;

public class Tooltip
{
    public string NameText
    {
        get
        {
            return $"<color=#{NameHexColor}>{Name}</color>";
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
    protected string NameHexColor;
    protected readonly List<string> Lines;

    public Tooltip(string name = "", string nameHexColor = "FFFFFF")
    {
        Name = name;
        NameHexColor = nameHexColor;
        Lines = new List<string>();
    }

    public void AddLine(string text, bool bold = false, string hexColor = null)
    {
        Debug.Log(hexColor);
        string currentLine = "";
        if (bold) currentLine += "<b>";
        if (hexColor != null) currentLine += $"<color={hexColor}>";
        currentLine += text;
        if (hexColor != null) currentLine += "</color>";
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
