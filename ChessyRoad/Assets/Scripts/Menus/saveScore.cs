using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using TMPro;

public class saveScore : MonoBehaviour
{

    public TMP_InputField nameID;
    public int score;//, ID;
    public gameController GC;
    public bool guardar = false;

    private void Start()
    {
        score = GC.score;
    }
    public void trueGuardar()
    {
        guardar = true;
    }
    public void Submit()
    {
        if (guardar)
        {
            if(score > PersistentData.boardScores["score5"])
            {
                if (score > PersistentData.boardScores["score4"])
                {
                    if (score > PersistentData.boardScores["score3"])
                    {
                        if (score > PersistentData.boardScores["score2"])
                        {
                            if (score > PersistentData.boardScores["score1"])
                            {
                                PersistentData.boardScores["score5"] = PersistentData.boardScores["score4"];
                                PersistentData.boardNames["name5"] = PersistentData.boardNames["name4"];
                                PersistentData.boardScores["score4"] = PersistentData.boardScores["score3"];
                                PersistentData.boardNames["name4"] = PersistentData.boardNames["name3"];
                                PersistentData.boardScores["score3"] = PersistentData.boardScores["score2"];
                                PersistentData.boardNames["name3"] = PersistentData.boardNames["name2"];
                                PersistentData.boardScores["score2"] = PersistentData.boardScores["score1"];
                                PersistentData.boardNames["name2"] = PersistentData.boardNames["name1"];

                                PersistentData.boardScores["score1"] = score;
                                PersistentData.boardNames["name1"] = nameID.text;
                            }
                            else
                            {
                                PersistentData.boardScores["score5"] = PersistentData.boardScores["score4"];
                                PersistentData.boardNames["name5"] = PersistentData.boardNames["name4"];
                                PersistentData.boardScores["score4"] = PersistentData.boardScores["score3"];
                                PersistentData.boardNames["name4"] = PersistentData.boardNames["name3"];
                                PersistentData.boardScores["score3"] = PersistentData.boardScores["score2"];
                                PersistentData.boardNames["name3"] = PersistentData.boardNames["name2"];

                                PersistentData.boardScores["score2"] = score;
                                PersistentData.boardNames["name2"] = nameID.text;
                            }
                        }
                        else
                        {
                            PersistentData.boardScores["score5"] = PersistentData.boardScores["score4"];
                            PersistentData.boardNames["name5"] = PersistentData.boardNames["name4"];
                            PersistentData.boardScores["score4"] = PersistentData.boardScores["score3"];
                            PersistentData.boardNames["name4"] = PersistentData.boardNames["name3"];

                            PersistentData.boardScores["score3"] = score;
                            PersistentData.boardNames["name3"] = nameID.text;
                        }
                    }
                    else
                    {
                        PersistentData.boardScores["score5"] = PersistentData.boardScores["score4"];
                        PersistentData.boardNames["name5"] = PersistentData.boardNames["name4"];

                        PersistentData.boardScores["score4"] = score;
                        PersistentData.boardNames["name4"] = nameID.text;
                    }
                }
                else
                {
                    PersistentData.boardScores["score5"] = score;
                    PersistentData.boardNames["name5"] = nameID.text;
                }
            }
            Debug.Log("1a posición - " + PersistentData.boardScores["score1"] + "-" + PersistentData.boardNames["name1"]);
            Debug.Log("2a posición" + PersistentData.boardScores["score2"] + "-" + PersistentData.boardNames["name2"]);
            Debug.Log("3a posición" + PersistentData.boardScores["score3"] + "-" + PersistentData.boardNames["name3"]);
            Debug.Log("4a posición" + PersistentData.boardScores["score4"] + "-" + PersistentData.boardNames["name4"]);
            Debug.Log("5a posición" + PersistentData.boardScores["score5"] + "-" + PersistentData.boardNames["name5"]);
        }
        guardar = false;
    }
}
