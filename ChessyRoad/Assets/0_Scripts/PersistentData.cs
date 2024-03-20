using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData PD;
    public static float MainVolume = 0.0f;

    //COLLECTIBLES
    public static Dictionary<string, string> boardNames = new Dictionary<string, string>();
    public static Dictionary<string, int> boardScores = new Dictionary<string, int>();

    private void Awake()
    {
        if (PersistentData.PD == null)
        {
            ScoresName();
            ScoresValue();
            PersistentData.PD = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void ScoresName()
    {
        boardNames.Add("name1", "---");
        boardNames.Add("name2", "---");
        boardNames.Add("name3", "---");
        boardNames.Add("name4", "---");
        boardNames.Add("name5", "---");
    }
    private void ScoresValue()
    {
        boardScores.Add("score1", 0);
        boardScores.Add("score2", 0);
        boardScores.Add("score3", 0);
        boardScores.Add("score4", 0);
        boardScores.Add("score5", 0);
    }
    /*
    public int indice(string value)
    {
        int index = 0;
        switch (value)
        {
            case "z":
                index = 0;
                break;
            case "y":
                index = 1;
                break;
            case "x":
                index = 2;
                break;
            case "w":
                index = 3;
                break;
            case "v":
                index = 4;
                break;
            case "u":
                index = 5;
                break;
            case "t":
                index = 6;
                break;
            case "s":
                index = 7;
                break;
            case "r":
                index = 8;
                break;
            case "q":
                index = 9;
                break;
            case "p":
                index = 10;
                break;
            case "o":
                index = 11;
                break;
            case "n":
                index = 12;
                break;
            case "m":
                index = 13;
                break;
            case "l":
                index = 14;
                break;
            case "k":
                index = 15;
                break;
            case "j":
                index = 16;
                break;
            case "i":
                index = 17;
                break;
            case "h":
                index = 18;
                break;
            case "g":
                index = 19;
                break;
            case "f":
                index = 20;
                break;
            case "e":
                index = 21;
                break;
            case "d":
                index = 22;
                break;
            case "c":
                index = 23;
                break;
            case "b":
                index = 24;
                break;
            case "a":
                index = 25;
                break;
        }
        return index;
    }
    */
}
