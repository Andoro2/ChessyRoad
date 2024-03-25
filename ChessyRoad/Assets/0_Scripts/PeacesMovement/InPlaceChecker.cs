using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlaceChecker : MonoBehaviour
{
    public float ExtraTime = 2.5f;
    public bool InPlace = true, PieceEnabled = false;
    void Start()
    {
        if(GameController.PlayerColor == GameController.PlayerColors.Black)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }   
    public void SetBool(bool value)
    {
        InPlace = value;
    }
    public void PieceEnable()
    {
        PieceEnabled = true;
    }
    public void Death()
    {
        GameController.ScorePoints(1);
        GameController.GetTime(ExtraTime);
        Destroy(gameObject);
    }
}
