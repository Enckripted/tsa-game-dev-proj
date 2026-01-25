using System;
using System.Collections.Generic;
using UnityEngine;


//wrote this entire class with goal of creating a typewriter effect without
//words jumping onto new lines only to realize that my method never worked, and
//textmeshpro already has a way to do this. leaving this here for now because
//implementation would be a pain to redo again.
public class Typewriter
{
    private readonly double timePerChar;

    private readonly List<string> words;
    private int curWord;
    private int curChar;
    private double timePassed;
    private string previousText;

    private readonly int charsPerLine;
    private readonly double commaTimeMult;
    private readonly double puncTimeMult;

    public string Text
    {
        get
        {
            return previousText +
            (!Finished ? words[curWord].Substring(0, curChar)
            + new string(' ', words[curWord].Length - curChar) : "");
        }
    }
    public bool Finished
    {
        get
        {
            return curWord == words.Count;
        }
    }

    private char GetPreviousChar()
    {
        if (curChar == 0 && curWord == 0) return ' ';

        if (curChar == 0) return words[curWord - 1][words[curWord - 1].Length - 1];
        else return words[curWord][curChar - 1];
    }

    private double TimeForCurrentChar()
    {
        if (Finished) return 0;

        if (GetPreviousChar() == ',') return timePerChar * commaTimeMult;
        else if (GetPreviousChar() == '.' || GetPreviousChar() == '!') return timePerChar * puncTimeMult;
        else return timePerChar;
    }

    private void NextChar()
    {
        timePassed -= TimeForCurrentChar();
        curChar++;
        if (curChar == words[curWord].Length) NextWord();
    }

    private void NextWord()
    {
        previousText += words[curWord] + " ";
        curChar = 0;
        curWord++;
        if (!Finished && Math.Floor((double)previousText.Length / charsPerLine) > Math.Floor((double)(previousText.Length + words[curWord].Length) / charsPerLine))
            previousText += "\n";
    }

    public Typewriter(string message, double charsPerSec, double commaTimeMult = 1, double periodTimeMult = 1)
    {
        timePerChar = 1 / charsPerSec;

        words = new List<string>(message.Split(' '));
        curWord = 0;
        curChar = 0;
        timePassed = 0;
        previousText = "";

        this.commaTimeMult = commaTimeMult;
        this.puncTimeMult = periodTimeMult;
    }

    public void IncrementTime(float deltaTime)
    {
        timePassed += deltaTime;

        while (!Finished && timePassed > TimeForCurrentChar())
            NextChar();
    }

    public void ForwardAction()
    {
        while (!Finished)
            NextWord();
    }
}
