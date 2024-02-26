using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop_Random : MonoBehaviour
{
    //public float speed;
    //public Transform movePoint;
    //public Rigidbody rigidbody;
    public int side;

    public GameObject master;
    public GameObject target;

    private void Awake()
    {
        target = this.gameObject.transform.GetChild(0).gameObject;
        target.transform.parent = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        master = GameObject.FindGameObjectWithTag("GameController");

        //target = this.gameObject.transform.GetChild(0).gameObject;
        //target.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow)
            && master.GetComponent<MasterMovement>().movable)
        {
            side = Random.Range(0, 2);
        }
    }
    /*bool isObjectHere(Vector3 position)
    {
        Collider[] intersecting = Physics.OverlapSphere(position, 0.5f);
        if (intersecting.Length == 0)
        {
            //Debug.Log("No overlap");
            return false;
        }
        else
        {
            //Debug.Log("Overlap");
            return true;
        }
    }*/
}
