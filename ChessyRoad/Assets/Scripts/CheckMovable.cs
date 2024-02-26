using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMovable : MonoBehaviour
{
    public bool inPlace;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        switch (this.transform.parent.tag)
        {
            case ("Rook"):
                target = GetComponentInParent<Rook_Line>().target.transform;
                break;
            case ("Knight"):
                target = GetComponentInParent<Knight>().target.transform;
                break;
            case ("BishopRandom"):
                target = GetComponentInParent<Bishop_Random>().target.transform;
                break;
            case ("BishopZigZag"):
                target = GetComponentInParent<Bishop_ZigZag>().target.transform;
                break;
            case ("BishopFullZigZag"):
                target = GetComponentInParent<Bishop_FullZigZag>().target.transform;
                break;
            case ("Pawn"):
                target = GetComponentInParent<Pawn>().target.transform;
                break;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.parent.position == target.transform.position)
        {
            inPlace = true;
        }
        else
        {
            inPlace = false;
        }
    }
}
