using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameController : MonoBehaviour
{
    public bool playerOK, CHECKIN = false;
    private GameObject player;//, inGameScore;
    public GameObject deathMenuUI;//, finalC, finalD, finalU;//, scoreText;

    public TMP_Text scoreText, finalScore;

    public int score = 0, zPos;

    //private scoreSprites SP;

    public string gameState = "TURN_KING";

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        player = GameObject.FindWithTag("Player").gameObject;
        //scoreText = GameObject.Find("inGameScore").gameObject.transform.GetChild(1).gameObject.transform.GetComponent<TextMeshPro>();
        //deathMenuUI = GameObject.Find("deathMenu");
        //deathMenuUI.SetActive(false);

        //SP = this.gameObject.GetComponent<scoreSprites>();
    }

    // Update is called once per frame
    void Update()
    {

        GameObject[] checkers = GameObject.FindGameObjectsWithTag("PlaceChecker");

        if (peacesInPlace(checkers) && gameState == "TURN_KING") //player.GetComponent<Movement_King>().movable)
        {
            if (playerOnPeace(checkers))
            {
                muelto();
            }

            if (zPos/2 > score)
            {
                score = zPos / 2;
                scoreText.text = score.ToString();
            }
        }

        zPos = (int)player.transform.position.z;
    }

    bool peacesInPlace(GameObject[] pieces)
    {
        int trues = 0;

        foreach (GameObject piece in pieces)
        {
            bool inPlace = piece.gameObject.GetComponent<CheckMovable>().inPlace;

            if (inPlace)
            {
                trues++;
            }
        }

        //Debug.Log("Fitxes:"+pieces.Length+"\nEn el lloc:"+trues);

        if (trues == pieces.Length)
        {
            //Debug.Log("gud");
            return true;
        }
        else
        {
            //Debug.Log("bado");
            return false;
        }
    }

    bool playerOnPeace(GameObject[] checkers)
    {
        int muelte = 0;
        foreach (GameObject check in checkers)
        {
            if (player.transform.position == check.transform.parent.transform.position)
            {
                muelte = 1;
            }
        }

        if (muelte == 1)
        {
            //Debug.Log("morido");
            return true;
        }
        else
        {
            //Debug.Log("vivido");
            return false;
        }
    }
    
    void muelto()
    {
        Time.timeScale = 0f;
        //inGameScore.SetActive(false);
        deathMenuUI.SetActive(true);

        finalScore.text = score.ToString();
    }
}
