using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class leaderboard : MonoBehaviour
{
    /*
    public Sprite names1st1, names1st2, names1st3,
        names2nd1, names2nd2, names2nd3,
        names3rd1, names3rd2, names3rd3,
        names4th1, names4th2, names4th3,
        names5th1, names5th2, names5th3,
        scores1stC, scores1stD, scores1stU,
        scores2ndC, scores2ndD, scores2ndU,
        scores3rdC, scores3rdD, scores3rdU,
        scores4thC, scores4thD, scores4thU,
        scores5thC, scores5thD, scores5thU;
    */
    
    public PersistentData PD;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //1st
        //name
        gameObject.transform.GetChild(2).GetChild(1).transform.gameObject.GetComponent<TMP_Text>().text = PersistentData.boardNames["name1"];
        //score
        gameObject.transform.GetChild(3).GetChild(1).transform.gameObject.GetComponent<TMP_Text>().text = (PersistentData.boardScores["score1"]).ToString();

        //2nd
        //name
        gameObject.transform.GetChild(2).GetChild(2).transform.gameObject.GetComponent<TMP_Text>().text = PersistentData.boardNames["name2"];
        //score
        gameObject.transform.GetChild(3).GetChild(2).transform.gameObject.GetComponent<TMP_Text>().text = (PersistentData.boardScores["score2"]).ToString();
        /*
        gameObject.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.gameObject.GetComponent<Image>().sprite = letter[PD.indice("" + PersistentData.boardNames["score2"][0])];
        gameObject.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.gameObject.GetComponent<Image>().sprite = letter[PD.indice("" + PersistentData.boardNames["score2"][1])];
        gameObject.transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).transform.gameObject.GetComponent<Image>().sprite = letter[PD.indice("" + PersistentData.boardNames["score2"][2])];
        */
        //3rd
        //name
        gameObject.transform.GetChild(2).GetChild(3).transform.gameObject.GetComponent<TMP_Text>().text = PersistentData.boardNames["name3"];
        //score
        gameObject.transform.GetChild(3).GetChild(3).transform.gameObject.GetComponent<TMP_Text>().text = (PersistentData.boardScores["score3"]).ToString();
        /*
        gameObject.transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.gameObject.GetComponent<Image>().sprite = letter[PD.indice("" + PersistentData.boardNames["score3"][0])];
        gameObject.transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).transform.gameObject.GetComponent<Image>().sprite = letter[PD.indice("" + PersistentData.boardNames["score3"][1])];
        gameObject.transform.GetChild(0).transform.GetChild(2).transform.GetChild(2).transform.gameObject.GetComponent<Image>().sprite = letter[PD.indice("" + PersistentData.boardNames["score3"][2])];
        */
        //4th
        //name
        gameObject.transform.GetChild(2).GetChild(4).transform.gameObject.GetComponent<TMP_Text>().text = PersistentData.boardNames["name4"];
        //score
        gameObject.transform.GetChild(3).GetChild(4).transform.gameObject.GetComponent<TMP_Text>().text = (PersistentData.boardScores["score4"]).ToString();
        /*
        gameObject.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).transform.gameObject.GetComponent<Image>().sprite = letter[PD.indice("" + PersistentData.boardNames["score4"][0])];
        gameObject.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).transform.gameObject.GetComponent<Image>().sprite = letter[PD.indice("" + PersistentData.boardNames["score4"][1])];
        gameObject.transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).transform.gameObject.GetComponent<Image>().sprite = letter[PD.indice("" + PersistentData.boardNames["score4"][2])];
        */
        //5th
        //name
        gameObject.transform.GetChild(2).GetChild(5).transform.gameObject.GetComponent<TMP_Text>().text = PersistentData.boardNames["name5"];
        //score
        gameObject.transform.GetChild(3).GetChild(5).transform.gameObject.GetComponent<TMP_Text>().text = (PersistentData.boardScores["score5"]).ToString();
        /*
        gameObject.transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).transform.gameObject.GetComponent<Image>().sprite = letter[PD.indice("" + PersistentData.boardNames["score5"][0])];
        gameObject.transform.GetChild(0).transform.GetChild(4).transform.GetChild(1).transform.gameObject.GetComponent<Image>().sprite = letter[PD.indice("" + PersistentData.boardNames["score5"][1])];
        gameObject.transform.GetChild(0).transform.GetChild(4).transform.GetChild(2).transform.gameObject.GetComponent<Image>().sprite = letter[PD.indice("" + PersistentData.boardNames["score5"][2])];
        */
    }
    
}
