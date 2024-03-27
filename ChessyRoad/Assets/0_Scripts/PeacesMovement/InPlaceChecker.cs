using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InPlaceChecker : MonoBehaviour
{
    public float ExtraTime = 2.5f;
    public bool InPlace = true, PieceEnabled = false;
    public GameObject ExtraTimeText;
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

        if(GameController.GameMode == GameController.GameModes.Timer)
        {
            GameObject TiempoTexto = Instantiate(ExtraTimeText, new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z), Quaternion.identity);
            TiempoTexto.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + ExtraTime + "s";
        }

        Destroy(gameObject);
    }
}
