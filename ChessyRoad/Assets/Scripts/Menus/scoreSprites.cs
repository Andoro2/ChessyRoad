using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreSprites : MonoBehaviour
{
    public List<Sprite> number = new List<Sprite>();
    public gameController GC;
    //private GameObject scoreC, scoreD, scoreU;

    // Start is called before the first frame update
    void Start()
    {
        GC = this.gameObject.GetComponent<gameController>();
        
        //scoreC = GameObject.Find("scoreC").gameObject;
        //scoreD = GameObject.Find("scoreD").gameObject;
        //scoreU = GameObject.Find("scoreU").gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        //scoreC.GetComponent<Image>().sprite = number[GC.score / 100];
        //scoreD.GetComponent<Image>().sprite = number[GC.score / 10];
        //scoreU.GetComponent<Image>().sprite = number[GC.score % 10];
    }
}
