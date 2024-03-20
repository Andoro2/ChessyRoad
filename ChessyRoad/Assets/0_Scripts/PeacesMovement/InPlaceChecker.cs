using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlaceChecker : MonoBehaviour
{
    public bool InPlace = true, PieceEnabled = false;
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
        Destroy(gameObject);
    }
}
